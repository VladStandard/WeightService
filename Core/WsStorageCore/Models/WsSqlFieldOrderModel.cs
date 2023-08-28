namespace WsStorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlFieldOrderModel(string Name, WsSqlEnumOrder Direction = WsSqlEnumOrder.Asc)
{
    public override int GetHashCode() => (Name.ToUpper(), Direction).GetHashCode();

    public override string ToString() => $"{Name} {Direction}";
}