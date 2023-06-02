// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MDSoft.BarcodePrintUtils.Tsc;
using System;
using System.Windows;
using MDSoft.BarcodePrintUtils.Utils;
using WsPrintCore.Common;
using WsPrintCore.Zpl;

namespace TscPrintDemoWpf.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Public and private fields and properties

    private TscDriverHelper TscDriver { get; } = TscDriverHelper.Instance;

    #endregion

    #region Constructor and destructor

    public MainWindow()
    {
        InitializeComponent();

        object context = FindResource("PrintControlEntityViewModel");
        if (context is TscDriverHelper printControl)
        {
            TscDriver = printControl;
            TscDriver.Properties.PrintName = "SCALES-PRN-005";
            TscDriver.Properties.PrintIp = "192.168.4.159";
            TscDriver.Properties.PrintPort = 9100;
        }
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        //
    }

    #endregion

    #region Public and private methods

    private void Usb_Click(object sender, RoutedEventArgs e)
    {
        //string WT1 = "TSC Printers";
        //string B1 = "20080101";
        //byte[] result_unicode = System.Text.Encoding.GetEncoding("utf-16").GetBytes("unicode test");
        //byte[] result_utf8 = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,\"ARIAL.TTF\",0,12,12,\"utf8 test Wörter auf Deutsch\"");

        //byte status = TscLib.usbportqueryprinter();//0 = idle, 1 = head open, 16 = pause, following <ESC>!? command of TSPL manual
        //TSCSDK.usb. openport("usb");
        //TscLib.sendcommand("SIZE 100 mm, 120 mm");
        //TscLib.sendcommand("SPEED 4");
        //TscLib.sendcommand("DENSITY 12");
        //TscLib.sendcommand("DIRECTION 1");
        //TscLib.sendcommand("SET TEAR ON");
        //TscLib.sendcommand("CODEPAGE UTF-8");
        //TscLib.clearbuffer();
        //TscLib.downloadpcx("UL.PCX", "UL.PCX");
        //TscLib.windowsfont(40, 490, 48, 0, 0, 0, "Arial", "Windows Font Test");
        //TscLib.windowsfontUnicode(40, 550, 48, 0, 0, 0, "Arial", result_unicode);
        //TscLib.sendcommand("PUTPCX 40,40,\"UL.PCX\"");
        //TscLib.sendBinaryData(result_utf8, result_utf8.Length);
        //TscLib.barcode("40", "300", "128", "80", "1", "0", "2", "2", B1);
        //TscLib.printerfont("40", "440", "0", "0", "15", "15", WT1);
        //TscLib.printlabel("1", "1");
        //TscLib.closeport();
    }

    private void SetupByName_Click(object sender, RoutedEventArgs e)
    {
        TscDriver.Setup(WsEnumPrintChannel.Name, TscDriver.Properties.PrintName, TscDriver.Properties.Size, TscDriver.Properties.Dpi);
    }

    private void SetupByIpPort_Click(object sender, RoutedEventArgs e)
    {
        TscDriver.Setup(WsEnumPrintChannel.Ethernet, TscDriver.Properties.PrintIp, TscDriver.Properties.PrintPort, TscDriver.Properties.Size, TscDriver.Properties.Dpi);
    }

    private void CmdCalibrate_Click(object sender, RoutedEventArgs e)
    {
        TscDriver.SendCmd("GAPDETECT");
    }

    private void CmdSendCustom_Click(object sender, RoutedEventArgs e)
    {
        TscDriver.SendCmd(TscDriver.Cmd);
    }

    private void CmdConvertZpl_Click(object sender, RoutedEventArgs e) => DataFormatUtils.CmdConvertZpl(TscDriver, true);

    private void CmdSetCutter_Click(object sender, RoutedEventArgs e)
    {
        TscDriver.SendCmdCutter(TscDriver.Properties.CutterValue);
    }

    private void CmdPrintTest_Click(object sender, RoutedEventArgs e)
    {
        TscDriver.SendCmdTest();
    }

    private void Feed_Click(object sender, RoutedEventArgs e)
    {
        var value = TscDriver.Properties.Dpi switch
        {
            WsEnumPrintLabelDpi.Dpi100 => 4 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi200 => 8 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi300 => 12 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi400 => 16 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi500 => 20 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi600 => 24 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi700 => 28 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi800 => 32 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi900 => 36 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi1000 => 40 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi1100 => 44 * TscDriver.Properties.FeedMm,
            WsEnumPrintLabelDpi.Dpi1200 => 48 * TscDriver.Properties.FeedMm,
            _ => throw new ArgumentOutOfRangeException(nameof(TscDriver.Properties.Dpi), TscDriver.Properties.Dpi.ToString()),
        };
        if (value > 0)
            TscDriver.SendCmd($"FEED {value}");
    }

    #endregion

    #region Public and private methods - ZPL

    private void GetZpl1_OnClick(object sender, RoutedEventArgs e)
    {
        TscDriver.TextPrepare = ZplSamples.GetSample1;
        DataFormatUtils.CmdConvertZpl(TscDriver, true);
    }

    private void GetZpl2_OnClick(object sender, RoutedEventArgs e)
    {
        TscDriver.TextPrepare = ZplSamples.GetSample2;
        DataFormatUtils.CmdConvertZpl(TscDriver, true);
    }

    private void GetZpl3_OnClick(object sender, RoutedEventArgs e)
    {
        TscDriver.TextPrepare = ZplSamples.GetSample3;
        DataFormatUtils.CmdConvertZpl(TscDriver, true);
    }

    private void GetZplFull_OnClick(object sender, RoutedEventArgs e)
    {
        TscDriver.TextPrepare = ZplSamples.GetSampleFull;
        DataFormatUtils.CmdConvertZpl(TscDriver, true);
    }

    #endregion
}