// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Helpers;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;
using WeightCore.Print.Tsc;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace WeightCore.Print
{
    public class PrintManagerEntity
    {
        #region Public and private fields and properties - Manager

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        public int WaitWhileMiliSeconds { get; }
        public int WaitExceptionMiliSeconds { get; }
        public int WaitCloseMiliSeconds { get; }
        public string ExceptionMsg { get; private set; }
        public delegate void Callback();
        public bool IsExecute { get; set; }

        #endregion

        #region Public and private fields and properties

        private readonly LogHelper _log = LogHelper.Instance;
        public string Peeler { get; private set; }
        public int UserLabelCount { get; private set; }
        public PrinterStatus CurrentStatus { get; private set; }
        public Connection Con { get; }
        public ConcurrentQueue<string> PrintCmdQueue { get; } = new ConcurrentQueue<string>();
        private readonly object _locker = new();
        public PrintControlEntity PrintControl { get; set; }
        private ZebraPrinter _zebraPrinter;
        public ZebraPrinter ZebraPrinter => _zebraPrinter ?? (_zebraPrinter = ZebraPrinterFactory.GetInstance(Con));

        #endregion

        #region Constructor and destructor

        public PrintManagerEntity(int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds)
        {
            WaitWhileMiliSeconds = waitWhileMiliSeconds;
            WaitExceptionMiliSeconds = waitExceptionMiliSeconds;
            WaitCloseMiliSeconds = waitCloseMiliSeconds;
            IsExecute = false;
        }

        public PrintManagerEntity(Connection connection, string ip, int port, int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds) :
            this(waitWhileMiliSeconds, waitExceptionMiliSeconds, waitCloseMiliSeconds)
        {
            Con = connection;
            PrintControl = new PrintControlEntity(PrintInterface.Ethernet, ip, port);
        }

        public PrintManagerEntity(string ip, int port, int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds) :
            this(waitWhileMiliSeconds, waitExceptionMiliSeconds, waitCloseMiliSeconds)
        {
            Con = new TcpConnection(ip, port);
            PrintControl = new PrintControlEntity(PrintInterface.Ethernet, ip, port);
        }

        #endregion

        #region Public and private methods - Manager

        public void Open(bool isTscPrinter, Callback callbackPrintManager)
        {
            IsExecute = true;
            while (IsExecute)
            {
                try
                {
                    OpenJob(isTscPrinter);
                    callbackPrintManager();
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
                }
                catch (TaskCanceledException)
                {
                    // Console.WriteLine(tcex.Message);
                    // Not the problem.
                }
                catch (Exception ex)
                {
                    _exception.Catch(null, ref ex);
                    throw;
                }
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void Close()
        {
            try
            {
                IsExecute = false;
                CloseJob();
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
                throw;
            }
        }

        #endregion

        #region Public and private methods

        public async void SendAsync(string printCmd)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            PrintCmdQueue.Enqueue(printCmd);
        }

        public void OpenJob(bool isTscPrinter)
        {
            Con.Open();
            if (isTscPrinter)
                OpenTsc();
            else
                OpenZebra();
        }

        public void OpenZebra()
        {
            try
            {
                lock (_locker)
                {
                    if (!PrintCmdQueue.IsEmpty)
                    {
                        CurrentStatus = ZebraPrinter.GetCurrentStatus();
                        UserLabelCount = int.Parse(SGD.GET("odometer.user_label_count", ZebraPrinter.Connection));
                        if (CurrentStatus.isReadyToPrint)
                        {
                            Peeler = SGD.GET("sensor.peeler", ZebraPrinter.Connection);
                            if (Peeler == "clear")
                            {
                                if (PrintCmdQueue.TryDequeue(out string request))
                                {
                                    request = request.Replace("|", "\\&");
                                    ZebraPrinter.SendCommand(request);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
            }
        }

        public void OpenTsc()
        {
            UserLabelCount = 1;
            try
            {
                lock (_locker)
                {
                    if (!PrintCmdQueue.IsEmpty)
                    {
                        if (PrintCmdQueue.TryDequeue(out string request))
                        {
                            request = request.Replace("|", "\\&");
                            if (!request.Equals("^XA~JA^XZ") && !request.Contains("odometer.user_label_count"))
                            {
                                PrintControl?.Cmd?.SendCustom(true, request, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(null, ref ex);
            }
        }

        public void CloseJob()
        {
            Con.Close();
        }

        public void ClearPrintBuffer(bool isTscPrinter)
        {
            while (!PrintCmdQueue.IsEmpty)
            {
                PrintCmdQueue.TryDequeue(out _);
            }
            if (isTscPrinter)
            {
                PrintControl?.Cmd?.ClearBuffer(true);
            }
            else
            {
                PrintCmdQueue.Enqueue("^XA~JA^XZ");
            }
        }

        public void SetOdometorUserLabel(int value)
        {
            PrintCmdQueue.Enqueue($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
        }

        #endregion
    }
}
