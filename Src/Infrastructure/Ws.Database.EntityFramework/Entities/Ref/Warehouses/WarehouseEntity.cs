using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

namespace Ws.Database.EntityFramework.Entities.Ref.Warehouses;

public sealed class WarehouseEntity : EfEntityBase
{
    public Guid Uid1C { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProductionSiteEntity ProductionSite { get; set; } = new();

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}