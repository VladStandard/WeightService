// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MDSoft.SerialPorts
{
    public interface ISerialPortView
    {
        public void SetController(ISerialPortController controller);
        public void OpenComEvent(object sender, SerialPortEventArgs e);
        public void CloseComEvent(object sender, SerialPortEventArgs e);
        public void ReceiveDataEvent(object sender, SerialPortEventArgs e);
    }
}
