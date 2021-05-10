using log4net;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using TscBarcode.ViewModels;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace ZplCommonLib
{
    public class ZebraPrinterWithSdk
    {
        #region Public and private fields and properties

        public string Peeler { get; private set; }
        public int UserLabelCount { get; private set; }
        public PrinterStatus CurrentStatus { get; private set; }
        public int CommandThreadTimeOut { get; private set; } = 100;

        public delegate void OnHandler(ZebraPrinterWithSdk state);
        public event OnHandler Notify;

        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Connection Con { get; private set; }
        public ConcurrentQueue<string> RequestQueue { get; private set; } = new ConcurrentQueue<string>();
        private readonly object _locker = new object();
        private Thread _sessionSharingThread = null;
        private bool _isThreadWork = true;
        private ProgramSettings Settings { get; set; }

        #endregion

        #region Constructor and destructor

        public ZebraPrinterWithSdk(Connection connection, int commandThreadTimeOut = 100)
        {
            CommandThreadTimeOut = commandThreadTimeOut;
            Con = connection;
        }

        public ZebraPrinterWithSdk(string ip, int port, int commandThreadTimeOut = 100)
        {
            ZebraState ZebraCurrentState = new ZebraState();
            CommandThreadTimeOut = commandThreadTimeOut;
            Con = new TcpConnection(ip, port);
        }

        #endregion

        #region Public and private methods

        public void Send(string content)
        {
            RequestQueue.Enqueue(content);
        }

        public async void SendAsync(string content)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Send(content);
        }

        public void PrinterOpenZebra()
        {
            ZebraPrinter printerDevice = ZebraPrinterFactory.GetInstance(Con);
            _sessionSharingThread = new Thread(t =>
            {
                while (_isThreadWork)
                {
                    lock (_locker)
                    {
                        try
                        {
                            CurrentStatus = printerDevice.GetCurrentStatus();
                            UserLabelCount = int.Parse(SGD.GET("odometer.user_label_count", printerDevice.Connection));
                            Peeler = SGD.GET("sensor.peeler", printerDevice.Connection);

                            if (CurrentStatus.isReadyToPrint)
                            {
                                if (Peeler == "clear")
                                {
                                    if (RequestQueue.TryDequeue(out var request))
                                    {
                                        request = request.Replace("|", "\\&");
                                        printerDevice.SendCommand(request);
                                    }
                                }
                            }
                            Notify?.Invoke(this);
                        }
                        catch (ConnectionException e)
                        {
                            _log.Error(e.ToString());
                        }
                        catch (ZebraPrinterLanguageUnknownException e)
                        {
                            _log.Error(e.ToString());
                        }
                        catch (Exception e)
                        {
                            _log.Error(e.ToString());
                        }
                    }
                    Thread.Sleep(CommandThreadTimeOut);
                }
            })
            { IsBackground = true };
            _sessionSharingThread.Start();
            Thread.Sleep(CommandThreadTimeOut);
        }

        public void PrinterOpenTsc()
        {
            if (Settings == null)
                Settings = new ProgramSettings();
            //ZebraPrinter printerDevice = ZebraPrinterFactory.GetInstance(Con);
            _sessionSharingThread = new Thread(t =>
            {
                while (_isThreadWork)
                {
                    lock (_locker)
                    {
                        try
                        {
                            //CurrentStatus = printerDevice.GetCurrentStatus();
                            //UserLabelCount = int.Parse(SGD.GET("odometer.user_label_count", printerDevice.Connection));
                            //UserLabelCount = 1;
                            //Peeler = SGD.GET("sensor.peeler", printerDevice.Connection);

                            //if (CurrentStatus.isReadyToPrint)
                            {
                                //if (Peeler == "clear")
                                {
                                    if (RequestQueue.TryDequeue(out var request))
                                    {
                                        request = request.Replace("|", "\\&");
                                        //printerDevice.SendCommand(request);
                                        Settings.EthernetPrint(request);
                                    }
                                }
                            }
                            Notify?.Invoke(this);
                        }
                        catch (ConnectionException e)
                        {
                            _log.Error(e.ToString());
                        }
                        catch (ZebraPrinterLanguageUnknownException e)
                        {
                            _log.Error(e.ToString());
                        }
                        catch (Exception e)
                        {
                            _log.Error(e.ToString());
                        }
                    }
                    Thread.Sleep(CommandThreadTimeOut);
                }
            })
            { IsBackground = true };
            _sessionSharingThread.Start();
            Thread.Sleep(CommandThreadTimeOut);
        }

        public void Open(string printerType)
        {
            Con.Open();
            _isThreadWork = true;
            if (printerType.Contains("TSC "))
            {
                PrinterOpenTsc();
            }
            else
            {
                PrinterOpenZebra();
            }
        }

        public void Close()
        {
            if (_sessionSharingThread != null && _sessionSharingThread.IsAlive)
            {
                _isThreadWork = false;
                Thread.Sleep(5 * CommandThreadTimeOut);
                _sessionSharingThread.Join(10);
                _sessionSharingThread.Abort();
                _sessionSharingThread = null;
            }
            Con.Close();
        }

        public void ClearZebraPrintBuffer()
        {
            // если очередь не пустая
            // очищаем
            // запускаем команду очистки очереди печати
            while (!RequestQueue.IsEmpty)
            {
                var msg = string.Empty;
                RequestQueue.TryDequeue(out msg);
            }
            var zplContent = $"^XA~JA^XZ";
            RequestQueue.Enqueue(zplContent);
        }

        public void SetOdometorUserLabel(int value)
        {
            var zplContent = $"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n";
            RequestQueue.Enqueue(zplContent);
        }

        #endregion
    }
}
