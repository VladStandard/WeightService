// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO.Ports;

namespace WeightCore.MassaK
{
    public static class SerialPortUtils
    {
        public static SerialPort GetDefault(string portName, int readTimeout = 1_000, int writeTimeout = 1_000) => new(portName)
        {
            BaudRate = 4800,
            Parity = Parity.Even,
            DataBits = 8,
            StopBits = StopBits.One,
            Handshake = Handshake.RequestToSend,
            ReadTimeout = readTimeout,
            WriteTimeout = writeTimeout,
            // FlowControl = Hardware
        };

        public static SerialPort GetNew(string portName, int baudRate, Parity parity, int dataBits,
            StopBits stopBits, Handshake handshake, int readTimeout, int writeTimeout) => new(portName)
        {
            BaudRate = baudRate,
            Parity = parity,
            DataBits = dataBits,
            StopBits = stopBits,
            Handshake = handshake,
            ReadTimeout = readTimeout,
            WriteTimeout = writeTimeout,
        };
    }
}
