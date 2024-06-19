using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Print.Labels;

public sealed class LabelEntity : EfEntityBase
{
    #region ForeignKeys

    public Guid? PalletEntityId { get; set; }
    public PluEntity? Plu { get; set; }
    public LineEntity Line { get; set; } = new();

    #endregion

    #region ProductDt
    public DateTime ProductDt { get; set; }
    public DateTime ExpirationDt { get; set; }

    #endregion

    #region Barcodes

    public string BarcodeTop { get; set; } = string.Empty;
    public string BarcodeRight { get; set; } = string.Empty;
    public string BarcodeBottom { get; set; } = string.Empty;

    #endregion

    #region Weight

    public decimal WeightNet { get; set; }
    public decimal WeightTare { get; set; }

    #endregion

    #region Other

    public ushort BundleCount { get; set; }
    public short Kneading { get; set; }
    public string Zpl { get; set; } = string.Empty;

    #endregion

    #region Date

    public DateTime CreateDt { get; init; }

    #endregion
}