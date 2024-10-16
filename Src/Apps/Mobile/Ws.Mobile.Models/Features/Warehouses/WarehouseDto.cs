namespace Ws.Mobile.Models.Features.Warehouses;

[Serializable]
public class WarehouseDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("productionSiteName")]
    public required string ProductionSiteName { get; set; } = string.Empty;

    [JsonPropertyName("warehouseName")]
    public required string WarehouseName { get; set; } = string.Empty;

    [JsonPropertyName("warehouseAddress")]
    public required string WarehouseAddress { get; set; } = string.Empty;
}