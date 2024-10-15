namespace Ws.Tablet.Models.Features.Arms;

[Serializable]
public class ArmDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("warehouseName")]
    private string WarehouseName { get; set; } = string.Empty;
}