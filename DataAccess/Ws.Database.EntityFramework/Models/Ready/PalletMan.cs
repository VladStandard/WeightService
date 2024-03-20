using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.PalletMen)]
[Index(nameof(Name),nameof(Surname), nameof(Patronymic), Name = $"UQ_{SqlTables.PalletMen}_FIO", IsUnique = true)]
[Index(nameof(Uid1C), Name = $"UQ_{SqlTables.PalletMen}_UID_1C", IsUnique = true)]
public sealed class PalletMan : EfEntityBase
{
    [Column(SqlColumns.Uid1C)]
    public Guid Uid1C { get; set; }
    
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("SURNAME")]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Surname { get; set; } = string.Empty;

    [Column("PATRONYMIC")]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Patronymic { get; set; } = string.Empty;

    [Column("PASSWORD")]
    [StringLength(4, ErrorMessage = "Name must be between 4 characters")]
    public string Password { get; set; } = string.Empty;

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
    
    // public virtual ICollection<Pallet> Pallets { get; set; } = new List<Pallet>();
}
