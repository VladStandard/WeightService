// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class ClaimEntity : EntityBase
{
    public ClaimEntity() : base(SqlEnumFieldIdentity.Uid) {}
    
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ClaimEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(ClaimEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item);
}