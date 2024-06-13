using System.Text.Json.Serialization;
using Ws.Desktop.Models.ValueTypes;

namespace Ws.Desktop.Models.Features.Pallets.Output;

public sealed record PalletInfo
{
    [JsonPropertyName("Id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("number")]
    public required uint Number { get; init; }

    [JsonPropertyName("pluName")]
    public required string PluName { get; init; }

    [JsonPropertyName("pluNumber")]
    public required ushort PluNumber { get; init; }

    [JsonPropertyName("labelCount")]
    public required uint LabelCount { get; init; }

    [JsonPropertyName("palletMan")]
    public required Fio PalletMan { get; init; }

    [JsonPropertyName("weightTray")]
    public required decimal WeightTray { get; init; }

    [JsonPropertyName("weightBrutto")]
    public required decimal WeightBrutto { get; init; }

    [JsonPropertyName("weightNet")]
    public required decimal WeightNet { get; init; }

    [JsonPropertyName("Barcode")]
    public required string Barcode { get; init; }

    [JsonPropertyName("prodDt")]
    public required DateTime ProdDt { get; init; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; init; }
}