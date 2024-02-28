// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Print;

public class ViewPallet: EntityBase
{ 
    public virtual DateTime ProdDt { get; set; }
    public virtual int Counter { get; set; } = 0;
    public virtual string? Line { get; set; } = string.Empty;
    public virtual string? Warehouse { get; set; } = string.Empty;
    public virtual int LabelCount { get; set; } = 0;
    public virtual string? Plu { get; set; } = string.Empty;
    public virtual decimal? WeightNet { get; set; } = 0;
    public virtual decimal? WeightBrut { get; set; } = 0;
    public virtual string Barcode { get; set; } = string.Empty;
    public virtual string PalletMan { get; set; } = string.Empty;
    public virtual int Kneading { get; set; } = 0;
}