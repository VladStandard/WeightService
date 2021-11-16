// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO.Ports;
using System.Threading;

namespace WeightCore.MassaK
{
    public class MassaDeviceEntity : IDisposable
    {
        #region Classes

        public class MassaConnectionException : Exception
        {
            public MassaConnectionException() : base("Failed connect to the scales") { }
            public MassaConnectionException(Exception e) : base("Failed connect to the scales", e) { }
        }

        #endregion

        #region Public and private fields and properties

        public object Locker { get; private set; } = new();
        public bool IsConnected { get; private set; }
        public string PortName { get; private set; }
        public int ReadTimeout { get; private set; }
        public int WriteTimeout { get; private set; }
        public SerialPort SerialPortItem { get; set; }

        #endregion

        #region Constructor and destructor

        public MassaDeviceEntity(string portName, int readTimeout, int writeTimeout)
        {
            PortName = portName;
            ReadTimeout = readTimeout;
            WriteTimeout = writeTimeout;
        }

        #endregion

        #region Public and private methods

        public void Close()
        {
            lock (Locker)
            {
                IsConnected = false;
                if (SerialPortItem == null)
                    return;
                if (SerialPortItem.IsOpen)
                    SerialPortItem.Close();
                SerialPortItem.Dispose();
            }
        }

        public void Dispose()
        {
            Close();
        }

        public void Open()
        {
            lock (Locker)
            {
                try
                {
                    if (string.IsNullOrEmpty(PortName))
                    {
                        IsConnected = false;
                        throw new ArgumentNullException(PortName);
                    }

                    if (SerialPortItem == null && !string.IsNullOrEmpty(PortName))
                        SerialPortItem = SerialPortItem.GetDefault(PortName, ReadTimeout, WriteTimeout);

                    if (SerialPortItem == null)
                    {
                        IsConnected = false;
                        return;
                    }
                    if (!SerialPortItem.IsOpen)
                    {
                        SerialPortItem.Open();
                        IsConnected = true;
                        return;
                    }
                    else
                        IsConnected = true;
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    throw new MassaConnectionException(ex);
                }
            }
        }

        private byte[] ReadFromPort()
        {
            if (SerialPortItem == null)
                return null;
            int length = SerialPortItem.BytesToRead;
            byte[] response = new byte[length];
            if (length > 0)
            {
                SerialPortItem.Read(response, 0, length);
            }
            return response;
        }

        public byte[] WriteToPort(MassaExchangeEntity cmd)
        {
            lock (Locker)
            {
                if (SerialPortItem == null)
                    return null;
                SerialPortItem.Write(cmd.Request, 0, cmd.Request.Length);
                Thread.Sleep(50);
                byte[] result = ReadFromPort();
                Thread.Sleep(50);
                return result;
            }
        }

        #endregion
    }
}
