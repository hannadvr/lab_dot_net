namespace ClusterTelemetryApp.DTOs;

public class ClusterDto
{
    public string DatacenterRegion { get; set; } = string.Empty;
    public string ClusterName { get; set; } = string.Empty;
    public List<NodeDto> Nodes { get; set; } = new();
}