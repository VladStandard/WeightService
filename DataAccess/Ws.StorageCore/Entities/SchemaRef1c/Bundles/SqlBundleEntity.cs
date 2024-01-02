// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
namespace Ws.StorageCore.Entities.SchemaRef1c.Bundles;

[DebuggerDisplay("{ToString()}")]
public class SqlBundleEntity : SqlTable1CBase
{
    public virtual decimal Weight { get; set; }

    public SqlBundleEntity() : base(SqlEnumFieldIdentity.Uid)
    {
       Weight = 0;
    }

    public SqlBundleEntity(SqlBundleEntity item) : base(item)
    {
        Weight = item.Weight;
    }

    public override string ToString() =>
        $"{Name} | {Weight} | {Uid1C}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlBundleEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(SqlBundleEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);
}

