using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Nestings;

[Table(SqlTables.Nestings, Schema = SqlSchemas.Ref1C)]
public sealed class NestingEntity : EfEntityBase
{
    [Column("BUNDLE_COUNT")]
    [Range(1, 100)]
    public short BundleCount { get; set; }

    #region Box

    [ForeignKey("BOX_UID"), Column("BOX_UID")]
    public Guid BoxId { get; set; }
    public BoxEntity Box { get; set; } = new();

    #endregion

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    [NotMapped] public override bool IsNew => CreateDt.Equals(DateTime.MinValue);

    public NestingEntity() { }

    public NestingEntity(Guid uid, DateTime updateDt)
    {
        Id = uid;
        ChangeDt = updateDt;
    }
}