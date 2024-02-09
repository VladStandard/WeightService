// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class WarehouseEntity() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual ProductionSiteEntity ProductionSite { get; set; } = new();

    protected override bool CastEquals(EntityBase obj)
    {
        WarehouseEntity item = (WarehouseEntity)obj;
        return ProductionSite.Equals(item.ProductionSite);
    }

    public override string ToString() => $"{Name} {ProductionSite}";
}