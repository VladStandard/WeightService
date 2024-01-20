using System.Diagnostics;

namespace Ws.Domain.Models.Common;

[DebuggerDisplay("{ToString()}")]
public class Table1CBase : EntityBase
{
    public virtual Guid Uid1C { get; set; }

    public Table1CBase() : base()
    {
        Uid1C = Guid.Empty;
    }

    public Table1CBase(SqlEnumFieldIdentity identityName) : base(identityName)
    {
        Uid1C = Guid.Empty;
    }

    public Table1CBase(Table1CBase item) : base(item)
    {
        Uid1C = item.Uid1C;
    }
    
    public virtual bool Equals(Table1CBase item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&Equals(Uid1C, item.Uid1C);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Table1CBase)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
}