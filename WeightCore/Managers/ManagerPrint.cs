// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.Wmi;
using System;
using System.Runtime.CompilerServices;
using WeightCore.Print;
using WeightCore.Print.Tsc;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using ZebraConnectionBuilder = Zebra.Sdk.Comm.ConnectionBuilder;
//using ZebraPrinterStatus = Zebra.Sdk.Printer.PrinterStatus;

namespace WeightCore.Managers
{
    public class ManagerPrint : ManagerBase
    {
        #region Public and private fields and properties

        public string Peeler { get; private set; }
        public PrinterStatus CurrentStatus { get; private set; }
        public Connection ZebraConnection { get; private set; }
        private ZebraPrinter _zebraPrinter;
        public ZebraPrinter ZebraPrinter
        {
            get
            {
                if (ZebraConnection != null && _zebraPrinter == null)
                {
                    _zebraPrinter = ZebraPrinterFactory.GetInstance(ZebraConnection);
                }

                return _zebraPrinter;
            }
        }

        public TscPrintControlHelper TscPrintControl { get; private set; } = TscPrintControlHelper.Instance;
        private WmiHelper Wmi { get; set; } = WmiHelper.Instance;
        public PrintBrand PrintBrand { get; private set; }
        public WmiWin32PrinterEntity Win32Printer() => Wmi.GetWin32Printer(TscPrintControl.PrintName);

        #endregion

        #region Constructor and destructor

        public ManagerPrint() : base()
        {
            Init(CloseMethod, ReleaseManaged, ReleaseUnmanaged);
        }

        #endregion

        #region Public and private methods

        public void Init(PrintBrand printBrand, string name, string ip, int port)
        {
            Init(ProjectsEnums.TaskType.MemoryManager, () =>
            {
                PrintBrand = printBrand;
                if (PrintBrand == PrintBrand.Zebra)
                {
                    //ZebraConnection = new TcpConnection(ip, port);
                    ZebraConnection = ZebraConnectionBuilder.Build(ip);
                    ZebraConnection.Open();
                }
                TscPrintControl.Init(name);
            }, 1_000);
        }

        public void Open(SqlViewModelEntity sqlViewModel)
        {
            try
            {
                Open(sqlViewModel, false,
                () =>
                {
                    //
                },
                null, null);
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        public new void CloseMethod()
        {
            base.CloseMethod();
        }

        public new void ReleaseManaged()
        {
            base.ReleaseManaged();

            if (PrintBrand == PrintBrand.Zebra)
            {
                ZebraConnection?.Close();
            }

            CurrentStatus = null;
            ZebraConnection = null;
            Wmi = null;
        }

        public new void ReleaseUnmanaged()
        {
            base.ReleaseUnmanaged();

            Peeler = null;
        }

        public void SendCmd(string printCmd)
        {
            CheckIsDisposed();
            switch (PrintBrand)
            {
                case PrintBrand.Zebra:
                    SendCmdToZebra(printCmd);
                    break;
                case PrintBrand.TSC:
                    SendCmdToTsc(printCmd);
                    break;
                case PrintBrand.Default:
                default:
                    break;
            }
        }

        public void SetCurrentStatus()
        {
            CurrentStatus = ZebraPrinter.GetCurrentStatus();
        }

        private void SendCmdToZebra(string printCmd,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrEmpty(printCmd) || ZebraPrinter == null)
                return;
            try
            {
                SetCurrentStatus();
                if (CurrentStatus.isReadyToPrint)
                {
                    Peeler = SGD.GET("sensor.peeler", ZebraPrinter.Connection);
                    if (Peeler == "clear")
                    {
                        string docReplace = printCmd.Replace("|", "\\&");
                        ZebraPrinter.SendCommand(docReplace);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false, filePath, lineNumber, memberName);
            }
        }

        private void SendCmdToTsc(string printCmd,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrEmpty(printCmd))
                return;
            try
            {
                string docReplace = printCmd.Replace("|", "\\&");
                if (!docReplace.Equals("^XA~JA^XZ") && !docReplace.Contains("odometer.user_label_count"))
                {
                    TscPrintControl.SendCmd(docReplace);
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false, filePath, lineNumber, memberName);
            }
        }

        public void ClearPrintBuffer(bool isSetOdometer, int odometerValue)
        {
            CheckIsDisposed();

            switch (PrintBrand)
            {
                case PrintBrand.Default:
                    break;
                case PrintBrand.Zebra:
                    SendCmdToZebra("^XA~JA^XZ");
                    break;
                case PrintBrand.TSC:
                    TscPrintControl.ClearBuffer();
                    break;
            }
            if (isSetOdometer)
                SetOdometorUserLabel(odometerValue);
        }

        private void SetOdometorUserLabel(int value)
        {
            CheckIsDisposed();

            switch (PrintBrand)
            {
                case PrintBrand.Default:
                    break;
                case PrintBrand.Zebra:
                    SendCmdToZebra($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
                    break;
                case PrintBrand.TSC:
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
