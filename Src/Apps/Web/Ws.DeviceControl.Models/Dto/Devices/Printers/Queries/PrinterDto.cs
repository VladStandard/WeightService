using System.Net;
using System.Text.Json.Serialization;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Shared.Converters.Json;

namespace Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;

public record PrinterDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("ip")]
    [JsonConverter(typeof(IpAddressJsonConverter))]
    public required IPAddress Ip  { get; set; }

    [JsonPropertyName("type")]
    public required PrinterTypes Type  { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; set; }
}