namespace Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;

public class ProductionSiteCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
}