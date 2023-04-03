// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusLabels;
using MDSoft.BarcodePrintUtils.Wmi.Enums;
using MDSoft.BarcodePrintUtils.Wmi.Models;
using System.Net.Sockets;
using Label = System.Windows.Forms.Label;

namespace WsWeight.Plugins.Helpers;

public class PluginPrintModel : PluginHelperBase
{
    #region Public and private fields and properties

    private Connection? ZebraConnection { get; set; }
    public byte LabelPrintedCount { get; set; }
    private Label FieldPrint { get; set; }
    private Label FieldPrintExt { get; set; }
    public PrintBrand PrintBrand { get; private set; }
    public PrinterModel Printer { get; private set; }
    public string ZebraPeelerStatus { get; private set; }
    private TscDriverHelper TscDriver { get; } = TscDriverHelper.Instance;
    public WmiWin32PrinterModel TscWmiPrinter => GetWin32Printer(TscDriver.Properties.PrintName);
    private ZebraPrinter _zebraDriver;
    private ZebraPrinter ZebraDriver { get { if (ZebraConnection is not null && _zebraDriver is null) _zebraDriver = ZebraPrinterFactory.GetInstance(ZebraConnection); return _zebraDriver; } }
    private ZebraPrinterStatus ZebraStatus { get; set; }
    private bool IsMain { get; set; }

    #endregion

    #region Constructor and destructor

    public PluginPrintModel()
    {
        TskType = TaskType.TaskPrint;
        LabelPrintedCount = 0;
    }

    #endregion

    #region Public and private methods

