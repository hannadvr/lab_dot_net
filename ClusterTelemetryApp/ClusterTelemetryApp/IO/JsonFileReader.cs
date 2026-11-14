using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ClusterTelemetryApp.IO;

public class JsonFileReader : IFileLoader
{
    private readonly JsonSerializerOptions _defaultOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async IAsyncEnumerable<T> LoadStreamAsync<T>(
        string filePath,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath))
        {
            yield break;
        }

        await using var stream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096,
            useAsync: true);

        var asyncEnumerable = JsonSerializer.DeserializeAsyncEnumerable<T>(
            stream,
            _defaultOptions,
            cancellationToken);

        await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (item is not null)
            {
                yield return item;
            }
        }
    }
}