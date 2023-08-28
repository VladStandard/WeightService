namespace DeviceControl.Pages.Menu.References.Organizations;

public sealed partial class Organizations : SectionBase<WsSqlOrganizationModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlOrganizationRepository().GetList(SqlCrudConfigSection);
    }
}
