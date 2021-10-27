// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO.Ports;

namespace WeightCore.MassaK
{
    public static class SerialPortExtension
    {
        public static SerialPort GetDefault(this SerialPort serialPort, string portName, int readTimeout = 1_000, int writeTimeout = 1_000) => new(portName)
        {
            BaudRate = 9600,
            //BaudRate = 4800,  // 2
            Parity = Parity.Even,
            //Parity = Parity.None,  // 1
            //Parity = Parity.Even,  // 2
            DataBits = 8,
            StopBits = StopBits.One,
            Handshake = Handshake.None,
            ReadTimeout = readTimeout,
            //ReadTimeout = 3000,
            WriteTimeout = writeTimeout,
            //WriteTimeout = 3000,
            // FlowControl = Hardware
        };
    }
}
