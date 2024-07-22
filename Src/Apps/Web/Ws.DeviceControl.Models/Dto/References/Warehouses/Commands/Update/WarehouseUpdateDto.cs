using System.Text.Json.Serialization;

namespace Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Update;

public class WarehouseUpdateDto
{
    [JsonPropertyName("id1C")]
    public Guid Id1C { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}