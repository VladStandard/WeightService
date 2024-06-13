using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ws.Desktop.Models.ValueTypes;

namespace Ws.Desktop.Models.Features.Pallets.Output;

[Serializable]
public sealed record PalletInfo
{
    [Required]
    [JsonPropertyName("Id")]
    public required Guid Id { get; init; }

    [Required]
    [JsonPropertyName("number")]
    public required uint Number { get; init; }

    [Required]
    [JsonPropertyName("pluName")]
    public required string PluName { get; init; }

    [Required]
    [JsonPropertyName("pluNumber")]
    public required ushort PluNumber { get; init; }

    [Required]
    [JsonPropertyName("labelCount")]
    public required uint LabelCount { get; init; }

    [Required]
    [JsonPropertyName("palletMan")]
    public required Fio PalletMan { get; init; }

    [Required]
    [JsonPropertyName("weightTray")]
    public required decimal WeightTray { get; init; }

    [Required]
    [JsonPropertyName("weightBrutto")]
    public required decimal WeightBrutto { get; init; }

    [Required]
    [JsonPropertyName("weightNet")]
    public required decimal WeightNet { get; init; }

    [Required]
    [JsonPropertyName("Barcode")]
    public required string Barcode { get; init; }

    [Required]
    [JsonPropertyName("prodDt")]
    public required DateTime ProdDt { get; init; }

    [Required]
    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; init; }
}