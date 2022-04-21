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
using ZebraPrinterStatus = Zebra.Sdk.Printer.PrinterStatus;
using LocalizationCore = DataCore.Localizations.LocaleCore;
using System.Windows.Forms;
using WeightCore.Helpers;
using DataCore.DAL.TableScaleModels;

namespace WeightCore.Managers
{
    public class ManagerPrint : ManagerBase
    {
        #region Public and private fields and properties

        private WmiHelper Wmi { get; set; } = WmiHelper.Instance;
        private ZebraPrinter _zebraDriver;
        public Connection ZebraConnection { get; private set; }
        public int CurrentLabels { get; set; }
        public int Port { get; private set; }
        public Label FieldPrint { get; private set; }
        public PrintBrand PrintBrand { get; private set; }
        public PrinterEntity Printer { get; private set; }
        public string Ip { get; private set; }
        public string ZebraPeelerStatus { get; private set; }
        public TscPrintControlHelper TscDriver { get; private set; } = TscPrintControlHelper.Instance;
        public WmiWin32PrinterEntity Win32Printer() => Wmi.GetWin32Printer(TscDriver.PrintName);
        public ZebraPrinter ZebraDriver { get { if (ZebraConnection != null && _zebraDriver == null) _zebraDriver = ZebraPrinterFactory.GetInstance(ZebraConnection); return _zebraDriver; } }
        public ZebraPrinterStatus ZebraStatus { get; private set; }

        #endregion

        #region Constructor and destructor

        public ManagerPrint() : base()
        {
            CurrentLabels = 0;
            Init(Close, ReleaseManaged, ReleaseUnmanaged);
        }

        #endregion

        #region Public and private methods

        public void Init(PrintBrand printBrand, PrinterEntity printer,
            string name, string ip, int port, Label fieldPrint, bool isMain)
        {
            try
            {
                Init(ProjectsEnums.TaskType.MemoryManager,
                    () =>
                    {
                        PrintBrand = printBrand;
                        Printer = printer;
                        switch (PrintBrand)
                        {
                            case PrintBrand.Zebra:
                                Ip = ip;
                                Port = port;

                                FieldPrint = fieldPrint;
                                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
                                    $"{(isMain ? LocalizationCore.Print.NameMainZebra : LocalizationCore.Print.NameShippingZebra)} | {Ip}");
                                break;
                            case PrintBrand.TSC:
                                TscDriver.Init(name);
                                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
                                    $"{(isMain ? LocalizationCore.Print.NameMainTsc : LocalizationCore.Print.NameShippingTsc)} | {Ip}");
                                break;
                        }
                        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPrint, true);
                    },
                    new(waitReopen: 2_000, waitRequest: 2_000, waitResponse: 0_100, waitClose: 1_000, waitException: 5_000));
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        public void Open(bool isMain)
        {
            try
            {
                Open(
                    () => {
                        OpenInside();
                    },
                    () => {
                        Request();
                    },
                    () => {
                        Response(isMain,
                            $"{LocalizationCore.Scales.Labels}: {CurrentLabels} / " +
                            $"{SessionStateHelper.Instance.WeighingSettings.CurrentLabelsCountMain}");
                    }
                    );
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        private void OpenInside()
        {
            if (Printer != null || ZebraConnection == null || ZebraConnection.Connected == false)
            {
                Printer.SetHttpStatus(1_000);
                if (Printer.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    ZebraConnection = ZebraConnectionBuilder.Build($"{Ip}");
                    ZebraConnection.Open();
                }
            }
        }

        private void Request()
        {
            switch (PrintBrand)
            {
                case PrintBrand.Zebra:
                    if (ZebraConnection?.Connected == true)
                        ZebraStatus = ZebraDriver?.GetCurrentStatus();
                    break;
                case PrintBrand.TSC:
                    break;
            }
        }

        private void Response(bool isMain, string value)
        {
            //LabelsCurrent = UserLabelCount < LabelsCount ? UserLabelCount : LabelsCount;
            if (CurrentLabels < 1) CurrentLabels = 1;
            switch (PrintBrand)
            {
                case PrintBrand.Zebra:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
                        $"{(isMain ? LocalizationCore.Print.NameMainZebra : LocalizationCore.Print.NameShippingZebra)} | {Ip} | " +
                        $"{GetZebraStatus()} | {value}"
                    );
                    break;
                case PrintBrand.TSC:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
                        $"{(isMain ? LocalizationCore.Print.NameMainTsc : LocalizationCore.Print.NameShippingTsc)} | {Ip} | " +
                        $"{Win32Printer()?.PrinterStatusDescription} ");
                    break;
            }
        }

