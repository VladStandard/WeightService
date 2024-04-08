using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;

[Table(SqlTables.Characteristics, Schema = SqlSchemas.Ref1C)]
[Index(nameof(PluId), nameof(BoxId), nameof(BundleCount), Name = $"UQ_{SqlTables.Characteristics}_UNIQ", IsUnique = true)]
public sealed class CharacteristicEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(64, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Column("BUNDLE_COUNT")]
    [Range(1, 100)]
    public short BundleCount { get; set; }

    #region Box

    [ForeignKey("BOX_UID"), Column("BOX_UID")]
    public Guid BoxId { get; set; }
    public BoxEntity Box { get; set; } = new();

    #endregion

    [Column("PLU_UID")]
    public Guid PluId { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    public CharacteristicEntity() { }

    public CharacteristicEntity(Guid uid, DateTime updateDt)
    {
        Id = uid;
        ChangeDt = updateDt;
    }
}