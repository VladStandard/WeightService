// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsLocalizationCore.Utils;

namespace WsBlazorCore.Razors;

public class UserSettingsModel
{
    #region Public and private fields, properties, constructor

    public bool AccessRightsIsAdmin => (byte)AccessRights >= (byte)WsEnumAccessRights.Admin;
    public bool AccessRightsIsNone => (byte)AccessRights == (byte)WsEnumAccessRights.None;
    public bool AccessRightsIsRead => (byte)AccessRights >= (byte)WsEnumAccessRights.Read;
    public bool AccessRightsIsWrite => (byte)AccessRights >= (byte)WsEnumAccessRights.Write;
    public WsEnumAccessRights AccessRights { get; }
    public string? UserName { get; }
    public string UserDescription => $"{WsLocaleCore.Strings.AuthorizingUserName}: {UserName}";

    /// <summary>
    /// Constructor.
    /// </summary>
    public UserSettingsModel()
    {
        AccessRights = WsEnumAccessRights.None;
        UserName = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="accessRights"></param>
    /// <param name="userName"></param>
    /// <param name="id"></param>
    /// <param name="ipAddress"></param>
    public UserSettingsModel(string? userName, WsEnumAccessRights accessRights)
    {
        UserName = userName;
        AccessRights = accessRights;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{nameof(UserName)}: {UserName}. " + $"{nameof(AccessRights)}: {AccessRights}. ";

    private string GetColorAccessRights(WsEnumAccessRights accessRights, uint rowCounter) =>
        accessRights switch
        {
            WsEnumAccessRights.Read => rowCounter % 2 == 0 ? "rz-datatable-even-read" : "rz-datatable-odd-read",
            WsEnumAccessRights.Write => rowCounter % 2 == 0 ? "rz-datatable-even-write" : "rz-datatable-odd-write",
            WsEnumAccessRights.Admin => rowCounter % 2 == 0 ? "rz-datatable-even-admin" : "rz-datatable-odd-admin",
            _ => rowCounter % 2 == 0 ? "rz-datatable-even-none" : "rz-datatable-odd-none",
        };

    public string GetColorAccessRights(WsEnumAccessRights accessRights) =>
        accessRights switch
        {
            WsEnumAccessRights.Read => "rz-datatable-read",
            WsEnumAccessRights.Write => "rz-datatable-write",
            WsEnumAccessRights.Admin => "rz-datatable-admin",
            _ => ".rz-datatable-none",
        };

    #endregion
}