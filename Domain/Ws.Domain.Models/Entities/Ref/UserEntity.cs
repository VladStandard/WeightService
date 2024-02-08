// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Utils;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class UserEntity() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual DateTime LoginDt { get; set; } = SqlTypeUtils.MinDateTime;
    public virtual ISet<ClaimEntity> Claims { get; set; } = new HashSet<ClaimEntity>();

    protected override bool CastEquals(EntityBase obj)
    {
        UserEntity item = (UserEntity)obj;
        return Claims.SetEquals(item.Claims) && Equals(LoginDt, item.LoginDt);
    }
}