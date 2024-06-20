// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Models.Entities.Users;

[DebuggerDisplay("{ToString()}")]
public class User : EntityBase
{
    public virtual ProductionSite ProductionSite { get; set; } = new();

    protected override bool CastEquals(EntityBase obj)
    {
        User item = (User)obj;
        return Equals(ProductionSite, item.ProductionSite);
    }
}