using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Nestings;

public sealed class NestingEntity : EfEntityBase
{
    public short BundleCount { get; set; }

    #region Box

    public Guid BoxId { get; set; }
    public BoxEntity Box { get; set; } = new();

    #endregion

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}