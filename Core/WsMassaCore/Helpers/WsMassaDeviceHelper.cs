// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Bases;

namespace WsMassaCore.Helpers;

public class WsMassaDeviceHelper : WsHelperBase
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static WsMassaDeviceHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static WsMassaDeviceHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields and properties

	public bool IsOpenPort => SerialPort.SerialPort.IsOpen;
    private bool IsOpenResult { get; set; }
    private bool IsCloseResult { get; set; }
    private bool IsResponseResult { get; set; }
    private bool IsExceptionResult { get; set; }
    private int ReadTimeout { get; set; }
    private int WriteTimeout { get; set; }
    private string PortName { get; set; }
	public WsSerialPortHelper SerialPort { get; private set; }
    private int SendBytesCount { get; set; }
    private int ReceiveBytesCount { get; set; }
	public delegate void MassaResponseCallback(byte[] response);
	private MassaResponseCallback _massaResponseCallback;
	private readonly object _locker = new();

    public WsMassaDeviceHelper()
    {
        PortName = string.Empty;
        SerialPort = new();
		_massaResponseCallback = _ => { };
    }

    public void Init(string portName, short? readTimeout, short? writeTimeout, MassaResponseCallback massaCallback)
	{
		base.Init();
		PortName = portName;
		ReadTimeout = readTimeout ?? 0_100;
		WriteTimeout = writeTimeout ?? 0_100;
		_massaResponseCallback = massaCallback;
		SerialPort = new(PortOpenCallback, PortCloseCallback, PortResponseCallback, PortExceptionCallback);
		IsOpenResult = false;
		IsCloseResult = false;
		IsResponseResult = false;
		IsExceptionResult = false;
    }

	#endregion

	#region Public and private methods - ISerialPortView

	public void SetController(WsSerialPortHelper controller)
	{
		SerialPort = controller;
	}

    private void PortOpenCallback(object sender, SerialPortEventArgs e)
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

    private void PortCloseCallback(object sender, SerialPortEventArgs e)
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

    private void PortResponseCallback(object sender, SerialPortEventArgs e)
	{
		lock (_locker)
		{
			IsResponseResult = true;
			ReceiveBytesCount += e.ReceivedBytes.Length;
			_massaResponseCallback(e.ReceivedBytes);
		}
	}

    private void PortExceptionCallback(Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		IsExceptionResult = true;
		WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);
	}

	#endregion

	#region Public and private methods

    public override void Execute()
	{
		base.Execute();
		if (IsOpenPort) return;
		SerialPort.Open(PortName, ReadTimeout, WriteTimeout);
	}

	public void SendData()
	{
		SerialPort.Send(WsMassaExchangeHelper.Instance.Request);
		SendBytesCount += WsMassaExchangeHelper.Instance.Request.Length;
	}

    public override void Close()
	{
		base.Close();
		SerialPort.Close();
	}

	#endregion
}
