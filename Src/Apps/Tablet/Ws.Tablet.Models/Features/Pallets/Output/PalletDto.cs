using Ws.Shared.Constants;
using Ws.Shared.ValueTypes;
using Ws.Tablet.Models.Features.Pallets.Input;

namespace Ws.Tablet.Models.Features.Pallets.Output;

[Serializable]
public class PalletDto
{
    [JsonPropertyName("documentBarcode")]
    public string DocumentBarcode { get; set; } = string.Empty;

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; } = string.Empty;

    [JsonPropertyName("batches")]
    public List<BatchCreateDto> Batches { get; set; } = [];

    [JsonConverter(typeof(FioJsonConverter))]
    public Fio User { get; set; } = DefaultTypes.Fio;

    [JsonConverter(typeof(FioJsonConverter))]
    public Fio WarehouseName { get; set; } = DefaultTypes.Fio;

    [JsonPropertyName("createDt")]
    public DateTime CreateDt { get; set; }

    [JsonIgnore]
    public decimal Weight => Batches.Sum(x => x.Weight);
}