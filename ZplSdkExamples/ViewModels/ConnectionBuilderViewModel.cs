using System.Collections.ObjectModel;
using ZplSdkExamples.Models;
using ZplSdkExamples.Utils;

namespace ZplSdkExamples.ViewModels
{
    public class ConnectionBuilderViewModel : IDemoViewModel
    {

        private string logData;

        private readonly ObservableCollection<string> _connectionPrefixes = new ObservableCollection<string>() {
                ConnectionPrefix.None,
                ConnectionPrefix.TcpMulti,
                ConnectionPrefix.Tcp,
                ConnectionPrefix.TcpStatus,
                ConnectionPrefix.Usb,
                ConnectionPrefix.UsbDirect
            };

        public string LogData
        {
            get => logData;
            set
            {
                if (logData != value)
                {
                    logData = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> ConnectionPrefixes
        {
            get => _connectionPrefixes;
        }

        public ConnectionBuilderViewModel()
        {
            Name = "Connection Builder";
            logData = "Log:\n\n";
            if (BluetoothHelper.IsBluetoothSupported())
            {
                _connectionPrefixes.Add(ConnectionPrefix.Bluetooth);
                _connectionPrefixes.Add(ConnectionPrefix.BluetoothMulti);
            }
        }
    }
}
