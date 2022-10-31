// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;
using BlazorCore.Settings;

namespace BlazorCore.Utils;

public static class BlazorCoreUtuls
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
            if (appSettings.IsSqlServerRelease)
                return "SidebarRelease";
            else if (appSettings.IsSqlServerDebug)
                return "SidebarDebug";
            return "SidebarDefault";
        }

        public static string MudSelect => "MudSelect";
        public static string RadzenButtonLong => "RadzenButtonLong";
        public static string MudSelectEdge => "MudSelectEdge";
        public static string MudSelectFlexible => "MudSelectFlexible";
        public static string NavMenu(bool collapseNavMenu) => collapseNavMenu ? "collapse" : string.Empty;
        public static string RadzenPanelMenu(BlazorAppSettingsHelper appSettings)
        {
            if (appSettings.IsSqlServerRelease)
                return "RadzenPanelMenuRelease";
            return appSettings.IsSqlServerDebug 
	            ? "RadzenPanelMenuDebug" : "RadzenPanelMenuDefault";
        }

        public static string RadzenPanelMenuItem(BlazorAppSettingsHelper appSettings)
        {
            if (appSettings.IsSqlServerRelease)
                return "RadzenPanelMenuItemRelease";
            return appSettings.IsSqlServerDebug 
	            ? "RadzenPanelMenuItemDebug" : "RadzenPanelMenuItemDefault";
        }
        public static string RadzenPanelMenuSubItem(BlazorAppSettingsHelper appSettings)
        {
            if (appSettings.IsSqlServerRelease)
                return "RadzenPanelMenuSubItemRelease";
            return appSettings.IsSqlServerDebug 
	            ? "RadzenPanelMenuSubItemDebug" : "RadzenPanelMenuSubItemDefault";
        }
        public static string RadzenProgressBar => "RadzenProgressBar";
    }

    #endregion
}
