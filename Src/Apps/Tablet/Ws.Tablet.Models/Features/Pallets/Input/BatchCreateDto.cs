namespace Ws.Tablet.Models.Features.Pallets.Input;

[Serializable]
public class BatchCreateDto
{
    [JsonPropertyName("pluId")]
    public Guid PluId { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("weight")]
    public decimal Weight { get; set; }
}