using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.Warehouses)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Warehouses}_NAME", IsUnique = true)]
public sealed class Warehouse : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;
    
    [ForeignKey("PRODUCTION_SITE_UID")]
    public ProductionSite ProductionSite { get; set; } = new();

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    
    // public virtual ICollection<Line> Lines { get; set; } = new List<Line>();
}
