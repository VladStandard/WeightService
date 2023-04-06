// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;
using DataCore.Helpers;

namespace BlazorCore.Utils;

public static class BlazorCoreUtils
{
    #region Public and private methods

    public static string GetLibVersion()
    {
        string result = string.Empty;
        FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
        if (string.IsNullOrEmpty(fieVersionInfo.FileVersion))
            return result;

        result = fieVersionInfo.FileVersion;
        if (result.EndsWith(".0"))
            result = result[..result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase)];
        return result;
    }

    public static class GetCssName
    {
        public static string NavMenu(bool collapseNavMenu) => collapseNavMenu ? "collapse" : string.Empty;
        public static string Sidebar() => 
            DebugHelper.Instance.IsDevelop ? "SidebarDebug" : "SidebarRelease";
        public static string RadzenPanelMenu() =>
            DebugHelper.Instance.IsDevelop ? "RadzenPanelMenuDebug": "RadzenPanelMenuRelease";
        public static string RadzenPanelMenuItem() =>
            DebugHelper.Instance.IsDevelop ? "RadzenPanelMenuItemDebug": "RadzenPanelMenuItemRelease";
        public static string RadzenPanelMenuSubItem() =>
            DebugHelper.Instance.IsDevelop ? "RadzenPanelMenuSubItemDebug" : "RadzenPanelMenuSubItemRelease";
    }

    #endregion
}