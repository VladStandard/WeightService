using PrinterCore.Enums;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaScale.PlusLabels;
using Ws.StorageCore.Enums;
using DataReceivedEventArgs=SuperSimpleTcp.DataReceivedEventArgs;

namespace WsLabelCore.Models;

/// <summary>
/// Плагин принтера TSC.
/// </summary>
#nullable enable
public sealed class PluginPrintTscModel : PluginPrintModel
{
    #region Public and private fields and properties
    private SqlContextItemHelper ContextItem => SqlContextItemHelper.Instance;
    private TscDriverHelper TscDriver { get; } = TscDriverHelper.Instance;
    private MdWmiWinPrinterModel TscWmiPrinter => GetWin32Printer(PrintName);
    private readonly object _lockTcpClient = new();
    private SimpleTcpClient? _wsTcpClient;
    private SimpleTcpClient WsTcpClient
    {
        get
        {
            // Открыть подключение.
            if (_wsTcpClient is not null) return _wsTcpClient;
            lock (_lockTcpClient)
            {
                _wsTcpClient = new(TscDriver.Properties.PrintIp, 9100);
                _wsTcpClient.Events.Connected += WsTcpClientConnected;
                _wsTcpClient.Events.DataReceived += WsTcpClientDataReceived;
                _wsTcpClient.Events.DataSent += WsTcpClientDataSent;
                _wsTcpClient.Events.Disconnected += WsTcpClientDisconnected;
                // TCP keepalives are disabled by default. To enable them:
                _wsTcpClient.Keepalive.EnableTcpKeepAlives = true;
                _wsTcpClient.Keepalive.TcpKeepAliveInterval = 2; // wait before sending subsequent keepalive
                _wsTcpClient.Keepalive.TcpKeepAliveTime = 2; // wait before sending a keepalive
                _wsTcpClient.Keepalive.TcpKeepAliveRetryCount = 2; // number of failed keepalive probes before terminating connection
            }
            //if (!IsConnected)
            //    _wsTcpClient.ConnectWithRetries(1_000);
            return _wsTcpClient;
        }
    }
    public bool IsConnected => _wsTcpClient is not null && _wsTcpClient.IsConnected;

    ~PluginPrintTscModel()
    {
        Dispose();
    }

    #endregion

    #region Public and private methods

    public void InitTsc(PluginConfigModel configReopen, PluginConfigModel configRequest, PluginConfigModel configResponse,
        SqlPrinterEntity printer, Label fieldPrint)
    {
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
     
        PrintModel = PrinterTypeEnum.Tsc;
        Printer = printer;
        FieldPrint = fieldPrint;
        PrintName = printer.Name;
        TscDriver.Setup(EnumPrintChannel.Ethernet, printer.Ip, printer.Port, EnumPrintLabelSize.Size80x100, EnumPrintLabelDpi.Dpi300);
    }

    public override void Execute()
    {
        base.Execute();
        //ReopenItem.Execute(ReopenTsc);
        RequestItem.Execute(RequestTsc);
        //ResponseItem.Execute(ResponseTsc);
    }

    public void ReopenTsc()
    {
        try
        {
            if (!IsConnected)
                WsTcpClient.ConnectWithRetries(ReopenItem.Config.WaitExecute);
        }
        catch (Exception ex)
        {
            ContextItem.SaveLogErrorWithDescription(ex, LocaleCore.LabelPrint.PluginPrintTsc);
        }
    }

    private void RequestTsc()
    {
        // Метки.
        MdInvokeControl.SetText(
        FieldPrint, LabelSession.WeighingSettings.GetPrintDescription(Printer.Ip, Printer.Name,
            IsConnected, LabelSession.Line.LabelCounter, LabelPrintedCount, LabelCount)
        );
        MdInvokeControl.SetForeColor(FieldPrint, IsConnected.Equals(true) ? Color.Green : Color.Red);
    }

    private void WsTcpClientConnected(object sender, ConnectionEventArgs e)
    {
        if (!DebugHelper.Instance.IsDevelop) return;
        ContextItem.SaveLogInformation(
            $"Server {e.IpPort} connected", LocaleCore.LabelPrint.PluginPrintTsc);
    }

    private void WsTcpClientDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (!DebugHelper.Instance.IsDevelop) return;
        string received = e.Data.Array is null ? string.Empty : Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);
        received = string.IsNullOrEmpty(received) ? "0" : $"{received.Length} bytes with data '{received}'";
        ContextItem.SaveLogInformation(
            $"Server {e.IpPort} data received {received}", LocaleCore.LabelPrint.PluginPrintTsc);
    }

    private void WsTcpClientDataSent(object sender, DataSentEventArgs e)
    {
        if (!DebugHelper.Instance.IsDevelop) return;
        // TODO: FIX
        // SqlContextManagerHelper.Instance.ContextItem.SaveLogInformation(
        //     $"Server {e.IpPort} data sent {e.BytesSent} bytes", LocaleCore.LabelPrint.PluginPrintTsc);
    }

    private void WsTcpClientDisconnected(object sender, ConnectionEventArgs e)
    {
        if (!DebugHelper.Instance.IsDevelop) return;
        ContextItem.SaveLogInformation(
            $"Server {e.IpPort} disconnected by {e.Reason} reason", LocaleCore.LabelPrint.PluginPrintTsc);
    }

    private string GetDeviceStatusTsc() => GetPrinterStatusDescription(LocaleCore.Lang, TscWmiPrinter.PrinterStatus);

    public bool CheckDeviceStatusTsc() => GetDeviceStatusTsc() == LocaleCore.Print.StatusIsReadyToPrint;

    public void SendCmdToTsc(SqlPluLabelEntity pluLabel)
    {
        if (string.IsNullOrEmpty(pluLabel.Zpl)) return;
        ReopenTsc();
        if (!IsConnected) return;
        WsTcpClient.Send(pluLabel.Zpl.Replace("|", "\\&"));
    }

    public void ClearPrintBuffer(int odometerValue = -1)
    {
        TscDriver.SendCmdClearBuffer();
        //if (odometerValue >= 0) SetOdometorUserLabel(odometerValue);
    }

    public override void Dispose()
    {
        base.Dispose();
        // Dispose WsTcpClient.
        if (_wsTcpClient is not null)
        {
            if (IsConnected)
                WsTcpClient.Disconnect();
            WsTcpClient.Dispose();
        }
    }
    
    #endregion
}