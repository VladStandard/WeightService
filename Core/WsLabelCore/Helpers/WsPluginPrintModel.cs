// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Helpers;

#nullable enable
public sealed class WsPluginPrintModel : WsPluginHelperBase
{
    #region Public and private fields and properties

    private Connection? ZebraConnection { get; set; }
    public byte LabelPrintedCount { get; set; }
    private Label FieldPrint { get; set; }
    private Label FieldPrintExt { get; set; }
    public PrintBrand PrintBrand { get; private set; }
    public MdPrinterModel Printer { get; private set; }
    public string ZebraPeelerStatus { get; private set; }
    private TscDriverHelper TscDriver { get; } = TscDriverHelper.Instance;
    public MdWmiWinPrinterModel TscWmiPrinter => GetWin32Printer(TscDriver.Properties.PrintName);
    private ZebraPrinter? _zebraDriver;
    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;

    private ZebraPrinter ZebraDriver
    {
        get
        {
            if (ZebraConnection is not null && _zebraDriver is null) 
                _zebraDriver = ZebraPrinterFactory.GetInstance(ZebraConnection); 
            return _zebraDriver;
        }
    }
    private ZebraPrinterStatus ZebraStatus { get; set; }
    private bool IsMain { get; set; }
    private SimpleTcpClient? _wsTcpClient;

    private SimpleTcpClient WsTcpClient
    {
        get
        {
            // Open connection.
            if (_wsTcpClient is null)
            {
                _wsTcpClient = new(TscDriver.Properties.PrintIp, 9100);
                _wsTcpClient.Events.Connected += WsTcpClientConnected;
                _wsTcpClient.Events.DataReceived += WsTcpClientDataReceived;
                _wsTcpClient.Events.DataSent += WsTcpClientDataSent;
                _wsTcpClient.Events.Disconnected += WsTcpClientDisconnected;
                // TCP keepalives are disabled by default. To enable them:
                _wsTcpClient.Keepalive.EnableTcpKeepAlives = true;
                _wsTcpClient.Keepalive.TcpKeepAliveInterval = 2;      // seconds to wait before sending subsequent keepalive
                _wsTcpClient.Keepalive.TcpKeepAliveTime = 2;          // seconds to wait before sending a keepalive
                _wsTcpClient.Keepalive.TcpKeepAliveRetryCount = 2;    // number of failed keepalive probes before terminating connection
            }
            if (!_wsTcpClient.IsConnected)
                _wsTcpClient.ConnectWithRetries(1_000);

            return _wsTcpClient;
        }
    }

    #endregion

    #region Constructor and destructor

    public WsPluginPrintModel()
    {
        Printer = new();
        TskType = WsEnumTaskType.TaskPrint;
        LabelPrintedCount = 0;
    }

    #endregion

    #region Public and private methods

    public void Init(WsConfigModel configReopen, WsConfigModel configRequest, WsConfigModel configResponse,
        PrintBrand printBrand, MdPrinterModel printer, Label fieldPrint, Label fieldPrintExt, bool isMain)
    {
        Init();
        ReopenItem.Config = configReopen;
        RequestItem.Config = configRequest;
        ResponseItem.Config = configResponse;
        try
        {
            PrintBrand = printBrand;
            Printer = printer;
            FieldPrint = fieldPrint;
            FieldPrintExt = fieldPrintExt;
            IsMain = isMain;
            MdInvokeControl.SetText(FieldPrintExt, $"{ReopenCounter} | {RequestCounter} | {ResponseCounter}");
            switch (PrintBrand)
            {
                case PrintBrand.Tsc:
                    TscDriver.Setup(PrintChannel.Ethernet, printer.Ip, printer.Port, PrintLabelSize.Size80x100, PrintLabelDpi.Dpi300);
                    MdInvokeControl.SetText(FieldPrint,
                        $"{(IsMain ? LocaleCore.Print.NameMainTsc : LocaleCore.Print.NameShippingTsc)} | {Printer.Ip}");
                    TscDriver.Properties.PrintName = printer.Name;
                    break;

                case PrintBrand.Zebra:
                    MdInvokeControl.SetText(FieldPrint,
                        $"{(IsMain ? LocaleCore.Print.NameMainZebra : LocaleCore.Print.NameShippingZebra)} | {Printer.Ip}");
                    break;
                
            }
        }
        catch (Exception ex)
        {
            WsWinFormNavigationUtils.CatchException(ex);
        }
    }

    public override void Execute()
    {
        base.Execute();
        ReopenItem.Execute(Reopen);
        RequestItem.Execute(Request);
        ResponseItem.Execute(Response);
    }

