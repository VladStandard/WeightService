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
using System.Windows.Forms;
using DataCore.Sql.TableScaleModels;
using DataCore.Protocols;
using DataCore.Localizations;
using WeightCore.Helpers;
using System.Net.NetworkInformation;
using WeightCore.Zpl;

namespace WeightCore.Managers
{
    public class ManagerPrint : ManagerBase
    {
        #region Public and private fields and properties

        private WmiHelper Wmi { get; set; } = WmiHelper.Instance;
        private ZebraPrinter _zebraDriver;
        public Connection ZebraConnection { get; private set; }
        public int LabelsCount { get; set; }
        public int Port { get; private set; }
        public Label FieldPrint { get; private set; }
        public PrintBrand PrintBrand { get; private set; }
        public PrinterEntity Printer { get; private set; }
        public string Ip { get; private set; }
        public string ZebraPeelerStatus { get; private set; }
        public TscPrintControlHelper TscDriver { get; private set; } = TscPrintControlHelper.Instance;
        public WmiWin32PrinterEntity Win32Printer => Wmi.GetWin32Printer(TscDriver.PrintName);
        public ZebraPrinter ZebraDriver { get { if (ZebraConnection != null && _zebraDriver == null) _zebraDriver = ZebraPrinterFactory.GetInstance(ZebraConnection); return _zebraDriver; } }
        public ZebraPrinterStatus ZebraStatus { get; private set; }

        #endregion

        #region Constructor and destructor

