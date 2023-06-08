// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net.Sockets;
using WsLocalizationCore.Utils;

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
                $"{(IsMain ? WsLocaleCore.Print.NameMainZebra : WsLocaleCore.Print.NameShippingZebra)} | {Printer.Ip}");
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
                LabelSession.Line.LabelCounter, GetDeviceStatusZebra(), LabelPrintedCount, GetLabelCount()));
        //MdInvokeControl.SetForeColor(FieldPrint, IsConnected.Equals(true) ? Color.Green : Color.Red);
    }

    public string GetDeviceStatusZebra()
    {
        if (ZebraStatus is null) return WsLocaleCore.Print.StatusIsUnavailable;
        if (ZebraStatus.isHeadCold)
            return WsLocaleCore.Print.StatusIsHeadCold;
        if (ZebraStatus.isHeadOpen)
            return WsLocaleCore.Print.StatusIsHeadOpen;
        if (ZebraStatus.isHeadTooHot)
            return WsLocaleCore.Print.StatusIsHeadTooHot;
        if (ZebraStatus.isPaperOut)
            return WsLocaleCore.Print.StatusIsPaperOut;
        if (ZebraStatus.isPartialFormatInProgress)
            return WsLocaleCore.Print.StatusIsPartialFormatInProgress;
        if (ZebraStatus.isPaused)
            return WsLocaleCore.Print.StatusIsPaused;
        if (ZebraStatus.isReadyToPrint)
            return WsLocaleCore.Print.StatusIsReadyToPrint;
        if (ZebraStatus.isReceiveBufferFull)
            return WsLocaleCore.Print.StatusIsReceiveBufferFull;
        if (ZebraStatus.isRibbonOut)
            return WsLocaleCore.Print.StatusIsRibbonOut;
        return WsLocaleCore.Print.StatusIsUnavailable;
    }

    public bool CheckDeviceStatusZebra() => GetDeviceStatusZebra() == WsLocaleCore.Print.StatusIsReadyToPrint;

    public string GetZebraPrintMode()
    {
        if (ZebraStatus is null) return WsLocaleCore.Print.ModeUnknown;
        lock (ZebraStatus)
        {
            if (ZebraStatus.printMode == ZplPrintMode.REWIND)
                return WsLocaleCore.Print.ModeRewind;
            if (ZebraStatus.printMode == ZplPrintMode.PEEL_OFF)
                return WsLocaleCore.Print.ModePeelOff;
            if (ZebraStatus.printMode == ZplPrintMode.TEAR_OFF)
                return WsLocaleCore.Print.ModeTearOff;
            if (ZebraStatus.printMode == ZplPrintMode.CUTTER)
                return WsLocaleCore.Print.ModeCutter;
            if (ZebraStatus.printMode == ZplPrintMode.APPLICATOR)
                return WsLocaleCore.Print.ModeApplicator;
            if (ZebraStatus.printMode == ZplPrintMode.DELAYED_CUT)
                return WsLocaleCore.Print.ModeDelayedCut;
            if (ZebraStatus.printMode == ZplPrintMode.LINERLESS_PEEL)
                return WsLocaleCore.Print.ModeLinerlessPeel;
            if (ZebraStatus.printMode == ZplPrintMode.LINERLESS_REWIND)
                return WsLocaleCore.Print.ModeLinerlessRewind;
            if (ZebraStatus.printMode == ZplPrintMode.PARTIAL_CUTTER)
                return WsLocaleCore.Print.ModePartialCutter;
            if (ZebraStatus.printMode == ZplPrintMode.RFID)
                return WsLocaleCore.Print.ModeRfid;
            if (ZebraStatus.printMode == ZplPrintMode.KIOSK)
                return WsLocaleCore.Print.ModeKiosk;
        }
        return WsLocaleCore.Print.ModeUnknown;
    }

    public void SendCmd(WsSqlPluLabelModel pluLabel)
    {
        if (string.IsNullOrEmpty(pluLabel.Zpl)) return;

        SendCmdToZebra(pluLabel.Zpl);
    }

    private void SendCmdToZebra(string cmd)
    {
        if (ZebraDriver is null || GetDeviceStatusZebra() != WsLocaleCore.Print.StatusIsReadyToPrint)
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
                        WsFormNavigationUtils.CatchExceptionSimple(new($"{WsLocaleCore.Print.SensorPeeler}: {ZebraPeelerStatus}"));
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