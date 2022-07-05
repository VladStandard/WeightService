// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.InteropServices;

namespace MDSoft.BarcodePrintUtils.Tsc
{
    public class TscLibNet64Native
    {
        [DllImport("tsclibnet_x64.dll", EntryPoint = "about")]
        public static extern int About();

        [DllImport("tsclibnet_x64.dll", EntryPoint = "barcode")]
        public static extern int BarCode(string x, string y, string type, string height, string readable, string rotation, string narrow, string wide, string code);

        [DllImport("tsclibnet_x64.dll", EntryPoint = "clearbuffer")]
        public static extern int ClearBuffer();

        [DllImport("tsclibnet_x64.dll", EntryPoint = "closeport")]
        public static extern int ClosePort();

        [DllImport("tsclibnet_x64.dll", EntryPoint = "downloadpcx")]
        public static extern int DownloadPcx(string fileName, string imageName);

        [DllImport("tsclibnet_x64.dll", EntryPoint = "formfeed")]
        public static extern int FormFeed();

        [DllImport("tsclibnet_x64.dll", EntryPoint = "nobackfeed")]
        public static extern int NobackFeed();

        [DllImport("tsclibnet_x64.dll", EntryPoint = "openport")]
        public static extern int OpenPort(string printername);

        [DllImport("tsclibnet_x64.dll", EntryPoint = "printerfont")]
        public static extern int PrinterFont(string x, string y, string fonttype, string rotation, string xmul, string ymul, string text);

        [DllImport("tsclibnet_x64.dll", EntryPoint = "printlabel")]
        public static extern int PrintLabel(string set, string copy);

        [DllImport("tsclibnet_x64.dll", EntryPoint = "sendBinaryData")]
        public static extern int SendBinaryData(byte[] content, int length);

        [DllImport("tsclibnet_x64.dll", EntryPoint = "sendcommand")]
        public static extern int SendCommand(string printercommand);

        [DllImport("tsclibnet_x64.dll", EntryPoint = "setup")]
        public static extern int Setup(string width, string height, string speed, string density, string sensor, string vertical, string offset);

        [DllImport("tsclibnet_x64.dll", EntryPoint = "socketsend")]
        public static extern string SocketSend(string ip, int port);
        
        [DllImport("tsclibnet_x64.dll", EntryPoint = "test")]
        public static extern string Test();

        [DllImport("tsclibnet_x64.dll", EntryPoint = "tscudp_run")]
        public static extern string UdpRun();

        [DllImport("tsclibnet_x64.dll", EntryPoint = "usbportqueryprinter")]
        public static extern byte UsbPortQueryPrinter();

        [DllImport("tsclibnet_x64.dll", EntryPoint = "windowsfont")]
        public static extern int WindowsFont(int x, int y, int fontheight, int rotation, int fontstyle, int fontunderline, string szFaceName, string content);
        
        [DllImport("tsclibnet_x64.dll", EntryPoint = "windowsfontUnicode")]
        public static extern int WindowsFontUnicode(int x, int y, int fontheight, int rotation, int fontstyle, int fontunderline, string szFaceName, byte[] content);
    }
}
