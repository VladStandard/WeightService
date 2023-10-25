namespace DeviceControl.Pages.Menu.References.Workshops;

public sealed partial class ItemWorkshop : ItemBase<WsSqlWorkShopEntity>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlProductionSiteEntity> ProductionSiteModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        ProductionSiteModels = new WsSqlProductionSiteRepository().GetEnumerable(WsSqlCrudConfigFactory.GetCrudActual()).ToList();
    }

    #endregion
}
