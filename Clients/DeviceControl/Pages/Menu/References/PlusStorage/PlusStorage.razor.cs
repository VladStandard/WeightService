namespace DeviceControl.Pages.Menu.References.PlusStorage;

public sealed partial class PlusStorage : SectionBase<WsSqlPluStorageMethodModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlPluStorageMethodRepository().GetList(SqlCrudConfigSection);
    }
}
