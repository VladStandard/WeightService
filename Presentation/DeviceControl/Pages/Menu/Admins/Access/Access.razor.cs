namespace DeviceControl.Pages.Menu.Admins.Access;

public sealed partial class Access : SectionBase<SqlAccessEntity>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlAccessRepository().GetList(SqlCrudConfigSection);
    }

    private static string GetAccessRightsDescription(byte accessRights) =>
        (EnumAccessRights)accessRights switch
        {
            EnumAccessRights.Read => LocaleCore.Strings.AccessRightsRead,
            EnumAccessRights.Write => LocaleCore.Strings.AccessRightsWrite,
            EnumAccessRights.Admin => LocaleCore.Strings.AccessRightsAdmin,
            _ => LocaleCore.Strings.AccessRightsNone
        };

}
