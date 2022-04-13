// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;

namespace WeightCore.Gui
{
    public static class GuiUtils
    {
        #region Public and private methods

        /// <summary>
        /// Show pin-code form.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static bool ShowPinCodeForm(IWin32Window owner)
        {
            using PasswordForm passwordForm = new();
            DialogResult resultPsw = passwordForm.ShowDialog(owner);
            passwordForm.Close();
            passwordForm.Dispose();
            return resultPsw == DialogResult.OK;
        }

        public static void ShowWpfSettings(IWin32Window owner, bool isDebug)
        {
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
            wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.OperationControl;
            wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.DeviceControlIsPreview;
            wpfPageLoader.MessageBox.ButtonYesVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.ButtonNoVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.Localization();
            DialogResult resultWpf = wpfPageLoader.ShowDialog(owner);
            wpfPageLoader.Close();
            wpfPageLoader.Dispose();
            if (resultWpf == DialogResult.Yes)
                Process.Start(isDebug
                    ? "https://device-control-dev-preview.kolbasa-vs.local/" : "https://device-control-prod-preview.kolbasa-vs.local/");
            else
                Process.Start(isDebug
                    ? "https://device-control-dev.kolbasa-vs.local/" : "https://device-control.kolbasa-vs.local/");
        }

        public static void ShowWpfCatch(IWin32Window owner, string message, int lineNumber, string memberName)
        {
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
            wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.Exception;
            wpfPageLoader.MessageBox.Message =
                @$"{@LocalizationData.ScalesUI.Method}: {memberName}." + Environment.NewLine +
                $"{@LocalizationData.ScalesUI.Line}: {lineNumber}." + Environment.NewLine + Environment.NewLine + message;
            wpfPageLoader.MessageBox.ButtonOkVisibility = System.Windows.Visibility.Visible;
            wpfPageLoader.MessageBox.Localization();
            if (owner != null)
                wpfPageLoader.ShowDialog(owner);
            else
                wpfPageLoader.ShowDialog();
            wpfPageLoader.Close();
            wpfPageLoader.Dispose();
        }

        #endregion
    }
}
