namespace Ws.Shared.ValueTypes;

public record BarcodeItem
{
    public string Property { get; set; } = string.Empty;
    public string FormatStr { get; set; } = string.Empty;
}