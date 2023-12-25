namespace DeviceControl.Pages.Menu.References.Workshops;

public sealed partial class ItemWorkshop : ItemBase<SqlWorkShopEntity>
{
    private List<SqlProductionSiteEntity> ProductionSiteModels { get; set; }

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        ProductionSiteModels = new SqlProductionSiteRepository().GetEnumerable(SqlCrudConfigFactory.GetCrudActual()).ToList();
    }
}
