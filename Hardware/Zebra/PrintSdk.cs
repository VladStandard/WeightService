using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using EntitiesLib;
using Hardware.Tsc;
using log4net;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace Hardware.Zebra
{
    public class PrintSdk
    {
        #region Public and private fields and properties

        public string Peeler { get; private set; }
        public int UserLabelCount { get; private set; }
        public PrinterStatus CurrentStatus { get; private set; }
        public int CommandThreadTimeOut { get; private set; } = 100;

        public delegate void OnHandler(PrintSdk state);
        public event OnHandler Notify;

        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Connection Con { get; private set; }
        public ConcurrentQueue<string> CmdQueue { get; } = new ConcurrentQueue<string>();
        private readonly object _locker = new object();
        private Thread _sessionSharingThread = null;
        private bool _isThreadWork = true;

        #endregion

        #region Constructor and destructor

        public PrintSdk(Connection connection, int commandThreadTimeOut = 100)
        {
            CommandThreadTimeOut = commandThreadTimeOut;
            Con = connection;
        }

        public PrintSdk(string ip, int port, int commandThreadTimeOut = 100)
        {
            //var zebraCurrentState = new StateEntity();
            CommandThreadTimeOut = commandThreadTimeOut;
            Con = new TcpConnection(ip, port);
        }

        #endregion

        #region Public and private methods

        /// <summary>
        /// Отправить задание в очередь печати.
        /// </summary>
        /// <param name="printCmd"></param>
        /// <param name="labelId"></param>
        /// <param name="printerType"></param>
        public async void SendAsync(string printCmd, int labelId, string printerType)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            if (printerType.Contains("TSC "))
            {
                printCmd = printCmd;
            }
            CmdQueue.Enqueue(printCmd);
            
            // Сохранить ZPL-запрос в таблицу [Labels].
            var zplLabel = new ZplLabel
            {
                WeighingFactId = labelId,
                Content = printCmd
            };
            zplLabel.Save();
        }

        public void PrinterOpenZebra()
        {
            var printerDevice = ZebraPrinterFactory.GetInstance(Con);
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
                                    if (CmdQueue.TryDequeue(out var request))
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
            //if (PrintControl == null)
            var printControl = new PrintControlEntity("192.168.6.132");
            _sessionSharingThread = new Thread(t =>
            {
                UserLabelCount = 1;
                while (_isThreadWork)
                {
                    lock (_locker)
                    {
                        try
                        {
                            if (CmdQueue.TryDequeue(out var request))
                            {
                                request = request.Replace("|", "\\&");
                                if (!request.Equals("^XA~JA^XZ") && !request.Contains("odometer.user_label_count"))
                                {
                                    printControl.EthernetOpen();
                                    //CurrentStatus = printerDevice.GetCurrentStatus();
                                    //UserLabelCount = int.Parse(SGD.GET("odometer.user_label_count", printerDevice.Connection));
                                    //UserLabelCount = 1;
                                    //Peeler = SGD.GET("sensor.peeler", printerDevice.Connection);

                                    //if (CurrentStatus.isReadyToPrint)
                                    if (printControl.IsStatusNormal)
                                    {
                                        //if (Peeler == "clear")
                                        {
                                    
                                            Console.WriteLine(request);
                                        }
                                        //printerDevice.SendCommand(request);
                                        printControl.EthernetSendCmd(request);
                                    }
                                    printControl.TscEthernet.closeport();
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
                PrinterOpenTsc();
            else
                PrinterOpenZebra();
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

        public void ClearPrintBuffer(string printerType)
        {
            while (!CmdQueue.IsEmpty)
            {
                CmdQueue.TryDequeue(out _);
            }
            if (printerType.Contains("TSC "))
            { }
            else
                CmdQueue.Enqueue("^XA~JA^XZ");
        }

        public void SetOdometorUserLabel(int value)
        {
            CmdQueue.Enqueue($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
        }

        #endregion
    }
}
