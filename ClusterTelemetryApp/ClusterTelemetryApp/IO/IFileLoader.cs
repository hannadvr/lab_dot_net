namespace ClusterTelemetryApp.IO;

public interface IFileLoader
{
    IAsyncEnumerable<T> LoadStreamAsync<T>(string filePath, CancellationToken cancellationToken = default);
}