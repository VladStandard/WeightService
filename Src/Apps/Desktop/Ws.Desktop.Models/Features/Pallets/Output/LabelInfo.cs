using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Features.Pallets.Output;

public sealed record LabelInfo
{
    [JsonPropertyName("zpl")]
    public required string Zpl { get; init; }

    [JsonPropertyName("barcode")]
    public required string Barcode { get; init; }
}