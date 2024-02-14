// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;

namespace Ws.Domain.Abstractions.Entities.Common;

[DebuggerDisplay("{ToString()}")]
public abstract class Entity1CBase : EntityBase
{
    public virtual Guid Uid1C { get; set; } = Guid.Empty;

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj) || !CastEquals((EntityBase)obj)) 
            return false;
        return Equals(Uid1C, ((Entity1CBase)obj).Uid1C);
    }
    
    public override int GetHashCode() => base.GetHashCode();
}