namespace WsMassaCore.Helpers;

public class MassaDeviceHelper : IDisposable
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static MassaDeviceHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static MassaDeviceHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields and properties
    private SqlContextItemHelper ContextItem => SqlContextItemHelper.Instance;
    
    private readonly object _locker = new();
    private bool IsOpenResult { get; set; }
    private bool IsCloseResult { get; set; }
    private bool IsResponseResult { get; set; }
    private bool IsExceptionResult { get; set; }
    private int ReadTimeout { get; set; }
    private int WriteTimeout { get; set; }
    private string PortName { get; set; }
    private int SendBytesCount { get; set; }
    private int ReceiveBytesCount { get; set; }
    private MassaResponseCallback _massaResponseCallback;
    
    public bool IsOpenPort => SerialPort.SerialPort.IsOpen;
    public SerialPortHelper SerialPort { get; private set; }
	public delegate void MassaResponseCallback(byte[] response);

    public string ComPort => PortName;

    public MassaDeviceHelper()
    {
        PortName = string.Empty;
        SerialPort = new();
		_massaResponseCallback = _ => { };
    }

    public void Init(string portName,  MassaResponseCallback massaCallback)
	{
		PortName = portName;
		ReadTimeout = 0_100;
		WriteTimeout = 0_100;
		_massaResponseCallback = massaCallback;
		SerialPort = new(PortOpenCallback, PortCloseCallback, PortResponseCallback, PortExceptionCallback);
		IsOpenResult = false;
		IsCloseResult = false;
		IsResponseResult = false;
		IsExceptionResult = false;
    }

	#endregion

	#region Public and private methods - ISerialPortView

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
		ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);
	}

	#endregion

	#region Public and private methods

    public void Execute()
	{
		if (IsOpenPort) return;
		SerialPort.Open(PortName, ReadTimeout, WriteTimeout);
	}

	public void SendData()
	{
		SerialPort.Send(MassaExchangeHelper.Instance.Request);
		SendBytesCount += MassaExchangeHelper.Instance.Request.Length;
	}

    public void Dispose()
	{
		SerialPort.Close();
	}

	#endregion
}
