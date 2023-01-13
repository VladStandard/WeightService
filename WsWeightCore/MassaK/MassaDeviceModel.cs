// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using DataCore.Protocols;
using MDSoft.SerialPorts;

namespace WeightCore.MassaK;

public class MassaDeviceModel : DisposableBase, IDisposableBase
{
	#region Public and private fields and properties

	public bool IsOpenPort => PortController.SerialPort.IsOpen;
	public bool IsOpenResult { get; set; }
	public bool IsCloseResult { get; set; }
	public bool IsResponseResult { get; set; }
	public bool IsExceptionResult { get; set; }
	public int ReadTimeout { get; }
	public int WriteTimeout { get; }
	public string PortName { get; }
	public BytesHelper Bytes { get; } = BytesHelper.Instance;
	public SerialPortController PortController { get; private set; }
	public int SendBytesCount { get; private set; }
	public int ReceiveBytesCount { get; private set; }
	public delegate void MassaResponseCallback(MassaExchangeModel massaExchange, byte[] response);
	private readonly MassaResponseCallback _massaResponseCallback;
	private MassaExchangeModel _massaExchange;
	private readonly object _locker = new();

	#endregion

	#region Constructor and destructor

	public MassaDeviceModel(string portName, short? readTimeout, short? writeTimeout, MassaResponseCallback massaCallback)
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
		DataAccessHelper.Instance.LogError(ex, NetUtils.GetLocalDeviceName(false), nameof(MassaDeviceModel), filePath, lineNumber, memberName);
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

	public void SendData(MassaExchangeModel massaExchange)
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