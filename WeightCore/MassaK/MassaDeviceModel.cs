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
    public partial class MassaDeviceModel : DisposableBase, IDisposableBase
    {
        #region Public and private fields and properties

        public bool IsConnected => PortController.Port?.SerialPort?.IsOpen == true;
        public int ReadTimeout { get; private set; }
        public int WriteTimeout { get; private set; }
        public string PortName { get; private set; }

        public BytesHelper Bytes { get; private set; } = BytesHelper.Instance;
        private SerialPortController PortController { get; set; }
        public int SendBytesCount { get; private set; } = 0;
        public int ReceiveBytesCount { get; private set; } = 0;
        public delegate void MassaResponseCallback(MassaExchangeEntity massaExchange, byte[] response);
        private readonly MassaResponseCallback _massaCallback = null;
        private MassaExchangeEntity _massaExchange = null;
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public MassaDeviceModel(string portName, short? readTimeout, short? writeTimeout, MassaResponseCallback massaCallback)
        {
            Init(Close, ReleaseManaged, ReleaseUnmanaged);

            PortName = portName;
            ReadTimeout = readTimeout ?? 100;
            WriteTimeout = writeTimeout ?? 100;
            _massaCallback = massaCallback;
            PortController = new(PortOpenCallback, PortCloseCallback, PortMassaResponseCallback, PortExceptionCallback);
        }

        #endregion

        #region Public and private methods - ISerialPortView

        public void SetController(SerialPortController controller)
        {
            PortController = controller;
        }

        public void PortOpenCallback(object sender, SerialPortEventArgs e)
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

        public void PortCloseCallback(object sender, SerialPortEventArgs e)
        {
            // Close successfully.
            if (!e.IsOpened)
            {
                //
            }
        }

        public void PortMassaResponseCallback(object sender, SerialPortEventArgs e)
        {
            lock (_locker)
            {
                CheckIsDisposed();
                ReceiveBytesCount += e.ReceivedBytes.Length;
                _massaCallback?.Invoke(_massaExchange, e.ReceivedBytes);
                _massaExchange = null;
            }
        }

        public void PortExceptionCallback(Exception ex,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            DataAccessHelper.Instance.Log.LogError(ex, NetUtils.GetLocalHostName(false), nameof(MassaDeviceModel), filePath, lineNumber, memberName);
        }

        #endregion

        #region Public and private methods

        public new void Open()
        {
            base.Open();
            //if (IsOpened) return;
            if (PortController.Port.SerialPort.IsOpen) return;

            try
            {
                if (string.IsNullOrEmpty(PortName))
                {
                    throw new ArgumentNullException(PortName);
                }
                PortController.OpenPort(PortName, ReadTimeout, WriteTimeout);
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
            PortController.SendData(massaExchange.Request);
            SendBytesCount += massaExchange.Request.Length;
        }

        public new void Close()
        {
            base.Close();
            
            //if (!IsOpened) return;
            CheckIsDisposed();

            PortController.ClosePort();
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
