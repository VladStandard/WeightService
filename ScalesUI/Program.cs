// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.DAL.Utils;
using DataShareCore.Helpers;
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
        private static readonly AppVersionHelper _appVersion = AppVersionHelper.Instance;

        internal static void TokenWrite(string conectionString, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string memberName = "")
        {
            try
            {
                Guid uuid = HostsUtils.TokenWrite(conectionString);
                CustomMessageBox messageBox = new();
                messageBox.Show(null,
                    "Моноблок зарегистрирован в информационной системе с идентификатором" + Environment.NewLine +
                    $"{uuid}" + Environment.NewLine +
                    "Перед повторным запуском сопоставьте его с текущей линией в приложении DeviceControl.",
                    LocalizationData.ScalesUI.Registration);
                if (messageBox.Result == DialogResult.OK)
                {
                    Clipboard.SetText($@"{uuid}");
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
                CustomMessageBox messageBox = new();
                messageBox.Show(null, message, LocalizationData.ScalesUI.Exception);
            }
        }

        [STAThread]
        internal static void Main()
        {
            _appVersion.Setup(Assembly.GetExecutingAssembly());

            string conectionString = Properties.Settings.Default.ConnectionString;
            try
            {
                _ = SqlConnectFactory.GetConnection(conectionString);
            }
            catch (Exception ex)
            {
                CustomMessageBox messageBox = new();
                messageBox.Show(null, $"База данных недоступна. {ex.Message}", LocalizationData.ScalesUI.Exception);
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
                CustomMessageBox messageBox = new();
                messageBox.Show(null,
                    "Моноблок зарегистрирован в информационной системе с идентификатором" + Environment.NewLine +
                    $"{host.IdRRef}" + Environment.NewLine +
                    "Перед повторным запуском сопоставьте его с текущей линией в приложении DeviceControl.",
                    LocalizationData.ScalesUI.Registration);
                if (messageBox.Result == DialogResult.OK)
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
