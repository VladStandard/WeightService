// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.DAL.TableDirectModels;
using DataCore.DAL.Utils;
using DataCore.Helpers;
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
        private static AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;

        internal static void TokenWrite(string conectionString, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            try
            {
                Guid uid = HostsUtils.TokenWrite(conectionString);
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.Registration;
                wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.RegistrationWarning1(uid);
                wpfPageLoader.MessageBox.ButtonYesVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.ButtonNoVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.Localization();
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
                wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.Exception;
                wpfPageLoader.MessageBox.Message = message;
                wpfPageLoader.MessageBox.ButtonOkVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.Localization();
                wpfPageLoader.ShowDialog();
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
            }
        }

        [STAThread]
        internal static void Main()
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());
#if DEBUG
            string conectionString = Properties.Settings.Default.ConnectionStringDebug;
#else
            string conectionString = Properties.Settings.Default.ConnectionStringRelease;
#endif
            try
            {
                _ = SqlConnectFactory.GetConnection(conectionString);
            }
            catch (Exception ex)
            {
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.Exception;
                wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.ExceptionSqlDb + Environment.NewLine + Environment.NewLine + ex.Message;
                wpfPageLoader.MessageBox.ButtonOkVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.Localization();
                wpfPageLoader.ShowDialog();
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
                throw new Exception(ex.Message);
            }

            // Exit.
            if (!HostsUtils.TokenExist())
            {
                TokenWrite(conectionString);
                Application.Exit();
                return;
            }

            HostDirect host = HostsUtils.TokenRead();
            // Exit.
            if (host.ScaleId == 0)
            {
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.Registration;
                wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.RegistrationWarning2(host.IdRRef);
                wpfPageLoader.MessageBox.ButtonOkVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.Localization();
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
    }
}
