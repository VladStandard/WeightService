using System.ComponentModel;

namespace DeviceControl.Source.Features.ThemeSwitcher;

public enum ThemeType
{
    [Description("ThemeLight")]
    Light,
    [Description("ThemeDark")]
    Dark,
    [Description("ThemeSystem")]
    System
}