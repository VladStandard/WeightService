using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using TscZebra.Plugin.Abstractions.Enums;

namespace Ws.Desktop.Models.Features.Arms.Output;

public sealed record PrinterValue {

    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [Required]
    [JsonPropertyName("ip")]
    public required IPAddress Ip { get; init; }

    [Required]
    [JsonPropertyName("type")]
    public required PrinterTypes Type { get; init; }
}