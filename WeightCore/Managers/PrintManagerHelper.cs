// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.Helpers;
using WeightCore.Print.Tsc;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace WeightCore.Managers
{
    public class PrintManagerHelper : ManagerEntity
    {
        #region Design pattern "Lazy Singleton"

        private static PrintManagerHelper _instance;
        public static PrintManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        public string Peeler { get; private set; }
        public int UserLabelCount { get; private set; }
        public PrinterStatus CurrentStatus { get; private set; }
        public Connection Con { get; private set; }
        public BlockingCollection<string> Documents { get; private set; } = new();
        private ZebraPrinter _zebraPrinter;
        public ZebraPrinter ZebraPrinter => _zebraPrinter ??= ZebraPrinterFactory.GetInstance(Con);
        public TscPrintControlHelper TscPrintControl = TscPrintControlHelper.Instance;

        #endregion

        #region Constructor and destructor

        //public new void Init(int waitReopen = 1_000, int waitResponse = 500, int waitRequest = 250, int waitClose = 2_000, int waitException = 1_000)
        //{
        //    if (IsInit)
        //        return;
        //    IsInit = true;
        //    Init(waitReopen, waitResponse, waitRequest, waitClose, waitException);
        //}

        public void Init(Connection connection, string name, string ip, int port,
            int waitReopen = 1_000, int waitResponse = 500, int waitRequest = 250, int waitClose = 2_000, int waitException = 1_000)
        {
            if (IsInit)
                return;
            IsInit = true;
            Init(waitReopen, waitResponse, waitRequest, waitClose, waitException);

            Con = connection;
            TscPrintControl.Init(name, ip, port);
        }

        public void Init(string name, string ip, int port,
            int waitReopen = 1_000, int waitResponse = 500, int waitRequest = 250, int waitClose = 2_000, int waitException = 1_000)
        {
            if (IsInit)
                return;
            IsInit = true;
            Init(waitReopen, waitResponse, waitRequest, waitClose, waitException);

            Con = new TcpConnection(ip, port);
            TscPrintControl.Init(name, ip, port);
        }

        #endregion

        #region Public and private methods - Manager

        public void Open(bool isTscPrinter, TscPrintControlHelper.Callback callbackPrintManagerClose)
        {
            lock (Locker)
            {
                Con?.Open();
                if (isTscPrinter)
                    OpenTsc(callbackPrintManagerClose);
                else
                    OpenZebra();
            }
        }

        public void Close()
        {
            lock (Locker)
            {
                Con?.Close();
            }
        }

        #endregion

        #region Public and private methods

        public async void SendAsync(string printCmd)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Documents.Add(printCmd);
        }

        public void OpenZebra()
        {
            try
            {
                //lock (_locker)
                {
                    if (Documents.Count > 0)
                    {
                        CurrentStatus = ZebraPrinter.GetCurrentStatus();
                        UserLabelCount = int.Parse(SGD.GET("odometer.user_label_count", ZebraPrinter.Connection));
                        if (CurrentStatus.isReadyToPrint)
                        {
                            Peeler = SGD.GET("sensor.peeler", ZebraPrinter.Connection);
                            if (Peeler == "clear")
                            {
                                foreach (string doc in Documents.GetConsumingEnumerable())
                                {
                                    //if (Documents.TryDequeue(out string doc))
                                    string docReplace = doc.Replace("|", "\\&");
                                    ZebraPrinter.SendCommand(docReplace);
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

        public void OpenTsc(TscPrintControlHelper.Callback callbackPrintManagerClose)
        {
            UserLabelCount = 1;
            try
            {
                //lock (_locker)
                {
                    if (Documents.Count > 0)
                    {
                        foreach (string doc in Documents.GetConsumingEnumerable())
                        {
                            //if (Documents.TryDequeue(out string request))
                            string docReplace = doc.Replace("|", "\\&");
                            if (!docReplace.Equals("^XA~JA^XZ") && !docReplace.Contains("odometer.user_label_count"))
                            {
                                TscPrintControl.CmdSendCustom(docReplace, callbackPrintManagerClose);
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

        public void ClearPrintBuffer(bool isTscPrinter)
        {
            if (Documents.Count > 0)
                Documents = new BlockingCollection<string>();
            if (isTscPrinter)
            {
                TscPrintControl.CmdClearBuffer();
            }
            else
            {
                Documents.Add("^XA~JA^XZ");
            }
        }

        public void SetOdometorUserLabel(int value)
        {
            Documents.Add($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
        }

        #endregion
    }
}
