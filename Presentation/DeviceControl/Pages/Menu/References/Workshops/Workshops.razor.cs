namespace DeviceControl.Pages.Menu.References.Workshops;

public sealed partial class Workshops : SectionBase<WsSqlWorkShopEntity>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlWorkShopRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
