using System.Net;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Shared.Converters.Json;

namespace Ws.DeviceControl.Models.Dto.Devices.Printers.Commands.Update;

public class PrinterUpdateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("ip")]
    [JsonConverter(typeof(IpAddressJsonConverter))]
    public IPAddress Ip { get; set; } = IPAddress.Parse("127.0.0.1");

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<PrinterTypes>))]
    public PrinterTypes Type { get; set; } = PrinterTypes.Tsc;
}