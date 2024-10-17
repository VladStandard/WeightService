namespace Ws.Desktop.Models.Shared.Models;

public sealed class ProxyDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}