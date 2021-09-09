﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Utils;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Print.Tsc;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace WeightCore.Print
{
    public class PrintManagerEntity
    {
        #region Public and private fields and properties - Manager

        public int WaitWhileMiliSeconds { get; }
        public int WaitExceptionMiliSeconds { get; }
        public int WaitCloseMiliSeconds { get; }
        public string ExceptionMsg { get; private set; }
        public delegate Task CallbackAsync(int wait);
        public bool IsExecute { get; set; }

        #endregion

        #region Public and private fields and properties

        public string Peeler { get; private set; }
        public int UserLabelCount { get; private set; }
        public PrinterStatus CurrentStatus { get; private set; }
        //public delegate void OnHandler(PrintManagerEntity state);
        //public event OnHandler Notify;
        private readonly ILog _ilog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Connection Con { get; }
        public ConcurrentQueue<string> PrintCmdQueue { get; } = new ConcurrentQueue<string>();
        private readonly object _locker = new();
        public PrintControlEntity PrintControl { get; set; }
        private readonly LogUtils _logUtils = LogUtils.Instance;
        private ZebraPrinter _zebraPrinter;
        public ZebraPrinter ZebraPrinter => _zebraPrinter ?? (_zebraPrinter = ZebraPrinterFactory.GetInstance(Con));

        #endregion

        #region Constructor and destructor

        public PrintManagerEntity(int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds)
        {
            // Manager.
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

        public void Open(bool isTscPrinter, CallbackAsync callbackPrintManagerAsync, [CallerFilePath] string filePath = "", 
            [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IsExecute = true;
            while (IsExecute)
            {
                try
                {
                    OpenJob(isTscPrinter);
                    callbackPrintManagerAsync(WaitWhileMiliSeconds).ConfigureAwait(true);
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
                }
                catch (TaskCanceledException)
                {
                    // Console.WriteLine(tcex.Message);
                    // Not the problem.
                }
                catch (Exception ex)
                {
                    ExceptionMsg = ex.Message;
                    if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                        ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
                    _logUtils.Error(ExceptionMsg, filePath, memberName, lineNumber);
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
                    throw;
                }
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void Close([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                IsExecute = false;
                //Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
                CloseJob();
            }
            catch (Exception ex)
            {
                ExceptionMsg = ex.Message;
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
                _logUtils.Error(ExceptionMsg, filePath, memberName, lineNumber);
                Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
                throw;
            }
        }

        #endregion

        #region Public and private methods

        /// <summary>
        /// Отправить задание в очередь печати.
        /// </summary>
        /// <param name="printCmd"></param>
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
            lock (_locker)
            {
                try
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
                                    //Console.WriteLine(request);
                                    //MessageBox.Show(request);
                                    ZebraPrinter.SendCommand(request);
                                }
                            }
                        }
                        //Notify?.Invoke(this);
                    }
                }
                catch (ConnectionException e)
                {
                    _ilog.Error(e.ToString());
                }
                catch (ZebraPrinterLanguageUnknownException e)
                {
                    _ilog.Error(e.ToString());
                }
                catch (Exception e)
                {
                    _ilog.Error(e.ToString());
                }
            }
        }

        public void OpenTsc()
        {
            UserLabelCount = 1;
            lock (_locker)
            {
                try
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
                        //Notify?.Invoke(this);
                    }
                }
                catch (ConnectionException e)
                {
                    _ilog.Error(e.ToString());
                }
                catch (ZebraPrinterLanguageUnknownException e)
                {
                    _ilog.Error(e.ToString());
                }
                catch (Exception e)
                {
                    _ilog.Error(e.ToString());
                }
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
