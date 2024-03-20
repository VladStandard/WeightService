// using Ws.Database.EntityFramework.Models.Ready;
//
// namespace Ws.Database.EntityFramework.Models;
//
// public partial class Label
// {
//     public Guid Uid { get; set; }
//
//     public DateTime CreateDt { get; set; }
//
//     public string BarcodeTop { get; set; } = null!;
//
//     public string BarcodeRight { get; set; } = null!;
//
//     public string BarcodeBottom { get; set; } = null!;
//
//     public decimal WeightNetto { get; set; }
//
//     public decimal WeightTare { get; set; }
//
//     public Guid? PalletUid { get; set; }
//
//     public short Kneading { get; set; }
//
//     public DateTime ProdDt { get; set; }
//
//     public DateTime ExpirationDt { get; set; }
//
//     public Guid PluUid { get; set; }
//
//     public Guid LineUid { get; set; }
//
//     public string Zpl { get; set; } = null!;
//
//     public virtual Line LineU { get; set; } = null!;
//
//     public virtual Pallet? PalletU { get; set; }
//
//     public virtual Plu PluU { get; set; } = null!;
// }
