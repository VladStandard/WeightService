using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Features.Pallets.Output;

public record PluPalletInfo
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("number")]
    public required ushort Number { get; init; }

    [JsonPropertyName("boxCount")]
    public required ushort BoxCount { get; init; }

    [JsonPropertyName("bundleCount")]
    public required ushort BundleCount { get; init; }

    [JsonPropertyName("weightBrutto")]
    public required decimal WeightBrutto { get; init; }

    [JsonPropertyName("weightNet")]
    public required decimal WeightNet { get; init; }
};