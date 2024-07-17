using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Features.Pallets.Input;

public sealed record PalletPieceCreateDto
{
    [Required]
    [JsonPropertyName("pluId")]
    public required Guid PluId { get; init; }

    [Required]
    [JsonPropertyName("characteristicId")]
    public required Guid CharacteristicId { get; init; }

    [Required]
    [JsonPropertyName("palletManId")]
    public required Guid PalletManId { get; init; }

    [Required]
    [JsonPropertyName("weightTray")]
    public required decimal WeightTray { get; init; }

    [Required]
    [JsonPropertyName("labelCount")]
    public required byte LabelCount { get; init; }

    [Required]
    [JsonPropertyName("kneading")]
    public required ushort Kneading { get; init; }

    [Required]
    [JsonPropertyName("prodDt")]
    public required DateTime ProdDt { get; init; }
}