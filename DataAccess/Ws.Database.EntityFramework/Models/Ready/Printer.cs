using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;
using Ws.Database.EntityFramework.ValueTypes;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.Printers)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Printers}_NAME", IsUnique = true)]
[Index(nameof(Ip), Name = $"UQ_{SqlTables.Printers}_IP_V4", IsUnique = true)]
public sealed class Printer : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(16, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 16 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("IP_V4")]
    public IPAddress Ip { get; set; } = IPAddress.Parse("127.0.0.1");
    
    [Column("TYPE", TypeName = "varchar(8)")]
    public PrinterType Type { get; set; } = PrinterType.Tsc;
    
    [ForeignKey("PRODUCTION_SITE_UID")]
    public ProductionSite ProductionSite { get; set; } = new();
    
    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    
    // public ICollection<Line> Lines { get; set; } = new List<Line>();
}
