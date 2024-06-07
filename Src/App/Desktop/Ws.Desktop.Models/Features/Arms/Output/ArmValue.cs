using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ws.Shared.Converters.Json;
using Ws.Shared.Enums;

namespace Ws.Desktop.Models.Features.Arms.Output;

[Serializable]
public sealed record ArmValue {

    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [Required]
    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ArmType>))]
    public required ArmType Type { get; init; }

    [Required]
    [JsonPropertyName("pcName")]
    public required string PcName { get; init; }

    [Required]
    [JsonPropertyName("counter")]
    public required uint Counter { get; init; }

    [Required]
    [JsonPropertyName("warehouse")]
    public required string Warehouse { get; init; }

    [Required]
    [JsonPropertyName("printer")]
    public required PrinterValue Printer { get; init; }
}