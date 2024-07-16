using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Features.Arms.Input;

public sealed record UpdateArmDto
{
    [JsonPropertyName("version")]
    public required string Version { get; init; }
}