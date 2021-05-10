// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using EntitiesLib;
using ScalesUI.Forms;
using System;
using System.Threading;
using System.Windows.Forms;
using WeightServices.Common;
// ReSharper disable IdentifierTypo

namespace ScalesUI
{
    internal static class Program
    {

        [STAThread]
        internal static void Main(string[] args)
        {

            string conectionString = Properties.Settings.Default.ConnectionString.ToString();
            try
            {
                var x = SqlConnectFactory.GetConnection(conectionString);
            }
            catch (Exception ex)
            {
                if (CustomMessageBox.Show($"База данных недоступна. {ex.Message}") == DialogResult.OK)
                {
                }
                throw new Exception(ex.Message);

            }

            // если нужного файла с токеном не нашлось
            // и не задана строка подключения к БД 
            // то софтина не запускается
            if (!HostEntity.TokenExist())
            {
                try
                {
                    var uuid = HostEntity.TokenWrite(conectionString);
                    if (CustomMessageBox.Show(
                        $"Моноблок зарегистрирован в информационной системе с идентификатором\n" +
                        $"{ uuid}"+
                        "Перед повторным запуском сопоставьте его с текущей линией в приложении DeviceControl."
                        ) == DialogResult.OK)
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
            else
            {
                HostEntity Host = new HostEntity();
                Host.TokenRead();
                if (Host.CurrentScaleId == 0)
                {
                    if (CustomMessageBox.Show($"Моноблок зарегистрирован в информационной системе с идентификатором\n"+
                        $"{Host.IdRRef.ToString()}\n"+
                        $"Перед повторным запуском сопоставьте его с текущей линией в приложении DeviceControl."
                        ) == DialogResult.OK)
                    {
                        Clipboard.SetText($@"{Host.IdRRef.ToString()}");
                    }
                    Application.Exit();
                    return;
                }
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
