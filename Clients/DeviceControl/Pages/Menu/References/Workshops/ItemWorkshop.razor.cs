using WsStorageCore.Tables.TableRefModels.ProductionSites;
using WsStorageCore.Tables.TableRefModels.WorkShops;

namespace DeviceControl.Pages.Menu.References.Workshops;

public sealed partial class ItemWorkshop : ItemBase<WsSqlWorkShopModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlProductionSiteModel> ProductionSiteModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        ProductionSiteModels = new WsSqlProductionSiteRepository().GetEnumerable(WsSqlCrudConfigFactory.GetCrudActual()).ToList();
    }

    #endregion
}
