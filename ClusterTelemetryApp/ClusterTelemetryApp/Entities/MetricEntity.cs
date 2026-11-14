namespace ClusterTelemetryApp.Entities;

public class MetricEntity
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public double CpuLoadPercent { get; set; }
    public double RamUsedMb { get; set; }

    public int NodeId { get; set; }
    public NodeEntity Node { get; set; } = null!;
}