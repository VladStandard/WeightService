namespace Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Create;

public sealed record ProductionSiteCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
}