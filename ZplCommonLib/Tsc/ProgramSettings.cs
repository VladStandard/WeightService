using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZplCommonLib.Tsc;

namespace TscBarcode.ViewModels
{
    public class ProgramSettings : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Constructor

        public ProgramSettings()
        {
            IpAddress = "192.168.6.132";
            Port = 9100;
            PrinterSetup = new PrinterSetupEntity();
        }

        #endregion

        #region Public properties

        private string _ipAddress;
        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                _ipAddress = value;
                OnPropertyRaised();
            }
        }

        private int _port;
        public int Port
        {
            get => _port;
            set
            {
                _port = value;
                OnPropertyRaised();
            }
        }

        private string _log;
        public string Log
        {
            get => _log;
            set
            {
                _log = value;
                OnPropertyRaised();
            }
        }

        private TSCSDK.ethernet _tscEthernet;
        public TSCSDK.ethernet TscEthernet
        {
            get => _tscEthernet;
            set
            {
                _tscEthernet = value;
                OnPropertyRaised();
            }
        }

        private PrinterSetupEntity _printerSetup;
        public PrinterSetupEntity PrinterSetup
        {
            get => _printerSetup;
            set
            {
                _printerSetup = value;
                OnPropertyRaised();
            }
        }
        #endregion

        #region Public and private methods

        public void EthernetOpen()
        {
            if (TscEthernet != null)
                EthernetClose();

            TscEthernet = new TSCSDK.ethernet();
            TscEthernet.openport(IpAddress, Port);
            Log = $"IP port open: {TscEthernet.printerstatus()}" + Environment.NewLine;
        }

        public void EthernetClose()
        {
            if (TscEthernet != null)
            {
                TscEthernet.closeport();
                Log += "IP port closed" + Environment.NewLine;
            }
        }

        public void EthernetPrintTest()
        {
            if (TscEthernet == null)
            {
                EthernetOpen();
            }

            TscEthernet.clearbuffer();
            EthernetSetup();

            TscEthernet.barcode("100", "200", "128", "100", "1", "0", "3", "3", "123456789");
            TscEthernet.printerfont("100", "100", "3", "0", "1", "1", "Printer Font Test");
            TscEthernet.sendcommand("BOX 50,50,500,400,3\n");
            TscEthernet.printlabel("1", "1");
        }

        public void EthernetPrint(string cmd)
        {
            if (TscEthernet == null)
            {
                EthernetOpen();
            }

            TscEthernet.clearbuffer();
            EthernetSetup();

            TscEthernet.sendcommand(cmd);
            TscEthernet.printlabel("1", "1");
        }

        public void EthernetSetup()
        {
            if (TscEthernet == null)
            {
                EthernetOpen();
            }
            
            if (PrinterSetup == null)
            {
                EthernetSetupReset();
            }
            else
            {
                TscEthernet.setup(
                    PrinterSetup.Width, 
                    PrinterSetup.Height, 
                    PrinterSetup.Speed,
                    PrinterSetup.Density, 
                    PrinterSetup.Sensor, 
                    PrinterSetup.Vertical, 
                    PrinterSetup.Offset);
                Log += "PrinterSetup. " +
                       $"{nameof(PrinterSetup.Width)}: {PrinterSetup.Width}. " +
                       $"{nameof(PrinterSetup.Height)}: {PrinterSetup.Height}. " +
                       $"{nameof(PrinterSetup.Speed)}: {PrinterSetup.Speed}. " +
                       $"{nameof(PrinterSetup.Density)}: {PrinterSetup.Density}. " +
                       $"{nameof(PrinterSetup.Sensor)}: {PrinterSetup.Sensor}. " +
                       $"{nameof(PrinterSetup.Vertical)}: {PrinterSetup.Vertical}. " +
                       $"{nameof(PrinterSetup.Offset)}: {PrinterSetup.Offset}. " + 
                       Environment.NewLine;
            }
        }

        public void EthernetSetupReset()
        {
            if (TscEthernet == null)
            {
                EthernetOpen();
            }
            PrinterSetup = new PrinterSetupEntity();
            TscEthernet.setup(PrinterSetup.Width, PrinterSetup.Height, PrinterSetup.Speed,
                PrinterSetup.Density, PrinterSetup.Sensor, PrinterSetup.Vertical, PrinterSetup.Offset);
            Log += $"PrinterSetup. {nameof(PrinterSetup.Width)}: {PrinterSetup.Width}. {nameof(PrinterSetup.Height)}: {PrinterSetup.Height}. " +
                   $"{nameof(PrinterSetup.Speed)}: {PrinterSetup.Speed}. {nameof(PrinterSetup.Density)}: {PrinterSetup.Density}. " +
                   $"{nameof(PrinterSetup.Sensor)}: {PrinterSetup.Sensor}. {nameof(PrinterSetup.Vertical)}: {PrinterSetup.Vertical}. " +
                   $"{nameof(PrinterSetup.Offset)}: {PrinterSetup.Offset}. " + Environment.NewLine;
        }

        #endregion
    }
}