// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Helpers;
using WsLocalizationCore.DeviceControlModels;

namespace DeviceControl.Components;

public sealed partial class NavMenu : RazorComponentBase
{
    #region Public and private fields, properties, constructor

    private static WsLocaleDeviceControl LocaleBlazor => new();

    private static string ItemMenuCss => WsDebugHelper.Instance.IsDevelop ? "MenuItemDebug" : "MenuItemRelease";

    private static string SidebarCss => WsDebugHelper.Instance.IsDevelop ? "SidebarDebug" : "SidebarRelease";

    #endregion
}