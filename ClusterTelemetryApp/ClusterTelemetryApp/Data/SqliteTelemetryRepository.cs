using ClusterTelemetryApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClusterTelemetryApp.Data;

public class SqliteTelemetryRepository : ITelemetryRepository
{
    private readonly AppDbContext _context;

    public SqliteTelemetryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ClusterEntity>> GetAllClustersWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Clusters
            .Include(c => c.Nodes)
                .ThenInclude(n => n.Metrics)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<MetricEntity>> GetAllMetricsWithNodesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Metrics
            .Include(m => m.Node)
                .ThenInclude(n => n.Cluster)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}