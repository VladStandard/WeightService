// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO.Ports;
using System.Threading;

namespace WeightCore.MassaK
{
    public class DeviceMassaEntity : IDisposable
    {
        #region Classes
        
        public class NotIsConnectedException : Exception
        {
            public NotIsConnectedException() : base("Not is connected to scales") { }
        }

        public class ConnectionException : Exception
        {
            public ConnectionException() : base("Failed to connect to scales") { }
            public ConnectionException(Exception e) : base("Failed to connect to scales", e) { }
        }

        #endregion

        #region Public and private fields and properties

        public SerialPort SerialPortItem { get; private set; }
        public object LockObject { get; private set; } = new();
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
            lock (LockObject)
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
            lock (LockObject)
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

                    //SerialPortItem.Write(new byte[] { 0x44 }, 0, 1);
                    //byte[] response = ReadResponse();
                    //if (response == null)
                    //    throw new ConnectionException();

                    IsConnected = true;
                }
                catch (Exception ex)
                {
                    throw new ConnectionException(ex);
                }
            }
        }

        //public int GetWeight()
        //{
        //    if (IsEnableReconnect)
        //        Open();
        //    lock (LockObject)
        //    {
        //        SerialPortItem.Write(new byte[] { 0x45 }, 0, 1);
        //        byte[] response = ReadResponse();
        //        return response[1] * 256 + response[0];
        //    }
        //}

        //private byte[] ReadResponse()
        //{
        //    int length = 2;
        //    byte[] response = new byte[length];
        //    int offset = 0;
        //    while (offset < length)
        //    {
        //        int b = SerialPortItem.ReadByte();
        //        response[offset] = (byte)b;
        //        offset++;
        //    }
        //    return response;
        //}

        private byte[] ReadFromPort()
        {
            int length = SerialPortItem.BytesToRead;
            //int length = GetBufferLength(cmd);
            byte[] response = new byte[length];
            if (length > 0)
            {
                SerialPortItem.Read(response, 0, length);
            }

            //int i = 0;
            //int pos = 0;
            //foreach (byte item in response)
            //{
            //    if (item == 0xF8)
            //        pos = i;
            //    i++;
            //}
            //if (pos > 0)
            //{
            //    byte[] buffer = new byte[response.Length - pos];
            //    int j = 0;
            //    for (i = pos; i < response.Length; i++)
            //    {
            //        buffer[j] = response[i];
            //        j++;
            //    }
            //    return buffer;
            //}
            return response;
        }

        public byte[] WriteToPort(CmdEntity cmd)
        {
            if (IsEnableReconnect)
                Open();
            lock (LockObject)
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
