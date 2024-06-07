using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Shared.Converters.Json;

namespace Ws.Desktop.Models.Features.Arms.Output;

[Serializable]
public sealed record PrinterValue {
    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [Required]
    [JsonPropertyName("ip")]
    [JsonConverter(typeof(IpAddressJsonConverter))]
    public required IPAddress Ip { get; init; }

    [Required]
    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<PrinterTypes>))]
    public required PrinterTypes Type { get; init; }
}