namespace Ws.Database.EntityFramework.Entities.Ref.PalletMen;

[Table(SqlTables.PalletMen, Schema = SqlSchemas.Ref)]
[Index(nameof(Name), nameof(Surname), nameof(Patronymic), Name = $"UQ_{SqlTables.PalletMen}_FIO", IsUnique = true)]
// [Index(nameof(Uid1C), Name = $"UQ_{SqlTables.PalletMen}_UID_1C", IsUnique = true)]
public sealed class PalletManEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32)]
    public string Name { get; set; } = string.Empty;

    [Column("SURNAME")]
    [StringLength(32)]
    public string Surname { get; set; } = string.Empty;

    [Column("PATRONYMIC")]
    [StringLength(32)]
    public string Patronymic { get; set; } = string.Empty;

    [Column("PASSWORD")]
    [StringLength(4)]
    public string Password { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    // public virtual ICollection<Pallet> Pallets { get; set; } = new List<Pallet>();
}