// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class WarehouseEntity() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual ProductionSiteEntity ProductionSite { get; set; } = new();

    public override string ToString() => $"{Name} {ProductionSite}";

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WarehouseEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(WarehouseEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        ProductionSite.Equals(item.ProductionSite);
}