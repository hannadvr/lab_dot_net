using System.IO;
using System.Text;
using ClusterTelemetryApp.Analytics;
using ClusterTelemetryApp.Data;
using ClusterTelemetryApp.DTOs;
using ClusterTelemetryApp.Entities;
using ClusterTelemetryApp.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ClusterTelemetryApp.Tests;

[TestClass]
public class TelemetryAnalyticsTests
{
    private SqliteConnection _connection = null!;
    private AppDbContext _context = null!;
    private TelemetryAnalyticsService _service = null!;

    [TestInitialize]
    public async Task SetupAsync()
    {
        // Піднімаємо тимчасову БД SQLite в оперативній пам'яті для тестів
        _connection = new SqliteConnection("DataSource=:memory:");
        await _connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new AppDbContext(options);
        await _context.Database.EnsureCreatedAsync();

        // Додаємо 1 кластер, 2 ноди та 4 метрики для перевірки
        var cluster = new ClusterEntity
        {
            DatacenterRegion = "eu-central-1",
            ClusterName = "Prod-Cluster",
            Nodes = new List<NodeEntity>
            {
                new()
                {
                    NodeIp = "10.0.0.1",
                    OsVersion = "Ubuntu 24.04",
                    Metrics = new List<MetricEntity>
                    {
                        new() { Timestamp = DateTime.UtcNow, CpuLoadPercent = 96.0, RamUsedMb = 15000 },
                        new() { Timestamp = DateTime.UtcNow, CpuLoadPercent = 40.0, RamUsedMb = 9000 }
                    }
                },
                new()
                {
                    NodeIp = "10.0.0.2",
                    OsVersion = "Debian 12",
                    Metrics = new List<MetricEntity>
                    {
                        new() { Timestamp = DateTime.UtcNow, CpuLoadPercent = 30.0, RamUsedMb = 6000 },
                        new() { Timestamp = DateTime.UtcNow, CpuLoadPercent = 50.0, RamUsedMb = 7000 }
                    }
                }
            }
        };

        await _context.Clusters.AddAsync(cluster);
        await _context.SaveChangesAsync();

        _service = new TelemetryAnalyticsService(_context);
    }

    [TestCleanup]
    public async Task CleanupAsync()
    {
        await _context.DisposeAsync();
        await _connection.DisposeAsync();
    }

    // Пошук CPU-аномалій (> 85%)
    [TestMethod]
    public async Task GetCpuAnomaliesAsync_ReturnsOnlyHighCpuMetrics()
    {
        var result = await _service.GetCpuAnomaliesAsync(85.0);

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("10.0.0.1", result[0].NodeIp);
        Assert.AreEqual(96.0, result[0].CpuLoadPercent);
    }

    // Зведення по кластерах (середній та максимальний CPU)
    [TestMethod]
    public async Task GetClusterSummaryStatisticsAsync_CalculatesAverageAndMax()
    {
        var result = await _service.GetClusterSummaryStatisticsAsync();

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(2, result[0].NodesCount);
        Assert.AreEqual(54.0, result[0].AvgCpuPercent); // (96 + 40 + 30 + 50) / 4 = 54
        Assert.AreEqual(96.0, result[0].MaxCpuPercent);
    }

    // Порівняння версій ОС
    [TestMethod]
    public async Task GetOsVersionPerformanceAsync_GroupsByOsVersion()
    {
        var result = await _service.GetOsVersionPerformanceAsync(85.0);

        Assert.AreEqual(2, result.Count);
        var ubuntu = result.First(x => x.OsVersion == "Ubuntu 24.04");
        Assert.AreEqual(1, ubuntu.HighLoadEventsCount);
    }

    // Рейтинг проблемних нод
    [TestMethod]
    public async Task GetHighRiskNodesRankingAsync_ReturnsOverloadedNodes()
    {
        var result = await _service.GetHighRiskNodesRankingAsync(85.0);

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("10.0.0.1", result[0].NodeIp);
        Assert.AreEqual(1, result[0].AnomalyCount);
    }

    // Каскадне видалення
    [TestMethod]
    public async Task DeleteCluster_CascadesToNodesAndMetrics()
    {
        var cluster = await _context.Clusters.FirstAsync();

        _context.Clusters.Remove(cluster);
        await _context.SaveChangesAsync();

        Assert.AreEqual(0, await _context.Clusters.CountAsync());
        Assert.AreEqual(0, await _context.Nodes.CountAsync());
        Assert.AreEqual(0, await _context.Metrics.CountAsync());
    }

    // Потокове асинхронне читання JSON
    [TestMethod]
    public async Task JsonFileReader_ReadsClustersAsynchronously()
    {
        string tempFile = Path.GetTempFileName();
        string json = """
        [
          {
            "datacenterRegion": "eu-central-1",
            "clusterName": "Test-Cluster",
            "nodes": []
          }
        ]
        """;
        await File.WriteAllTextAsync(tempFile, json, Encoding.UTF8);

        try
        {
            var reader = new JsonFileReader();
            var list = new List<ClusterDto>();

            await foreach (var item in reader.LoadStreamAsync<ClusterDto>(tempFile))
            {
                list.Add(item);
            }

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Test-Cluster", list[0].ClusterName);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }
}