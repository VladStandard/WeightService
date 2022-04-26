// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using System;
using System.Diagnostics;

namespace DataCore.Utils
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
                if (appSettings != null)
                {
                    if (appSettings.IsSqlServerRelease)
                        return "sidebarRelease";
                    if (appSettings.IsSqlServerDebug)
                        return "sidebarDebug";
                }
                return "sidebarUnknown";
            }

            public static string MudSelect => "MudSelect";
            public static string NavMenu(bool collapseNavMenu) => collapseNavMenu ? "collapse" : string.Empty;
            public static string RadzenPanelMenu => "RadzenPanelMenu";
            public static string RadzenPanelMenuItem => "RadzenPanelMenuItem";
            public static string RadzenPanelMenuSubItem => "RadzenPanelMenuSubItem";
            public static string RadzenProgressBar => "RadzenProgressBar";
        }

        #endregion
    }
}
