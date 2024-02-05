// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class BrandEntity() : Entity1CBase(SqlEnumFieldIdentity.Uid)
{
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BrandEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(BrandEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item);
}