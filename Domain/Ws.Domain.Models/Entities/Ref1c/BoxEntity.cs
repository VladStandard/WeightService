// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class BoxEntity : Entity1CBase
{
    public virtual decimal Weight { get; set; }

    public BoxEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Weight = 0;
    }

    public override string ToString() =>
        $"{Uid1C} | {Name} | {Weight}";

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BoxEntity)obj);
    }
    
    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(BoxEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);
}