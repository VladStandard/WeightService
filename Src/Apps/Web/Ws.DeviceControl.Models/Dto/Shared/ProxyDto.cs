using System.Text.Json.Serialization;

namespace Ws.DeviceControl.Models.Dto.Shared;

public record ProxyDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}