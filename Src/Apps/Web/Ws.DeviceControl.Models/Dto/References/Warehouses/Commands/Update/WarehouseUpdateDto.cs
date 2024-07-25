namespace Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Update;

public sealed record WarehouseUpdateDto
{
    [JsonPropertyName("id1C")]
    public Guid Id1C { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}