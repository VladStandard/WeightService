// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace MDSoft.SerialPorts
{
    public class SerialPortEventArgs : EventArgs
    {
        public bool IsOpened { get; set; } = false;
        public byte[] ReceivedBytes { get; set; } = null;
    }
}
