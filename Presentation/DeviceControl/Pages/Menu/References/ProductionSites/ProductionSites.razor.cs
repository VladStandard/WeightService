namespace DeviceControl.Pages.Menu.References.ProductionSites;

public sealed partial class ProductionSites : SectionBase<SqlProductionSiteEntity>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlProductionSiteRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
