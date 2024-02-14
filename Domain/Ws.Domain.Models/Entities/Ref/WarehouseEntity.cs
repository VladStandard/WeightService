// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class WarehouseEntity : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual ProductionSiteEntity ProductionSite { get; set; } = new();

    protected override bool CastEquals(EntityBase obj)
    {
        WarehouseEntity item = (WarehouseEntity)obj;
        return Equals(ProductionSite, item.ProductionSite) && Equals(Name, item.Name);
    }

    public override string ToString() => $"{Name} {ProductionSite}";
}