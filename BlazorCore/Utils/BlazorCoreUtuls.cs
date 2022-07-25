// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using BlazorCore.Models;

namespace BlazorCore.Utils
{
    public static class BlazorCoreUtuls
    {
        #region Public and private methods

        public static string GetLibVersion()
        {
            string result = string.Empty;
            FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (fieVersionInfo == null || string.IsNullOrEmpty(fieVersionInfo.FileVersion))
                return result;

            result = fieVersionInfo.FileVersion;
            if (result.EndsWith(".0"))
                result = result[..result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase)];
            return result ?? string.Empty;
        }

        public static class GetCssName
        {
            public static string Sidebar(AppSettingsHelper appSettings)
            {
                if (appSettings.IsSqlServerRelease)
                    return "SidebarRelease";
                else if (appSettings.IsSqlServerDebug)
                    return "SidebarDebug";
                return "SidebarDefault";
            }

            public static string MudSelect => "MudSelect";
            public static string MudSelectFlexible => "MudSelectFlexible";
            public static string NavMenu(bool collapseNavMenu) => collapseNavMenu ? "collapse" : string.Empty;
            public static string RadzenPanelMenu(AppSettingsHelper appSettings)
            {
                if (appSettings.IsSqlServerRelease)
                    return "RadzenPanelMenuRelease";
                else if (appSettings.IsSqlServerDebug)
                    return "RadzenPanelMenuDebug";
                return "RadzenPanelMenuDefault";
            }

            public static string RadzenPanelMenuItem(AppSettingsHelper appSettings)
            {
                if (appSettings.IsSqlServerRelease)
                    return "RadzenPanelMenuItemRelease";
                else if (appSettings.IsSqlServerDebug)
                    return "RadzenPanelMenuItemDebug";
                return "RadzenPanelMenuItemDefault";
            }
            public static string RadzenPanelMenuSubItem(AppSettingsHelper appSettings)
            {
                if (appSettings.IsSqlServerRelease)
                    return "RadzenPanelMenuSubItemRelease";
                else if (appSettings.IsSqlServerDebug)
                    return "RadzenPanelMenuSubItemDebug";
                return "RadzenPanelMenuSubItemDefault";
            }
            public static string RadzenProgressBar => "RadzenProgressBar";
        }

        #endregion
    }
}
