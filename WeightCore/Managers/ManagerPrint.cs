// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Protocols;
using MDSoft.BarcodePrintUtils;
using MDSoft.BarcodePrintUtils.Tsc;
using MDSoft.BarcodePrintUtils.Wmi;
using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using DataCore.Managers;
using DataCore.Models;
using WeightCore.Gui;
using WeightCore.Helpers;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using ZebraConnectionBuilder = Zebra.Sdk.Comm.ConnectionBuilder;
using ZebraPrinterStatus = Zebra.Sdk.Printer.PrinterStatus;
using DataCore.Enums;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.Printers;

namespace WeightCore.Managers;

public class ManagerPrint : ManagerBase
{
	#region Public and private fields and properties

	private ZebraPrinter _zebraDriver;
	public Connection ZebraConnection { get; private set; }
	public int LabelsCount { get; set; }
	public Label FieldPrint { get; private set; }
	public PrintBrand PrintBrand { get; private set; }
	public PrinterModel Printer { get; private set; }
	public string ZebraPeelerStatus { get; private set; }
	public TscDriverHelper TscDriver { get; } = TscDriverHelper.Instance;
	public WmiWin32PrinterEntity TscWmiPrinter => GetWin32Printer(TscDriver.Properties.PrintName);
	public ZebraPrinter ZebraDriver { get { if (ZebraConnection is not null && _zebraDriver is null) _zebraDriver = ZebraPrinterFactory.GetInstance(ZebraConnection); return _zebraDriver; } }
	public ZebraPrinterStatus ZebraStatus { get; private set; }
	public bool IsPrintBusy { get; set; }

	#endregion

	#region Constructor and destructor

	public ManagerPrint() : base()
	{
		LabelsCount = 0;
		Init(Close, ReleaseManaged, ReleaseUnmanaged);
	}

	#endregion

	#region Public and private methods

