namespace Ws.DeviceControl.Models.Dto.Shared;

public sealed record ProxyDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
