// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Utils;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class UserEntity : EntityBase
{
    public virtual DateTime LoginDt { get; set; }
    public virtual ISet<ClaimEntity> Claims { get; set; }

    public UserEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        LoginDt = SqlTypeUtils.MinDateTime;
        Claims = new HashSet<ClaimEntity>();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((UserEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(UserEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Claims.SetEquals(item.Claims) &&
        LoginDt.Equals(item.LoginDt);
}