	public void Init(PrintBrand printBrand, PrinterModel printer, Label fieldPrint, bool isMain)
	{
		try
		{
			Init(TaskTypeEnum.MemoryManager,
				() =>
				{
					PrintBrand = printBrand;
					Printer = printer;
					FieldPrint = fieldPrint;
					MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPrint, true);
					switch (PrintBrand)
					{
						case PrintBrand.Zebra:
							MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
								$"{(isMain ? LocaleCore.Print.NameMainZebra : LocaleCore.Print.NameShippingZebra)} | {Printer.Ip}");
							break;
						case PrintBrand.TSC:
							TscDriver.Setup(PrintChannel.Ethernet, printer.Ip, printer.Port, PrintLabelSize.Size80x100, PrintLabelDpi.Dpi300);
							MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
								$"{(isMain ? LocaleCore.Print.NameMainTsc : LocaleCore.Print.NameShippingTsc)} | {Printer.Ip}");
							TscDriver.Properties.PrintName = printer.Name;
							break;
					}
				},
				new(waitReopen: 1_000, waitRequest: 1_000, waitResponse: 0_500, waitClose: 0_500, waitException: 0_500,
					true, Application.DoEvents));
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(ex);
		}
	}

	public void Open(bool isMain)
	{
		try
		{
			Open(Reopen, Request,
				() =>
				{
					Response(isMain,
						$"{LocaleCore.Scales.Labels}: {LabelsCount} / " +
						$"{UserSessionHelper.Instance.WeighingSettings.LabelsCountMain}");
				});
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(ex);
		}
	}

	private void Reopen()
	{
		NetUtils.RequestPing(Printer, 1_000);
	}

	private void Request()
	{
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
							GuiUtils.WpfForm.CatchException(ex);
							SendCmdToZebra(MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ZplHostStatusReturn);
						}
					}
					break;
				case PrintBrand.TSC:
					break;
			}
		}
	}

	private void Response(bool isMain, string value)
	{
		MDSoft.WinFormsUtils.InvokeControl.SetText(FieldPrint,
			$"{GetDeviceNameShort(isMain)} | {Printer.Ip}: {Printer.PingStatus} | " +
			$"{LocaleCore.Table.Counter}: {UserSessionHelper.Instance.Scale.Counter} | " +
			$"{GetDeviceStatus()} | {value}");
		MDSoft.WinFormsUtils.InvokeControl.SetForeColor(FieldPrint, 
			Equals(Printer.PingStatus, IPStatus.Success) ? System.Drawing.Color.Green : System.Drawing.Color.Red);
	}

	public string GetDeviceName(bool isMain)
	{
		return isMain
			? PrintBrand switch
			{
				PrintBrand.Zebra => LocaleCore.Print.NameMainZebra,
				PrintBrand.TSC => LocaleCore.Print.NameMainTsc,
				_ => LocaleCore.Print.DeviceName,
			}
			: PrintBrand switch
			{
				PrintBrand.Zebra => LocaleCore.Print.NameShippingZebra,
				PrintBrand.TSC => LocaleCore.Print.NameShippingTsc,
				_ => LocaleCore.Print.DeviceNameIsUnavailable,
			};
	}

	public string GetDeviceNameShort(bool isMain)
	{
		return isMain
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
					else if (ZebraStatus.isHeadOpen)
						return LocaleCore.Print.StatusIsHeadOpen;
					else if (ZebraStatus.isHeadTooHot)
						return LocaleCore.Print.StatusIsHeadTooHot;
					else if (ZebraStatus.isPaperOut)
						return LocaleCore.Print.StatusIsPaperOut;
					else if (ZebraStatus.isPartialFormatInProgress)
						return LocaleCore.Print.StatusIsPartialFormatInProgress;
					else if (ZebraStatus.isPaused)
						return LocaleCore.Print.StatusIsPaused;
					else if (ZebraStatus.isReadyToPrint)
						return LocaleCore.Print.StatusIsReadyToPrint;
					else if (ZebraStatus.isReceiveBufferFull)
						return LocaleCore.Print.StatusIsReceiveBufferFull;
					else if (ZebraStatus.isRibbonOut)
						return LocaleCore.Print.StatusIsRibbonOut;
				}
				break;
			case PrintBrand.TSC:
				return IsPrintBusy
					? GetPrinterStatusDescription(LocaleCore.Lang, Win32PrinterStatusEnum.PendingDeletion)
					: GetPrinterStatusDescription(LocaleCore.Lang, TscWmiPrinter.PrinterStatus);
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
			else if (ZebraStatus.printMode == ZplPrintMode.PEEL_OFF)
				return LocaleCore.Print.ModePeelOff;
			else if (ZebraStatus.printMode == ZplPrintMode.TEAR_OFF)
				return LocaleCore.Print.ModeTearOff;
			else if (ZebraStatus.printMode == ZplPrintMode.CUTTER)
				return LocaleCore.Print.ModeCutter;
			else if (ZebraStatus.printMode == ZplPrintMode.APPLICATOR)
				return LocaleCore.Print.ModeApplicator;
			else if (ZebraStatus.printMode == ZplPrintMode.DELAYED_CUT)
				return LocaleCore.Print.ModeDelayedCut;
			else if (ZebraStatus.printMode == ZplPrintMode.LINERLESS_PEEL)
				return LocaleCore.Print.ModeLinerlessPeel;
			else if (ZebraStatus.printMode == ZplPrintMode.LINERLESS_REWIND)
				return LocaleCore.Print.ModeLinerlessRewind;
			else if (ZebraStatus.printMode == ZplPrintMode.PARTIAL_CUTTER)
				return LocaleCore.Print.ModePartialCutter;
			else if (ZebraStatus.printMode == ZplPrintMode.RFID)
				return LocaleCore.Print.ModeRfid;
			else if (ZebraStatus.printMode == ZplPrintMode.KIOSK)
				return LocaleCore.Print.ModeKiosk;
		}
		return LocaleCore.Print.ModeUnknown;
	}

	public new void Close()
	{
		base.Close();
	}

	public new void ReleaseManaged()
	{
		MDSoft.WinFormsUtils.InvokeControl.SetVisible(FieldPrint, false);

		if (PrintBrand == PrintBrand.Zebra)
		{
			ZebraConnection?.Close();
		}
		ZebraConnection = null;

		base.ReleaseManaged();
	}

	public new void ReleaseUnmanaged()
	{
		ZebraPeelerStatus = string.Empty;

		base.ReleaseUnmanaged();
	}

	public void SendCmd(PluLabelModel pluLabel)
	{
		CheckIsDisposed();
		if (string.IsNullOrEmpty(pluLabel.Zpl))
			return;
		if (Printer.PingStatus != IPStatus.Success)
			return;

		switch (PrintBrand)
		{
			case PrintBrand.Zebra:
				SendCmdToZebra(pluLabel.Zpl);
				break;
			case PrintBrand.TSC:
				SendCmdToTsc(pluLabel.Zpl);
				break;
		}
	}

	private void SendCmdToZebra(string zpl)
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
						ZebraDriver.SendCommand(zpl.Replace("|", "\\&"));
					}
					else
					{
						GuiUtils.WpfForm.CatchException(new Exception($"{LocaleCore.Print.SensorPeeler}: {ZebraPeelerStatus}"), true, true, true);
					}
				}
			}
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(ex, true, true, true);
		}
	}

	private void SendCmdToTsc(string printCmd)
	{
		try
		{
			string docReplace = printCmd.Replace("|", "\\&");
			if (!docReplace.Equals("^XA~JA^XZ") && !docReplace.Contains("odometer.user_label_count"))
			{
				TscDriver.SendCmd(docReplace);
			}
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(ex, true, true, true);
		}
	}

	public void ClearPrintBuffer(int odometerValue = -1)
	{
		CheckIsDisposed();

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
		CheckIsDisposed();

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
			default:
				break;
		}
	}

	public void GetOdometorUserLabel()
	{
		CheckIsDisposed();

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
			default:
				break;
		}
	}

	public WmiWin32PrinterEntity GetWin32Printer(string name)
	{
		if (string.IsNullOrEmpty(name))
			return new WmiWin32PrinterEntity(name, string.Empty, string.Empty, string.Empty, string.Empty, Win32PrinterStatusEnum.Error);
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
		return new WmiWin32PrinterEntity(name, driverName, portName, status, printerState, (Win32PrinterStatusEnum)printerStatus);
	}

	public string GetPrinterStatusDescription(Lang lang, Win32PrinterStatusEnum printerStatus)
	{
		return lang switch
		{
			Lang.Russian => printerStatus switch
			{
				Win32PrinterStatusEnum.Idle => "Бездействие",
				Win32PrinterStatusEnum.Paused => "Пауза",
				Win32PrinterStatusEnum.Error => "Ошибка",
				//Win32PrinterStatusEnum.PendingDeletion => "Ожидание печати", // Ожидание удаления
				Win32PrinterStatusEnum.PendingDeletion => LocaleCore.Print.StatusIsReadyToPrint,
				Win32PrinterStatusEnum.PaperJam => "Застревание бумаги",
				Win32PrinterStatusEnum.PaperOut => "Выдача бумаги",
				Win32PrinterStatusEnum.ManualFeed => "Ручная подача",
				Win32PrinterStatusEnum.PaperProblem => "Проблема с бумагой",
				Win32PrinterStatusEnum.Offline => "Не в сети",
				Win32PrinterStatusEnum.IoActive => "Ввод-вывод активен",
				Win32PrinterStatusEnum.Busy => "Занято",
				Win32PrinterStatusEnum.Printing => "Печать",
				Win32PrinterStatusEnum.OutputBinFull => "Выходной лоток полон",
				Win32PrinterStatusEnum.NotAvailable => "Недоступно",
				Win32PrinterStatusEnum.Waiting => "Ожидание",
				Win32PrinterStatusEnum.Processing => "Обработка",
				Win32PrinterStatusEnum.Initialization => "Инициализация",
				Win32PrinterStatusEnum.WarmingUp => "Прогрев",
				Win32PrinterStatusEnum.TonerLow => "Мало тонера",
				Win32PrinterStatusEnum.NoToner => "Нет тонера",
				Win32PrinterStatusEnum.PagePunt => "Страница беспечатана",
				Win32PrinterStatusEnum.UserInterventionRequired => "Требуется вмешательство пользователя",
				Win32PrinterStatusEnum.OutOfMemory => "Недостаточно памяти",
				Win32PrinterStatusEnum.DoorOpen => "Открыта дверца",
				Win32PrinterStatusEnum.ServerUnknown => "Сервер неизвестен",
				Win32PrinterStatusEnum.PowerSave => "Энергосбережение",
				_ => "Ошибка чтения статуса!",
			},
			_ => printerStatus switch
			{
				Win32PrinterStatusEnum.Idle => "Idle",
				Win32PrinterStatusEnum.Paused => "Paused",
				Win32PrinterStatusEnum.Error => "Error",
				Win32PrinterStatusEnum.PendingDeletion => "Waiting for printing", // "Pending deletion"
				Win32PrinterStatusEnum.PaperJam => "Paper jam",
				Win32PrinterStatusEnum.PaperOut => "Paper out",
				Win32PrinterStatusEnum.ManualFeed => "Manual feed",
				Win32PrinterStatusEnum.PaperProblem => "Paper problem",
				Win32PrinterStatusEnum.Offline => "Offline",
				Win32PrinterStatusEnum.IoActive => "Io active",
				Win32PrinterStatusEnum.Busy => "Busy",
				Win32PrinterStatusEnum.Printing => "Printing",
				Win32PrinterStatusEnum.OutputBinFull => "Output bin full",
				Win32PrinterStatusEnum.NotAvailable => "Not available",
				Win32PrinterStatusEnum.Waiting => "Waiting",
				Win32PrinterStatusEnum.Processing => "Processing",
				Win32PrinterStatusEnum.Initialization => "Initialization",
				Win32PrinterStatusEnum.WarmingUp => "Warming up",
				Win32PrinterStatusEnum.TonerLow => "Toner low",
				Win32PrinterStatusEnum.NoToner => "No toner",
				Win32PrinterStatusEnum.PagePunt => "Page punt",
				Win32PrinterStatusEnum.UserInterventionRequired => "User intervention required",
				Win32PrinterStatusEnum.OutOfMemory => "Out of memory",
				Win32PrinterStatusEnum.DoorOpen => "Door open",
				Win32PrinterStatusEnum.ServerUnknown => "Server unknown",
				Win32PrinterStatusEnum.PowerSave => "Power save",
				_ => "Status reading error!",
			},
		};
	}

	#endregion
}