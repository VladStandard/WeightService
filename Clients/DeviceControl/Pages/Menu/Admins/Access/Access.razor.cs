// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsStorageCore.Tables.TableScaleModels.Access;

namespace DeviceControl.Pages.Menu.Admins.Access;

public sealed partial class Access : SectionBase<WsSqlAccessModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlAccessRepository().GetList(SqlCrudConfigSection);
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