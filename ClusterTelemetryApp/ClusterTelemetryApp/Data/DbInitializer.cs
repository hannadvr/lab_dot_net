using ClusterTelemetryApp.DTOs;
using ClusterTelemetryApp.Entities;
using ClusterTelemetryApp.IO;
using Microsoft.EntityFrameworkCore;

namespace ClusterTelemetryApp.Data;

public class DbInitializer
{
    private readonly AppDbContext _context;
    private readonly IFileLoader _jsonReader;

    public DbInitializer(AppDbContext context, IFileLoader jsonReader)
    {
        _context = context;
        _jsonReader = jsonReader;
    }

    public async Task<int> ImportFromJsonAsync(
        string jsonFilePath,
        bool clearExisting = true,
        CancellationToken cancellationToken = default)
    {
        await _context.Database.EnsureCreatedAsync(cancellationToken);

        if (clearExisting && await _context.Clusters.AnyAsync(cancellationToken))
        {
            // Завдяки каскадному видаленню очищення Clusters автоматично видаляє Nodes та Metrics
            var existingClusters = await _context.Clusters.ToListAsync(cancellationToken);
            _context.Clusters.RemoveRange(existingClusters);
            await _context.SaveChangesAsync(cancellationToken);
        }

        int importedMetricsCount = 0;

        await foreach (var clusterDto in _jsonReader.LoadStreamAsync<ClusterDto>(jsonFilePath, cancellationToken))
        {
            var clusterEntity = new ClusterEntity
            {
                DatacenterRegion = clusterDto.DatacenterRegion,
                ClusterName = clusterDto.ClusterName
            };

            foreach (var nodeDto in clusterDto.Nodes)
            {
                var nodeEntity = new NodeEntity
                {
                    NodeIp = nodeDto.NodeIp,
                    OsVersion = nodeDto.OsVersion,
                    Cluster = clusterEntity
                };

                foreach (var metricDto in nodeDto.Metrics)
                {
                    nodeEntity.Metrics.Add(new MetricEntity
                    {
                        Timestamp = metricDto.Time,
                        CpuLoadPercent = metricDto.CpuLoadPercent,
                        RamUsedMb = metricDto.RamUsedMb,
                        Node = nodeEntity
                    });
                    importedMetricsCount++;
                }

                clusterEntity.Nodes.Add(nodeEntity);
            }

            await _context.Clusters.AddAsync(clusterEntity, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return importedMetricsCount;
    }
}