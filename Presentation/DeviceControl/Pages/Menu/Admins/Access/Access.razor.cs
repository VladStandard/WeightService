namespace DeviceControl.Pages.Menu.Admins.Access;

public sealed partial class Access : SectionBase<SqlAccessEntity>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlAccessRepository().GetList(SqlCrudConfigSection);
    }

    private static string GetAccessRightsDescription(byte accessRights) =>
        (WsEnumAccessRights)accessRights switch
        {
            WsEnumAccessRights.Read => WsLocaleCore.Strings.AccessRightsRead,
            WsEnumAccessRights.Write => WsLocaleCore.Strings.AccessRightsWrite,
            WsEnumAccessRights.Admin => WsLocaleCore.Strings.AccessRightsAdmin,
            _ => WsLocaleCore.Strings.AccessRightsNone
        };

}
