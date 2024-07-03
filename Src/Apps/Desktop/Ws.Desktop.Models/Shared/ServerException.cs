using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Shared;

public record ServerException
{
    [JsonPropertyName("messageLocalizeKey")]
    public required string MessageLocalizeKey { get; set; }
}