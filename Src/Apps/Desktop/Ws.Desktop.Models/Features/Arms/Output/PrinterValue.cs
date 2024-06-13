using System.Net;
using System.Text.Json.Serialization;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Shared.Converters.Json;

namespace Ws.Desktop.Models.Features.Arms.Output;

public sealed record PrinterValue {

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("ip")]
    [JsonConverter(typeof(IpAddressJsonConverter))]
    public required IPAddress Ip { get; init; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<PrinterTypes>))]
    public required PrinterTypes Type { get; init; }
}