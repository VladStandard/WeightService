using System.Text.Json.Serialization;

namespace Ws.Shared.Api.ApiException;

public record ApiExceptionClient
{
    [JsonPropertyName("errorLocalizeKey")]
    public required string ErrorLocalizeKey { get; set; }
}