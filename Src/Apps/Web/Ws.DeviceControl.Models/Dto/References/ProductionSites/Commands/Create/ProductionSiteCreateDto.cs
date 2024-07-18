using System.Text.Json.Serialization;

namespace Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;

public class ProductionSiteCreateDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public required string Address { get; set; } = string.Empty;
}