// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net.Sockets;

namespace WsLabelCore.Helpers;

/// <summary>
/// Плагин принтера ZEBRA.
/// </summary>
#nullable enable
public sealed class WsPluginPrintZebraModel : WsPluginPrintModel
{
    #region Public and private fields and properties

    private Connection? ZebraConnection { get; set; }
    private string ZebraPeelerStatus { get; set; }
    private ZebraPrinter? _zebraDriver;
    private ZebraPrinter? ZebraDriver
    {
        get
        {
            if (ZebraConnection is not null && _zebraDriver is null)
                _zebraDriver = ZebraPrinterFactory.GetInstance(ZebraConnection);
            return _zebraDriver;
        }
    }
    private ZebraPrinterStatus? ZebraStatus { get; set; }
    public bool IsConnected => ZebraStatus is not null && ZebraStatus.isReadyToPrint;

    #endregion

    #region Constructor and destructor

    public WsPluginPrintZebraModel()
    {
        ZebraPeelerStatus = string.Empty;
    }

    #endregion

    #region Public and private methods

    public void InitZebra(WsPluginConfigModel configReopen, WsPluginConfigModel configRequest, WsPluginConfigModel configResponse,
        MdPrinterModel printer, Label fieldPrint, Label fieldPrintExt, bool isMain)
    {
        Init();
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        try
        {
            PrintModel = WsEnumPrintModel.Zebra;
            Printer = printer;
            FieldPrint = fieldPrint;
            FieldPrintExt = fieldPrintExt;
            IsMain = isMain;
            PrintName = printer.Name;
            MdInvokeControl.SetText(FieldPrintExt, $"{ReopenCounter} | {RequestCounter} | {ResponseCounter}");
            MdInvokeControl.SetText(FieldPrint,
                $"{(IsMain ? LocaleCore.Print.NameMainZebra : LocaleCore.Print.NameShippingZebra)} | {Printer.Ip}");
        }
        catch (Exception ex)
        {
            WsFormNavigationUtils.CatchExceptionSimple(ex);
        }
    }

    public override void Execute()
    {
        base.Execute();
        //ReopenItem.Execute(ReopenZebra);
        RequestItem.Execute(RequestZebra);
        ResponseItem.Execute(ResponseZebra);
    }

