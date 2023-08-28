namespace DeviceControl.Pages.Menu.References.Workshops;

public sealed partial class Workshops : SectionBase<WsSqlWorkShopModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlWorkShopRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
