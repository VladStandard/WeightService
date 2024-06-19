namespace Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

public sealed class ProductionSiteEntity : EfEntityBase
{
    [Column(SqlColumns.Name), StringLength(64)]
    public string Name { get; set; } = string.Empty;

    [Column("ADDRESS"), StringLength(128)]
    public string Address { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}