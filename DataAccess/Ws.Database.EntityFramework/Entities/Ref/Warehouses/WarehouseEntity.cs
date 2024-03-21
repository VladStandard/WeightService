using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

namespace Ws.Database.EntityFramework.Entities.Ref.Warehouses;

[Table(SqlTables.Warehouses)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Warehouses}_NAME", IsUnique = true)]
public sealed class WarehouseEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;
    
    [ForeignKey("PRODUCTION_SITE_UID")]
    public ProductionSiteEntity ProductionSite { get; set; } = new();

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    
    // public virtual ICollection<LineEntity> Lines { get; set; } = new List<LineEntity>();
}
