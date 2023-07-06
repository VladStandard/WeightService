using System.Runtime.InteropServices;
using System.Windows;

namespace TscWpfExample;

/// <summary>
/// MainWindow.xaml 的互動邏輯
/// </summary>
/// 
public class TSCLIB_DLL
{
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
                    int rotation, int fontstyle, int fontunderline,
                    string szFaceName, string content);
    [DllImport("TSCLIB.dll", EntryPoint = "windowsfontUnicode")]
    public static extern int windowsfontUnicode(int x, int y, int fontheight,
                     int rotation, int fontstyle, int fontunderline,
                     string szFaceName, byte[] content);

    [DllImport("TSCLIB.dll", EntryPoint = "sendBinaryData")]
    public static extern int sendBinaryData(byte[] content, int length);

    [DllImport("TSCLIB.dll", EntryPoint = "usbportqueryprinter")]
    public static extern byte usbportqueryprinter();
}

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string WT1 = "TSC Printers";
        string B1 = "20080101";
        byte[] result_unicode = System.Text.Encoding.GetEncoding("utf-16").GetBytes("unicode test");
        byte[] result_utf8 = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,\"ARIAL.TTF\",0,12,12,\"utf8 test Wörter auf Deutsch\"");

        //TSCLIB_DLL.about();
        byte status = TSCLIB_DLL.usbportqueryprinter();//0 = idle, 1 = head open, 16 = pause, following <ESC>!? command of TSPL manual
        TSCLIB_DLL.openport("usb");
        TSCLIB_DLL.sendcommand("SIZE 100 mm, 120 mm");
        TSCLIB_DLL.sendcommand("SPEED 4");
        TSCLIB_DLL.sendcommand("DENSITY 12");
        TSCLIB_DLL.sendcommand("DIRECTION 1");
        TSCLIB_DLL.sendcommand("SET TEAR ON");
        TSCLIB_DLL.sendcommand("CODEPAGE UTF-8");
        TSCLIB_DLL.clearbuffer();
        TSCLIB_DLL.downloadpcx("UL.PCX", "UL.PCX");
        TSCLIB_DLL.windowsfont(40, 490, 48, 0, 0, 0, "Arial", "Windows Font Test");
        TSCLIB_DLL.windowsfontUnicode(40, 550, 48, 0, 0, 0, "Arial", result_unicode);
        TSCLIB_DLL.sendcommand("PUTPCX 40,40,\"UL.PCX\"");
        TSCLIB_DLL.sendBinaryData(result_utf8, result_utf8.Length);
        TSCLIB_DLL.barcode("40", "300", "128", "80", "1", "0", "2", "2", B1);
        TSCLIB_DLL.printerfont("40", "440", "0", "0", "15", "15", WT1);
        TSCLIB_DLL.printlabel("1", "1");
        TSCLIB_DLL.closeport();
    }
}
