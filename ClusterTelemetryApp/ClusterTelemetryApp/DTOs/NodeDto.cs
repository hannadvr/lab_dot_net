namespace ClusterTelemetryApp.DTOs;

public class NodeDto
{
    public string NodeIp { get; set; } = string.Empty;
    public string OsVersion { get; set; } = string.Empty;
    public List<MetricDto> Metrics { get; set; } = new();
}