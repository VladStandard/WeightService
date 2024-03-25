using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Boxes;

[Table(SqlTables.Boxes)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Boxes}_NAME", IsUnique = true)]
[Index(nameof(Uid1C), Name = $"UQ_{SqlTables.Boxes}_UID_1C", IsUnique = true)]
public sealed class BoxEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(64, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 64 characters")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 1.000, ErrorMessage = "Weight must be less than 1.000")]
    [Column(SqlColumns.Weight, TypeName = "decimal(4,3)")]
    public decimal Weight { get; set; }

    [Column("UID_1C")]
    public Guid Uid1C { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    public ICollection<PluNestingEntity> PlusNestingFks { get; set; } = [];
}