using System.Text.Json.Serialization;
using Ws.Shared.Api.ValueTypes;

namespace Ws.Desktop.Models.Features.Pallets.Output;

public sealed record PalletInfo
{
    [JsonPropertyName("Id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("number")]
    public required string Number { get; init; }

    [JsonPropertyName("palletMan")]
    public required Fio PalletMan { get; init; }

    [JsonPropertyName("arm")]
    public required string Arm { get; init; }

    [JsonPropertyName("warehouse")]
    public required string Warehouse { get; init; }

    [JsonPropertyName("plus")]
    public required HashSet<PluPalletInfo> Plus { get; init; }

    [JsonPropertyName("kneadings")]
    public required HashSet<ushort> Kneadings { get; init; }

    [JsonPropertyName("barcode")]
    public required string Barcode { get; init; }

    [JsonPropertyName("prodDt")]
    public required DateTime ProdDt { get; init; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; init; }

    [JsonPropertyName("deletedAt")]
    public required DateTime? DeletedAt { get; init; }

    [JsonPropertyName("isShipped")]
    public required bool IsShipped { get; init; }

    [JsonPropertyName("weightTray")]
    public required decimal WeightTray { get; init; }

    #region JsonIgnore

    [JsonIgnore]
    public decimal WeightNet => Plus.Sum(i => i.WeightNet);

    [JsonIgnore]
    public decimal WeightBrutto => Plus.Sum(i => i.WeightBrutto);

    [JsonIgnore]
    public ushort BundleCount => (ushort)Plus.Sum(i => i.BundleCount);

    [JsonIgnore]
    public ushort BoxCount => (ushort)Plus.Sum(i => i.BoxCount);

    #endregion
}