using System.Net;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

namespace Ws.Database.EntityFramework.Entities.Ref.Printers;

[Table(SqlTables.Printers)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Printers}_NAME", IsUnique = true)]
[Index(nameof(Ip), Name = $"UQ_{SqlTables.Printers}_IP_V4", IsUnique = true)]
public sealed class PrinterEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(16, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 16 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("IP_V4")]
    public IPAddress Ip { get; set; } = IPAddress.Parse("127.0.0.1");
    
    [Column("TYPE", TypeName = "varchar(8)")]
    public PrinterType Type { get; set; } = PrinterType.Tsc;
    
    [ForeignKey("PRODUCTION_SITE_UID")]
    public ProductionSiteEntity ProductionSite { get; set; } = new();
    
    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    
    // public ICollection<LineEntity> Lines { get; set; } = new List<LineEntity>();
}
