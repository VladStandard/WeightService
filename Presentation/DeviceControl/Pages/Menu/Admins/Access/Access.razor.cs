using Ws.Shared.Enums;

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
            EnumAccessRights.Read => Locale.AccessRightsRead,
            EnumAccessRights.Write => Locale.AccessRightsWrite,
            EnumAccessRights.Admin => Locale.AccessRightsAdmin,
            _ => Locale.AccessRightsNone
        };

}
