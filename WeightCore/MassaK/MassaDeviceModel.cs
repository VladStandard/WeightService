// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using MDSoft.SerialPorts;
using System;

namespace WeightCore.MassaK
{
    public partial class MassaDeviceModel : DisposableBase, IDisposableBase, ISerialPortView
    {
        #region Public and private fields and properties

        public bool IsClosedMethod { get; set; }
        public bool IsConnected => SerialPortController.SerialPortModel?.SerialPort?.IsOpen == true;
        public bool IsOpenedMethod { get; set; }
        public int ReadTimeout { get; private set; }
        public int WriteTimeout { get; private set; }
        public string PortName { get; private set; }

        public BytesHelper Bytes { get; private set; } = BytesHelper.Instance;
        private ISerialPortController SerialPortController { get; set; }
        public int SendBytesCount { get; private set; } = 0;
        public int ReceiveBytesCount { get; private set; } = 0;
        public delegate void ResponseCallback(MassaExchangeEntity massaExchange, byte[] response);
        private readonly ResponseCallback _responseCallback = null;
        private MassaExchangeEntity _massaExchange = null;
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public MassaDeviceModel(string portName, short? readTimeout, short? writeTimeout, ResponseCallback responseCallback)
        {
            Init(Close, ReleaseManaged, ReleaseUnmanaged);

            PortName = portName;
            ReadTimeout = readTimeout ?? 100;
            WriteTimeout = writeTimeout ?? 100;
            SerialPortController = new ISerialPortController(this);
            _responseCallback = responseCallback;
        }

        #endregion

        #region Public and private methods - ISerialPortView

        public void SetController(ISerialPortController controller)
        {
            SerialPortController = controller;
        }

        public void OpenComEvent(object sender, SerialPortEventArgs e)
        {
            if (e.IsOpened)
            {
                //
            }
            else
            {
                //
            }
        }

        public void CloseComEvent(object sender, SerialPortEventArgs e)
        {
            // Close successfully.
            if (!e.IsOpened)
            {
                //
            }
        }

        public void ReceiveDataEvent(object sender, SerialPortEventArgs e)
        {
            lock (_locker)
            {
                CheckIsDisposed();
                ReceiveBytesCount += e.ReceivedBytes.Length;
                _responseCallback?.Invoke(_massaExchange, e.ReceivedBytes);
                _massaExchange = null;
            }
        }

        #endregion

        #region Public and private methods

        public new void Open()
        {
            base.Open();
            if (IsOpenedMethod) return;
            IsOpenedMethod = true;
            IsClosedMethod = false;

            try
            {
                if (string.IsNullOrEmpty(PortName))
                {
                    throw new ArgumentNullException(PortName);
                }
                SerialPortController.OpenPort(PortName, ReadTimeout, WriteTimeout);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendData(MassaExchangeEntity massaExchange)
        {
            CheckIsDisposed();
            _massaExchange = massaExchange;
            SerialPortController.SendData(massaExchange.Request);
            SendBytesCount += massaExchange.Request.Length;
        }

        public new void Close()
        {
            if (IsClosedMethod) return;
            IsOpenedMethod = false;
            IsClosedMethod = true;
            CheckIsDisposed();

            SerialPortController.ClosePort();
            base.Close();
        }

        public void ReleaseManaged()
        {
            Close();

            //SerialPortController.SerialPortModel.SerialPort.Dispose();
            //SerialPortController.SerialPortModel.SerialPort = null;
        }

        public void ReleaseUnmanaged()
        {
            //
        }

        #endregion
    }
}
