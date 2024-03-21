using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;
using Ws.Database.EntityFramework.ValueTypes;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.Lines)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Lines}_NAME", IsUnique = true)]
[Index(nameof(PcName), Name = $"UQ_{SqlTables.Lines}_PC_NAME", IsUnique = true)]
[Index(nameof(Number), Name = $"UQ_{SqlTables.Lines}_NUMBER", IsUnique = true)]
public sealed class Line : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Column("COUNTER")]
    [Range(1000000, 9999999, ErrorMessage = "Counter must be between 1000000 and 9999999")]
    public int Counter { get; set; }
    
    [Column("NUMBER")]
    [Range(10000, 99999, ErrorMessage = "Number must be between 10000 and 99999")]
    public int Number { get; set; }

    [ForeignKey("WAREHOUSE_UID")]
    public Warehouse Warehouse { get; set; }

    [ForeignKey("PRINTER_UID")]
    public Printer Printer { get; set; }

    [Column("PC_NAME")]
    [StringLength(16, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 16 characters")]
    public string PcName { get; set; } = string.Empty;

    [Column("TYPE", TypeName = "varchar(8)")]
    public LineType Type { get; set; } = LineType.Tablet;

    public ICollection<Plu> Plus { get; set; } = [];
    
    #region Date

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ReadOnly(true)]
    [Column(SqlColumns.CreateDt)]
    public DateTime CreateDt { get; private set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [ReadOnly(true)]
    [Column(SqlColumns.ChangeDt)]
    public DateTime ChangeDt { get; private set; }

    #endregion
    
    // public virtual Printer PrinterU { get; set; } = null!;
    //
    // public virtual Warehouse WarehouseU { get; set; } = null!;
}
