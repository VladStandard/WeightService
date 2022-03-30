// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.Wmi;
using System;
using WeightCore.Print.Tsc;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace WeightCore.Managers
{
    public class ManagerPrintV2 : ManagerBase
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
                    _zebraPrinter = ZebraPrinterFactory.GetInstance(ZebraConnection);
                return _zebraPrinter;
            }
        }

        public TscPrintControlHelper TscPrintControl { get; private set; } = TscPrintControlHelper.Instance;
        private WmiHelper Wmi { get; set; } = WmiHelper.Instance;
        public bool IsTscPrinter { get; private set; }
        public WmiWin32PrinterEntity Win32Printer() => Wmi.GetWin32Printer(TscPrintControl.PrintName);

        #endregion

        #region Constructor and destructor

        public ManagerPrintV2() : base()
        {
            Init(CloseMethod, ReleaseManaged, ReleaseUnmanaged);
        }

        #endregion

        #region Public and private methods

        public void Init(bool isTscPrinter, string name, string ip, int port)
        {
            Init(ProjectsEnums.TaskType.MemoryManager, () =>
            {
                IsTscPrinter = isTscPrinter;
                if (!IsTscPrinter)
                {
                    ZebraConnection = new TcpConnection(ip, port);
                }
                TscPrintControl.Init(name);
            }, 1_000);
        }

        public void Open(SqlViewModelEntity sqlViewModel)
        {
            Open(sqlViewModel, () =>
            {
                //
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

            if (!IsTscPrinter)
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
            if (IsTscPrinter)
                SendCmdToTsc(printCmd);
            else
                SendCmdToZebra(printCmd);
        }

        public void SendCmdToZebra(string printCmd)
        {
            if (string.IsNullOrEmpty(printCmd) || ZebraPrinter == null)
                return;
            try
            {
                CurrentStatus = ZebraPrinter.GetCurrentStatus();
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
                Exception.Catch(null, ref ex, false);
            }
        }

        public void SendCmdToTsc(string printCmd)
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
                Exception.Catch(null, ref ex, false);
            }
        }

        public void ClearPrintBuffer(bool isTscPrinter)
        {
            CheckIsDisposed();

            if (isTscPrinter)
                TscPrintControl.ClearBuffer();
            else
                SendCmdToZebra("^XA~JA^XZ");
        }

        public void SetOdometorUserLabel(int value)
        {
            CheckIsDisposed();

            if (!IsTscPrinter)
                SendCmdToZebra($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
        }

        #endregion
    }
}
