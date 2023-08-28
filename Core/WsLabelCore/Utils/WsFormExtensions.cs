using System.Windows.Forms;

namespace WsLabelCore.Utils;

/// <summary>
/// WinForms расширения.
/// </summary>
#nullable enable
public static class WsFormExtensions
{
    #region Public and private methods

    public static void SwitchResolution(this Form form, WsEnumScreenResolution resolution)
    {
        switch (resolution)
        {
            case WsEnumScreenResolution.Value1024X768:
                form.WindowState = FormWindowState.Normal;
                form.Size = new(1024, 768);
                break;
            case WsEnumScreenResolution.Value1366X768:
                form.WindowState = FormWindowState.Normal;
                form.Size = new(1366, 768);
                break;
            case WsEnumScreenResolution.Value1600X1024:
                form.WindowState = FormWindowState.Normal;
                form.Size = new(1600, 1024);
                break;
            case WsEnumScreenResolution.Value1920X1080:
                form.WindowState = FormWindowState.Normal;
                form.Size = new(1920, 1080);
                break;

            case WsEnumScreenResolution.Value800X600:
                form.WindowState = FormWindowState.Normal;
                form.Size = new(800, 600);
                break;
            default:
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
                break;
        
        }
        WsFontsSettingsHelper.Instance.Transform(form.Width, form.Height);
    }

    #endregion
}