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
            public MassaConnectionException() : base("Failed to connect to scales") { }
            public MassaConnectionException(Exception e) : base("Failed to connect to scales", e) { }
        }

        #endregion

        #region Public and private fields and properties

        public SerialPort SerialPortItem { get; private set; }
        public object Locker { get; private set; } = new();
        public bool IsConnected { get; private set; }
        public string PortName { get; private set; }

        #endregion

        #region Constructor and destructor

        public MassaDeviceEntity(string portName, int readTimeout, int writeTimeout)
        {
            PortName = portName;
            SerialPortItem = SerialPortItem.GetDefault(portName, readTimeout, writeTimeout);
        }

        #endregion

        #region Public and private methods

        public void Close()
        {
            lock (Locker)
            {
                IsConnected = false;
                if (SerialPortItem != null)
                {
                    if (SerialPortItem.IsOpen)
                        SerialPortItem.Close();
                    SerialPortItem.Dispose();
                    SerialPortItem = null;
                }
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
                    //if (IsConnected) return;
                    if (string.IsNullOrEmpty(PortName))
                    {
                        IsConnected = false;
                        throw new ArgumentNullException(PortName);
                    }
                    
                    if (SerialPortItem == null)
                        SerialPortItem = SerialPortItem.GetDefault(PortName);
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
            //if (IsEnableReconnect)
            //    Open();
            lock (Locker)
            {
                SerialPortItem.Write(cmd.Request, 0, cmd.Request.Length);
                Thread.Sleep(10);
                byte[] result = ReadFromPort();
                Thread.Sleep(10);
                return result;
            }
        }

        #endregion
    }
}
