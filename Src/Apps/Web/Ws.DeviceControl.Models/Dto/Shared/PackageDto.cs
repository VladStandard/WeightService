using System.Text.Json.Serialization;

namespace Ws.DeviceControl.Models.Dto.Shared;

public class PackageDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("weight")]
    public required decimal Weight { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; set; }
}