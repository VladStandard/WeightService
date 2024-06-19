// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Users;

[DebuggerDisplay("{ToString()}")]
public class Claim : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        Claim item = (Claim)obj;
        return Equals(Name, item.Name);
    }
};