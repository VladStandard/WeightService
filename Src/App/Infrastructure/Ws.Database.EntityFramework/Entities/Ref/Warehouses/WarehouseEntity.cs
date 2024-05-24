using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

namespace Ws.Database.EntityFramework.Entities.Ref.Warehouses;

[Table(SqlTables.Warehouses, Schema = SqlSchemas.Ref)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Warehouses}_NAME", IsUnique = true)]
public sealed class WarehouseEntity : EfEntityBase
{
    [Column(SqlColumns.Name), StringLength(32)]
    public string Name { get; set; } = string.Empty;

    [ForeignKey("PRODUCTION_SITE_UID")]
    public ProductionSiteEntity ProductionSite { get; set; } = new();

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}