        public ManagerPrint() : base()
        {
            LabelsCount = 0;
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
                                    $"{(isMain ? LocaleCore.Print.NameMainZebra : LocaleCore.Print.NameShippingZebra)} | {Ip}");
                                break;
                            case PrintBrand.TSC:
                                TscDriver.Init(name);
                                MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
                                    $"{(isMain ? LocaleCore.Print.NameMainTsc : LocaleCore.Print.NameShippingTsc)} | {Ip}");
                                break;
                        }
                        MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPrint, true);
                    },
                    new(waitReopen: 5_000, waitRequest: 2_000, waitResponse: 0_100, waitClose: 1_000, waitException: 2_500));
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
                        Reopen();
                    },
                    () => {
                        Request();
                    },
                    () => {
                        Response(isMain,
                            $"{LocaleCore.Scales.Labels}: {LabelsCount} / " +
                            $"{UserSessionHelper.Instance.WeighingSettings.LabelsCountMain}");
                    }
                    );
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        private void Reopen()
        {
            NetUtils.RequestPing(Printer, 0_500);
        }

        private void Request([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (Printer?.PingStatus == IPStatus.Success)
            {
                switch (PrintBrand)
                {
                    case PrintBrand.Zebra:
                        if (ZebraConnection == null)
                            ZebraConnection = ZebraConnectionBuilder.Build(Ip);
                        if (!ZebraConnection.Connected)
                            ZebraConnection.Open();
                        //if (!ZebraDriver.Connection.Connected)
                        //    ZebraDriver.Connection.Open();
                        if (Printer == null || ZebraDriver == null || ZebraConnection == null || !ZebraConnection.Connected)
                            ZebraStatus = null;
                        else
                        {
                            try
                            {
                                ZebraStatus = ZebraDriver.GetCurrentStatus();
                            }
                            catch (Exception ex)
                            {
                                Exception.Catch(null, ref ex, false, filePath, lineNumber, memberName);
                                SendCmdToZebra(ZplPipeUtils.ZplHostStatusReturn);
                            }
                        }
                        break;
                    case PrintBrand.TSC:
                        break;
                }
            }
        }

        private void Response(bool isMain, string value)
        {
            //if (LabelsCount < 1) LabelsCount = 1;
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
                $"{GetDeviceName(isMain)} | " +
                $"{LocaleCore.Print.Communication} ({Ip}): {Printer.PingStatus} | " +
                $"{LocaleCore.Print.Status}: {GetDeviceStatus()} | {value}");
        }

        public string GetDeviceName(bool isMain)
        {
            return isMain
            ? PrintBrand switch
            {
                PrintBrand.Zebra => LocaleCore.Print.NameMainZebra,
                PrintBrand.TSC => LocaleCore.Print.NameMainTsc,
                _ => LocaleCore.Print.DeviceName,
            }
            : PrintBrand switch
            {
                PrintBrand.Zebra => LocaleCore.Print.NameShippingZebra,
                PrintBrand.TSC => LocaleCore.Print.NameShippingTsc,
                _ => LocaleCore.Print.DeviceNameIsUnavailable,
            };
        }

        public string GetDeviceStatus()
        {
            switch (PrintBrand)
            {
                case PrintBrand.Zebra:
                    if (ZebraStatus == null)
                        return LocaleCore.Print.StatusIsUnavailable;
                    lock (ZebraStatus)
                    {
                        if (ZebraStatus.isHeadCold)
                            return LocaleCore.Print.StatusIsHeadCold;
                        else if (ZebraStatus.isHeadOpen)
                            return LocaleCore.Print.StatusIsHeadOpen;
                        else if (ZebraStatus.isHeadTooHot)
                            return LocaleCore.Print.StatusIsHeadTooHot;
                        else if (ZebraStatus.isPaperOut)
                            return LocaleCore.Print.StatusIsPaperOut;
                        else if (ZebraStatus.isPartialFormatInProgress)
                            return LocaleCore.Print.StatusIsPartialFormatInProgress;
                        else if (ZebraStatus.isPaused)
                            return LocaleCore.Print.StatusIsPaused;
                        else if (ZebraStatus.isReadyToPrint)
                            return LocaleCore.Print.StatusIsReadyToPrint;
                        else if (ZebraStatus.isReceiveBufferFull)
                            return LocaleCore.Print.StatusIsReceiveBufferFull;
                        else if (ZebraStatus.isRibbonOut)
                            return LocaleCore.Print.StatusIsRibbonOut;
                    }
                    break;
                case PrintBrand.TSC:
                    return $"{Win32Printer?.PrinterStatusDescription}";
            }
            return LocaleCore.Print.StatusIsUnavailable;
        }

        public bool CheckDeviceStatus()
        {
            switch (PrintBrand)
            {
                case PrintBrand.Zebra:
                    if (ZebraStatus == null)
                        return false;
                    return GetDeviceStatus() == LocaleCore.Print.StatusIsReadyToPrint;
                case PrintBrand.TSC:
                    //return $"{Win32Printer?.PrinterStatusDescription}";
                    return false;
            }
            return false;
        }

        public string GetZebraPrintMode()
        {
            if (ZebraStatus == null)
                return LocaleCore.Print.ModeUnknown;
            lock (ZebraStatus)
            {
                if (ZebraStatus.printMode == ZplPrintMode.REWIND)
                    return LocaleCore.Print.ModeRewind;
                else if (ZebraStatus.printMode == ZplPrintMode.PEEL_OFF)
                    return LocaleCore.Print.ModePeelOff;
                else if (ZebraStatus.printMode == ZplPrintMode.TEAR_OFF)
                    return LocaleCore.Print.ModeTearOff;
                else if (ZebraStatus.printMode == ZplPrintMode.CUTTER)
                    return LocaleCore.Print.ModeCutter;
                else if (ZebraStatus.printMode == ZplPrintMode.APPLICATOR)
                    return LocaleCore.Print.ModeApplicator;
                else if (ZebraStatus.printMode == ZplPrintMode.DELAYED_CUT)
                    return LocaleCore.Print.ModeDelayedCut;
                else if (ZebraStatus.printMode == ZplPrintMode.LINERLESS_PEEL)
                    return LocaleCore.Print.ModeLinerlessPeel;
                else if (ZebraStatus.printMode == ZplPrintMode.LINERLESS_REWIND)
                    return LocaleCore.Print.ModeLinerlessRewind;
                else if (ZebraStatus.printMode == ZplPrintMode.PARTIAL_CUTTER)
                    return LocaleCore.Print.ModePartialCutter;
                else if (ZebraStatus.printMode == ZplPrintMode.RFID)
                    return LocaleCore.Print.ModeRfid;
                else if (ZebraStatus.printMode == ZplPrintMode.KIOSK)
                    return LocaleCore.Print.ModeKiosk;
            }
            return LocaleCore.Print.ModeUnknown;
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
                            GuiUtils.WpfForm.ShowNewCatch(null, $"{LocaleCore.Print.SensorPeeler}: {ZebraPeelerStatus}",
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

        public void ClearPrintBuffer(int odometerValue = -1)
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
            if (odometerValue >= 0)
                SetOdometorUserLabel(odometerValue);
        }

        public void SetOdometorUserLabel(int value)
        {
            CheckIsDisposed();

            switch (PrintBrand)
            {
                case PrintBrand.Default:
                    break;
                case PrintBrand.Zebra:
                    //SendCmdToZebra($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
                    SendCmdToZebra($@"! U1 setvar ""odometer.user_label_count"" ""{value}""");
                    break;
                case PrintBrand.TSC:
                    break;
                default:
                    break;
            }
        }

        public void GetOdometorUserLabel()
        {
            CheckIsDisposed();

            switch (PrintBrand)
            {
                case PrintBrand.Default:
                    break;
                case PrintBrand.Zebra:
                    //SendCmdToZebra($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
                    SendCmdToZebra($@"! U1 getvar ""odometer.user_label_count""");
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
