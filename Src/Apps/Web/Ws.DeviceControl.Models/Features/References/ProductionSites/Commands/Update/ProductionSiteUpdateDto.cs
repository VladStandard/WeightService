namespace Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Update;

public sealed record ProductionSiteUpdateDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public required string Address { get; set; } = string.Empty;
}