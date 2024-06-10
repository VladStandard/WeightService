using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Features.Labels.Output;

[Serializable]
public sealed record WeightLabel
{
    [Required]
    [JsonPropertyName("armCounter")]
    public required uint ArmCounter { get; init; }

    [Required]
    [JsonPropertyName("zpl")]
    public required string Zpl { get; init; }
}