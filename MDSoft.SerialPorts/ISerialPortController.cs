// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Text;

namespace MDSoft.SerialPorts
{
    public class ISerialPortController
    {
        #region Public and private fields and properties

        public SerialPortModel SerialPortModel { get; private set; } = new();

        #endregion

        #region Constructor and destructor

        public ISerialPortController(ISerialPortView view)
        {
            view.SetController(this);
            SerialPortModel.ComCloseEvent += new SerialPortEventHandler(view.CloseComEvent);
            SerialPortModel.ComOpenEvent += new SerialPortEventHandler(view.OpenComEvent);
            SerialPortModel.ComReceiveDataEvent += new SerialPortEventHandler(view.ReceiveDataEvent);
        }

        #endregion

        #region Public and private methods

        public bool SendData(byte[] data)
        {
            return SerialPortModel.Send(data);
        }

        public bool SendData(string str)
        {
            if (str != null && str != "")
            {
                return SerialPortModel.Send(Encoding.Default.GetBytes(str));
            }
            return true;
        }

        public void OpenPort(string portName, string baudRate, string dataBits, string stopBits, string parity, string handshake)
        {
            if (portName != null && portName != "")
            {
                SerialPortModel.Open(portName, baudRate, dataBits, stopBits, parity, handshake);
            }
        }

        public void OpenPort(string portName, int readTimeout, int writeTimeout)
        {
            if (!string.IsNullOrEmpty(portName))
            {
                SerialPortModel.Open(SerialPortUtils.GetSerialPort(portName, readTimeout, writeTimeout));
            }
        }

        public void ClosePort()
        {
            SerialPortModel.Close();
        }

        #endregion
    }
}
