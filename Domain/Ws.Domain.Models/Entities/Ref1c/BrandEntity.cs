// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class BrandEntity : Table1CBase
{
    public BrandEntity() : base(SqlEnumFieldIdentity.Uid)
    {
    }

    public BrandEntity(BrandEntity item) : base(item)
    {
    }

    public override bool Equals(object obj)
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