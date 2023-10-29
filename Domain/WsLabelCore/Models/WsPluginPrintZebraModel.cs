using PrinterCore.Common;
using PrinterCore.Connectors;
using PrinterCore.Enums;
using WsStorageCore.Entities.SchemaRef.Printers;
using WsStorageCore.Enums;
namespace WsLabelCore.Models;

/// <summary>
/// Плагин принтера ZEBRA.
/// </summary>
#nullable enable
public sealed class WsPluginPrintZebraModel : WsPluginPrintModel
{
    #region Public and private fields and properties
    // private Connection? ZebraConnection { get; set; }
    // private string ZebraPeelerStatus { get; set; }
    // private ZebraPrinter? ZebraDriver { get; set; }
    // private ZebraPrinterStatus? ZebraStatus { get; set; }
    // public bool IsConnected => ZebraStatus != null;

    private readonly IPrinterConnector _connector;

    #endregion

    #region Public and private methods

    public WsPluginPrintZebraModel()
    {
        _connector = new ZebraConnector();
    }
    
    public void InitZebra(WsPluginConfigModel configReopen, WsPluginConfigModel configRequest, WsPluginConfigModel configResponse,
        WsSqlPrinterEntity printer, Label fieldPrint)
    {
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        PrintModel = PrinterTypeEnum.Zebra;
        Printer = printer;
        FieldPrint = fieldPrint;
        PrintName = printer.Name;
    }

    public override void Execute()
    {
        base.Execute();
        ReopenItem.Execute(() => _connector.Connect(Printer.Ip));
        ResponseItem.Execute(RequestZebra);
    }

    private void RequestZebra()
    {
        _connector.UpdateStatus();
        MdInvokeControl.SetForeColor(FieldPrint, _connector.IsConnected ? Color.Green : Color.Red);
        MdInvokeControl.SetText(
            FieldPrint, LabelSession.WeighingSettings.GetPrintDescription(Printer.Ip, Printer.Name,
            _connector.IsConnected, LabelSession.Line.LabelCounter, LabelPrintedCount, LabelCount)
        );
    }

    public string GetDeviceStatusZebra()
    {
        return _connector.GetState() switch
        {

            PrinterStates.Unknown =>  WsLocaleCore.Print.StatusIsUnavailable,
            PrinterStates.Paused => WsLocaleCore.Print.StatusIsPaused,
            PrinterStates.ReadyToPrint =>  WsLocaleCore.Print.StatusIsReadyToPrint,
            PrinterStates.HeadOpen =>  WsLocaleCore.Print.StatusIsHeadOpen,
            PrinterStates.PaperOut => WsLocaleCore.Print.StatusIsPaperOut,
            PrinterStates.HeadTooHot => WsLocaleCore.Print.StatusIsHeadTooHot,
            PrinterStates.HeadTooCold => WsLocaleCore.Print.StatusIsHeadCold,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public bool CheckDeviceStatusZebra() => _connector.GetState() is PrinterStates.ReadyToPrint && _connector.IsConnected;

    public bool SendCmdToZebra(string cmd)
    {
       return _connector.SendCommand(cmd);
    }

    // public void ClearPrintBuffer(int odometerValue = -1)
    // {
    //     SendCmdToZebra("^XA~JA^XZ");
    //     if (odometerValue >= 0) SetOdometorUserLabel(odometerValue);
    // }
    //
    // public void SetOdometorUserLabel(int value) => SendCmdToZebra($@"! U1 setvar ""odometer.user_label_count"" ""{value}""");
    
    public override void Dispose()
    {
        base.Dispose();
        _connector.Dispose();
    }

    #endregion
}