// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql;
using MDSoft.SerialPorts;
using System;
using System.Runtime.CompilerServices;

namespace WeightCore.MassaK
{
    public partial class MassaDeviceEntity : DisposableBase, IDisposableBase
    {
        #region Public and private fields and properties

        public bool IsOpenPort => PortController.SerialPort.IsOpen == true;
        public bool IsOpenResult { get; set; }
        public bool IsCloseResult { get; set; }
        public bool IsResponseResult { get; set; }
        public bool IsExceptionResult { get; set; }
        public int ReadTimeout { get; private set; }
        public int WriteTimeout { get; private set; }
        public string PortName { get; private set; }
        public BytesHelper Bytes { get; private set; } = BytesHelper.Instance;
        public SerialPortController PortController { get; private set; }
        public int SendBytesCount { get; private set; } = 0;
        public int ReceiveBytesCount { get; private set; } = 0;
        public delegate void MassaResponseCallback(MassaExchangeEntity massaExchange, byte[] response);
        private readonly MassaResponseCallback _massaResponseCallback = null;
        private MassaExchangeEntity _massaExchange = null;
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public MassaDeviceEntity(string portName, short? readTimeout, short? writeTimeout, MassaResponseCallback massaCallback)
        {
            Init(Close, ReleaseManaged, ReleaseUnmanaged);

            PortName = portName;
            ReadTimeout = readTimeout ?? 0_100;
            WriteTimeout = writeTimeout ?? 0_100;
            _massaResponseCallback = massaCallback;
            PortController = new(PortOpenCallback, PortCloseCallback, PortResponseCallback, PortExceptionCallback);
            IsOpenResult = false;
            IsCloseResult = false;
            IsResponseResult = false;
            IsExceptionResult = false;
        }

        #endregion

        #region Public and private methods - ISerialPortView

        public void SetController(SerialPortController controller)
        {
            PortController = controller;
        }

        public void PortOpenCallback(object sender, SerialPortEventArgs e)
        {
            if (e.SerialPort.IsOpen)
            {
                IsOpenResult = true;
                IsCloseResult = false;
            }
            else
            {
                IsOpenResult = false;
            }
        }

        public void PortCloseCallback(object sender, SerialPortEventArgs e)
        {
            // Close successfully.
            if (!e.SerialPort.IsOpen)
            {
                IsCloseResult = true;
                IsOpenResult = false;
            }
            else
            {
                IsCloseResult = false;
            }
        }

        public void PortResponseCallback(object sender, SerialPortEventArgs e)
        {
            lock (_locker)
            {
                IsResponseResult = true;
                CheckIsDisposed();
                ReceiveBytesCount += e.ReceivedBytes.Length;
                _massaResponseCallback?.Invoke(_massaExchange, e.ReceivedBytes);
                _massaExchange = null;
            }
        }

        public void PortExceptionCallback(Exception ex,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IsExceptionResult = true;
            DataAccessHelper.Instance.Log.LogError(ex, NetUtils.GetLocalHostName(false), nameof(MassaDeviceEntity), filePath, lineNumber, memberName);
        }

        #endregion

        #region Public and private methods

        public new void Open()
        {
            base.Open();
            //if (IsOpen) return;
            if (IsOpenPort) return;

            PortController.Open(PortName, ReadTimeout, WriteTimeout);
        }

        public void SendData(MassaExchangeEntity massaExchange)
        {
            CheckIsDisposed();
            _massaExchange = massaExchange;
            PortController.Send(massaExchange.Request);
            SendBytesCount += massaExchange.Request.Length;
        }

        public new void Close()
        {
            base.Close();

            //if (!IsOpen) return;
            CheckIsDisposed();

            PortController.Close();
        }

        public void ReleaseManaged()
        {
            Close();
        }

        public void ReleaseUnmanaged()
        {
            //
        }

        #endregion
    }
}
