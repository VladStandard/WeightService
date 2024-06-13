// ReSharper disable VirtualMemberCallInConstructor

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace Ws.Domain.Models.Entities.Print;

[DebuggerDisplay("{ToString()}")]
public class Label : EntityBase
{
    public virtual string Zpl { get; set; } = string.Empty;
    public virtual string BarcodeTop { get; set; } = string.Empty;
    public virtual string BarcodeRight { get; set; } = string.Empty;
    public virtual string BarcodeBottom { get; set; } = string.Empty;
    public virtual decimal WeightNet { get; set; }
    public virtual decimal WeightTare { get; set; }
    public virtual short Kneading { get; set; }
    public virtual Guid? PalletUid { get; set; }
    public virtual Plu? Plu { get; set; }
    public virtual Arm Line { get; set; } = new();
    public virtual DateTime ProductDt { get; set; }
    public virtual DateTime ExpirationDt { get; set; }
    public virtual decimal WeightGross => WeightNet + WeightTare;

    public override string ToString() => $"{CreateDt} : {Plu?.Name}";

    protected override bool CastEquals(EntityBase obj)
    {
        Label item = (Label)obj;
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
               Equals(PalletUid, item.PalletUid);
    }
}