using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.StorageMethods)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.StorageMethods}_NAME", IsUnique = true)]
[Index(nameof(Zpl), Name = $"UQ_{SqlTables.StorageMethods}_ZPL", IsUnique = true)]
public sealed class StorageMethod : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("ZPL")]
    [StringLength(1024, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 1024 characters")]
    public string Zpl { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    // public virtual ICollection<Plu> Plus { get; set; } = new List<Plu>();
}
