using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.ProductionSites)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.ProductionSites}_NAME", IsUnique = true)]
[Index(nameof(Address), Name = $"UQ_{SqlTables.ProductionSites}_ADDRESS", IsUnique = true)]
public sealed class ProductionSite : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(64, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 64 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("ADDRESS")]
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 128 characters")]
    public string Address { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    // public virtual ICollection<Printer> Printers { get; set; } = new List<Printer>();
    //
    // public virtual ICollection<User> Users { get; set; } = new List<User>();
    //
    // public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
