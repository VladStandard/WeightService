// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.Wmi;
using System;
using System.Collections.Concurrent;
using WeightCore.Print;
using WeightCore.Print.Tsc;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace WeightCore.Managers
{
    [Obsolete(@"Use ManagerPrintV2")]
    public class ManagerPrint : ManagerBase
    {
        #region Public and private fields and properties

        public string Peeler { get; private set; }
        public int UserLabelCount { get; private set; }
        public PrinterStatus CurrentStatus { get; private set; }
        public Connection ZebraConnection { get; private set; }
        public BlockingCollection<string> Documents { get; private set; } = new();
        private ZebraPrinter _zebraPrinter;
        public ZebraPrinter ZebraPrinter
        {
            get
            {
                if (ZebraConnection != null && _zebraPrinter == null)
                    _zebraPrinter = ZebraPrinterFactory.GetInstance(ZebraConnection);
                return _zebraPrinter;
            }
        }

        public TscPrintControlHelper TscPrintControl { get; private set; } = TscPrintControlHelper.Instance;
        private WmiHelper Wmi { get; set; } = WmiHelper.Instance;
        public PrintBrand PrintBrand { get; private set; }
        public WmiWin32PrinterEntity Win32Printer() => Wmi?.GetWin32Printer(TscPrintControl.PrintName);

        #endregion

        #region Constructor and destructor

        [Obsolete(@"Use ManagerPrintV2")]
        public ManagerPrint() : base()
        {
            Init(CloseMethod, ReleaseManaged, ReleaseUnmanaged);
        }

        #endregion

        #region Public and private methods

        public void Init(PrintBrand printBrand, string name, string ip, int port)
        {
            Init(ProjectsEnums.TaskType.MemoryManager,
            () => {
                PrintBrand = printBrand;
                switch (PrintBrand)
                {
                    case PrintBrand.Default:
                        break;
                    case PrintBrand.Zebra:
                        ZebraConnection = new TcpConnection(ip, port);
                        break;
                    case PrintBrand.TSC:
                        break;
                    default:
                        break;
                }
                //TscPrintControl.Init(name, ip, port);
                TscPrintControl.Init(name);
            }, 1_000);
        }

        public void Open(SqlViewModelEntity sqlViewModel)
        {
            Open(sqlViewModel,
            () => {
                switch (PrintBrand)
                {
                    case PrintBrand.Default:
                        break;
                    case PrintBrand.Zebra:
                        ZebraConnection?.Open();
                        OpenZebra();
                        break;
                    case PrintBrand.TSC:
                        OpenTsc();
                        break;
                    default:
                        break;
                }
            },
            null, null);
        }

        public new void CloseMethod()
        {
            base.CloseMethod();
        }

        public new void ReleaseManaged()
        {
            base.ReleaseManaged();

            switch (PrintBrand)
            {
                case PrintBrand.Default:
                    break;
                case PrintBrand.Zebra:
                    ZebraConnection?.Close();
                    break;
                case PrintBrand.TSC:
                    break;
                default:
                    break;
            }
            
            CurrentStatus = null;
            ZebraConnection = null;
            Documents?.Dispose();
            Documents = null;
            Wmi = null;
        }

        public new void ReleaseUnmanaged()
        {
            base.ReleaseUnmanaged();

            Peeler = null;
        }

        public void Send(string printCmd)
        {
            CheckIsDisposed();
            Documents.Add(printCmd);
        }

        public void OpenZebra()
        {
            try
            {
                if (Documents?.Count > 0)
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
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        public void OpenTsc()
        {
            UserLabelCount = 1;
            try
            {
                if (Documents.Count > 0)
                {
                    foreach (string doc in Documents.GetConsumingEnumerable())
                    {
                        //if (Documents.TryDequeue(out string request))
                        string docReplace = doc.Replace("|", "\\&");
                        if (!docReplace.Equals("^XA~JA^XZ") && !docReplace.Contains("odometer.user_label_count"))
                        {
                            TscPrintControl.SendCmd(docReplace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        public void ClearPrintBuffer(PrintBrand printBrand)
        {
            CheckIsDisposed();

            Documents = new BlockingCollection<string>();
            switch (printBrand)
            {
                case PrintBrand.Default:
                    break;
                case PrintBrand.Zebra:
                    Documents.Add("^XA~JA^XZ");
                    break;
                case PrintBrand.TSC:
                    //TscPrintControl.CmdClearBuffer();
                    break;
                default:
                    break;
            }
        }

        public void SetOdometorUserLabel(int value)
        {
            CheckIsDisposed();

            Documents.Add($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
        }

        #endregion
    }
}
