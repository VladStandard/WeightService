using System;
using System.Threading;
using System.Windows.Forms;
// ReSharper disable IdentifierTypo

namespace TapangaMaha
{
    internal static class Program
    {

        [STAThread]
        internal static void Main()
        {
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
