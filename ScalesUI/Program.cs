// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.DAL.TableDirectModels;
using DataCore.DAL.Utils;
using DataCore.Localizations;
using DataCore.Settings;
using ScalesUI.Forms;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using WeightCore.Gui;

// ReSharper disable IdentifierTypo
namespace ScalesUI
{
    internal static class Program
    {
        #region Public and private fields and properties

        private static AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;
        private static DataAccessHelper DataAccess { get; set; } = DataAccessHelper.Instance;

        #endregion

        #region Public and private methods

        internal static void TokenWrite(string conectionString, 
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                Guid uid = HostsUtils.TokenWrite(conectionString);
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.Registration;
                wpfPageLoader.MessageBox.Message = LocaleCore.Scales.RegistrationWarning1(uid);
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonYesVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonNoVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.Localization();
                wpfPageLoader.ShowDialog();
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
                if (result == DialogResult.OK)
                {
                    Clipboard.SetText($@"{uid}");
                    return;
                }
            }
            catch (Exception ex)
            {
                filePath = Path.GetFileName(filePath);
                string message = $"Файл: {filePath}" + Environment.NewLine +
                                 $"Метод: {memberName}. Строка: {lineNumber}" + Environment.NewLine + Environment.NewLine +
                                 ex.Message;
                if (ex.InnerException != null)
                    message += Environment.NewLine + ex.InnerException;
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.Exception;
                wpfPageLoader.MessageBox.Message = message;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.Localization();
                wpfPageLoader.ShowDialog();
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
            }
        }

        [STAThread]
        internal static void Main()
        {
            MainInside();
        }

        internal static void MainInside(
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                AppVersion.Setup(Assembly.GetExecutingAssembly());
                if (!DataAccess.Setup(Directory.GetCurrentDirectory()))
                {
                    if (!DataAccess.DownloadAppSettings(Directory.GetCurrentDirectory()))
                    {
                        MessageBox.Show(LocaleCore.System.SystemSettingsNotFound);
                        return;
                    }
                    if (!DataAccess.Setup(Directory.GetCurrentDirectory()))
                    {
                        MessageBox.Show(LocaleCore.System.SystemSettingsNotFound);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.ShowNewCatch(null,
                    LocaleCore.Scales.ExceptionSqlDb + Environment.NewLine + Environment.NewLine + ex.Message,
                    false, filePath, lineNumber, memberName);
                throw new Exception(ex.Message);
            }

            // Exit.
            if (!HostsUtils.TokenExist())
            {
                TokenWrite(DataAccess.ConnectionString);
                Application.Exit();
                return;
            }

            HostDirect host = HostsUtils.TokenRead();
            // Exit.
            if (host.ScaleId == 0)
            {
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.Registration;
                wpfPageLoader.MessageBox.Message = LocaleCore.Scales.RegistrationWarning2(host.IdRRef);
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.VisibilitySettings.Localization();
                wpfPageLoader.ShowDialog();
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
                if (result == DialogResult.OK)
                {
                    Clipboard.SetText($@"{host.IdRRef}");
                }
                Application.Exit();
                return;
            }

            _ = new Mutex(true, Application.ProductName, out bool createdNew);
            if (!createdNew)
            {
                MessageBox.Show($@"{LocaleCore.Strings.Application} {Application.ProductName} already running!");
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }

        #endregion
    }
}
