using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Print;

[Table(SqlTables.Labels, Schema = SqlSchemas.Print),
 Index(nameof(BarcodeTop), Name = $"UQ_{SqlTables.Plus}_BARCODE_TOP", IsUnique = true)]
public class LabelEntity : EfEntityBase
{
    [Column("ZPL")]
    public virtual string Zpl { get; set; } = string.Empty;

    [Column("BARCODE_TOP"), StringLength(128)]
    public virtual string BarcodeTop { get; set; } = string.Empty;

    [Column("BARCODE_RIGHT"), StringLength(128)]
    public virtual string BarcodeRight { get; set; } = string.Empty;

    [Column("BARCODE_BOTTOM"), StringLength(128)]
    public virtual string BarcodeBottom { get; set; } = string.Empty;

    [Column("WEIGHT_NET", TypeName = "decimal(5,3)")]
    public virtual decimal WeightNet { get; set; }

    [Column("WEIGHT_TARE", TypeName = "decimal(5,3)")]
    public virtual decimal WeightTare { get; set; }

    [Column("KNEADING")] public virtual short Kneading { get; set; }

    [ForeignKey("PLU_UID")] public virtual PluEntity? Plu { get; set; }

    [ForeignKey("ARM_UID")] public virtual LineEntity Line { get; set; } = new();

    [Column("PRODUCT_DT")] public virtual DateTime ProductDt { get; set; }

    [Column("EXPIRATION_DT")] public virtual DateTime ExpirationDt { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }

    #endregion
}