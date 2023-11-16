using System.Windows.Forms;

namespace Ws.LabelCore.Utils;

public static class FormExtensions
{
    #region Public and private methods

    // TODO: FIX SETTINGS (link with enum)
    public static void SetupResolution(this Form form)
    {
        if (LabelSessionHelper.Instance.Line.Host.Name == "PC473")
        {
            form.MaximizeBox = true;
            form.MinimizeBox = true;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        else
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
        }
        FontsSettingsHelper.Instance.Transform(form.Width, form.Height);
     
    }

    // private void ReturnOkFromDeviceSettings()
    // {
    //     bool isFormFullScreen = true;
    //     foreach (WsSqlDeviceSettingsFkEntity deviceSettingsFk in ContextManager.DeviceSettingsFksRepository.GetEnumerableByLine(LabelSession.Line))
    //     {
    //         switch (deviceSettingsFk.Setting.Name)
    //         {
    //             // Отображать кнопку максимизации.
    //             case "IsShowMaximizeButton":
    //                 MaximizeBox = deviceSettingsFk.IsEnabled;
    //                 FormBorderStyle = deviceSettingsFk.IsEnabled ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
    //                 if (deviceSettingsFk.IsEnabled)
    //                     isFormFullScreen = false;
    //                 break;
    //             // Отображать кнопку минимизации.
    //             case "IsShowMinimizeButton":
    //                 MinimizeBox = deviceSettingsFk.IsEnabled;
    //                 FormBorderStyle = deviceSettingsFk.IsEnabled ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
    //                 if (deviceSettingsFk.IsEnabled)
    //                     isFormFullScreen = false;
    //                 break;
    //             // Отображать кнопку печати.
    //             case "IsShowPrintButton":
    //                 ButtonPrint.Visible = deviceSettingsFk.IsEnabled;
    //                 break;
    //         }
    //     }
    //     if (isFormFullScreen)
    //         this.SetupResolution();
    // }
 
    
    #endregion
}