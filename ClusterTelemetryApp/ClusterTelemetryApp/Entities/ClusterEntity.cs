namespace ClusterTelemetryApp.Entities;

public class ClusterEntity
{
    public int Id { get; set; }
    public string DatacenterRegion { get; set; } = string.Empty;
    public string ClusterName { get; set; } = string.Empty;

    public ICollection<NodeEntity> Nodes { get; set; } = new List<NodeEntity>();
}