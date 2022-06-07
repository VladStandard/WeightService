// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MvvmHelpers;
using System;
using System.Threading;
using WeightCore.Zpl;

namespace WeightCore.Print.Tsc
{
    public class TscDriverHelper : BaseViewModel
    {
        #region Design pattern "Lazy Singleton"

        private static TscDriverHelper _instance;
        public static TscDriverHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        //private TSCSDK.driver _driver = null;
        //public TSCSDK.driver Driver
        //{
        //    get
        //    {
        //        if (_driver == null)
        //            _driver = new();
        //        return _driver;
        //    }
        //}
        //private TSCSDK.ethernet _ethernet = null;
        //public TSCSDK.ethernet Ethernet
        //{
        //    get
        //    {
        //        if (_ethernet == null)
        //            _ethernet = new();
        //        return _ethernet;
        //    }
        //}
        public TscPrintPropertiesHelper Properties { get; set; } = TscPrintPropertiesHelper.Instance;
        private string _textPrepare;
        public string TextPrepare
        {
            get => _textPrepare;
            set
            {
                _textPrepare = value;
                OnPropertyChanged();
            }
        }
        private string _cmd;
        public string Cmd
        {
            get => _cmd;
            private set
            {
                _cmd = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Public and private methods

        public void SetupFirst(PrintLabelSize size = PrintLabelSize.Size80x100, PrintDpi dpi = PrintDpi.Dpi300)
        {
            Properties.Size = size;
            Properties.Dpi = dpi;
            Properties.Setup(Properties.Size);
        }

        public void Setup(PrintChannel channel, string name, PrintLabelSize size, PrintDpi dpi)
        {
            SetupFirst(size, dpi);
            Properties.Channel = channel;
            Properties.PrintName = name;
        }

        public void Setup(PrintChannel channel, string ip, int port, PrintLabelSize size, PrintDpi dpi)
        {
            SetupFirst(size, dpi);
            Properties.Channel = channel;
            Properties.PrintIp = ip;
            Properties.PrintPort = port;
        }

        public PrintStatus GetStatusAsEnum(byte? value)
        {
            return value switch
            {
                // Normal
                0x00 => 0x00,
                // Head opened
                0x01 => (PrintStatus)0x01,
                // Paper Jam
                0x02 => (PrintStatus)0x02,
                // Paper Jam and head opened
                0x03 => (PrintStatus)0x03,
                // Out of paper
                0x04 => (PrintStatus)0x04,
                // Out of paper and head opened
                0x05 => (PrintStatus)0x05,
                // Out of ribbon
                0x08 => (PrintStatus)0x08,
                // Out of ribbon and head opened
                0x09 => (PrintStatus)0x09,
                // Out of ribbon and paper jam
                0x0A => (PrintStatus)0x0A,
                // Out of ribbon, paper jam and head opened
                0x0B => (PrintStatus)0x0B,
                // Out of ribbon and out of paper
                0x0C => (PrintStatus)0x0C,
                // Out of ribbon, out of paper and head opened
                0x0D => (PrintStatus)0x0D,
                // Pause
                0x10 => (PrintStatus)0x10,
                // Printing
                0x20 => (PrintStatus)0x20,
                _ => PrintStatus.HundredTwentyEight,
            };
        }

        public bool GetDriverStatus()
        {
            bool result = false;
            switch (Properties.Channel)
            {
                case PrintChannel.Name:
                    TSCSDK.driver driver = new();
                    driver.openport(Properties.PrintName);
                    driver.clearbuffer();
                    result = driver.driver_status(Properties.PrintName);
                    driver.closeport();
                    break;
                case PrintChannel.Ethernet:
                    //
                    break;
            }
            return result;
        }

        public string GetStatusAsStringRus(byte? value) => value switch
        {
            // Normal
            0x00 => "Нормальный",
            // Head opened
            0x01 => "Голова открыта",
            // Paper Jam
            0x02 => "Замятие бумаги",
            // Paper Jam and head opened
            0x03 => "Замятие бумаги и открыта голова",
            // Out of paper
            0x04 => "Нет бумаги",
            // Out of paper and head opened
            0x05 => "Нет бумаги и голова открыта",
            // Out of ribbon
            0x08 => "Нет ленты",
            // Out of ribbon and head opened
            0x09 => "Нет ленты и голова открыта",
            // Out of ribbon and paper jam
            0x0A => "Закончилась лента и застряла бумага",
            // Out of ribbon, paper jam and head opened
            0x0B => "Закончилась лента, застряла бумага и голова открыта",
            // Out of ribbon and out of paper
            0x0C => "Нет ленты и нет бумаги",
            // Out of ribbon, out of paper and head opened
            0x0D => "Нет ленты, нет бумаги и голова открыта",
            // Pause
            0x10 => "Пауза",
            // Printing
            0x20 => "Печать",
            _ => "Другая ошибка",
        };

        public string GetStatusAsStringEng(byte? value) => value switch
        {
            // Normal
            0x00 => "Normal",
            // Head opened
            0x01 => "Head opened",
            // Paper Jam
            0x02 => "Paper Jam",
            // Paper Jam and head opened
            0x03 => "Paper Jam and head opened",
            // Out of paper
            0x04 => "Out of paper",
            // Out of paper and head opened
            0x05 => "Out of paper and head opened",
            // Out of ribbon
            0x08 => "Out of ribbon",
            // Out of ribbon and head opened
            0x09 => "Out of ribbon and head opened",
            // Out of ribbon and paper jam
            0x0A => "Out of ribbon and paper jam",
            // Out of ribbon, paper jam and head opened
            0x0B => "Out of ribbon, paper jam and head opened",
            // Out of ribbon and out of paper
            0x0C => "Out of ribbon and out of paper",
            // Out of ribbon, out of paper and head opened
            0x0D => "Out of ribbon, out of paper and head opened",
            // Pause
            0x10 => "Pause",
            // Printing
            0x20 => "Printing",
            _ => "Other error",
        };

        public void SendCmd(string cmd = "", bool isTest = false)
        {
            Cmd = cmd;

            switch (Properties.Channel)
            {
                case PrintChannel.Name:
                    TSCSDK.driver driver = new();
                    driver.openport(Properties.PrintName);
                    driver.clearbuffer();
                    //driver.setup(Properties.Width.ToString(), Properties.Height.ToString(), Properties.Speed.ToString(),
                    //    Properties.Density.ToString(), Properties.Sensor.ToString(), Properties.Vertical.ToString(), Properties.Offset.ToString());
                    if (isTest)
                    {
                        driver.barcode("100", "200", "128", "100", "1", "0", "3", "3", "123456789");
                        driver.printerfont("100", "100", "3", "0", "1", "1", "Printer Font Test");
                        driver.sendcommand("BOX 50,50,500,400,3" + Environment.NewLine);
                        driver.printlabel("1", "1");
                    }
                    else if (!string.IsNullOrEmpty(Cmd))
                    {
                        driver.sendcommand(Cmd);
                    }
                    driver.closeport();
                    //driver.closeport_mult(9100);
                    break;
                case PrintChannel.Ethernet:
                    TSCSDK.ethernet ethernet = new();
                    ethernet.openport(Properties.PrintIp, Properties.PrintPort);
                    ethernet.clearbuffer();
                    //ethernet.setup(Properties.Width.ToString(), Properties.Height.ToString(), Properties.Speed.ToString(),
                    //    Properties.Density.ToString(), Properties.Sensor.ToString(), Properties.Vertical.ToString(), Properties.Offset.ToString());
                    if (isTest)
                    {
                        ethernet.barcode("100", "200", "128", "100", "1", "0", "3", "3", "123456789");
                        ethernet.printerfont("100", "100", "3", "0", "1", "1", "Printer Font Test");
                        ethernet.sendcommand("BOX 50,50,500,400,3" + Environment.NewLine);
                        ethernet.printlabel("1", "1");
                    }
                    else if (!string.IsNullOrEmpty(Cmd))
                    {
                        ethernet.sendcommand(Cmd);
                    }
                    ethernet.closeport();
                    //ethernet.closeport_mult(9100, 0);
                    break;
            }
        }

        public void SendCmdClearBuffer()
        {
            switch (Properties.Channel)
            {
                case PrintChannel.Name:
                    TSCSDK.driver driver = new();
                    driver.openport(Properties.PrintName);
                    driver.clearbuffer();
                    driver.closeport();
                    break;
                case PrintChannel.Ethernet:
                    TSCSDK.ethernet ethernet = new();
                    ethernet.openport(Properties.PrintIp, Properties.PrintPort);
                    ethernet.clearbuffer();
                    ethernet.closeport();
                    break;
            }
        }

        public void SendCmdGap(double gapSize = 3.5, double gapOffset = 0.0)
        {
            string strGapSize = $"{gapSize}".Replace(',', '.');
            string strGapOffset = $"{gapOffset}".Replace(',', '.');
            SendCmd($"GAP {strGapSize} mm, {strGapOffset} mm");
        }

        public void SendCmdCutter(int value)
        {
            if (value >= 0)
                SendCmd($"SET CUTTER {value}");
        }

        public void SendCmdTest()
        {
            SendCmd("", true);
        }

        //public void ClearBuffer()
        //{
        //    //if (!Open()) return;
        //    switch (Properties.Channel)
        //    {
        //        case PrintChannel.Name:
        //            Driver.clearbuffer();
        //            break;
        //        case PrintChannel.Ethernet:
        //            Driver.closeport();
        //            break;
        //    }
        //}

        public void CmdConvertZpl(bool isUsePicReplace)
        {
            Cmd = ZplUtils.ConvertStringToHex(TextPrepare);
            if (isUsePicReplace)
            {
                Cmd = ZplUtils.PrintCmdReplaceZplResources(Cmd);
                //Cmd = Cmd.Replace("[EAC_107x109_090]", ZplSamples.GetEac);
                //Cmd = Cmd.Replace("[FISH_94x115_000]", ZplSamples.GetFish);
                //Cmd = Cmd.Replace("[TEMP6_116x113_090]", ZplSamples.GetTemp6);
            }
        }

        #endregion
    }
}
