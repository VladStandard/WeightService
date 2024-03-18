using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.Brands)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Brands}_NAME")]
[Index(nameof(Uid1C), Name = $"UQ_{SqlTables.Brands}_UID_1C")]
public partial class Brand : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = null!;

    [Column("UID_1C")]
    public Guid Uid1C { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ReadOnly(true)]
    [Column(SqlColumns.CreateDt)]
    public DateTime CreateDt { get; private set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [ReadOnly(true)]
    [Column(SqlColumns.ChangeDt)]
    public DateTime ChangeDt { get; private set; }
    
    // public virtual ICollection<Plu> Plus { get; set; } = new List<Plu>();
}
