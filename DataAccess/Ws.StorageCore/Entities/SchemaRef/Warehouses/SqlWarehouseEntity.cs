// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using Ws.StorageCore.Entities.SchemaRef.ProductionSites;

namespace Ws.StorageCore.Entities.SchemaRef.Warehouses;

[DebuggerDisplay("{ToString()}")]
public class SqlWarehouseEntity : SqlEntityBase
{
    public virtual SqlProductionSiteEntity ProductionSite { get; set; }

    public SqlWarehouseEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        ProductionSite = new();
    }

    public SqlWarehouseEntity(SqlWarehouseEntity item) : base(item)
    {
        ProductionSite = new(item.ProductionSite);
    }

    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(ProductionSite)}: {ProductionSite}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlWarehouseEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(SqlWarehouseEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        ProductionSite.Equals(item.ProductionSite);
}
