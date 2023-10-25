namespace DeviceControl.Pages.Menu.References.Organizations;

public sealed partial class Organizations : SectionBase<WsSqlOrganizationEntity>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlOrganizationRepository().GetList(SqlCrudConfigSection);
    }
}
