namespace DeviceControl.Components;

public sealed partial class NavMenu : ComponentBase
{
    #region Public and private fields, properties, constructor
    
    private static WsLocaleDeviceControl LocaleBlazor => new();

    private static string ItemMenuCss => WsDebugHelper.Instance.IsDevelop ? "MenuItemDebug" : "MenuItemRelease";

    private static string SidebarCss => WsDebugHelper.Instance.IsDevelop ? "SidebarDebug" : "SidebarRelease";

    #endregion
}
