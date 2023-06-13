// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Models;

/// <summary>
/// Плагин принтера TSC.
/// </summary>
#nullable enable
public sealed class WsPluginPrintTscModel : WsPluginPrintModel
{
    #region Public and private fields and properties

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

    ~WsPluginPrintTscModel()
    {
        Close();
    }

    #endregion

    #region Public and private methods

    public void InitTsc(WsPluginConfigModel configReopen, WsPluginConfigModel configRequest, WsPluginConfigModel configResponse,
        MdPrinterModel printer, Label fieldPrint, Label fieldPrintExt, bool isMain)
    {
        Init();
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        try
        {
            PrintModel = WsEnumPrintModel.Tsc;
            Printer = printer;
            FieldPrint = fieldPrint;
            FieldPrintExt = fieldPrintExt;
            IsMain = isMain;
            PrintName = printer.Name;
            MdInvokeControl.SetText(FieldPrintExt, $"{ReopenCounter} | {RequestCounter} | {ResponseCounter}");
            TscDriver.Setup(WsEnumPrintChannel.Ethernet, printer.Ip, printer.Port, WsEnumPrintLabelSize.Size80x100, WsEnumPrintLabelDpi.Dpi300);
            MdInvokeControl.SetText(FieldPrint,
                $"{(IsMain ? WsLocaleCore.Print.NameMainTsc : WsLocaleCore.Print.NameShippingTsc)} | {Printer.Ip}");
            TscDriver.Properties.PrintName = printer.Name;
        }
        catch (Exception ex)
        {
            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, WsLocaleCore.LabelPrint.PluginPrintTsc);
        }
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
            WsSqlContextManagerHelper.Instance.ContextItem.SaveLogErrorWithDescription(ex, WsLocaleCore.LabelPrint.PluginPrintTsc);
        }
    }

    private void RequestTsc()
    {
        // Метки.
        MdInvokeControl.SetText(FieldPrintExt, $"{ReopenCounter} | {RequestCounter} | {ResponseCounter}");
        MdInvokeControl.SetText(FieldPrint,
            LabelSession.WeighingSettings.GetPrintDescription(IsMain, PrintModel, Printer, IsConnected,
                LabelSession.Line.LabelCounter, GetDeviceStatusTsc(), LabelPrintedCount, GetLabelCount()));
        MdInvokeControl.SetForeColor(FieldPrint, IsConnected.Equals(true) ? Color.Green : Color.Red);
    }

    private void WsTcpClientConnected(object sender, ConnectionEventArgs e)
    {
        if (!WsDebugHelper.Instance.IsDevelop) return;
        WsSqlContextManagerHelper.Instance.ContextItem.SaveLogInformationWithDescription(
            $"Server {e.IpPort} connected", WsLocaleCore.LabelPrint.PluginPrintTsc);
    }

    private void WsTcpClientDataReceived(object sender, SuperSimpleTcp.DataReceivedEventArgs e)
    {
        if (!WsDebugHelper.Instance.IsDevelop) return;
        string received = e.Data.Array is null ? string.Empty : Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);
        received = string.IsNullOrEmpty(received) ? "0" : $"{received.Length} bytes with data '{received}'";
        WsSqlContextManagerHelper.Instance.ContextItem.SaveLogInformationWithDescription(
            $"Server {e.IpPort} data received {received}", WsLocaleCore.LabelPrint.PluginPrintTsc);
    }

    private void WsTcpClientDataSent(object sender, DataSentEventArgs e)
    {
        if (!WsDebugHelper.Instance.IsDevelop) return;
        WsSqlContextManagerHelper.Instance.ContextItem.SaveLogInformationWithDescription(
            $"Server {e.IpPort} data sent {e.BytesSent} bytes", WsLocaleCore.LabelPrint.PluginPrintTsc);
    }

    private void WsTcpClientDisconnected(object sender, ConnectionEventArgs e)
    {
        if (!WsDebugHelper.Instance.IsDevelop) return;
        WsSqlContextManagerHelper.Instance.ContextItem.SaveLogInformationWithDescription(
            $"Server {e.IpPort} disconnected by {e.Reason} reason", WsLocaleCore.LabelPrint.PluginPrintTsc);
    }

    private string GetDeviceStatusTsc() => GetPrinterStatusDescription(WsLocaleCore.Lang, TscWmiPrinter.PrinterStatus);

    public bool CheckDeviceStatusTsc() => GetDeviceStatusTsc() == WsLocaleCore.Print.StatusIsReadyToPrint;

    public void SendCmdToTsc(WsSqlPluLabelModel pluLabel)
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

    public override void Close()
    {
        base.Close();
        // Close WsTcpClient.
        if (_wsTcpClient is not null)
        {
            if (IsConnected)
                WsTcpClient.Disconnect();
            WsTcpClient.Dispose();
        }
    }
    
    #endregion
}