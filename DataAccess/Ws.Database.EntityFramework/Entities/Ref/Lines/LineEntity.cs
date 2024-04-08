using Ws.Database.EntityFramework.Entities.Ref.Printers;
using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Ref.Lines;

[Table(SqlTables.Arms)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Arms}_NAME", IsUnique = true)]
[Index(nameof(PcName), Name = $"UQ_{SqlTables.Arms}_PC_NAME", IsUnique = true)]
[Index(nameof(Number), Name = $"UQ_{SqlTables.Arms}_NUMBER", IsUnique = true)]
public sealed class LineEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32)]
    public string Name { get; set; } = string.Empty;

    [Column("COUNTER")]
    [Range(1000000, 9999999)]
    public int Counter { get; set; }

    [Column("NUMBER")]
    [Range(10000, 99999)]
    public int Number { get; set; }

    [ForeignKey("WAREHOUSE_UID")]
    public WarehouseEntity Warehouse { get; set; } = new();

    [ForeignKey("PRINTER_UID")]
    public PrinterEntity Printer { get; set; } = new();

    [Column("PC_NAME")]
    [StringLength(16, ErrorMessage = "Name must be between 1 and 16 characters")]
    public string PcName { get; set; } = string.Empty;

    [Column("TYPE", TypeName = "varchar(8)")]
    public LineType Type { get; set; } = LineType.Tablet;

    public ICollection<PluEntity> Plus { get; set; } = [];

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    // public virtual PrinterEntity PrinterU { get; set; } = null!;
    //
    // public virtual WarehouseEntity WarehouseU { get; set; } = null!;
}