using TscZebra.Plugin.Abstractions.Enums;

namespace Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Create;

public sealed record PrinterCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("ip")]
    [JsonConverter(typeof(IpV4AddressJsonConverter))]
    public IPAddress Ip { get; set; } = DefaultTypes.IpLocal;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<PrinterTypes>))]
    public PrinterTypes Type { get; set; } = PrinterTypes.Tsc;

    [JsonPropertyName("productionSite")]
    public Guid ProductionSiteId { get; set; }
}