    private void Reopen()
    {
        MdNetUtils.RequestPing(Printer, 1_000);
    }

    private void Request()
    {
        // FieldPrintManager.
        MdInvokeControl.SetText(FieldPrintExt, $"{ReopenCounter} | {RequestCounter} | {ResponseCounter}");
        
        if (Printer.PingStatus.Equals(IPStatus.Success))
        {
            switch (PrintBrand)
            {
                case PrintBrand.Tsc:
                    break;

                case PrintBrand.Zebra:
                    if (ZebraConnection is null)
                        ZebraConnection = ZebraConnectionBuilder.Build(Printer.Ip);
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
                            WsWinFormNavigationUtils.CatchException(ex);
                            SendCmdToZebra(ZplUtils.ZplHostStatusReturn);
                        }
                    }
                    break;
                
            }
        }
    }

    private byte GetLabelCount() =>
        IsMain ? LabelSession.WeighingSettings.LabelsCountMain : LabelSession.WeighingSettings.LabelsCountShipping;

    private void Response()
    {
        MdInvokeControl.SetText(FieldPrint,
            LabelSession.WeighingSettings.GetPrintDescription(IsMain, PrintBrand, Printer,
                LabelSession.Line.Counter, GetDeviceStatus(), LabelPrintedCount, GetLabelCount()));
        MdInvokeControl.SetForeColor(FieldPrint,
            Equals(Printer.PingStatus, IPStatus.Success) ? Color.Green : Color.Red);
    }

    public string GetDeviceNameShort()
    {
        return IsMain
            ? PrintBrand switch
            {
                PrintBrand.Zebra => LocaleCore.Print.NameMainZebraShort,
                PrintBrand.Tsc => LocaleCore.Print.NameMainTscShort,
                _ => LocaleCore.Print.DeviceNameShort,
            }
            : PrintBrand switch
            {
                PrintBrand.Zebra => LocaleCore.Print.NameShippingZebraShort,
                PrintBrand.Tsc => LocaleCore.Print.NameShippingTscShort,
                _ => LocaleCore.Print.DeviceNameIsUnavailable,
            };
    }

    public string GetDeviceStatus()
    {
        switch (PrintBrand)
        {
            case PrintBrand.Tsc:
                //return IsPrintBusy ? GetPrinterStatusDescription(LocaleCore.Lang, MdWinPrinterStatus.PendingDeletion)
                return GetPrinterStatusDescription(LocaleCore.Lang, TscWmiPrinter.PrinterStatus);

            case PrintBrand.Zebra:
                if (ZebraStatus is null)
                    return LocaleCore.Print.StatusIsUnavailable;
                lock (ZebraStatus)
                {
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
                }
                break;
            
        }
        return LocaleCore.Print.StatusIsUnavailable;
    }

    public bool CheckDeviceStatus()
    {
        string status = GetDeviceStatus();
        switch (PrintBrand)
        {
            case PrintBrand.Tsc:
                return status == LocaleCore.Print.StatusIsReadyToPrint;

            case PrintBrand.Zebra:
                if (ZebraStatus is null)
                    return false;
                return status == LocaleCore.Print.StatusIsReadyToPrint;
            
        }
        return false;
    }

    public string GetZebraPrintMode()
    {
        if (ZebraStatus is null)
            return LocaleCore.Print.ModeUnknown;
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

    public override void Close()
    {
        base.Close();
        
        // Close WsTcpClient.
        if (_wsTcpClient is not null)
        {
            if (WsTcpClient.IsConnected)
                WsTcpClient.Disconnect();
            WsTcpClient.Dispose();
        }
        
        switch (PrintBrand)
        {
            case PrintBrand.Tsc:
                break;

            case PrintBrand.Zebra:
                ZebraConnection?.Close();
                break;
            
        }
    }

    public void SendCmd(WsSqlPluLabelModel pluLabel)
    {
        if (string.IsNullOrEmpty(pluLabel.Zpl)) return;
        if (!Printer.PingStatus.Equals(IPStatus.Success)) return;
        
        switch (PrintBrand)
        {
            case PrintBrand.Tsc:
                //SendCmdToTsc(pluLabel);
                SendCmdToTcp(pluLabel.Zpl);
                break;

            case PrintBrand.Zebra:
                SendCmdToZebra(pluLabel);
                break;
            
        }
    }

    private void SendCmdToZebra(WsSqlPluLabelModel pluLabel) => SendCmdToZebra(pluLabel.Zpl);

    private void SendCmdToZebra(string cmd)
    {
        if (ZebraDriver is null || GetDeviceStatus() != LocaleCore.Print.StatusIsReadyToPrint)
            return;
        try
        {
            if (ZebraStatus is null) return;
            lock (ZebraStatus)
            {
                if (ZebraStatus.isReadyToPrint)
                {
                    ZebraPeelerStatus = SGD.GET("sensor.peeler", ZebraDriver.Connection);
                    if (ZebraPeelerStatus == "clear")
                    {
                        ZebraDriver.SendCommand(cmd.Replace("|", "\\&"));
                    }
                    else
                    {
                        WsWinFormNavigationUtils.CatchException(new($"{LocaleCore.Print.SensorPeeler}: {ZebraPeelerStatus}"));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            WsWinFormNavigationUtils.CatchException(ex);
        }
    }

    //private void SendCmdToTsc(string cmd)
    //{
    //    try
    //    {
    //        cmd = cmd.Replace("|", "\\&");
    //        if (!cmd.Equals("^XA~JA^XZ") && !cmd.Contains("odometer.user_label_count"))
    //        {
    //            TscDriver.SendCmd(cmd);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        WsWinFormNavigationUtils.CatchException(ex, true, true);
    //    }
    //}

    private void SendCmdToTcp(string cmd) => WsTcpClient.Send(cmd.Replace("|", "\\&"));

    private void WsTcpClientConnected(object sender, ConnectionEventArgs e)
    {
        if (!DebugHelper.Instance.IsDevelop) return;
        WsSqlContextManagerHelper.Instance.ContextItem.SaveLogInformation($"Server {e.IpPort} connected");
    }

    private void WsTcpClientDataReceived(object sender, SuperSimpleTcp.DataReceivedEventArgs e)
    {
        if (!DebugHelper.Instance.IsDevelop) return;
        string received = e.Data.Array is null ? string.Empty : Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);
        received = string.IsNullOrEmpty(received) ? "0" : $"{received.Length} bytes with data '{received}'";
        WsSqlContextManagerHelper.Instance.ContextItem.SaveLogInformation($"Server {e.IpPort} data received {received}");
    }

    private void WsTcpClientDataSent(object sender, DataSentEventArgs e)
    {
        if (!DebugHelper.Instance.IsDevelop) return;
        WsSqlContextManagerHelper.Instance.ContextItem.SaveLogInformation($"Server {e.IpPort} data sent {e.BytesSent} bytes");
    }

    private void WsTcpClientDisconnected(object sender, ConnectionEventArgs e)
    {
        if (!DebugHelper.Instance.IsDevelop) return;
        WsSqlContextManagerHelper.Instance.ContextItem.SaveLogInformation($"Server {e.IpPort} disconnected by {e.Reason} reason");
    }

    public void ClearPrintBuffer(int odometerValue = -1)
    {
        switch (PrintBrand)
        {
            case PrintBrand.Default:
                break;
            case PrintBrand.Tsc:
                TscDriver.SendCmdClearBuffer();
                break;
            case PrintBrand.Zebra:
                SendCmdToZebra("^XA~JA^XZ");
                break;
            
        }
        if (odometerValue >= 0)
            SetOdometorUserLabel(odometerValue);
    }

    public void SetOdometorUserLabel(int value)
    {
        switch (PrintBrand)
        {
            case PrintBrand.Default:
                break;
            case PrintBrand.Tsc:
                break;
            case PrintBrand.Zebra:
                //SendCmdToZebra($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
                SendCmdToZebra($@"! U1 setvar ""odometer.user_label_count"" ""{value}""");
                break;
            
        }
    }

    public void GetOdometorUserLabel()
    {
        switch (PrintBrand)
        {
            case PrintBrand.Default:
                break;
            case PrintBrand.Tsc:
                break;
            case PrintBrand.Zebra:
                //SendCmdToZebra($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
                SendCmdToZebra(@"! U1 getvar ""odometer.user_label_count""");
                break;
            
        }
    }

    private MdWmiWinPrinterModel GetWin32Printer(string name)
    {
        if (string.IsNullOrEmpty(name))
            return new(name, string.Empty, string.Empty, string.Empty, string.Empty, MdWinPrinterStatus.Error);
        // PowerShell: gwmi Win32_Printer | select DriverName, PortName, Status, PrinterState, PrinterStatus
        // PowerShell: gwmi -query "select DriverName, PortName, Status, PrinterState, PrinterStatus from Win32_Printer where Name='SCALES-PRN-DEV'"
        ObjectQuery wql = new($"select DriverName, PortName, Status, PrinterState, PrinterStatus from Win32_Printer where Name = '{name}'");
        ManagementObjectSearcher searcher = new(wql);
        ManagementObjectCollection items = searcher.Get();
        string driverName = string.Empty;
        string portName = string.Empty;
        string status = string.Empty;
        string printerState = string.Empty;
        short printerStatus = -1;
        if (items.Count > 0)
        {
            foreach (ManagementBaseObject item in items)
            {
                driverName = Convert.ToString(item["DriverName"]);
                portName = Convert.ToString(item["PortName"]);
                status = Convert.ToString(item["Status"]);
                printerState = Convert.ToString(item["PrinterState"]);
                printerStatus = Convert.ToInt16(item["PrinterStatus"]);
            }
        }
        return new(name, driverName, portName, status, printerState, (MdWinPrinterStatus)printerStatus);
    }

    public string GetPrinterStatusDescription(Lang lang, MdWinPrinterStatus printerStatus)
    {
        return lang switch
        {
            Lang.Russian => printerStatus switch
            {
                MdWinPrinterStatus.Idle => "Бездействие",
                MdWinPrinterStatus.Paused => "Пауза",
                MdWinPrinterStatus.Error => "Ошибка",
                //Win32PrinterStatusEnum.PendingDeletion => "Ожидание печати", // Ожидание удаления
                MdWinPrinterStatus.PendingDeletion => LocaleCore.Print.StatusIsReadyToPrint,
                MdWinPrinterStatus.PaperJam => "Застревание бумаги",
                MdWinPrinterStatus.PaperOut => "Выдача бумаги",
                MdWinPrinterStatus.ManualFeed => "Ручная подача",
                MdWinPrinterStatus.PaperProblem => "Проблема с бумагой",
                MdWinPrinterStatus.Offline => "Не в сети",
                MdWinPrinterStatus.IoActive => "Ввод-вывод активен",
                MdWinPrinterStatus.Busy => "Занято",
                MdWinPrinterStatus.Printing => "Печать",
                MdWinPrinterStatus.OutputBinFull => "Выходной лоток полон",
                MdWinPrinterStatus.NotAvailable => "Недоступно",
                MdWinPrinterStatus.Waiting => "Ожидание",
                MdWinPrinterStatus.Processing => "Обработка",
                MdWinPrinterStatus.Initialization => "Инициализация",
                MdWinPrinterStatus.WarmingUp => "Прогрев",
                MdWinPrinterStatus.TonerLow => "Мало тонера",
                MdWinPrinterStatus.NoToner => "Нет тонера",
                MdWinPrinterStatus.PagePunt => "Страница беспечатана",
                MdWinPrinterStatus.UserInterventionRequired => "Требуется вмешательство пользователя",
                MdWinPrinterStatus.OutOfMemory => "Недостаточно памяти",
                MdWinPrinterStatus.DoorOpen => "Открыта дверца",
                MdWinPrinterStatus.ServerUnknown => "Сервер неизвестен",
                MdWinPrinterStatus.PowerSave => "Энергосбережение",
                _ => "Ошибка чтения статуса!",
            },
            _ => printerStatus switch
            {
                MdWinPrinterStatus.Idle => "Idle",
                MdWinPrinterStatus.Paused => "Paused",
                MdWinPrinterStatus.Error => "Error",
                MdWinPrinterStatus.PendingDeletion => "Waiting for printing", // "Pending deletion"
                MdWinPrinterStatus.PaperJam => "Paper jam",
                MdWinPrinterStatus.PaperOut => "Paper out",
                MdWinPrinterStatus.ManualFeed => "Manual feed",
                MdWinPrinterStatus.PaperProblem => "Paper problem",
                MdWinPrinterStatus.Offline => "Offline",
                MdWinPrinterStatus.IoActive => "Io active",
                MdWinPrinterStatus.Busy => "Busy",
                MdWinPrinterStatus.Printing => "Printing",
                MdWinPrinterStatus.OutputBinFull => "Output bin full",
                MdWinPrinterStatus.NotAvailable => "Not available",
                MdWinPrinterStatus.Waiting => "Waiting",
                MdWinPrinterStatus.Processing => "Processing",
                MdWinPrinterStatus.Initialization => "Initialization",
                MdWinPrinterStatus.WarmingUp => "Warming up",
                MdWinPrinterStatus.TonerLow => "Toner low",
                MdWinPrinterStatus.NoToner => "No toner",
                MdWinPrinterStatus.PagePunt => "Page punt",
                MdWinPrinterStatus.UserInterventionRequired => "User intervention required",
                MdWinPrinterStatus.OutOfMemory => "Out of memory",
                MdWinPrinterStatus.DoorOpen => "Door open",
                MdWinPrinterStatus.ServerUnknown => "Server unknown",
                MdWinPrinterStatus.PowerSave => "Power save",
                _ => "Status reading error!",
            },
        };
    }

    #endregion
}