        public string GetZebraStatus()
        {
            if (ZebraStatus == null)
                return LocalizationCore.Print.StatusUnavailable;
            lock (ZebraStatus)
            {
                if (ZebraStatus.isHeadCold)
                    return LocalizationCore.Print.StatusIsHeadCold;
                else if (ZebraStatus.isHeadOpen)
                    return LocalizationCore.Print.StatusIsHeadOpen;
                else if (ZebraStatus.isHeadTooHot)
                    return LocalizationCore.Print.StatusIsHeadTooHot;
                else if (ZebraStatus.isPaperOut)
                    return LocalizationCore.Print.StatusIsPaperOut;
                else if (ZebraStatus.isPartialFormatInProgress)
                    return LocalizationCore.Print.StatusIsPartialFormatInProgress;
                else if (ZebraStatus.isPaused)
                    return LocalizationCore.Print.StatusIsPaused;
                else if (ZebraStatus.isReadyToPrint)
                    return LocalizationCore.Print.StatusIsReadyToPrint;
                else if (ZebraStatus.isReceiveBufferFull)
                    return LocalizationCore.Print.StatusIsReceiveBufferFull;
                else if (ZebraStatus.isRibbonOut)
                    return LocalizationCore.Print.StatusIsRibbonOut;
            }
            return LocalizationCore.Print.StatusUnavailable;
        }

        public string GetZebraPrintMode()
        {
            if (ZebraStatus == null)
                return LocalizationCore.Print.ModeUnknown;
            lock (ZebraStatus)
            {
                if (ZebraStatus.printMode == ZplPrintMode.REWIND)
                    return LocalizationCore.Print.ModeRewind;
                else if (ZebraStatus.printMode == ZplPrintMode.PEEL_OFF)
                    return LocalizationCore.Print.ModePeelOff;
                else if (ZebraStatus.printMode == ZplPrintMode.TEAR_OFF)
                    return LocalizationCore.Print.ModeTearOff;
                else if (ZebraStatus.printMode == ZplPrintMode.CUTTER)
                    return LocalizationCore.Print.ModeCutter;
                else if (ZebraStatus.printMode == ZplPrintMode.APPLICATOR)
                    return LocalizationCore.Print.ModeApplicator;
                else if (ZebraStatus.printMode == ZplPrintMode.DELAYED_CUT)
                    return LocalizationCore.Print.ModeDelayedCut;
                else if (ZebraStatus.printMode == ZplPrintMode.LINERLESS_PEEL)
                    return LocalizationCore.Print.ModeLinerlessPeel;
                else if (ZebraStatus.printMode == ZplPrintMode.LINERLESS_REWIND)
                    return LocalizationCore.Print.ModeLinerlessRewind;
                else if (ZebraStatus.printMode == ZplPrintMode.PARTIAL_CUTTER)
                    return LocalizationCore.Print.ModePartialCutter;
                else if (ZebraStatus.printMode == ZplPrintMode.RFID)
                    return LocalizationCore.Print.ModeRfid;
                else if (ZebraStatus.printMode == ZplPrintMode.KIOSK)
                    return LocalizationCore.Print.ModeKiosk;
            }
            return LocalizationCore.Print.ModeUnknown;
        }

        public new void Close()
        {
            base.Close();
        }

        public new void ReleaseManaged()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPrint, false);

            if (PrintBrand == PrintBrand.Zebra)
            {
                ZebraConnection?.Close();
            }
            ZebraConnection = null;
            Wmi = null;
            
            base.ReleaseManaged();
        }

        public new void ReleaseUnmanaged()
        {
            ZebraPeelerStatus = string.Empty;
            
            base.ReleaseUnmanaged();
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

        private void SendCmdToZebra(string printCmd,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrEmpty(printCmd) || ZebraDriver == null)
                return;
            try
            {
                if (ZebraStatus == null) return;
                lock (ZebraStatus)
                {
                    if (ZebraStatus.isReadyToPrint)
                    {
                        ZebraPeelerStatus = SGD.GET("sensor.peeler", ZebraDriver.Connection);
                        if (ZebraPeelerStatus == "clear")
                        {
                            ZebraDriver.SendCommand(printCmd.Replace("|", "\\&"));
                        }
                        else
                        {
                            GuiUtils.WpfForm.ShowNewCatch(null, $"{LocalizationCore.Print.SensorPeeler}: {ZebraPeelerStatus}",
                                true, filePath, lineNumber, memberName);
                        }
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
                    TscDriver.SendCmd(docReplace);
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
                    TscDriver.ClearBuffer();
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
