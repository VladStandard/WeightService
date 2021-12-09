// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace WeightCore.SerialPorts
{
    public class SerialPortEventArgs : EventArgs
    {
        public bool isOpend = false;
        public byte[] receivedBytes = null;
    }
}
