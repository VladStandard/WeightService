namespace Ws.DeviceControl.Models.Shared;

public sealed record ProxyDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}