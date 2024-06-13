using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Features.Pallets.Output;

public sealed record LabelInfo
{
    [JsonPropertyName("zpl")]
    public required string Zpl { get; init; }
}