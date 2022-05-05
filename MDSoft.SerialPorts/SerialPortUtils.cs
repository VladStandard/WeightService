// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace MDSoft.SerialPorts
{
    public static class SerialPortUtils
    {
        #region Public and private fields and properties

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

        #endregion

        #region Public and private methods

        public static SerialPort GetSerialPort(string portName) => new(portName)
        {
            BaudRate = GetDefaultBaudRate(),
            Parity = GetDefaultParity(),
            DataBits = GetDefaultDataBits(),
            StopBits = GetDefaultStopBits(),
            Handshake = GetDefaultHandshake(),
            ReadTimeout = GetDefaultReadTimeout(),
            WriteTimeout = GetDefaultWriteTimeout(),
        };

        public static SerialPort GetSerialPort(string portName, int readTimeout, int writeTimeout) => new(portName)
        {
            BaudRate = GetDefaultBaudRate(),
            Parity = GetDefaultParity(),
            DataBits = GetDefaultDataBits(),
            StopBits = GetDefaultStopBits(),
            Handshake = GetDefaultHandshake(),
            ReadTimeout = readTimeout,
            WriteTimeout = writeTimeout,
        };

        public static SerialPort GetSerialPort(string portName, int baudRate, Parity parity, int dataBits,
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

        public static List<string> GetBaudRates()
        {
            List<string> result = new();
            foreach (object value in Enum.GetValues(typeof(BaudRate)))
            {
                result.Add(((int)value).ToString());
            }
            return result;
        }

        public static int GetDefaultBaudRate() => 4_800;

        public static List<string> GetDataBits()
        {
            List<string> result = new();
            foreach (object value in Enum.GetValues(typeof(DataBits)))
            {
                result.Add(((int)value).ToString());
            }
            return result;
        }
        
        public static int GetDefaultDataBits() => 8;

        public static List<string> GetStopBits()
        {
            List<string> result = new();
            foreach (object value in Enum.GetValues(typeof(StopBits)))
            {
                result.Add(value.ToString());
            }
            return result;
        }
        
        public static StopBits GetDefaultStopBits() => StopBits.One;

        public static List<string> GetParity()
        {
            List<string> result = new();
            foreach (object value in Enum.GetValues(typeof(Parity)))
            {
                result.Add(value.ToString());
            }
            return result;
        }
        
        public static Parity GetDefaultParity() => Parity.Even;

        public static string[] GetPorts()
        {
            string[] result = SerialPort.GetPortNames();
            Array.Sort(result);
            return result;
        }

        public static List<string> GetHandshaking()
        {
            List<string> result = new();
            foreach (object value in Enum.GetValues(typeof(Handshake)))
            {
                result.Add(value.ToString());
            }
            return result;
        }
        
        public static Handshake GetDefaultHandshake() => Handshake.RequestToSend;

        public static int GetDefaultReadTimeout() => 1_000;
        
        public static int GetDefaultWriteTimeout() => 1_000;

        #endregion
    }
}
