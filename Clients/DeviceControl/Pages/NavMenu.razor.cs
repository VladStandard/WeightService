// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Helpers;

namespace DeviceControl.Pages;

public sealed partial class NavMenu : RazorComponentBase
{
    #region Public and private fields, properties, constructor
    
    private static LocaleDeviceControl LocaleBlazor => LocaleDeviceControl.Instance;

    private static string ItemMenuCss => WsDebugHelper.Instance.IsDevelop ? "MenuItemDebug": "MenuItemRelease";

    private static string SidebarCss => WsDebugHelper.Instance.IsDevelop ? "SidebarDebug" : "SidebarRelease";

    #endregion

    #region Public and private methods
    
    #endregion
}
