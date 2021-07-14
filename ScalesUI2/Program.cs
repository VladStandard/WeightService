// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using EntitiesLib;
using ScalesUI.Forms;
using System;
using System.Threading;
using System.Windows.Forms;
using ScalesUI.Utils;

// ReSharper disable IdentifierTypo

namespace ScalesUI
{
    internal static class Program
    {
        [STAThread]
        internal static void Main(string[] args)
        {
            var conectionString = Properties.Settings.Default.ConnectionString;
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
                    var uuid = HostEntity.TokenWrite(conectionString);
                    var messageBox = CustomMessageBox.Show(null,
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
                finally
                {
                }
                Application.Exit();
                return;
            }

            var host = new HostEntity();
            //var memory = new MemorySizeEntity();
            host.TokenRead();
            if (host.CurrentScaleId == 0)
            {
                var messageBox = CustomMessageBox.Show(null,
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
            _ = new Mutex(true, Application.ProductName, out var first);
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
    }
}
