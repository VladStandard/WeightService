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

        private SerialPort serialPort;
        private readonly object lockObject = new();
        public bool IsConnected { get; private set; }

        #endregion

        #region Public and private methods

        public void Disconnect()
        {
            lock (lockObject)
            {
                serialPort?.Dispose();
                serialPort = null;
                IsConnected = false;
            }
        }

        public void Connect(string comPort)
        {
            lock (lockObject)
            {
                Disconnect();

                try
                {
                    serialPort = new SerialPort(comPort)
                    {
                        BaudRate = 4800,
                        Parity = Parity.Even,
                        DataBits = 8,
                        StopBits = StopBits.One,
                        ReadTimeout = 3000
                    };
                    serialPort.Open();

                    serialPort.Write(new byte[] { 0x44 }, 0, 1);
                    var response = ReadResponse();
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
            lock (lockObject)
            {
                if (!IsConnected)
                {
                    throw new ConnectionException();
                }

                serialPort.Write(new byte[] { 0x45 }, 0, 1);

                var response = ReadResponse();

                var w = response[1] * 256 + response[0];

                return w;
            }
        }

        private byte[] ReadResponse()
        {
            var length = 2;
            var response = new byte[length];
            var offset = 0;
            while (offset < length)
            {
                var b = serialPort.ReadByte();
                response[offset] = (byte)b;
                offset++;
            }
            return response;
        }

        public void Dispose()
        {
            Disconnect();
        }

        #endregion
    }
}