    public void ReopenZebra()
    {
        try
        {
            //if (!IsConnected)
            //    WsTcpClient.ConnectWithRetries(ReopenItem.Config.WaitExecute);
        }
        catch (Exception ex)
        {
            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, PluginType.ToString());
        }
    }

    private void RequestZebra()
    {
        // Метка.
        MdInvokeControl.SetText(FieldPrintExt, $"{ReopenCounter} | {RequestCounter} | {ResponseCounter}");

        ZebraConnection ??= ZebraConnectionBuilder.Build(Printer.Ip);
        if (!ZebraConnection.Connected)
            ZebraConnection.Open();
        if (ZebraDriver is null || ZebraConnection is null || !ZebraConnection.Connected)
            ZebraStatus = null;
        else
        {
            try
            {
                ZebraStatus = ZebraDriver.GetCurrentStatus();
            }
            catch (Exception ex)
            {
                WsFormNavigationUtils.CatchExceptionSimple(ex);
                SendCmdToZebra(ZplUtils.ZplHostStatusReturn);
            }
        }
    }

    private void ResponseZebra()
    {
        MdInvokeControl.SetText(FieldPrint,
            LabelSession.WeighingSettings.GetPrintDescription(IsMain, PrintModel, Printer, IsConnected,
                LabelSession.Line.Counter, GetDeviceStatusZebra(), LabelPrintedCount, GetLabelCount()));
        //MdInvokeControl.SetForeColor(FieldPrint, IsConnected.Equals(true) ? Color.Green : Color.Red);
    }

    public string GetDeviceStatusZebra()
    {
        if (ZebraStatus is null) return LocaleCore.Print.StatusIsUnavailable;
        if (ZebraStatus.isHeadCold)
            return LocaleCore.Print.StatusIsHeadCold;
        if (ZebraStatus.isHeadOpen)
            return LocaleCore.Print.StatusIsHeadOpen;
        if (ZebraStatus.isHeadTooHot)
            return LocaleCore.Print.StatusIsHeadTooHot;
        if (ZebraStatus.isPaperOut)
            return LocaleCore.Print.StatusIsPaperOut;
        if (ZebraStatus.isPartialFormatInProgress)
            return LocaleCore.Print.StatusIsPartialFormatInProgress;
        if (ZebraStatus.isPaused)
            return LocaleCore.Print.StatusIsPaused;
        if (ZebraStatus.isReadyToPrint)
            return LocaleCore.Print.StatusIsReadyToPrint;
        if (ZebraStatus.isReceiveBufferFull)
            return LocaleCore.Print.StatusIsReceiveBufferFull;
        if (ZebraStatus.isRibbonOut)
            return LocaleCore.Print.StatusIsRibbonOut;
        return LocaleCore.Print.StatusIsUnavailable;
    }

    public bool CheckDeviceStatusZebra() => GetDeviceStatusZebra() == LocaleCore.Print.StatusIsReadyToPrint;

    public string GetZebraPrintMode()
    {
        if (ZebraStatus is null) return LocaleCore.Print.ModeUnknown;
        lock (ZebraStatus)
        {
            if (ZebraStatus.printMode == ZplPrintMode.REWIND)
                return LocaleCore.Print.ModeRewind;
            if (ZebraStatus.printMode == ZplPrintMode.PEEL_OFF)
                return LocaleCore.Print.ModePeelOff;
            if (ZebraStatus.printMode == ZplPrintMode.TEAR_OFF)
                return LocaleCore.Print.ModeTearOff;
            if (ZebraStatus.printMode == ZplPrintMode.CUTTER)
                return LocaleCore.Print.ModeCutter;
            if (ZebraStatus.printMode == ZplPrintMode.APPLICATOR)
                return LocaleCore.Print.ModeApplicator;
            if (ZebraStatus.printMode == ZplPrintMode.DELAYED_CUT)
                return LocaleCore.Print.ModeDelayedCut;
            if (ZebraStatus.printMode == ZplPrintMode.LINERLESS_PEEL)
                return LocaleCore.Print.ModeLinerlessPeel;
            if (ZebraStatus.printMode == ZplPrintMode.LINERLESS_REWIND)
                return LocaleCore.Print.ModeLinerlessRewind;
            if (ZebraStatus.printMode == ZplPrintMode.PARTIAL_CUTTER)
                return LocaleCore.Print.ModePartialCutter;
            if (ZebraStatus.printMode == ZplPrintMode.RFID)
                return LocaleCore.Print.ModeRfid;
            if (ZebraStatus.printMode == ZplPrintMode.KIOSK)
                return LocaleCore.Print.ModeKiosk;
        }
        return LocaleCore.Print.ModeUnknown;
    }

    public void SendCmd(WsSqlPluLabelModel pluLabel)
    {
        if (string.IsNullOrEmpty(pluLabel.Zpl)) return;

        SendCmdToZebra(pluLabel.Zpl);
    }

    private void SendCmdToZebra(string cmd)
    {
        if (ZebraDriver is null || GetDeviceStatusZebra() != LocaleCore.Print.StatusIsReadyToPrint)
            return;
        try
        {
            if (ZebraStatus is null) return;
            lock (ZebraStatus)
            {
                if (IsConnected)
                {
                    ZebraPeelerStatus = SGD.GET("sensor.peeler", ZebraDriver.Connection);
                    if (ZebraPeelerStatus == "clear")
                    {
                        ZebraDriver.SendCommand(cmd.Replace("|", "\\&"));
                    }
                    else
                    {
                        WsFormNavigationUtils.CatchExceptionSimple(new($"{LocaleCore.Print.SensorPeeler}: {ZebraPeelerStatus}"));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            WsFormNavigationUtils.CatchExceptionSimple(ex);
        }
    }

    public void ClearPrintBuffer(int odometerValue = -1)
    {
        SendCmdToZebra("^XA~JA^XZ");
        if (odometerValue >= 0) SetOdometorUserLabel(odometerValue);
    }

    public void SetOdometorUserLabel(int value) => SendCmdToZebra($@"! U1 setvar ""odometer.user_label_count"" ""{value}""");

    public void GetOdometorUserLabel() => SendCmdToZebra(@"! U1 getvar ""odometer.user_label_count""");

    public override void Close()
    {
        base.Close();
        ZebraConnection?.Close();
    }

    #endregion
}