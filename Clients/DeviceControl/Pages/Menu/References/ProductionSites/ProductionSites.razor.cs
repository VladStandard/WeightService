using WsStorageCore.Tables.TableRefModels.ProductionSites;

namespace DeviceControl.Pages.Menu.References.ProductionSites;

public sealed partial class ProductionSites : SectionBase<WsSqlProductionSiteModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlProductionSiteRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
