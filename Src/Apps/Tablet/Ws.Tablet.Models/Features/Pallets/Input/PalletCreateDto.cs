namespace Ws.Tablet.Models.Features.Pallets.Input;

[Serializable]
public class PalletCreateDto
{
    [JsonPropertyName("documentBarcode")]
    public string DocumentBarcode { get; set; } = string.Empty;

    [JsonPropertyName("batches")]
    public List<BatchCreateDto> Batches { get; set; } = [];
}
