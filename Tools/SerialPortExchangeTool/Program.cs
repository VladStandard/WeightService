using System;
using System.Windows.Forms;

namespace SerialPortExchangeTool;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        MainForm view = new()
        {
            StartPosition = FormStartPosition.CenterScreen
        };
        Application.Run(view);
    }
}