namespace Ws.Domain.Models.ValueTypes;

public class BarcodeItem
{
    public int Len { get; set; }
    public bool IsConst { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        BarcodeItem item = (BarcodeItem)obj;
        return Len == item.Len && IsConst == item.IsConst;
    }

    public override int GetHashCode() => Len.GetHashCode() ^ IsConst.GetHashCode();
}