// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO.Ports;

namespace WeightCore.MassaK
{
    public static class SerialPortExtension
    {
        public static SerialPort GetDefaultSerialPort(this SerialPort, string portName) => new(portName)
        {
            BaudRate = 9600,
            //BaudRate = 4800,
            Parity = Parity.Even,
            //Parity = Parity.None,  // 1
            //Parity = Parity.Even,  // 2
            DataBits = 8,
            StopBits = StopBits.One,
            Handshake = Handshake.None,
            ReadTimeout = 1_000,
            //ReadTimeout = 3000,
            WriteTimeout = 1_000,
            //WriteTimeout = 3000,
            // FlowControl = Hardware
        };
    }
}
