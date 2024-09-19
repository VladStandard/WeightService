namespace Ws.Shared.ValueTypes;

public class BarcodeItem
{
    public string Property { get; set; } = string.Empty;
    public string FormatStr { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        if (obj is BarcodeItem item)
            return Property == item.Property && FormatStr == item.FormatStr;
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(Property, FormatStr);
}