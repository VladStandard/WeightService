// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MDSoft.SerialPorts
{
    public class SerialPortEnums
    {
        public enum BaudRate
        {
            Value_100 = 100,
            Value_300 = 300,
            Value_600 = 600,
            Value_1200 = 1_200,
            Value_2400 = 2_400,
            Value_4800 = 4_800,
            Value_9600 = 9_600,
            Value_19200 = 19_200,
            Value_38400 = 38_400,
            Value_57600 = 57_600,
            Value_115200 = 115_200,
            Value_128000 = 128_000,
            Value_256000 = 256_000,
        }

        public enum DataBits
        {
            Value_5 = 5,
            Value_6 = 6,
            Value_7 = 7,
            Value_8 = 8,
        }
    }
}
