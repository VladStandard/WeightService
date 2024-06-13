using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Database.EntityFramework.Entities.Ref.PalletMen;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Print;

[Table(SqlTables.Pallets, Schema = SqlSchemas.Print)]
[Index(nameof(Barcode), Name = $"UQ_{SqlTables.Pallets}_BARCODE", IsUnique = true)]
[Index(nameof(Counter), Name = $"UQ_{SqlTables.Pallets}_COUNTER", IsUnique = true)]
public class PalletEntity : EfEntityBase
{
    #region FK

    [ForeignKey("PLU_UID"), Column("PLU_UID")]
    public PluEntity Plu { get; set; } = new();

    [ForeignKey("ARM_UID"), Column("ARM_UID")]
    public LineEntity Arm { get; set; } = new();

    [ForeignKey("PALLET_MAN_UID"), Column("PALLET_MAN_UID")]
    public PalletManEntity PalletMan { get; set; } = new();

    #endregion

    [Column("NUMBER")]
    public uint Number { get; set; }

    [Column("COUNTER")]
    public uint Counter { get; set; }

    [Column("PRODUCT_DT")]
    public DateTime ProductDt { get; set; }

    [Column("TRAY_WEIGHT", TypeName = "decimal(5,3)")]
    public decimal TrayWeight { get; set; }

    [Column("BARCODE"), StringLength(128)]
    public string Barcode { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }

    #endregion
}