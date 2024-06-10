using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Features.Labels.Input;

[Serializable]
public sealed record CreateWeightLabelDto
{
    [Required]
    [JsonPropertyName("kneading")]
    public required ushort Kneading { get; init; }

    [Required]
    [JsonPropertyName("weightNet")]
    public required decimal WeightNet { get; init; }

    [Required]
    [JsonPropertyName("weightTare")]
    public required decimal WeightTare { get; init; }

    [Required]
    [JsonPropertyName("productDt")]
    public required DateTime ProductDt { get; init; }
}