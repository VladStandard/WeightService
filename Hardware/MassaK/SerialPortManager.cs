// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace Hardware.MassaK
{
    public sealed class SerialPortManager
    {
        #region Constructor

        public SerialPortManager()
        {
            SerialPort = new SerialPort();
            _readerThread = null;
            _keepReading = false;
        }

        #endregion

        #region Public fields and properties

        public SerialPort SerialPort;
        /// <summary>
        /// Update the serial port status to the event subscriber
        /// </summary>
        public event EventHandler<string> OnStatusChanged;
        public event EventHandler<byte[]> OnDataReceived;
        public event EventHandler<bool> OnSerialPortOpened;

        #endregion

        #region Private fields and properties

        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Thread _readerThread;
        private volatile bool _keepReading;

        #endregion

        #region Public/Private methods

        /// <summary>
        /// Open the serial port connection using basic serial port settings
        /// </summary>
        /// <param name="portName">COM1 / COM3 / COM4 / etc.</param>
        /// <param name="baudRate">0 / 100 / 300 / 600 / 1200 / 2400 / 4800 / 9600 / 14400 / 19200 / 38400 / 56000 / 57600 / 115200 / 128000 / 256000</param>
        /// <param name="parity">None / Odd / Even / Mark / Space</param>
        /// <param name="dataBits">5 / 6 / 7 / 8</param>
        /// <param name="stopBits">None / One / Two / OnePointFive</param>
        /// <param name="handshake">None / XOnXOff / RequestToSend / RequestToSendXOnXOff</param>
        public void Open(string portName = "COM1", int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8,
            StopBits stopBits = StopBits.One, Handshake handshake = Handshake.None)
        {
            Close();

            try
            {
                SerialPort.PortName = portName;
                SerialPort.BaudRate = baudRate;
                SerialPort.Parity = parity;
                SerialPort.DataBits = dataBits;
                SerialPort.StopBits = stopBits;
                SerialPort.Handshake = handshake;

                SerialPort.ReadTimeout = 50;
                SerialPort.WriteTimeout = 50;

                SerialPort.Open();
                StartReading();
            }
            catch (IOException)
            {
                OnStatusChanged?.Invoke(this, $"{portName} does not exist.");
            }
            catch (UnauthorizedAccessException)
            {
                OnStatusChanged?.Invoke(this, $"{portName} already in use.");
            }
            catch (Exception ex)
            {
                OnStatusChanged?.Invoke(this, $"Error: {ex.Message}");
            }

            if (SerialPort.IsOpen)
            {
                string sb = StopBits.None.ToString().Substring(0, 1);
                switch (SerialPort.StopBits)
                {
                    case StopBits.One:
                        sb = "1"; break;
                    case StopBits.OnePointFive:
                        sb = "1.5"; break;
                    case StopBits.Two:
                        sb = "2"; break;
                }

                string p = SerialPort.Parity.ToString().Substring(0, 1);
                string hs = SerialPort.Handshake == Handshake.None ? "No Handshake" : SerialPort.Handshake.ToString();

                OnStatusChanged?.Invoke(this,
                    $"Connected to {SerialPort.PortName}: {SerialPort.BaudRate} bps, {SerialPort.DataBits}{p}{sb}, {hs}.");

                OnSerialPortOpened?.Invoke(this, true);
            }
            else
            {
                OnStatusChanged?.Invoke(this, $"{portName} already in use.");
                OnSerialPortOpened?.Invoke(this, false);
            }
        }

        public void Close()
        {
            StopReading();
            SerialPort.Close();

            OnStatusChanged?.Invoke(this, "Connection closed.");
            OnSerialPortOpened?.Invoke(this, false);
        }

        public void SendString(string message)
        {
            if (SerialPort.IsOpen)
            {
                try
                {
                    _keepReading = false;
                    Thread.Sleep(3);
                    SerialPort.Write(message);
                    OnStatusChanged?.Invoke(this, $"Message sent: {message}");
                }
                catch (Exception ex)
                {
                    OnStatusChanged?.Invoke(this, $"Failed to send string: {ex.Message}");
                }
                finally
                {
                    Thread.Sleep(3);
                    _keepReading = true;
                }
            }
        }

        public async void SendStringAsync(string message)
        {
            await Task.Run(() => SendString(message));
        }

        private void SendArrayBytes(byte[] data)
        {
            if (SerialPort.IsOpen)
            {
                try
                {
                    _keepReading = false;
                    Thread.Sleep(3);
                    SerialPort.Write(data, 0, data.Length);
                    OnStatusChanged?.Invoke(this, $"Message sent: {data}");
                }
                catch (Exception ex)
                {
                    OnStatusChanged?.Invoke(this, $"Failed to send string: {ex.Message}");
                }
                finally
                {
                    Thread.Sleep(3);
                    _keepReading = true;
                }
            }
        }

        public async void SendArrayBytesAsync(byte[] data)
        {
            await Task.Run(() => SendArrayBytes(data));
        }

        #endregion

        #region Private methods

        private void StartReading()
        {
            if (_readerThread == null)
            {
                _keepReading = true;
                _readerThread = new Thread(t =>
                   {
                       while (true)
                       {
                           ReadPort();
                            //Thread.Sleep(250);
                        }
                   })
                { IsBackground = true };
                _readerThread.Start();
            }
        }

        private void StopReading()
        {
            if (_keepReading)
            {
                _keepReading = false;
                _readerThread.Join();
                _readerThread = null;
            }
        }

        private void ReadPort()
        {
            byte[] data;
            while (_keepReading)
            {
                if (SerialPort.IsOpen && (SerialPort.BytesToRead > 0))
                {
                    try
                    {
                        using (var ms = new MemoryStream())
                        {
                            int bytes = SerialPort.BytesToRead;
                            byte[] buffer = new byte[bytes];
                            SerialPort.Read(buffer, 0, bytes);

                            foreach (byte b in buffer)
                            {
                                ms.Write(buffer, 0, buffer.Length);
                            }
                            data = ms.ToArray();
                        }
                        //string data = Encoding.ASCII.GetString(readBuffer, 0, count);
                        //string data = _serialPort.ReadLine();
                        OnDataReceived?.Invoke(this, data);
                    }
                    catch (TimeoutException)
                    {
                    }
                }
                else
                {
                    var waitTime = new TimeSpan(0, 0, 0, 0, 50);
                    Thread.Sleep(waitTime);
                }
            }
        }

        #endregion
    }
}