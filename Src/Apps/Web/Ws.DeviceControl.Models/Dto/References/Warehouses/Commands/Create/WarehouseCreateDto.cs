namespace Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Create;

public sealed record WarehouseCreateDto
{
    [JsonPropertyName("id1C")]
    public Guid Id1C { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("productionSiteId")]
    public Guid ProductionSiteId { get; set; }
}
