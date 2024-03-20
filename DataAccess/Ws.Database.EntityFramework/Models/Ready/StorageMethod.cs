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

    [Column("DESCRIPTION")]
    [StringLength(1024, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 1024 characters")]
    public string Zpl { get; set; } = string.Empty;

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
    // public virtual ICollection<Plu> Plus { get; set; } = new List<Plu>();
}
