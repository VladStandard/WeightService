// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;
using BlazorCore.Settings;

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
        public static string Sidebar(BlazorAppSettingsHelper appSettings)
        {
            if (appSettings.DataAccess.IsSqlServerReleaseVs)
                return "SidebarRelease";
            if (appSettings.DataAccess.IsSqlServerDevelopVs)
                return "SidebarDebug";
            return "SidebarDefault";
        }
        public static string NavMenu(bool collapseNavMenu) => collapseNavMenu ? "collapse" : string.Empty;
        public static string RadzenPanelMenu(BlazorAppSettingsHelper appSettings)
        {
            if (appSettings.DataAccess.IsSqlServerReleaseVs)
                return "RadzenPanelMenuRelease";
            return appSettings.DataAccess.IsSqlServerDevelopVs 
	            ? "RadzenPanelMenuDebug" : "RadzenPanelMenuDefault";
        }
        public static string RadzenPanelMenuItem(BlazorAppSettingsHelper appSettings)
        {
            if (appSettings.DataAccess.IsSqlServerReleaseVs)
                return "RadzenPanelMenuItemRelease";
            return appSettings.DataAccess.IsSqlServerDevelopVs 
	            ? "RadzenPanelMenuItemDebug" : "RadzenPanelMenuItemDefault";
        }
        public static string RadzenPanelMenuSubItem(BlazorAppSettingsHelper appSettings)
        {
            if (appSettings.DataAccess.IsSqlServerReleaseVs)
                return "RadzenPanelMenuSubItemRelease";
            return appSettings.DataAccess.IsSqlServerDevelopVs 
	            ? "RadzenPanelMenuSubItemDebug" : "RadzenPanelMenuSubItemDefault";
        }
    }

    #endregion
}
