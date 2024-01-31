using System.Diagnostics;

namespace Ws.Domain.Models.Common;

[DebuggerDisplay("{ToString()}")]
public class Entity1CBase : EntityBase
{
    public virtual Guid Uid1C { get; set; }

    public Entity1CBase() : base()
    {
        Uid1C = Guid.Empty;
    }

    public Entity1CBase(SqlEnumFieldIdentity identityName) : base(identityName)
    {
        Uid1C = Guid.Empty;
    }
    
    public virtual bool Equals(Entity1CBase item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&Equals(Uid1C, item.Uid1C);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Entity1CBase)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
}