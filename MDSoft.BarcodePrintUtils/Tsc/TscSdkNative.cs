// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.InteropServices;

namespace MDSoft.BarcodePrintUtils.Tsc
{
    public class TscSdkNative
    {
        [DllImport("tscsdk.dll", EntryPoint = "about")]
        public static extern int About();

        [DllImport("tscsdk.dll", EntryPoint = "barcode")]
        public static extern int BarCode(string x, string y, string type, string height, string readable, string rotation, string narrow, string wide, string code);

        [DllImport("tscsdk.dll", EntryPoint = "clearbuffer")]
        public static extern int ClearBuffer();

        [DllImport("tscsdk.dll", EntryPoint = "closeport")]
        public static extern int ClosePort();

        [DllImport("tscsdk.dll", EntryPoint = "downloadpcx")]
        public static extern int DownloadPcx(string fileName, string imageName);

        [DllImport("tscsdk.dll", EntryPoint = "formfeed")]
        public static extern int FormFeed();

        [DllImport("tscsdk.dll", EntryPoint = "nobackfeed")]
        public static extern int NobackFeed();

        [DllImport("tscsdk.dll", EntryPoint = "openport")]
        public static extern int OpenPort(string printername);

        [DllImport("tscsdk.dll", EntryPoint = "printerfont")]
        public static extern int PrinterFont(string x, string y, string fonttype, string rotation, string xmul, string ymul, string text);
        
        [DllImport("tscsdk.dll", EntryPoint = "printlabel")]
        public static extern int PrintLabel(string set, string copy);

        [DllImport("tscsdk.dll", EntryPoint = "sendBinaryData")]
        public static extern int SendBinaryData(byte[] content, int length);

        [DllImport("tscsdk.dll", EntryPoint = "sendcommand")]
        public static extern int SendCommand(string printercommand);

        [DllImport("tscsdk.dll", EntryPoint = "setup")]
        public static extern int Setup(string width, string height, string speed, string density, string sensor, string vertical,string offset);

        [DllImport("tsclibnet.dll", EntryPoint = "socketsend")]
        public static extern string SocketSend(string ip, int port);

        [DllImport("tscsdk.dll", EntryPoint = "test")]
        public static extern string Test();

        [DllImport("tscsdk.dll", EntryPoint = "tscudp_run")]
        public static extern string UdpRun();

        [DllImport("tscsdk.dll", EntryPoint = "usbportqueryprinter")]
        public static extern byte UsbPortQueryPrinter();

        [DllImport("tscsdk.dll", EntryPoint = "windowsfont")]
        public static extern int WindowsFont(int x, int y, int fontheight, int rotation, int fontstyle, int fontunderline, string szFaceName, string content);
        
        [DllImport("tscsdk.dll", EntryPoint = "windowsfontunicode")]
        public static extern int WindowsFontUnicode(int x, int y, int fontheight, int rotation, int fontstyle, int fontunderline, string szFaceName, byte[] content);
    }
}
