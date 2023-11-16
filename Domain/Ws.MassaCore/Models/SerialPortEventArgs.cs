namespace Ws.MassaCore.Models;

public class SerialPortEventArgs : EventArgs
{
	public SerialPort SerialPort { get; set; } = new();
	public byte[] ReceivedBytes { get; set; } = new byte[0];
}