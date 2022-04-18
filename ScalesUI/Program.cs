// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.TableDirectModels;
using DataCore.DAL.Utils;
using DataCore.DAL;
using DataCore.Helpers;
using DataCore;
using LocalizationCore = DataCore.Localization.Core;
using ScalesUI.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System;
using WeightCore.Gui;

// ReSharper disable IdentifierTypo
namespace ScalesUI
{
    internal static class Program
    {
        #region Public and private fields and properties

        private static AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;
        public static SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

        #endregion

        #region Public and private methods

        internal static void TokenWrite(string conectionString, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
                    [CallerMemberName] string memberName = "")
        {
            try
            {
                Guid uid = HostsUtils.TokenWrite(conectionString);
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationCore.Scales.Registration;
                wpfPageLoader.MessageBox.Message = LocalizationCore.Scales.RegistrationWarning1(uid);
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
                wpfPageLoader.MessageBox.Caption = LocalizationCore.Scales.Exception;
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
            AppVersion.Setup(Assembly.GetExecutingAssembly());
            try
            {
                SqlConnect.GetConnection();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.ShowNewCatch(null,
                    LocalizationCore.Scales.ExceptionSqlDb + Environment.NewLine + Environment.NewLine + ex.Message,
                    filePath, lineNumber, memberName);
                throw new Exception(ex.Message);
            }

            // Exit.
            if (!HostsUtils.TokenExist())
            {
                TokenWrite(DataAccessHelper.Instance.ConnectionString);
                Application.Exit();
                return;
            }

            HostDirect host = HostsUtils.TokenRead();
            // Exit.
            if (host.ScaleId == 0)
            {
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationCore.Scales.Registration;
                wpfPageLoader.MessageBox.Message = LocalizationCore.Scales.RegistrationWarning2(host.IdRRef);
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
                MessageBox.Show($@"Application {Application.ProductName} already running!");
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
