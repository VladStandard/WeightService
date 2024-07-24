using Ws.Shared.Converters.Json;
using Ws.Shared.Enums;

namespace Ws.DeviceControl.Models.Dto.Devices.Arms.Commands.Update;

public record ArmUpdateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ArmType>))]
    public ArmType Type { get; set; }

    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("counter")]
    public int Counter  { get; set; }

    [JsonPropertyName("pc")]
    public string PcName { get; set; } = string.Empty;

    [JsonPropertyName("printerId")]
    public Guid PrinterId { get; set; }

    [JsonPropertyName("warehouseId")]
    public Guid WarehouseId { get; set; }
}
