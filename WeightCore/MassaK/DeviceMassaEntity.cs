// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO.Ports;

namespace WeightCore.MassaK
{
    public class DeviceMassaEntity : IDisposable
    {
        public class NotIsConnectedException : Exception
        {
            public NotIsConnectedException() : base("Not is connected to scales") { }
        }

        public class ConnectionException : Exception
        {
            public ConnectionException() : base("Failed to connect to scales") { }
            public ConnectionException(Exception e) : base("Failed to connect to scales", e) { }
        }

        #region Public and private fields and properties

        private SerialPort SerialPortItem { get; set; }
        private readonly object _lockObject = new();
        public bool IsConnected { get; private set; }
        public bool IsEnableReconnect { get; private set; }
        public string PortName { get; private set; }

        #endregion

        #region Constructor and destructor

        public DeviceMassaEntity(string portName, bool isEnableReconnect, int readTimeout, int writeTimeout)
        {
            PortName = portName;
            IsEnableReconnect = isEnableReconnect;
            SerialPortItem = SerialPortItem.GetDefault(portName, readTimeout, writeTimeout);
            Open();
        }

        #endregion

        #region Public and private methods

        public void Close()
        {
            lock (_lockObject)
            {
                if (SerialPortItem != null)
                {
                    if (SerialPortItem.IsOpen)
                        SerialPortItem.Close();
                    SerialPortItem.Dispose();
                    SerialPortItem = null;
                }
                IsConnected = false;
            }
        }

        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Create connection.
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="serialPort"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ConnectionException"></exception>
        public void Open()
        {
            lock (_lockObject)
            {
                try
                {
                    if (IsConnected) return;
                    if (string.IsNullOrEmpty(PortName))
                        throw new ArgumentNullException(PortName);
                    
                    if (SerialPortItem == null)
                        SerialPortItem = SerialPortItem.GetDefault(PortName);
                    if (!SerialPortItem.IsOpen)
                        SerialPortItem.Open();

                    SerialPortItem.Write(new byte[] { 0x44 }, 0, 1);
                    byte[] response = ReadResponse();
                    if (response == null)
                        throw new ConnectionException();

                    IsConnected = true;
                }
                catch (Exception ex)
                {
                    throw new ConnectionException(ex);
                }
            }
        }

        /// <summary>
        /// Weight in gramms.
        /// </summary>
        /// <returns></returns>
        public int GetWeight()
        {
            if (IsEnableReconnect)
                Open();
            lock (_lockObject)
            {
                SerialPortItem.Write(new byte[] { 0x45 }, 0, 1);
                byte[] response = ReadResponse();
                return response[1] * 256 + response[0];
            }
        }

        private byte[] ReadResponse()
        {
            int length = 2;
            byte[] response = new byte[length];
            int offset = 0;
            while (offset < length)
            {
                int b = SerialPortItem.ReadByte();
                response[offset] = (byte)b;
                offset++;
            }
            return response;
        }

        private byte[] ReadResponse2()
        {
            int bytes = SerialPortItem.BytesToRead;
            byte[] response = new byte[bytes];
            if (bytes > 0)
            {
                SerialPortItem.Read(response, 0, bytes);
            }
            return response;
        }

        public byte[] GetResponse(byte[] request)
        {
            if (IsEnableReconnect)
                Open();
            lock (_lockObject)
            {
                SerialPortItem.Write(request, 0, request.Length);
                return ReadResponse2();
            }
        }

        #endregion
    }
}
