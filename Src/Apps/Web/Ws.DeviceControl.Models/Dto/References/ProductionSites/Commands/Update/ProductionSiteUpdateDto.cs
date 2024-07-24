namespace Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;

public class ProductionSiteUpdateDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public required string Address { get; set; } = string.Empty;
}