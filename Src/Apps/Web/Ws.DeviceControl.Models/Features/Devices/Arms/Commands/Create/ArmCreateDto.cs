namespace Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Create;

public sealed record ArmCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ArmType>))]
    public ArmType Type { get; set; }

    [JsonPropertyName("number")]
    public int Number { get; set; } = new Random().Next(10001, 100000);

    [JsonPropertyName("pc")]
    public string PcName { get; set; } = string.Empty;

    [JsonPropertyName("printerId")]
    public Guid PrinterId { get; set; }

    [JsonPropertyName("warehouse")]
    public Guid WarehouseId { get; set; }
}