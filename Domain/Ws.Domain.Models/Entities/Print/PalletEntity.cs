// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Models.Entities.Print;

[DebuggerDisplay("{ToString()}")]
public class PalletEntity : EntityBase
{
    public virtual LineEntity Line { get; set; }
    public virtual PluEntity Plu { get; set; }
    public virtual DateTime ProductDt { get; set; }
    public virtual DateTime ExpirationDt { get => ProductDt.AddDays(Plu.ShelfLifeDays); set => _ = value; }
    public virtual short Kneading { get; set; }
    
    public PalletEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Line = new();
        ProductDt = DateTime.MinValue;
        ExpirationDt = DateTime.MinValue;
    }

    public PalletEntity(PalletEntity item) : base(item)
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
        return Equals((PalletEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(PalletEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(ProductDt, item.ProductDt) &&
        Equals(ExpirationDt, item.ExpirationDt) &&
        Equals(Plu, item.Plu) &&
        Equals(Line, item.Line) && 
        Equals(Kneading, item.Kneading);
}