    public void Init(ConfigModel configReopen, ConfigModel configRequest, ConfigModel configResponse,
        PrintBrand printBrand, PrinterModel printer, Label fieldPrint, Label fieldPrintExt, bool isMain)
    {
        base.Init();
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
            MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrintExt, $"{ReopenCounter} | {RequestCounter} | {ResponseCounter}");
            switch (PrintBrand)
            {
                case PrintBrand.Zebra:
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
                        $"{(IsMain ? LocaleCore.Print.NameMainZebra : LocaleCore.Print.NameShippingZebra)} | {Printer.Ip}");
                    break;
                case PrintBrand.TSC:
                    TscDriver.Setup(PrintChannel.Ethernet, printer.Ip, printer.Port, PrintLabelSize.Size80x100, PrintLabelDpi.Dpi300);
                    MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
                        $"{(IsMain ? LocaleCore.Print.NameMainTsc : LocaleCore.Print.NameShippingTsc)} | {Printer.Ip}");
                    TscDriver.Properties.PrintName = printer.Name;
                    break;
            }
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex);
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
        NetUtils.RequestPing(Printer, 1_000);
    }

    private void Request()
    {
        // FieldPrintManager.
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrintExt, $"{ReopenCounter} | {RequestCounter} | {ResponseCounter}");
        
        if (Printer.PingStatus == IPStatus.Success)
        {
            switch (PrintBrand)
            {
                case PrintBrand.Zebra:
                    if (ZebraConnection is null)
                        ZebraConnection = ZebraConnectionBuilder.Build(Printer.Ip);
                    if (!ZebraConnection.Connected)
                        ZebraConnection.Open();
                    if (Printer is null || ZebraDriver is null || ZebraConnection is null || !ZebraConnection.Connected)
                        ZebraStatus = null;
                    else
                    {
                        try
                        {
                            ZebraStatus = ZebraDriver.GetCurrentStatus();
                        }
                        catch (Exception ex)
                        {
                            WpfUtils.CatchException(ex);
                            SendCmdToZebra(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ZplHostStatusReturn);
                        }
                    }
                    break;
                case PrintBrand.TSC:
                    break;
            }
        }
    }

    private byte GetLabelCount() =>
        IsMain
            ? UserSessionHelper.Instance.WeighingSettings.LabelsCountMain
            : UserSessionHelper.Instance.WeighingSettings.LabelsCountShipping;

    private void Response()
    {
        MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
            UserSessionHelper.Instance.WeighingSettings.GetPrintDescription(IsMain, PrintBrand, Printer,
                UserSessionHelper.Instance.Scale.Counter, GetDeviceStatus(), LabelPrintedCount, GetLabelCount()));
        MDSoft.WinFormsUtils.InvokeControl.SetForeColor(FieldPrint,
            Equals(Printer.PingStatus, IPStatus.Success) ? Color.Green : Color.Red);
    }

    public string GetDeviceNameShort()
    {
        return IsMain
            ? PrintBrand switch
            {
                PrintBrand.Zebra => LocaleCore.Print.NameMainZebraShort,
                PrintBrand.TSC => LocaleCore.Print.NameMainTscShort,
                _ => LocaleCore.Print.DeviceNameShort,
            }
            : PrintBrand switch
            {
                PrintBrand.Zebra => LocaleCore.Print.NameShippingZebraShort,
                PrintBrand.TSC => LocaleCore.Print.NameShippingTscShort,
                _ => LocaleCore.Print.DeviceNameIsUnavailable,
            };
    }

    public string GetDeviceStatus()
    {
        switch (PrintBrand)
        {
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
            case PrintBrand.TSC:
                //return IsPrintBusy ? GetPrinterStatusDescription(LocaleCore.Lang, Win32PrinterStatus.PendingDeletion)
                return GetPrinterStatusDescription(LocaleCore.Lang, TscWmiPrinter.PrinterStatus);
        }
        return LocaleCore.Print.StatusIsUnavailable;
    }

    public bool CheckDeviceStatus()
    {
        string status = GetDeviceStatus();
        switch (PrintBrand)
        {
            case PrintBrand.Zebra:
                if (ZebraStatus is null)
                    return false;
                return status == LocaleCore.Print.StatusIsReadyToPrint;
            case PrintBrand.TSC:
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
        if (PrintBrand == PrintBrand.Zebra)
        {
            ZebraConnection?.Close();
        }
        ZebraConnection = null;
        ZebraPeelerStatus = string.Empty;
    }

    public void SendCmd(PluLabelModel pluLabel)
    {
        if (string.IsNullOrEmpty(pluLabel.Zpl))
            return;
        if (Printer.PingStatus != IPStatus.Success)
            return;

        switch (PrintBrand)
        {
            case PrintBrand.Zebra:
                SendCmdToZebra(pluLabel);
                break;
            case PrintBrand.TSC:
                //SendCmdToTsc(pluLabel);
                SendCmdToTcp(pluLabel);
                break;
        }
    }

    private void SendCmdToZebra(PluLabelModel pluLabel) => SendCmdToZebra(pluLabel.Zpl);

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
                        WpfUtils.CatchException(new($"{LocaleCore.Print.SensorPeeler}: {ZebraPeelerStatus}"), true, true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, true, true);
        }
    }

    private void SendCmdToTsc(PluLabelModel pluLabel) => SendCmdToTsc(pluLabel.Zpl);

    private void SendCmdToTsc(string cmd)
    {
        try
        {
            cmd = cmd.Replace("|", "\\&");
            if (!cmd.Equals("^XA~JA^XZ") && !cmd.Contains("odometer.user_label_count"))
            {
                TscDriver.SendCmd(cmd);
            }
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, true, true);
        }
    }

    private void SendCmdToTcp(PluLabelModel pluLabel) => SendCmdToTcp(pluLabel.Zpl);

    private void SendCmdToTcp(string cmd)
    {
        cmd = cmd.Replace("|", "\\&");

        // Open connection.
        using TcpClient tcpClient = new();
        //tcpClient.Connect(TscDriver.Properties.PrintIp, TscDriver.Properties.PrintPort);
        tcpClient.Connect(TscDriver.Properties.PrintIp, 9100);

        // Send Zpl data to printer.
        using StreamWriter writer = new(tcpClient.GetStream());
        writer.Write(cmd);
        writer.Flush();

        // Close Connection.
        writer.Close();
        tcpClient.Close();
    }

    public void ClearPrintBuffer(int odometerValue = -1)
    {
        switch (PrintBrand)
        {
            case PrintBrand.Default:
                break;
            case PrintBrand.Zebra:
                SendCmdToZebra("^XA~JA^XZ");
                break;
            case PrintBrand.TSC:
                TscDriver.SendCmdClearBuffer();
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
            case PrintBrand.Zebra:
                //SendCmdToZebra($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");
                SendCmdToZebra($@"! U1 setvar ""odometer.user_label_count"" ""{value}""");
                break;
            case PrintBrand.TSC:
                break;
        }
    }

    public void GetOdometorUserLabel()
    {
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
        }
    }

    private WmiWin32PrinterModel GetWin32Printer(string name)
    {
        if (string.IsNullOrEmpty(name))
            return new(name, string.Empty, string.Empty, string.Empty, string.Empty, Win32PrinterStatus.Error);
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
            foreach (ManagementObject item in items)
            {
                driverName = Convert.ToString(item["DriverName"]);
                portName = Convert.ToString(item["PortName"]);
                status = Convert.ToString(item["Status"]);
                printerState = Convert.ToString(item["PrinterState"]);
                printerStatus = Convert.ToInt16(item["PrinterStatus"]);
            }
        }
        return new(name, driverName, portName, status, printerState, (Win32PrinterStatus)printerStatus);
    }

    public string GetPrinterStatusDescription(Lang lang, Win32PrinterStatus printerStatus)
    {
        return lang switch
        {
            Lang.Russian => printerStatus switch
            {
                Win32PrinterStatus.Idle => "Бездействие",
                Win32PrinterStatus.Paused => "Пауза",
                Win32PrinterStatus.Error => "Ошибка",
                //Win32PrinterStatusEnum.PendingDeletion => "Ожидание печати", // Ожидание удаления
                Win32PrinterStatus.PendingDeletion => LocaleCore.Print.StatusIsReadyToPrint,
                Win32PrinterStatus.PaperJam => "Застревание бумаги",
                Win32PrinterStatus.PaperOut => "Выдача бумаги",
                Win32PrinterStatus.ManualFeed => "Ручная подача",
                Win32PrinterStatus.PaperProblem => "Проблема с бумагой",
                Win32PrinterStatus.Offline => "Не в сети",
                Win32PrinterStatus.IoActive => "Ввод-вывод активен",
                Win32PrinterStatus.Busy => "Занято",
                Win32PrinterStatus.Printing => "Печать",
                Win32PrinterStatus.OutputBinFull => "Выходной лоток полон",
                Win32PrinterStatus.NotAvailable => "Недоступно",
                Win32PrinterStatus.Waiting => "Ожидание",
                Win32PrinterStatus.Processing => "Обработка",
                Win32PrinterStatus.Initialization => "Инициализация",
                Win32PrinterStatus.WarmingUp => "Прогрев",
                Win32PrinterStatus.TonerLow => "Мало тонера",
                Win32PrinterStatus.NoToner => "Нет тонера",
                Win32PrinterStatus.PagePunt => "Страница беспечатана",
                Win32PrinterStatus.UserInterventionRequired => "Требуется вмешательство пользователя",
                Win32PrinterStatus.OutOfMemory => "Недостаточно памяти",
                Win32PrinterStatus.DoorOpen => "Открыта дверца",
                Win32PrinterStatus.ServerUnknown => "Сервер неизвестен",
                Win32PrinterStatus.PowerSave => "Энергосбережение",
                _ => "Ошибка чтения статуса!",
            },
            _ => printerStatus switch
            {
                Win32PrinterStatus.Idle => "Idle",
                Win32PrinterStatus.Paused => "Paused",
                Win32PrinterStatus.Error => "Error",
                Win32PrinterStatus.PendingDeletion => "Waiting for printing", // "Pending deletion"
                Win32PrinterStatus.PaperJam => "Paper jam",
                Win32PrinterStatus.PaperOut => "Paper out",
                Win32PrinterStatus.ManualFeed => "Manual feed",
                Win32PrinterStatus.PaperProblem => "Paper problem",
                Win32PrinterStatus.Offline => "Offline",
                Win32PrinterStatus.IoActive => "Io active",
                Win32PrinterStatus.Busy => "Busy",
                Win32PrinterStatus.Printing => "Printing",
                Win32PrinterStatus.OutputBinFull => "Output bin full",
                Win32PrinterStatus.NotAvailable => "Not available",
                Win32PrinterStatus.Waiting => "Waiting",
                Win32PrinterStatus.Processing => "Processing",
                Win32PrinterStatus.Initialization => "Initialization",
                Win32PrinterStatus.WarmingUp => "Warming up",
                Win32PrinterStatus.TonerLow => "Toner low",
                Win32PrinterStatus.NoToner => "No toner",
                Win32PrinterStatus.PagePunt => "Page punt",
                Win32PrinterStatus.UserInterventionRequired => "User intervention required",
                Win32PrinterStatus.OutOfMemory => "Out of memory",
                Win32PrinterStatus.DoorOpen => "Door open",
                Win32PrinterStatus.ServerUnknown => "Server unknown",
                Win32PrinterStatus.PowerSave => "Power save",
                _ => "Status reading error!",
            },
        };
    }

    #endregion
}