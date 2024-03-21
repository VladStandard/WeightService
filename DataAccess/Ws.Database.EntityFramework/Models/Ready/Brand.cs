using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.Brands)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Brands}_NAME", IsUnique = true)]
[Index(nameof(Uid1C), Name = $"UQ_{SqlTables.Brands}_UID_1C", IsUnique = true)]
public sealed class Brand : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("UID_1C")]
    public Guid Uid1C { get; set; }
    
    #region Date
    
    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    
    // public virtual ICollection<Plu> Plus { get; set; } = new List<Plu>();
}
