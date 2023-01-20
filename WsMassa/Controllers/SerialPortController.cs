// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsMassa.Utils;

namespace WsMassa.Controllers;

public class SerialPortController
{
	public enum EnumUsbAdapterStatus
	{
		Default,
		IsConnectWithMassa,
		IsNotConnectWithMassa,
		IsDataExists,
		IsDataNotExists,
		IsException,
	}

	#region Public and private fields and properties

	public delegate void PortCallback(object sender, SerialPortEventArgs e);
	public delegate void PortExceptionCallback(Exception ex, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "");
	public SerialPort SerialPort { get; }
    private PortCallback OpenCallback { get; }
    private PortCallback CloseCallback { get; }
    private PortCallback ResponseCallback { get; }
    private PortExceptionCallback ExceptionCallback { get; }
	private readonly object _locker = new();
	public EnumUsbAdapterStatus AdapterStatus { get; private set; }
	public Exception CatchException { get; private set; }

    public SerialPortController()
    {
        SerialPort = new();
        AdapterStatus = EnumUsbAdapterStatus.Default;
        OpenCallback = (_, _) => { };
        CloseCallback = (_, _) => { };
        ResponseCallback = (_, _) => { };
        ExceptionCallback = (_, _, _, _) => { };
    }

    public SerialPortController(PortCallback openCallback, PortCallback closeCallback, PortCallback responseCallback, 
        PortExceptionCallback exceptionCallback) : this()
	{
		OpenCallback = openCallback;
		CloseCallback = closeCallback;
		ResponseCallback = responseCallback;
		ExceptionCallback = exceptionCallback;
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
		CatchException = null;
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
				AdapterStatus = EnumUsbAdapterStatus.IsNotConnectWithMassa;
				return;
			}
			else
				AdapterStatus = EnumUsbAdapterStatus.IsConnectWithMassa;

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
			SerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
			args.SerialPort = SerialPort;

			OpenCallback?.Invoke(this, args);
		}
		catch (Exception ex)
		{
			AdapterStatus = EnumUsbAdapterStatus.IsException;
			CatchException = ex;
			args.SerialPort = new();
			//ExceptionCallback?.Invoke(ex);
		}
	}

	private void DataReceived(object sender, SerialDataReceivedEventArgs e)
	{
		lock (_locker)
		{
			if (!SerialPort.IsOpen || SerialPort.BytesToRead <= 0)
				return;

			try
			{
				int len = SerialPort.BytesToRead;
				byte[] data = new byte[len];

				SerialPort.Read(data, 0, len);

				SerialPortEventArgs args = new()
				{
					ReceivedBytes = data
				};
				AdapterStatus = data.All(x => x == 0x00) ? EnumUsbAdapterStatus.IsDataNotExists : EnumUsbAdapterStatus.IsDataExists;
				ResponseCallback?.Invoke(this, args);
			}
			catch (Exception ex)
			{
				ExceptionCallback?.Invoke(ex);
			}
		}
	}

	public bool Send(string str)
	{
		if (str != null && str != "")
		{
			return Send(Encoding.Default.GetBytes(str));
		}
		return false;
	}

	public bool Send(byte[] bytes)
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
			ExceptionCallback?.Invoke(ex);
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
		Thread closeThread = new(new ThreadStart(CloseThread));
		closeThread.Start();
	}

	private void CloseThread()
	{
		SerialPortEventArgs args = new() { SerialPort = new() };
		try
		{
			SerialPort.Close();
			SerialPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceived);
		}
		catch (Exception ex)
		{
			args.SerialPort = new();
			ExceptionCallback?.Invoke(ex);
		}
		finally
		{
			CloseCallback?.Invoke(this, args);
		}
	}

	#endregion
}