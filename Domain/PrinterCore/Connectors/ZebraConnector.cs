using PrinterCore.Common;
using PrinterCore.Enums;
using WsLocalizationCore.Utils;
using Ws.StorageCore.Helpers;

namespace PrinterCore.Connectors;

public class ZebraConnector : IPrinterConnector
{
    private SqlContextItemHelper ContextItem => SqlContextItemHelper.Instance;
    private Connection? Connector { get; set; }
    private ZebraPrinter? Printer { get; set; }
    private ZebraPrinterStatus? Status { get; set; }
    public bool IsConnected => Connector?.Connected ?? false;
    
    #region Public
    
    public void Connect(string ip)
    {
        try
        {
            Connector = null;
            Connector = new TcpConnection(ip, TcpConnection.DEFAULT_ZPL_TCP_PORT, 500, 500);
            Connector?.Open();
            Printer ??= ZebraPrinterFactory.GetInstance(Connector);
        }
        catch (Exception ex)
        {
            ContextItem.SaveLogErrorWithDescription(ex,
            LocaleCore.LabelPrint.PluginPrintZebra);
            Dispose();
        }
    }
    
    public bool SendCommand(string cmd)
    {
        if (!IsConnected) 
            return false;
        try {
            Printer?.SendCommand(cmd.Replace("|", "\\&"));
            return true;
        }
        catch (Exception ex)
        {
            ContextItem.SaveLogErrorWithDescription(ex, LocaleCore.LabelPrint.PluginPrintZebra);
            Dispose();
        }
        return false;
    }
    
    public void UpdateStatus()
    {
        if (!IsConnected) 
            return;
        Status = Connector != null ? Printer?.GetCurrentStatus() : null;
    }
    
    public PrinterStates GetState()
    {
        if (Status is null) 
            return PrinterStates.Unknown;
        if (Status.isHeadCold)
            return PrinterStates.HeadTooCold;
        if (Status.isHeadOpen)
            return PrinterStates.HeadOpen;
        if (Status.isHeadTooHot)
            return PrinterStates.HeadTooHot;
        if (Status.isPaperOut)
            return PrinterStates.PaperOut;
        if (Status.isPaused)
            return PrinterStates.Paused;
        if (Status.isReadyToPrint)
            return PrinterStates.ReadyToPrint;
        return PrinterStates.Unknown;
    }
    
    public void Dispose()
    {
        Printer = null;
        Status = null;
        Connector = null;
    }

    #endregion
}