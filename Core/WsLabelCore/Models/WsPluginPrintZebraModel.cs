namespace WsLabelCore.Models;

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
        MdPrinterModel printer, Label fieldPrint)
    {
        Init();
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        PrintModel = WsEnumPrintModel.Zebra;
        Printer = printer;
        FieldPrint = fieldPrint;
        PrintName = printer.Name;
    }

    public override void Execute()
    {
        base.Execute();
        //ReopenItem.Execute(ReopenZebra);
        RequestItem.Execute(RequestZebra);
        //ResponseItem.Execute(ResponseZebra);
    }

    public void ReopenZebra()
    {
        try
        {
            ZebraConnection ??= ZebraConnectionBuilder.Build($"TCP_MULTI:{Printer.Ip}");
            if (!ZebraConnection.Connected)
            {
                ZebraConnection.Open();
            }
            if (ZebraDriver is null || ZebraConnection is null || !ZebraConnection.Connected)
                ZebraStatus = null;
            else
            {
                ZebraStatus = ZebraDriver.GetCurrentStatus();
            }
        }
        catch (Exception ex)
        {
            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, WsLocaleCore.LabelPrint.PluginPrintZebra);
            //SendCmdToZebra(ZplUtils.ZplHostStatusReturn);
        }
    }

    private void RequestZebra()
    {
        MdInvokeControl.SetText(FieldPrint,
            LabelSession.WeighingSettings.GetPrintDescription(Printer, IsConnected,
                LabelSession.Line.LabelCounter, LabelPrintedCount, LabelCount));
        MdInvokeControl.SetForeColor(FieldPrint, IsConnected.Equals(true) ? Color.Green : Color.Red);
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

    public void SendCmdToZebra(string cmd)
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
            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, WsLocaleCore.LabelPrint.PluginPrintZebra);
        }
    }

    public void ClearPrintBuffer(int odometerValue = -1)
    {
        SendCmdToZebra("^XA~JA^XZ");
        if (odometerValue >= 0) SetOdometorUserLabel(odometerValue);
    }

    public void SetOdometorUserLabel(int value) => SendCmdToZebra($@"! U1 setvar ""odometer.user_label_count"" ""{value}""");

    public void GetOdometorUserLabel() => SendCmdToZebra(@"! U1 getvar ""odometer.user_label_count""");

    public override void Dispose()
    {
        base.Dispose();
        ZebraConnection?.Close();
    }

    #endregion
}