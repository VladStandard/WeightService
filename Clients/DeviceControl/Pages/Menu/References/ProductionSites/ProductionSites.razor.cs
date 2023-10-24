namespace DeviceControl.Pages.Menu.References.ProductionSites;

public sealed partial class ProductionSites : SectionBase<WsSqlProductionSiteEntity>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlProductionSiteRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
