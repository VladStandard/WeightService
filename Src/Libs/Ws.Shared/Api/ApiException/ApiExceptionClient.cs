using System.Text.Json.Serialization;

namespace Ws.Shared.Api.ApiException;

public record ApiExceptionClient
{
    [JsonPropertyName("localizeMessage")]
    public required string LocalizeMessage { get; set; }
}