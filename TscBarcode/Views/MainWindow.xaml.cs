// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using WeightCore.Print;
using WeightCore.Print.Tsc;
using WeightCore.Zpl;

namespace TscBarcode.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Public and private fields and properties

        public TscPrintControlHelper PrintControl { get; set; }

        #endregion

        #region Constructor and destructor

        public MainWindow()
        {
            InitializeComponent();

            object context = FindResource("PrintControlEntityViewModel");
            if (context is TscPrintControlHelper printControl)
            {
                PrintControl = printControl;
                if (string.IsNullOrEmpty(PrintControl.PrintIp))
                    PrintControl.PrintIp = "192.168.7.41";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ButtonClose_Click(sender, null);
        }

        #endregion

        #region Public and private methods

        private void ButtonUsb_Click(object sender, RoutedEventArgs e)
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

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            //PrintControl.Open();
        }

        private void ButtonCmdCalibrate_Click(object sender, RoutedEventArgs e)
        {
            //PrintControl.CmdSendCustom("GAPDETECT");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            //PrintControl.Close();
        }

        private void ButtonCmdSendCustom_Click(object sender, RoutedEventArgs e)
        {
            //PrintControl.CmdSendCustom(PrintControl.Text);
        }

        private void ButtonCmdConvertZpl_Click(object sender, RoutedEventArgs e)
        {
            PrintControl.CmdConvertZpl(true);
        }

        private void ButtonCmdSetCutter_Click(object sender, RoutedEventArgs e)
        {
            //if (PrintControl.CutterValue >= 0)
            //    PrintControl.CmdSendCustom($"SET CUTTER {PrintControl.CutterValue}");
        }

        private void ButtonCmdPrintTest_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Name))
                return;
            TSCSDK.driver tscDriver = new();
            if (!tscDriver.openport(Name))
                return;
            tscDriver.clearbuffer();

            tscDriver.barcode("100", "200", "128", "100", "1", "0", "3", "3", "123456789");
            tscDriver.printerfont("100", "100", "3", "0", "1", "1", "Printer Font Test");
            tscDriver.sendcommand("BOX 50,50,500,400,3\n");
            tscDriver.printlabel("1", "1");

            tscDriver.closeport();
        }

        private void ButtonCmdClearBuffer_Click(object sender, RoutedEventArgs e)
        {
            //PrintControl.CmdClearBuffer();
        }

        private void ButtonPrintSetupReset_Click(object sender, RoutedEventArgs e)
        {
            PrintControl.SetupHardware();
        }

        private void ButtonPrintSetup_Click(object sender, RoutedEventArgs e)
        {
            PrintControl.SetupHardware(PrintControl.Size);
        }

        private void ButtonFeed_Click(object sender, RoutedEventArgs e)
        {
            var value = PrintControl.Dpi switch
            {
                PrintDpi.Dpi100 => 4 * PrintControl.FeedMm,
                PrintDpi.Dpi200 => 8 * PrintControl.FeedMm,
                PrintDpi.Dpi300 => 12 * PrintControl.FeedMm,
                PrintDpi.Dpi400 => 16 * PrintControl.FeedMm,
                PrintDpi.Dpi500 => 20 * PrintControl.FeedMm,
                PrintDpi.Dpi600 => 24 * PrintControl.FeedMm,
                PrintDpi.Dpi700 => 28 * PrintControl.FeedMm,
                PrintDpi.Dpi800 => 32 * PrintControl.FeedMm,
                PrintDpi.Dpi900 => 36 * PrintControl.FeedMm,
                PrintDpi.Dpi1000 => 40 * PrintControl.FeedMm,
                PrintDpi.Dpi1100 => 44 * PrintControl.FeedMm,
                PrintDpi.Dpi1200 => 48 * PrintControl.FeedMm,
                _ => throw new ArgumentOutOfRangeException(nameof(PrintControl.Dpi), PrintControl.Dpi, null),
            };
            if (value > 0)
                PrintControl.SendCmd($"FEED {value}");
        }

        #endregion

        #region Public and private methods - ZPL

        public void ButtonGetZpl1_OnClick(object sender, RoutedEventArgs e)
        {
            PrintControl.TextPrepare = ZplSamples.GetSample1;
            PrintControl.CmdConvertZpl(true);
        }

        public void ButtonGetZpl2_OnClick(object sender, RoutedEventArgs e)
        {
            PrintControl.TextPrepare = ZplSamples.GetSample2;
            PrintControl.CmdConvertZpl(true);
        }

        public void ButtonGetZpl3_OnClick(object sender, RoutedEventArgs e)
        {
            PrintControl.TextPrepare = ZplSamples.GetSample3;
            PrintControl.CmdConvertZpl(true);
        }

        public void ButtonGetZplFull_OnClick(object sender, RoutedEventArgs e)
        {
            PrintControl.TextPrepare = ZplSamples.GetSampleFull;
            PrintControl.CmdConvertZpl(true);
        }

        #endregion
    }
}
