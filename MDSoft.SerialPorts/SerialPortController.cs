// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Text;

namespace MDSoft.SerialPorts
{
    public class SerialPortController
    {
        #region Public and private fields and properties

        public SerialPortEntity Port { get; private set; }

        #endregion

        #region Constructor and destructor

        public SerialPortController(PortCallback openCallback, PortCallback closeCallback, PortCallback responseCallback, PortExceptionCallback exceptionCallback)
        {
            Port = new(openCallback, closeCallback, responseCallback, exceptionCallback);
        }

        #endregion

        #region Public and private methods

        public bool SendData(byte[] data)
        {
            return Port.Send(data);
        }

        public bool SendData(string str)
        {
            if (str != null && str != "")
            {
                return Port.Send(Encoding.Default.GetBytes(str));
            }
            return true;
        }

        public void OpenPort(string portName, string baudRate, string dataBits, string stopBits, string parity, string handshake)
        {
            if (portName != null && portName != "")
            {
                Port.Open(portName, baudRate, dataBits, stopBits, parity, handshake);
            }
        }

        public void OpenPort(string portName, int readTimeout, int writeTimeout)
        {
            if (!string.IsNullOrEmpty(portName))
            {
                Port.Open(SerialPortUtils.GetSerialPort(portName, readTimeout, writeTimeout));
            }
        }

        public void ClosePort()
        {
            Port.Close();
        }

        #endregion
    }
}
