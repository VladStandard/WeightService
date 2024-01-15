// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace Ws.StorageCore.Entities.SchemaPrint.Pallets;

[DebuggerDisplay("{ToString()}")]
public class SqlPalletEntity : SqlEntityBase
{
    public virtual SqlLineEntity Line { get; set; }
    public virtual SqlPluEntity Plu { get; set; }
    public virtual DateTime ProductDt { get; set; }
    public virtual DateTime ExpirationDt { get => ProductDt.AddDays(Plu.ShelfLifeDays); set => _ = value; }
    public virtual short Kneading { get; set; }
    
    public SqlPalletEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Line = new();
        ProductDt = DateTime.MinValue;
        ExpirationDt = DateTime.MinValue;
    }

    public SqlPalletEntity(SqlPalletEntity item) : base(item)
    {
        Line = new(item.Line);
        Plu = new(item.Plu);
        ProductDt = item.ProductDt;
        ExpirationDt = item.ExpirationDt;
        Kneading = item.Kneading;
    }
    
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPalletEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(SqlPalletEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(ProductDt, item.ProductDt) &&
        Equals(ExpirationDt, item.ExpirationDt) &&
        Equals(Plu, item.Plu) &&
        Equals(Line, item.Line) && 
        Equals(Kneading, item.Kneading);
}