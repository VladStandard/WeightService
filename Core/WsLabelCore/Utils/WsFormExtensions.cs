using System.Windows.Forms;
namespace WsLabelCore.Utils;

public static class WsFormExtensions
{
    #region Public and private methods

    public static void SetupResolution(this Form form)
    {   
        form.FormBorderStyle = FormBorderStyle.None;
        form.WindowState = FormWindowState.Maximized;
        WsFontsSettingsHelper.Instance.Transform(form.Width, form.Height);
    }

    #endregion
}