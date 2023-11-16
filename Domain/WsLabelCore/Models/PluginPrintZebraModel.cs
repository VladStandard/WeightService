using PrinterCore.Common;
using PrinterCore.Connectors;
using PrinterCore.Enums;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Enums;

namespace WsLabelCore.Models;

/// <summary>
/// Плагин принтера ZEBRA.
/// </summary>
#nullable enable
public sealed class PluginPrintZebraModel : PluginPrintModel
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

    public PluginPrintZebraModel()
    {
        _connector = new ZebraConnector();
    }
    
    public void InitZebra(PluginConfigModel configReopen, PluginConfigModel configRequest, PluginConfigModel configResponse,
        SqlPrinterEntity printer, Label fieldPrint)
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

            PrinterStates.Unknown =>  LocaleCore.Print.StatusIsUnavailable,
            PrinterStates.Paused => LocaleCore.Print.StatusIsPaused,
            PrinterStates.ReadyToPrint =>  LocaleCore.Print.StatusIsReadyToPrint,
            PrinterStates.HeadOpen =>  LocaleCore.Print.StatusIsHeadOpen,
            PrinterStates.PaperOut => LocaleCore.Print.StatusIsPaperOut,
            PrinterStates.HeadTooHot => LocaleCore.Print.StatusIsHeadTooHot,
            PrinterStates.HeadTooCold => LocaleCore.Print.StatusIsHeadCold,
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