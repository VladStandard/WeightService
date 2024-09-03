using System.Net;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Shared.Constants;

namespace Ws.DeviceControl.Models.Features.Devices.Printers.Commands.Create;

public sealed record PrinterCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("ip")]
    [JsonConverter(typeof(IpAddressJsonConverter))]
    public IPAddress Ip { get; set; } = DefaultConsts.IpLocal;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<PrinterTypes>))]
    public PrinterTypes Type { get; set; } = PrinterTypes.Tsc;

    [JsonPropertyName("productionSite")]
    public Guid ProductionSiteId { get; set; }
}