namespace Ws.DeviceControl.Models.Features.References.Template.Universal;

public sealed record BarcodeItemDto
{
    [JsonPropertyName("property")]
    public required string Property { get; set; }

    [JsonPropertyName("formatStr")]
    public required string FormatStr { get; set; }
}