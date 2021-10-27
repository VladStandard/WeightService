// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO.Ports;

namespace WeightCore.MassaK
{
    public class NotIsConnectedException : Exception
    {
        public NotIsConnectedException() : base("Not is connected to scales")
        {
        }
    }
    
    public class ConnectionException : Exception
    {
        public ConnectionException() : base("Failed to connect to scales")
        {
        }
        public ConnectionException(Exception e) : base("Failed to connect to scales", e)
        {
        }
    }

    public class MassaKScales : IDisposable
    {
        #region Public and private fields and properties

        private SerialPort _serialPort;
        private readonly object _lockObject = new();
        public bool IsConnected { get; private set; }

        #endregion

        #region Public and private methods

        public void Disconnect()
        {
            lock (_lockObject)
            {
                _serialPort?.Dispose();
                _serialPort = null;
                IsConnected = false;
            }
        }

        public void Dispose()
        {
            Disconnect();
        }

        public void Connect(string portName)
        {
            if (string.IsNullOrEmpty(portName))
                throw new ArgumentNullException(portName);

            lock (_lockObject)
            {
                Disconnect();

                try
                {
                    _serialPort = _serialPort.GetDefault(portName);
                    _serialPort.Open();

                    _serialPort.Write(new byte[] { 0x44 }, 0, 1);
                    byte[] response = ReadResponse();
                    if (response == null)
                        throw new ConnectionException();

                    IsConnected = true;
                }
                catch (Exception e)
                {
                    throw new ConnectionException(e);
                }
            }
        }

        /// <summary>
        /// Weight in gramms.
        /// </summary>
        /// <returns></returns>
        public double GetWeight()
        {
            lock (_lockObject)
            {
                if (!IsConnected)
                {
                    throw new ConnectionException();
                }

                _serialPort.Write(new byte[] { 0x45 }, 0, 1);

                byte[] response = ReadResponse();

                int w = response[1] * 256 + response[0];

                return w;
            }
        }

        private byte[] ReadResponse()
        {
            int length = 2;
            byte[] response = new byte[length];
            int offset = 0;
            while (offset < length)
            {
                int b = _serialPort.ReadByte();
                response[offset] = (byte)b;
                offset++;
            }
            return response;
        }

        #endregion
    }
}
