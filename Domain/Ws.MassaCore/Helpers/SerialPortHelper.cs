namespace Ws.MassaCore.Helpers;

public class SerialPortHelper
{
	#region Public and private fields and properties
    
    private readonly object _locker = new();
	public SerialPort SerialPort { get; }
    private Action<object, SerialPortEventArgs> OpenCallback { get; }
    private Action<object, SerialPortEventArgs> CloseCallback { get; }
    private Action<object, SerialPortEventArgs> ResponseCallback { get; }
	private Action<Exception, string, int, string> ExceptionAction { get; }
	public UsbAdapterStatus AdapterStatus { get; private set; }
	public Exception Exception { get; private set; }

    public SerialPortHelper()
    {
        SerialPort = new();
        AdapterStatus = UsbAdapterStatus.Default;
        OpenCallback = (_, _) => { };
        CloseCallback = (_, _) => { };
        ResponseCallback = (_, _) => { };
        ExceptionAction = (_, _, _, _) => { };
    }

    public SerialPortHelper(Action<object, SerialPortEventArgs> openCallback, Action<object, SerialPortEventArgs> closeCallback,
        Action<object, SerialPortEventArgs> responseCallback, 
        Action<Exception, string, int, string> exceptionAction) : this()
    {
		OpenCallback = openCallback;
		CloseCallback = closeCallback;
		ResponseCallback = responseCallback;
		ExceptionAction = exceptionAction;
	}

	#endregion

	#region Public and private methods

	public void Open(string portName, string baudRate, string parity, string dataBits, string stopBits, string handshake)
	{
		if (string.IsNullOrEmpty(portName))
			return;
		Open(portName, baudRate, parity, dataBits, stopBits, handshake, 0_100, 0_100);
	}

	public void Open(string portName, int readTimeout, int writeTimeout)
	{
		if (string.IsNullOrEmpty(portName))
			return;
		Open(SerialPortUtils.GetSerialPort(portName, readTimeout, writeTimeout));
	}

	public void Open(SerialPort serialPort)
	{
		Open(serialPort.PortName, serialPort.BaudRate.ToString(), serialPort.Parity.ToString(), serialPort.DataBits.ToString(), 
			serialPort.StopBits.ToString(), serialPort.Handshake.ToString(), serialPort.ReadTimeout, serialPort.WriteTimeout);
	}

	public void Open(string portName, string baudRate, string parity, string dataBits, string stopBits, string handshake, int readTimeout, int writeTimeout)
	{
		SerialPortEventArgs args = new();
		Exception = null;
		try
		{
			if (SerialPort.IsOpen)
			{
				Close();
				return;
			}
			List<string> comPorts = SerialPort.GetPortNames().ToList();
			if (!comPorts.Contains(portName))
			{
				AdapterStatus = UsbAdapterStatus.IsNotConnectWithMassa;
				return;
			}
            AdapterStatus = UsbAdapterStatus.IsConnectWithMassa;

			SerialPort.PortName = portName;
			SerialPort.BaudRate = Convert.ToInt32(baudRate);
			SerialPort.DataBits = Convert.ToInt16(dataBits);

			/*
			 *  If the Handshake property is set to None the DTR and RTS pins 
			 *  are then freed up for the common use of Power, the PC on which
			 *  this is being typed gives +10.99 volts on the DTR pin & +10.99
			 *  volts again on the RTS pin if set to true. If set to false 
			 *  it gives -9.95 volts on the DTR, -9.94 volts on the RTS. 
			 *  These values are between +3 to +25 and -3 to -25 volts this 
			 *  give a dead zone to allow for noise immunity.
			 *  http://www.codeproject.com/Articles/678025/Serial-Comms-in-Csharp-for-Beginners
			*/
			if (handshake == "None")
			{
				// Never delete this property.
				SerialPort.RtsEnable = true;
				SerialPort.DtrEnable = true;
			}

			SerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits);
			SerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), parity);
			SerialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), handshake);
			SerialPort.ReadTimeout = readTimeout;
			SerialPort.WriteTimeout = writeTimeout;
			SerialPort.Open();
			SerialPort.DataReceived += DataReceived;
			args.SerialPort = SerialPort;

			OpenCallback?.Invoke(this, args);
		}
		catch (Exception ex)
		{
			AdapterStatus = UsbAdapterStatus.IsException;
			Exception = ex;
			args.SerialPort = new();
			//ExceptionCallback?.Invoke(ex);
		}
	}

    private void DataReceivedCore(
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        if (!SerialPort.IsOpen || SerialPort.BytesToRead <= 0) return;

        try
        {
            int len = SerialPort.BytesToRead;
            byte[] data = new byte[len];

            SerialPort.Read(data, 0, len);

            SerialPortEventArgs args = new()
            {
                ReceivedBytes = data
            };
            AdapterStatus = data.All(item => item == 0x00) ? UsbAdapterStatus.IsDataNotExists : UsbAdapterStatus.IsDataExists;
            ResponseCallback(this, args);
        }
        catch (Exception ex)
        {
            ExceptionAction.Invoke(ex, filePath, lineNumber, memberName);
        }
    }


    private void DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        lock (_locker) DataReceivedCore();
    }

	public bool Send(string str) => !string.IsNullOrEmpty(str) && Send(Encoding.Default.GetBytes(str));

    public bool Send(byte[] bytes,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		if (!SerialPort.IsOpen)
		{
			return false;
		}

		try
		{
			SerialPort.Write(bytes, 0, bytes.Length);
			return true;
		}
		catch (Exception ex)
		{
			ExceptionAction.Invoke(ex, filePath, lineNumber, memberName);
		}
		return false;
	}

	/**
         *  Take care to avoid deadlock when calling Close on the SerialPort in response to a GUI event.
         *   An app involving the UI and the SerialPort freezes up when closing the SerialPort
         *   Deadlock can occur if Control.Invoke() is used in serial port event handlers
         * 
         *  The typical scenario we encounter is occasional deadlock in an app 
         *  that has a data received handler trying to update the GUI at the 
         *  same time the GUI thread is trying to close the SerialPort (for 
         *  example, in response to the user clicking a Close button).
         * 
         *  The reason deadlock happens is that Close() waits for events to 
         *  finish executing before it closes the port. You can address this 
         *  problem in your apps in two ways:
         * 
         *  (1)In your event handlers, replace every Control.Invoke call with 
         *  Control.BeginInvoke, which executes asynchronously and avoids 
         *  the deadlock condition. This is commonly used for deadlock avoidance 
         *  when working with GUIs.
         *  
         *  (2)Call serialPort.Close() on a separate thread. You may prefer this
         *  because this is less invasive than updating your Invoke calls.
         */
	public void Close()
	{
		Thread closeThread = new(CloseThread);
		closeThread.Start();
	}

    private void CloseThreadCore(
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string memberName = "")
    {
        SerialPortEventArgs args = new() { SerialPort = new() };
        try
        {
            SerialPort.Close();
            SerialPort.DataReceived -= DataReceived;
        }
        catch (Exception ex)
        {
            args.SerialPort = new();
            ExceptionAction?.Invoke(ex, filePath, lineNumber, memberName);
        }
        finally
        {
            CloseCallback?.Invoke(this, args);
        }
    }


    private void CloseThread() => CloseThreadCore();

    #endregion
}