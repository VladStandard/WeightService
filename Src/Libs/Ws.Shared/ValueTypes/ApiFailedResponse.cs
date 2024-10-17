namespace Ws.Shared.ValueTypes;

public record ApiFailedResponse
{
    [JsonPropertyName("localizeMessage")]
    public required string LocalizeMessage { get; init; }
}