// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsMassa.Helpers;

public class MassaDeviceHelper : HelperBase //DisposableBase, IDisposableBase
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static MassaDeviceHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static MassaDeviceHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields and properties

	public bool IsOpenPort => PortController.SerialPort.IsOpen;
    private bool IsOpenResult { get; set; }
    private bool IsCloseResult { get; set; }
    private bool IsResponseResult { get; set; }
    private bool IsExceptionResult { get; set; }
    private int ReadTimeout { get; set; }
    private int WriteTimeout { get; set; }
    private string PortName { get; set; }
	public SerialPortHelper PortController { get; private set; }
    private int SendBytesCount { get; set; }
    private int ReceiveBytesCount { get; set; }
	public delegate void MassaResponseCallback(byte[] response);
	private MassaResponseCallback _massaResponseCallback;
	private readonly object _locker = new();

    public MassaDeviceHelper()
    {
        PortName = string.Empty;
        PortController = new();
		_massaResponseCallback = _ => { };
    }

    public void Init(string portName, short? readTimeout, short? writeTimeout, MassaResponseCallback massaCallback)
	{
		base.Init();
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

	public void SetController(SerialPortHelper controller)
	{
		PortController = controller;
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

    private void PortExceptionCallback(Exception ex)
	{
		IsExceptionResult = true;
		DataAccessHelper.Instance.LogError(ex);
	}

	#endregion

	#region Public and private methods

    public override void Execute()
	{
		base.Execute();
		if (IsOpenPort) return;
		PortController.Open(PortName, ReadTimeout, WriteTimeout);
	}

	public void SendData()
	{
		PortController.Send(MassaExchangeHelper.Instance.Request);
		SendBytesCount += MassaExchangeHelper.Instance.Request.Length;
	}

    public override void Close()
	{
		base.Close();
		PortController.Close();
	}

	#endregion
}
