// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO.Ports;

namespace MDSoft.SerialPorts;

public class SerialPortEventArgs : EventArgs
{
	public SerialPort SerialPort { get; set; } = new();
	public byte[] ReceivedBytes { get; set; } = new byte[0];
}