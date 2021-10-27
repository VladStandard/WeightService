// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO.Ports;

namespace WeightCore.MassaK
{
    public static class SerialPortExtension
    {
        public static SerialPort GetDefaultSerialPort(SerialPort serialPort, string portName) => 
            new SerialPort(portName)
        {

        };
    }
}
