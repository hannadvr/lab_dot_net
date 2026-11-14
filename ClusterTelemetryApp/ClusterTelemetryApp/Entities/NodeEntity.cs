namespace ClusterTelemetryApp.Entities;

public class NodeEntity
{
    public int Id { get; set; }
    public string NodeIp { get; set; } = string.Empty;
    public string OsVersion { get; set; } = string.Empty;

    public int ClusterId { get; set; }
    public ClusterEntity Cluster { get; set; } = null!;

    public ICollection<MetricEntity> Metrics { get; set; } = new List<MetricEntity>();
}