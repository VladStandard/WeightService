// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class ClaimEntity : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        ClaimEntity item = (ClaimEntity)obj;
        return Equals(Name, item.Name);
    }
};