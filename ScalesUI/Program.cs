// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WeightCore.Db;
using ScalesUI.Forms;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WeightCore.Utils;
using System.Runtime.CompilerServices;

// ReSharper disable IdentifierTypo

namespace ScalesUI
{
    internal static class Program
    {
        internal static void MainExec([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            string conectionString = Properties.Settings.Default.ConnectionString;
            try
            {
                _ = SqlConnectFactory.GetConnection(conectionString);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(null, $"База данных недоступна. {ex.Message}", Messages.Exception);
                throw new Exception(ex.Message);
            }

            // если нужного файла с токеном не нашлось и не задана строка подключения к БД - то софтина не запускается
            if (!HostEntity.TokenExist())
            {
                try
                {
                    Guid uuid = HostEntity.TokenWrite(conectionString);
                    CustomMessageBox messageBox = CustomMessageBox.Show(null,
                        "Моноблок зарегистрирован в информационной системе с идентификатором" + Environment.NewLine +
                        $"{uuid}" + Environment.NewLine +
                        "Перед повторным запуском сопоставьте его с текущей линией в приложении DeviceControl.",
                        Messages.Registration);
                    messageBox.Wait();
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
                    CustomMessageBox messageBox = CustomMessageBox.Show(null, message, Messages.Exception);
                }
                Application.Exit();
                return;
            }

            HostEntity host = new HostEntity();
            //var memory = new MemorySizeEntity();
            host.TokenRead();
            if (host.CurrentScaleId == 0)
            {
                CustomMessageBox messageBox = CustomMessageBox.Show(null,
                    "Моноблок зарегистрирован в информационной системе с идентификатором" + Environment.NewLine +
                    $"{host.IdRRef}" + Environment.NewLine +
                    "Перед повторным запуском сопоставьте его с текущей линией в приложении DeviceControl.",
                    Messages.Registration);
                messageBox.Wait();
                if (messageBox.Result == DialogResult.OK)
                {
                    Clipboard.SetText($@"{host.IdRRef}");
                }
                Application.Exit();
                return;
            }
            
            _ = new Mutex(true, Application.ProductName, out bool first);
            if (first != true)
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

        [STAThread]
        internal static void Main()
        {
            MainExec();
        }
    }
}
