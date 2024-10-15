namespace Ws.Tablet.Models.Features.Pallets.Output;

[Serializable]
public class BatchDto
{
    [JsonPropertyName("pluName")]
    public string PluName { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("weight")]
    public decimal Weight { get; set; }
}