// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Utils;

public static class WsFormUtils
{
    #region Public and private methods

    public static void SwitchResolution(this Form form, WsEnumScreenResolution resolution)
    {
        switch (resolution)
        {
            case WsEnumScreenResolution.Value800x600:
                form.WindowState = FormWindowState.Normal;
                form.Size = new(800, 600);
                break;
            case WsEnumScreenResolution.Value1024x768:
                form.WindowState = FormWindowState.Normal;
                form.Size = new(1024, 768);
                break;
            case WsEnumScreenResolution.Value1366x768:
                form.WindowState = FormWindowState.Normal;
                form.Size = new(1366, 768);
                break;
            case WsEnumScreenResolution.Value1600x1024:
                form.WindowState = FormWindowState.Normal;
                form.Size = new(1600, 1024);
                break;
            case WsEnumScreenResolution.Value1920x1080:
                form.WindowState = FormWindowState.Normal;
                form.Size = new(1920, 1080);
                break;
            default:
                form.WindowState = FormWindowState.Maximized;
                break;
        }
        WsFontsSettingsHelper.Instance.Transform(form.Width, form.Height);
    }

    #endregion
}