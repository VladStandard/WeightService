using System.Net;
using TscZebra.Plugin.Abstractions.Enums;

namespace Ws.DeviceControl.Models.Dto.Devices.Printers.Commands.Create;

public sealed record PrinterCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("ip")]
    [JsonConverter(typeof(IpAddressJsonConverter))]
    public IPAddress Ip { get; set; } = IPAddress.Parse("127.0.0.1");

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<PrinterTypes>))]
    public PrinterTypes Type { get; set; } = PrinterTypes.Tsc;

    [JsonPropertyName("productionSite")]
    public Guid ProductionSiteId { get; set; }
}