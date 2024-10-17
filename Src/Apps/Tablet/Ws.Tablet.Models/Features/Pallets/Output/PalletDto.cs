namespace Ws.Tablet.Models.Features.Pallets.Output;

public sealed class PalletDto
{
    [JsonPropertyName("documentBarcode")]
    public required string DocumentBarcode { get; set; } = string.Empty;

    [JsonPropertyName("palletBarcode")]
    public required string PalletBarcode { get; set; } = string.Empty;

    [JsonPropertyName("zplLabel")]
    public required string ZplLabel { get; set; } = string.Empty;

    [JsonPropertyName("batches")]
    public required List<BatchDto> Batches { get; set; } = [];

    [JsonConverter(typeof(FioJsonConverter))]
    public required Fio User { get; set; } = DefaultTypes.Fio;

    public required string WarehouseName { get; set; } = string.Empty;

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    public string Number { get; set; } = string.Empty;

    public decimal WeightBrutto { get; set; } = decimal.Zero;

    [JsonIgnore]
    public decimal Weight => Batches.Sum(x => x.Weight);
}