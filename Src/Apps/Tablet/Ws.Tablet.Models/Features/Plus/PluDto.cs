namespace Ws.Tablet.Models.Features.Plus;

[Serializable]
public class PluDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("number")]
    public required short Number { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}