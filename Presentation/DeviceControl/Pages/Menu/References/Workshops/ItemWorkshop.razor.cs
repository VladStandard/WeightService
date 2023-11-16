namespace DeviceControl.Pages.Menu.References.Workshops;

public sealed partial class ItemWorkshop : ItemBase<SqlWorkShopEntity>
{
    #region Public and private fields, properties, constructor

    private List<SqlProductionSiteEntity> ProductionSiteModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        ProductionSiteModels = new SqlProductionSiteRepository().GetEnumerable(SqlCrudConfigFactory.GetCrudActual()).ToList();
    }

    #endregion
}
