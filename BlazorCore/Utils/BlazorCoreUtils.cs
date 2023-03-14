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
            if (appSettings.DataAccess.IsSqlServerRelease)
                return "SidebarRelease";
            if (appSettings.DataAccess.IsSqlServerDevelop)
                return "SidebarDebug";
            return "SidebarDefault";
        }
        public static string NavMenu(bool collapseNavMenu) => collapseNavMenu ? "collapse" : string.Empty;
        public static string RadzenPanelMenu(BlazorAppSettingsHelper appSettings)
        {
            if (appSettings.DataAccess.IsSqlServerRelease)
                return "RadzenPanelMenuRelease";
            return appSettings.DataAccess.IsSqlServerDevelop 
	            ? "RadzenPanelMenuDebug" : "RadzenPanelMenuDefault";
        }
        public static string RadzenPanelMenuItem(BlazorAppSettingsHelper appSettings)
        {
            if (appSettings.DataAccess.IsSqlServerRelease)
                return "RadzenPanelMenuItemRelease";
            return appSettings.DataAccess.IsSqlServerDevelop 
	            ? "RadzenPanelMenuItemDebug" : "RadzenPanelMenuItemDefault";
        }
        public static string RadzenPanelMenuSubItem(BlazorAppSettingsHelper appSettings)
        {
            if (appSettings.DataAccess.IsSqlServerRelease)
                return "RadzenPanelMenuSubItemRelease";
            return appSettings.DataAccess.IsSqlServerDevelop 
	            ? "RadzenPanelMenuSubItemDebug" : "RadzenPanelMenuSubItemDefault";
        }
    }

    #endregion
}
