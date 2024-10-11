using Ws.Desktop.Models.Shared.Models;
using Ws.Shared.Enums;

namespace Ws.Desktop.Models.Features.Arms.Output;

public sealed record ArmValue
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ArmType>))]
    public required ArmType Type { get; init; }

    [JsonPropertyName("pcName")]
    public required string PcName { get; init; }

    [JsonPropertyName("counter")]
    public required uint Counter { get; init; }

    [JsonPropertyName("warehouse")]
    public required ProxyDto Warehouse { get; init; }

    [JsonPropertyName("printer")]
    public required PrinterValue Printer { get; init; }
}