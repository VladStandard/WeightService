// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Models.Entities.Users;

[DebuggerDisplay("{ToString()}")]
public class User : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual DateTime LoginDt { get; set; }

    public virtual ProductionSite? ProductionSite { get; set; }
    public virtual ISet<Claim> Claims { get; set; } = new HashSet<Claim>();

    protected override bool CastEquals(EntityBase obj)
    {
        User item = (User)obj;
        return
            Claims.SetEquals(item.Claims) &&
            Equals(LoginDt, item.LoginDt) &&
            Equals(ProductionSite, item.ProductionSite) &&
            Equals(Name, item.Name);
    }
}