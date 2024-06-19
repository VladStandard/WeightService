// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class Warehouse : EntityBase
{
    public virtual Guid Uid1C { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual ProductionSite ProductionSite { get; set; } = new();

    protected override bool CastEquals(EntityBase obj)
    {
        Warehouse item = (Warehouse)obj;
        return Equals(ProductionSite, item.ProductionSite) && Equals(Name, item.Name) && Equals(Uid, item.Uid1C);
    }

    public override string ToString() => Name;
}