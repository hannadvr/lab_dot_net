namespace ClusterTelemetryApp.Analytics;

// Проєкція для виявлення CPU-аномалій
public class CpuAnomalyReport
{
    public string Region { get; set; } = string.Empty;
    public string ClusterName { get; set; } = string.Empty;
    public string NodeIp { get; set; } = string.Empty;
    public string OsVersion { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public double CpuLoadPercent { get; set; }
    public double RamUsedMb { get; set; }
    public string Severity { get; set; } = string.Empty;
}

// Проєкція підсумкової статистики по кластерах
public class ClusterSummaryReport
{
    public string Region { get; set; } = string.Empty;
    public string ClusterName { get; set; } = string.Empty;
    public int NodesCount { get; set; }
    public int TotalMetricsCount { get; set; }
    public double AvgCpuPercent { get; set; }
    public double MaxCpuPercent { get; set; }
    public double AvgRamMb { get; set; }
}

// Проєкція аналітики за операційними системами
public class OsPerformanceReport
{
    public string OsVersion { get; set; } = string.Empty;
    public int DistinctNodesCount { get; set; }
    public int TotalSamples { get; set; }
    public double AvgCpuPercent { get; set; }
    public double MaxRamUsedMb { get; set; }
    public int HighLoadEventsCount { get; set; }
}

// Проєкція рейтингу нод за частотою аномалій
public class NodeRiskReport
{
    public string NodeIp { get; set; } = string.Empty;
    public string ClusterName { get; set; } = string.Empty;
    public int TotalMeasurements { get; set; }
    public int AnomalyCount { get; set; }
    public double AnomalyRatePercent { get; set; }
    public double PeakCpuPercent { get; set; }
}