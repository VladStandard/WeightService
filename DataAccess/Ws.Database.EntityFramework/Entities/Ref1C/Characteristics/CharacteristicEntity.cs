using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;

[Table(SqlTables.Characteristics)]
[Index(nameof(BundleCount), nameof(BoxId), Name = $"UQ_{SqlTables.Characteristics}_BUNDLE_BOX", IsUnique = true)]
public sealed class CharacteristicEntity : EfEntityBase
{
    [Column("UID_1C")]
    public Guid Uid1C { get; set; }

    [Column("BUNDLE_COUNT")]
    [Range(1, 100, ErrorMessage = "BundleCount must be between 1 and 100")]
    public short BundleCount { get; set; }

    #region Box

    [ForeignKey("BOX_UID"), Column("BOX_UID")]
    public Guid BoxId { get; set; }
    public BoxEntity Box { get; set; } = new();

    #endregion

    [ForeignKey("PLU_UID"), Column("PLU_UID")]
    public Guid PluEntityId { get; set; }

    [Column("IS_DEFAULT")]
    public bool IsDefault { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}