namespace ClusterTelemetryApp.DTOs;

public class MetricDto
{
    public DateTime Time { get; set; }
    public double CpuLoadPercent { get; set; }
    public double RamUsedMb { get; set; }
}