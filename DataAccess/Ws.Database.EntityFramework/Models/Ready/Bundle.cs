using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.Bundles)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Bundles}_NAME", IsUnique = true)]
[Index(nameof(Uid1C), Name = $"UQ_{SqlTables.Bundles}_UID_1C", IsUnique = true)]
public sealed class Bundle : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Range(0, 1.000, ErrorMessage = "Weight must be less than 1.000")]
    [RegularExpression(@"^\d+\.\d{3}$", ErrorMessage = "Weight must have exactly three decimal places")]
    [Column(SqlColumns.Weight, TypeName = "decimal(4,3)")]
    public decimal Weight { get; set; }
    
    [Column("UID_1C")]
    public Guid Uid1C { get; set; }
    
    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }
    
    #endregion
    // public virtual ICollection<Plu> Plus { get; set; } = new List<Plu>();
}
