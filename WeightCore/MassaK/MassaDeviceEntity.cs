// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Models;
using System;
using System.IO.Ports;
using System.Threading;

namespace WeightCore.MassaK
{
    public partial class MassaDeviceEntity : DisposableBase, IDisposableBase
    {
        #region Public and private fields and properties

        public bool IsClosedMethod { get; set; }
        public bool IsConnected { get; private set; }
        public bool IsOpenedMethod { get; set; }
        public int ReadTimeout { get; private set; }
        public int WriteTimeout { get; private set; }
        public SerialPort SerialPortItem { get; set; }
        public string PortName { get; private set; }
        public delegate void SerialPortEventHandler(object sender, SerialPortEventArgs e);
        public event SerialPortEventHandler comReceiveDataEvent = null;
        public event SerialPortEventHandler comOpenEvent = null;
        public event SerialPortEventHandler comCloseEvent = null;
        private object thisLock = new();

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
            //base.Open();
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
                    SerialPortItem.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
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

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (SerialPortItem.BytesToRead <= 0)
            {
                return;
            }
            //Thread Safety explain in MSDN:
            // Any public static (Shared in Visual Basic) members of this type are thread safe. 
            // Any instance members are not guaranteed to be thread safe.
            // So, we need to synchronize I/O
            lock (thisLock)
            {
                int len = SerialPortItem.BytesToRead;
                byte[] data = new byte[len];
                try
                {
                    SerialPortItem.Read(data, 0, len);
                }
                catch (System.Exception)
                {
                    //catch read exception
                }
                SerialPortEventArgs args = new();
                args.receivedBytes = data;
                if (comReceiveDataEvent != null)
                {
                    comReceiveDataEvent.Invoke(this, args);
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
