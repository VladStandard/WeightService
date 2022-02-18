using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Runtime.InteropServices;
public class TSCLIB_DLL
{
    /*
    [DllImport("tsclibnet.dll", EntryPoint = "SocketSend")]
    public static extern string SocketSend(string ip, int port);

    [DllImport("tsclibnet.dll", EntryPoint = "SocketSend")]
    public static extern string SocketSend(string ip, int port);

    [DllImport("tsclibnet.dll", EntryPoint = "SocketSend")]
    public static extern string SocketSend(string ip, int port);
    */

    [DllImport("tsclibnet.dll", EntryPoint = "test")]
    public static extern string test();

    [DllImport("tsclibnet.dll", EntryPoint = "TscUDP_Run")]
    public static extern string TscUDP_Run();

	[DllImport("TSCLIB.dll", EntryPoint = "about")]
    public static extern int about();

	[DllImport("TSCLIB.dll", EntryPoint = "openport")]
    public static extern int openport(string printername);

	[DllImport("TSCLIB.dll", EntryPoint = "barcode")]
    public static extern int barcode(string x, string y, string type,
                string height, string readable, string rotation,
                string narrow, string wide, string code);

	[DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
    public static extern int clearbuffer();

	[DllImport("TSCLIB.dll", EntryPoint = "closeport")]
    public static extern int closeport();

	[DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
    public static extern int downloadpcx(string filename, string image_name);

	[DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
    public static extern int formfeed();

	[DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
    public static extern int nobackfeed();

	[DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
    public static extern int printerfont(string x, string y, string fonttype,
                    string rotation, string xmul, string ymul,
                    string text);

	[DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
    public static extern int printlabel(string set, string copy);

	[DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
    public static extern int sendcommand(string printercommand);

	[DllImport("TSCLIB.dll", EntryPoint = "setup")]
    public static extern int setup(string width, string height,
              string speed, string density,
              string sensor, string vertical,
              string offset);

	[DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
    public static extern int windowsfont(int x, int y, int fontheight, 
					int rotation,		int fontstyle,	int fontunderline,
                    string szFaceName, string content);
   
}


namespace TSCLIB_DLL_IN_C_Sharp
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}