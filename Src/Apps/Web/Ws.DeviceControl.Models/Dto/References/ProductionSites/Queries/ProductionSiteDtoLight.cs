namespace Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;

public record ProductionSiteDtoLight
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}