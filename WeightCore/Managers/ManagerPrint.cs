// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Wmi;
using System;
using System.Runtime.CompilerServices;
using WeightCore.Gui;
using WeightCore.Print;
using WeightCore.Print.Tsc;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using ZebraConnectionBuilder = Zebra.Sdk.Comm.ConnectionBuilder;
//using ZebraPrinterStatus = Zebra.Sdk.Printer.PrinterStatus;
using LocalizationCore = DataCore.Localization.Core;

namespace WeightCore.Managers
{
    public class ManagerPrint : ManagerBase
    {
        #region Public and private fields and properties

        public string SensorPeelerStatus { get; private set; }
        public PrinterStatus CurrentStatus { get => CurrentPrinter.GetCurrentStatus(); }
        public Connection CurrentConnection { get; private set; }
        private ZebraPrinter _currentPrinter;
        public ZebraPrinter CurrentPrinter
        {
            get
            {
                if (CurrentConnection != null && _currentPrinter == null)
                {
                    _currentPrinter = ZebraPrinterFactory.GetInstance(CurrentConnection);
                }

                return _currentPrinter;
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
                    CurrentConnection = ZebraConnectionBuilder.Build(ip);
                    CurrentConnection.Open();
                }
                TscPrintControl.Init(name);
            }, 1_000);
        }

        public new void Open()
        {
            try
            {
                // Fix here. 2022-04-15.
                //Open(sqlViewModel, false,
                //() =>
                //{
                //    //
                //},
                //null, null);
                Open(null, null, null);
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
                CurrentConnection?.Close();
            }

            //CurrentStatus = null;
            CurrentConnection = null;
            Wmi = null;
        }

        public new void ReleaseUnmanaged()
        {
            base.ReleaseUnmanaged();

            SensorPeelerStatus = null;
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

        //public void SetCurrentStatus()
        //{
        //    CurrentStatus = CurrentPrinter.GetCurrentStatus();
        //}

        private void SendCmdToZebra(string printCmd,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrEmpty(printCmd) || CurrentPrinter == null)
                return;
            try
            {
                //SetCurrentStatus();
                if (CurrentStatus.isReadyToPrint)
                {
                    SensorPeelerStatus = SGD.GET("sensor.peeler", CurrentPrinter.Connection);
                    if (SensorPeelerStatus == "clear")
                    {
                        string docReplace = printCmd.Replace("|", "\\&");
                        CurrentPrinter.SendCommand(docReplace);
                    }
                    else {
                        GuiUtils.WpfForm.ShowNewCatch(null, $"{LocalizationCore.Print.SensorPeeler}: {SensorPeelerStatus}",
                            filePath, lineNumber, memberName);
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
