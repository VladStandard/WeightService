// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;
using BlazorCore.Settings;
using DataCore.Enums;
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
        public static string Sidebar(BlazorAppSettingsHelper appSettings) =>
            DebugHelper.Instance.Config switch
            {
                Configuration.DevelopAleksandrov => "SidebarDebug",
                Configuration.DevelopMorozov => "SidebarDebug",
                Configuration.DevelopVS => "SidebarDebug",
                Configuration.ReleaseAleksandrov => "SidebarRelease",
                Configuration.ReleaseMorozov => "SidebarRelease",
                Configuration.ReleaseVS => "SidebarRelease",
                _ => "SidebarDefault"
            };
        public static string NavMenu(bool collapseNavMenu) => collapseNavMenu ? "collapse" : string.Empty;
        public static string RadzenPanelMenu(BlazorAppSettingsHelper appSettings) =>
            DebugHelper.Instance.Config switch
            {
                Configuration.DevelopAleksandrov => "RadzenPanelMenuDebug",
                Configuration.DevelopMorozov => "RadzenPanelMenuDebug",
                Configuration.DevelopVS => "RadzenPanelMenuDebug",
                Configuration.ReleaseAleksandrov => "RadzenPanelMenuRelease",
                Configuration.ReleaseMorozov => "RadzenPanelMenuRelease",
                Configuration.ReleaseVS => "RadzenPanelMenuRelease",
                _ => "RadzenPanelMenuDefault"
            };

        public static string RadzenPanelMenuItem(BlazorAppSettingsHelper appSettings) =>
            DebugHelper.Instance.Config switch
            {
                Configuration.DevelopAleksandrov => "RadzenPanelMenuItemDebug",
                Configuration.DevelopMorozov => "RadzenPanelMenuItemDebug",
                Configuration.DevelopVS => "RadzenPanelMenuItemDebug",
                Configuration.ReleaseAleksandrov => "RadzenPanelMenuItemRelease",
                Configuration.ReleaseMorozov => "RadzenPanelMenuItemRelease",
                Configuration.ReleaseVS => "RadzenPanelMenuItemRelease",
                _ => "RadzenPanelMenuItemDefault"
            };
        public static string RadzenPanelMenuSubItem(BlazorAppSettingsHelper appSettings) =>
            DebugHelper.Instance.Config switch
            {
                Configuration.DevelopAleksandrov => "RadzenPanelMenuSubItemDebug",
                Configuration.DevelopMorozov => "RadzenPanelMenuSubItemDebug",
                Configuration.DevelopVS => "RadzenPanelMenuSubItemDebug",
                Configuration.ReleaseAleksandrov => "RadzenPanelMenuSubItemRelease",
                Configuration.ReleaseMorozov => "RadzenPanelMenuSubItemRelease",
                Configuration.ReleaseVS => "RadzenPanelMenuSubItemRelease",
                _ => "RadzenPanelMenuSubItemDefault"
            };
    }

    #endregion
}