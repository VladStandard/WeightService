using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Print;

[Table(SqlTables.Labels, Schema = SqlSchemas.Print)]
[Index(nameof(BarcodeTop), Name = $"UQ_{SqlTables.Plus}_BARCODE_TOP", IsUnique = true)]
public sealed class LabelEntity : EfEntityBase
{
    [ForeignKey("PALLET_UID"), Column("PALLET_UID")]
    public Guid? PalletEntityId { get; set; }

    [Column("ZPL")]
    public string Zpl { get; set; } = string.Empty;

    [Column("BARCODE_TOP"), StringLength(128)]
    public string BarcodeTop { get; set; } = string.Empty;

    [Column("BARCODE_RIGHT"), StringLength(128)]
    public string BarcodeRight { get; set; } = string.Empty;

    [Column("BARCODE_BOTTOM"), StringLength(128)]
    public string BarcodeBottom { get; set; } = string.Empty;

    [Column("WEIGHT_NET", TypeName = "decimal(5,3)")]
    public decimal WeightNet { get; set; }

    [Column("WEIGHT_TARE", TypeName = "decimal(5,3)")]
    public decimal WeightTare { get; set; }

    [Column("KNEADING")]
    public short Kneading { get; set; }

    [ForeignKey("PLU_UID")]
    public PluEntity? Plu { get; set; }

    [ForeignKey("ARM_UID")]
    public LineEntity Line { get; set; } = new();

    [Column("PRODUCT_DT")]
    public DateTime ProductDt { get; set; }

    [Column("EXPIRATION_DT")]
    public DateTime ExpirationDt { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }

    #endregion
}