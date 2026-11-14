using ClusterTelemetryApp.Entities;

namespace ClusterTelemetryApp.Data;

public interface ITelemetryRepository
{
    Task<List<ClusterEntity>> GetAllClustersWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<List<MetricEntity>> GetAllMetricsWithNodesAsync(CancellationToken cancellationToken = default);
}