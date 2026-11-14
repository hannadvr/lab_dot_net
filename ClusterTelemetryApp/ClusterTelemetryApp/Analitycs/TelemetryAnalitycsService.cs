using ClusterTelemetryApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ClusterTelemetryApp.Analytics;

public class TelemetryAnalyticsService
{
    private readonly AppDbContext _context;

    public TelemetryAnalyticsService(AppDbContext context)
    {
        _context = context;
    }

    // Виявлення аномалій: де навантаження CPU перевищило 85%
    public async Task<List<CpuAnomalyReport>> GetCpuAnomaliesAsync(
        double cpuThreshold = 85.0,
        CancellationToken cancellationToken = default)
    {
        return await _context.Metrics
            .AsNoTracking()
            .Where(m => m.CpuLoadPercent > cpuThreshold)
            .OrderByDescending(m => m.CpuLoadPercent)
            .Select(m => new CpuAnomalyReport
            {
                Region = m.Node.Cluster.DatacenterRegion,
                ClusterName = m.Node.Cluster.ClusterName,
                NodeIp = m.Node.NodeIp,
                OsVersion = m.Node.OsVersion,
                Timestamp = m.Timestamp,
                CpuLoadPercent = m.CpuLoadPercent,
                RamUsedMb = m.RamUsedMb,
                Severity = m.CpuLoadPercent >= 95.0 ? "CRITICAL" : "WARNING"
            })
            .ToListAsync(cancellationToken);
    }

    // Підсумкова статистика по кластерах
    public async Task<List<ClusterSummaryReport>> GetClusterSummaryStatisticsAsync(
        CancellationToken cancellationToken = default)
    {
        var metrics = await _context.Metrics
            .AsNoTracking()
            .Include(m => m.Node)
                .ThenInclude(n => n.Cluster)
            .ToListAsync(cancellationToken);

        return metrics
            .GroupBy(m => new { m.Node.Cluster.DatacenterRegion, m.Node.Cluster.ClusterName })
            .Select(g => new ClusterSummaryReport
            {
                Region = g.Key.DatacenterRegion,
                ClusterName = g.Key.ClusterName,
                NodesCount = g.Select(x => x.NodeId).Distinct().Count(),
                TotalMetricsCount = g.Count(),
                AvgCpuPercent = Math.Round(g.Average(x => x.CpuLoadPercent), 1),
                MaxCpuPercent = g.Max(x => x.CpuLoadPercent),
                AvgRamMb = Math.Round(g.Average(x => x.RamUsedMb), 0)
            })
            .ToList();
    }

    // Статистика за версіями ОС
    public async Task<List<OsPerformanceReport>> GetOsVersionPerformanceAsync(
        double anomalyThreshold = 85.0,
        CancellationToken cancellationToken = default)
    {
        var metrics = await _context.Metrics
            .AsNoTracking()
            .Include(m => m.Node)
            .ToListAsync(cancellationToken);

        return metrics
            .GroupBy(m => m.Node.OsVersion)
            .Select(g => new OsPerformanceReport
            {
                OsVersion = g.Key,
                DistinctNodesCount = g.Select(x => x.NodeId).Distinct().Count(),
                TotalSamples = g.Count(),
                AvgCpuPercent = Math.Round(g.Average(x => x.CpuLoadPercent), 1),
                MaxRamUsedMb = g.Max(x => x.RamUsedMb),
                HighLoadEventsCount = g.Count(x => x.CpuLoadPercent > anomalyThreshold)
            })
            .ToList();
    }

    // Кількість аномалій по кожній ноді
    public async Task<List<NodeRiskReport>> GetHighRiskNodesRankingAsync(
        double cpuThreshold = 85.0,
        CancellationToken cancellationToken = default)
    {
        var metrics = await _context.Metrics
            .AsNoTracking()
            .Include(m => m.Node)
                .ThenInclude(n => n.Cluster)
            .Where(m => m.CpuLoadPercent > cpuThreshold)
            .ToListAsync(cancellationToken);

        return metrics
            .GroupBy(m => new { m.Node.NodeIp, m.Node.Cluster.ClusterName })
            .Select(g => new NodeRiskReport
            {
                NodeIp = g.Key.NodeIp,
                ClusterName = g.Key.ClusterName,
                TotalMeasurements = g.Count(),
                AnomalyCount = g.Count(),
                AnomalyRatePercent = 100.0,
                PeakCpuPercent = g.Max(x => x.CpuLoadPercent)
            })
            .OrderByDescending(r => r.AnomalyCount)
            .ToList();
    }
}