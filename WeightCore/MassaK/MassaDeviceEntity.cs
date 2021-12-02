// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Models;
using System;
using System.IO.Ports;
using System.Threading;

namespace WeightCore.MassaK
{
    public class MassaDeviceEntity : DisposableBase, IDisposableBase
    {
        #region Public and private fields and properties

        public bool IsConnected { get; private set; }
        public string PortName { get; private set; }
        public int ReadTimeout { get; private set; }
        public int WriteTimeout { get; private set; }
        public SerialPort SerialPortItem { get; set; }
        public bool IsOpenedMethod { get; set; }
        public bool IsClosedMethod { get; set; }

        #endregion

        #region Constructor and destructor

        public MassaDeviceEntity(string portName, int readTimeout, int writeTimeout)
        {
            Init(CloseMethod, ReleaseManaged, ReleaseUnmanaged);

            PortName = portName;
            ReadTimeout = readTimeout;
            WriteTimeout = writeTimeout;
        }

        #endregion

        #region Public and private methods

        public new void Open()
        {
            lock (this)
            {
                if (IsOpenedMethod) return;
                IsOpenedMethod = true;
                IsClosedMethod = false;
                CheckIsDisposed();
                
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
            lock (this)
            {
                CheckIsDisposed();
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
        }

        public byte[] WriteToPort(MassaExchangeEntity cmd)
        {
            lock (this)
            {
                CheckIsDisposed();
                if (SerialPortItem == null)
                    return null;
                SerialPortItem.Write(cmd.Request, 0, cmd.Request.Length);
                Thread.Sleep(50);
                byte[] result = ReadFromPort();
                Thread.Sleep(50);
                return result;
            }
        }

        public void CloseMethod()
        {
            lock (this)
            {
                if (IsClosedMethod) return;
                IsOpenedMethod = false;
                IsClosedMethod = true;
                CheckIsDisposed();
                
                IsConnected = false;
                if (SerialPortItem != null)
                {
                    if (SerialPortItem.IsOpen)
                        SerialPortItem.Close();
                }
            }
        }

        public void ReleaseManaged()
        {
            CloseMethod();

            SerialPortItem?.Dispose();
            SerialPortItem = null;
        }

        public void ReleaseUnmanaged()
        {
            //
        }

        #endregion
    }
}
