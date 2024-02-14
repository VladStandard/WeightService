// ReSharper disable VirtualMemberCallInConstructor
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Utils;

namespace Ws.Domain.Models.Entities.Print;

[DebuggerDisplay("{ToString()}")]
public class LabelEntity : EntityBase
{
    public virtual string Zpl { get; set; } = string.Empty;
    public virtual string BarcodeTop { get; set; } = string.Empty;
    public virtual string BarcodeRight { get; set; } = string.Empty;
    public virtual string BarcodeBottom { get; set; } = string.Empty;
    public virtual decimal WeightNet { get; set; }
    public virtual decimal WeightTare { get; set; }
    public virtual short Kneading { get; set; }
    public virtual PalletEntity? Pallet { get; set; }
    public virtual PluEntity Plu { get; set; } = new();
    public virtual LineEntity Line { get; set; } = new();
    public virtual DateTime ProductDt { get; set; } = SqlTypeUtils.MinDateTime;
    public virtual DateTime ExpirationDt { get; set; } = SqlTypeUtils.MinDateTime;

    public override string ToString() => $"{CreateDt} : {Plu.Name}";

    protected override bool CastEquals(EntityBase obj)
    {
        LabelEntity item = (LabelEntity)obj;
        return Equals(Zpl, item.Zpl) &&
               Equals(BarcodeTop, item.BarcodeTop) &&
               Equals(BarcodeRight, item.BarcodeRight) &&
               Equals(BarcodeBottom, item.BarcodeBottom) &&
               Equals(WeightNet, item.WeightNet) &&
               Equals(WeightTare, item.WeightTare) &&
               Equals(ProductDt, item.ProductDt) &&
               Equals(ExpirationDt, item.ExpirationDt) &&
               Equals(Kneading, item.Kneading) &&
               Equals(Plu, item.Plu) &&
               Equals(Line, item.Line) &&
               Equals(Pallet, item.Pallet);
    }
}