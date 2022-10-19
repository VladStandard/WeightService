// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Models;
using DataCore.Schedulers;
using DataCore.Settings;
using DataCore.Sql.Core;
using DataCore.Sql.TableScaleModels;
using DataCore.Wmi;
using Gma.System.MouseKeyHook;
using MDSoft.BarcodePrintUtils.Wmi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using DataCore.Helpers;
using WeightCore.Gui;
using WeightCore.Helpers;
using WeightCore.Managers;

namespace ScalesUI.Forms;

/// <summary>
/// Main form.
/// </summary>
public partial class MainForm : Form
{
    #region Private fields and properties - Helpers

    private AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
    private DebugHelper Debug { get; } = DebugHelper.Instance;
    private ProcHelper Proc { get; } = ProcHelper.Instance;
    private QuartzHelper Quartz { get; } = QuartzHelper.Instance;
    private UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;

    #endregion

    #region Public and private fields, properties, constructor

    private Button ButtonDevice { get; set; }
    private Button ButtonPackage { get; set; }
    private Button ButtonKneading { get; set; }
    private Button ButtonMore { get; set; }
    private Button ButtonNewPallet { get; set; }
    private Button ButtonOrder { get; set; }
    private Button ButtonPlu { get; set; }
    private Button ButtonPrint { get; set; }
    private Button ButtonScalesInit { get; set; }
    private Button ButtonScalesTerminal { get; set; }
    private FontsSettingsHelper FontsSettings { get; } = FontsSettingsHelper.Instance;
    private readonly object _lockerHours = new();
    private readonly object _lockerDays = new();
    private IKeyboardMouseEvents KeyboardMouseEvents { get; set; }
    private bool IsKeyboardMouseEventsSubscribe { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public MainForm()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods - MainForm

    private void MainForm_Load(object sender, EventArgs e)
    {
        try
        {
            UserSession.StopwatchMain = Stopwatch.StartNew();
            UserSession.StopwatchMain.Restart();
            UserSession.DataAccess.SetupLog(UserSession.HostName, typeof(Program).Assembly.GetName().Name);
            FormBorderStyle = Debug.IsDebug ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
            TopMost = !Debug.IsDebug;
            MainForm_ButtonsCreate();
            MainForm_LoadResources();
            UserSession.NewPallet();
        }
        catch (Exception ex)
        {
            ActionMakeScreenShot();
            GuiUtils.WpfForm.CatchException(this, ex);
		}
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            SetComboBoxItems(fieldLang, FieldLang_SelectedIndexChanged, LocaleCore.Scales.ListLanguages);
            UserSession.StopwatchMain.Stop();
        }

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                Quartz.AddJob(QuartzUtils.CronExpression.EveryHours(), ScheduleEveryHours,
                    $"job{nameof(ScheduleEveryHours)}", $"trigger{nameof(ScheduleEveryHours)}",
                    $"triggerGroup{nameof(ScheduleEveryHours)}");
				Quartz.AddJob(QuartzUtils.CronExpression.EveryDays(), ScheduleEveryDays,
	                $"job{nameof(ScheduleEveryDays)}", $"trigger{nameof(ScheduleEveryDays)}", 
	                $"triggerGroup{nameof(ScheduleEveryDays)}");
                LoadManagerControl();
            }
            catch (Exception ex)
            {
	            ActionMakeScreenShot();
				GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                SetComboBoxItems(fieldLang, FieldLang_SelectedIndexChanged, LocaleCore.Scales.ListLanguages);
                UserSession.DataAccess.LogInformation($"{LocaleCore.Scales.ScreenResolution}: {Width} x {Height}",
                    UserSession.HostName, nameof(ScalesUI));
                UserSession.DataAccess.LogInformation(
                    LocaleData.Program.IsLoaded +
                    $" {nameof(UserSession.StopwatchMain.Elapsed)}: {UserSession.StopwatchMain.Elapsed}.",
                    UserSession.HostName, nameof(ScalesUI));

                UserSession.StopwatchMain.Stop();
                ActionMakeScreenShot();
            }
        }).ConfigureAwait(false);
    }

    private void MainForm_LoadResources()
    {
        try
        {
            System.Resources.ResourceManager resourceManager = new("ScalesUI.Properties.Resources", Assembly.GetExecutingAssembly());
            object exit = resourceManager.GetObject("exit_1");
            if (exit is not null)
            {
                Bitmap bmpExit = new((Bitmap)exit);
                pictureBoxClose.Image = bmpExit;
            }

            MDSoft.WinFormsUtils.InvokeControl.SetText(this, AppVersion.AppTitle);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldProductDate, string.Empty);
        }
        catch (Exception ex)
        {
	        ActionMakeScreenShot();
			GuiUtils.WpfForm.CatchException(ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private void LoadManagerControl()
    {
        try
        {
            // Labels.
            UserSession.ManagerControl.Labels.Init(fieldTitle, fieldPlu, fieldSscc,
                labelProductDate, fieldProductDate, labelKneading, fieldKneading, fieldResolution, fieldLang,
                ButtonDevice, ButtonPackage, ButtonKneading, ButtonMore, ButtonNewPallet, ButtonOrder, ButtonPlu, ButtonPrint,
                ButtonScalesInit, ButtonScalesTerminal, pictureBoxClose,
                fieldPrintMainManager, fieldPrintShippingManager, fieldMassaManager);
            UserSession.ManagerControl.Labels.Open();
            // Memory.
            UserSession.ManagerControl.Memory.Init(fieldMemory, fieldTasks);
            UserSession.ManagerControl.Memory.Open();
            // Massa.
            UserSession.ManagerControl.Massa.Init(labelNettoWeight, fieldNettoWeight, 
	            labelPackageWeight, fieldPackageWeight,
                fieldMassaThreshold, fieldMassaGet, fieldMassaPluDescription);
            UserSession.ManagerControl.Massa.Open();
            // PrintMain.
            if (UserSession.Scale.PrinterMain is not null)
                UserSession.ManagerControl.PrintMain.Init(UserSession.PrintBrandMain, UserSession.Scale.PrinterMain,
                    fieldPrintMain, true);
            UserSession.ManagerControl.PrintMain.Open(true);
            UserSession.ManagerControl.PrintMain.SetOdometorUserLabel(1);
            // PrintShipping.
            if (UserSession.Scale.IsShipping)
            {
                if (UserSession.Scale.PrinterShipping is not null)
                    UserSession.ManagerControl.PrintShipping.Init(UserSession.PrintBrandShipping,
                        UserSession.Scale.PrinterShipping,
                        fieldPrintShipping, false);
                UserSession.ManagerControl.PrintShipping.Open(false);
                UserSession.ManagerControl.PrintShipping.SetOdometorUserLabel(1);
            }
            //ButtonScalesInit_Click(sender, e);
            UserSession.ManagerControl.Labels.SetControlsVisible();
        }
        catch (Exception ex)
        {
	        ActionMakeScreenShot();
			GuiUtils.WpfForm.CatchException(ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        try
        {
            KeyboardMouseUnsubscribe();
            GuiUtils.WpfForm.Dispose();
            UserSession.StopwatchMain.Restart();
            if (Quartz is not null)
            {
                Quartz.Close();
                Quartz.Dispose();
            }
            UserSession.ManagerControl.Labels.Close();
            UserSession.ManagerControl.Massa.Close();
            UserSession.ManagerControl.Memory.Close();
            UserSession.ManagerControl.PrintMain.Close();
            UserSession.ManagerControl.PrintShipping.Close();
            UserSession.ManagerControl.Close();
            UserSession.ManagerControl.Dispose();
        }
        catch (Exception ex)
        {
	        ActionMakeScreenShot();
			GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            UserSession.DataAccess.LogInformation(LocaleData.Program.IsClosed +
                    $" {nameof(UserSession.StopwatchMain.Elapsed)}: {UserSession.StopwatchMain.Elapsed}.",
                    UserSession.HostName, nameof(ScalesUI));
            UserSession.StopwatchMain.Stop();
            ActionMakeScreenShot();
        }
    }

    private void MainForm_FontsSet()
    {
        fieldResolution.Font = FontsSettings.FontMinimum;
        fieldLang.Font = FontsSettings.FontMinimum;

        fieldTitle.Font = FontsSettings.FontLabelsTitle;

        fieldNettoWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldPackageWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldPlu.Font = FontsSettings.FontLabelsMaximum;
        fieldProductDate.Font = FontsSettings.FontLabelsMaximum;

        fieldMassaThreshold.Font = FontsSettings.FontLabelsGray;
        fieldSscc.Font = FontsSettings.FontLabelsGray;
        fieldTasks.Font = FontsSettings.FontLabelsGray;
        fieldPrintMain.Font = FontsSettings.FontLabelsGray;
        fieldPrintShipping.Font = FontsSettings.FontLabelsGray;
        fieldMassaPluDescription.Font = FontsSettings.FontLabelsGray;
        fieldMassaManager.Font = FontsSettings.FontLabelsGray;
        fieldMassaGet.Font = FontsSettings.FontLabelsGray;
        fieldPrintMainManager.Font = FontsSettings.FontLabelsGray;
        fieldPrintShippingManager.Font = FontsSettings.FontLabelsGray;
        fieldMemory.Font = FontsSettings.FontLabelsGray;

        labelNettoWeight.Font = FontsSettings.FontLabelsBlack;
        labelPackageWeight.Font = FontsSettings.FontLabelsBlack;
        labelKneading.Font = FontsSettings.FontLabelsBlack;
        fieldKneading.Font = FontsSettings.FontLabelsBlack;
        labelProductDate.Font = FontsSettings.FontLabelsBlack;

        if (ButtonDevice is not null)
            ButtonDevice.Font = FontsSettings.FontButtonsSmall;
		if (ButtonPlu is not null)
			ButtonPlu.Font = FontsSettings.FontButtonsSmall;
        if (ButtonPackage is not null)
	        ButtonPackage.Font = FontsSettings.FontButtonsSmall;

        if (ButtonScalesTerminal is not null)
            ButtonScalesTerminal.Font = FontsSettings.FontButtons;
        if (ButtonScalesInit is not null)
            ButtonScalesInit.Font = FontsSettings.FontButtons;
        if (ButtonOrder is not null)
            ButtonOrder.Font = FontsSettings.FontButtons;
        if (ButtonNewPallet is not null)
            ButtonNewPallet.Font = FontsSettings.FontButtons;
        if (ButtonKneading is not null)
            ButtonKneading.Font = FontsSettings.FontButtons;
        if (ButtonMore is not null)
            ButtonMore.Font = FontsSettings.FontButtons;
        if (ButtonPrint is not null)
            ButtonPrint.Font = FontsSettings.FontButtons;
    }

    private void MainForm_ButtonsCreate()
    {
        ActionSettingsModel buttonsSettings = new()
        {
            // Device.
            IsDevice = true,
            IsPlu = true,
            IsPackage = true,
            // Actions.
            IsKneading = false,
            IsMore = true,
            IsNewPallet = true,
            IsOrder = UserSession.Scale.IsOrder,
            IsPrint = true,
            IsScalesInit = false,
            IsScalesTerminal = true,
        };

        MainForm_ButtonsCreate_TableDevice(buttonsSettings);
        MainForm_ButtonsCreate_TableActions(buttonsSettings);
    }

    private void MainForm_ButtonsCreate_TableDevice(ActionSettingsModel buttonsSettings)
    {
	    TableLayoutPanel tableLayoutPanelDevice = GuiUtils.WinForm.NewTableLayoutPanel(tableLayoutPanelMain, nameof(tableLayoutPanelDevice),
		    1, 14, 1, 98);
	    int rowCount = 0;

		if (buttonsSettings.IsDevice)
	    {
		    ButtonDevice = GuiUtils.WinForm.NewTableLayoutPanelButton(tableLayoutPanelDevice, nameof(ButtonDevice), 1, rowCount++);
		    ButtonDevice.Click += ActionDevice;
	    }

		if (buttonsSettings.IsPlu)
		{
			ButtonPlu = GuiUtils.WinForm.NewTableLayoutPanelButton(tableLayoutPanelDevice, nameof(ButtonPlu), 1, rowCount++);
			ButtonPlu.Click += ActionPlu;
		}

	    if (buttonsSettings.IsPackage)
	    {
		    ButtonPackage = GuiUtils.WinForm.NewTableLayoutPanelButton(tableLayoutPanelDevice, nameof(ButtonPackage), 1, rowCount++);
		    ButtonPackage.Click += ActionPackage;
	    }

	    tableLayoutPanelDevice.ColumnCount = 1;
		GuiUtils.WinForm.SetTableLayoutPanelColumnStyles(tableLayoutPanelDevice);
		tableLayoutPanelDevice.RowCount = rowCount;
		GuiUtils.WinForm.SetTableLayoutPanelRowStyles(tableLayoutPanelDevice);
	}

    private void MainForm_ButtonsCreate_TableActions(ActionSettingsModel buttonsSettings)
    {
	    TableLayoutPanel tableLayoutPanelActions = GuiUtils.WinForm.NewTableLayoutPanel(tableLayoutPanelMain, nameof(tableLayoutPanelActions),
		    2, 14, tableLayoutPanelMain.ColumnCount - 2, 99);
	    int columnCount = 0;

	    if (buttonsSettings.IsScalesTerminal)
	    {
		    ButtonScalesTerminal =
			    GuiUtils.WinForm.NewTableLayoutPanelButton(tableLayoutPanelActions, nameof(ButtonScalesTerminal), columnCount++, 0);
		    ButtonScalesTerminal.Click += ActionScalesTerminal;
	    }

	    if (buttonsSettings.IsScalesInit)
	    {
		    ButtonScalesInit =
			    GuiUtils.WinForm.NewTableLayoutPanelButton(tableLayoutPanelActions, nameof(ButtonScalesInit), columnCount++, 0);
		    ButtonScalesInit.Click += ActionScalesInit;
	    }

	    if (buttonsSettings.IsOrder)
	    {
		    ButtonOrder =
			    GuiUtils.WinForm.NewTableLayoutPanelButton(tableLayoutPanelActions, nameof(ButtonOrder), columnCount++, 0);
			ButtonOrder.Click += ActionOrder;
	    }

	    if (buttonsSettings.IsNewPallet)
	    {
		    ButtonNewPallet =
			    GuiUtils.WinForm.NewTableLayoutPanelButton(tableLayoutPanelActions, nameof(ButtonNewPallet), columnCount++, 0);
			ButtonNewPallet.Click += ActionNewPallet;
	    }

	    if (buttonsSettings.IsKneading)
	    {
		    ButtonKneading =
			    GuiUtils.WinForm.NewTableLayoutPanelButton(tableLayoutPanelActions, nameof(ButtonKneading), columnCount++, 0);
			ButtonKneading.Click += ActionKneading;
	    }

	    if (buttonsSettings.IsMore)
	    {
		    ButtonMore = GuiUtils.WinForm.NewTableLayoutPanelButton(tableLayoutPanelActions, nameof(ButtonMore), columnCount++, 0);
			ButtonMore.Click += ActionMore;
	    }

	    if (buttonsSettings.IsPrint)
	    {
		    ButtonPrint =
			    GuiUtils.WinForm.NewTableLayoutPanelButton(tableLayoutPanelActions, nameof(ButtonPrint), columnCount++, 0);
			ButtonPrint.Click += ActionPrint;
		    ButtonPrint.Focus();
	    }

	    tableLayoutPanelActions.ColumnCount = columnCount;
		GuiUtils.WinForm.SetTableLayoutPanelColumnStyles(tableLayoutPanelActions);
		tableLayoutPanelActions.RowCount = 1;
		GuiUtils.WinForm.SetTableLayoutPanelRowStyles(tableLayoutPanelActions);
	}

    #endregion

    #region Public and private methods - Schedulers

    private void ScheduleEveryHours()
    {
        lock (_lockerHours)
        {
            if (Quartz is null)
                return;
	        ActionMakeScreenShot();
	        //UserSession.DataAccess.LogInformation(LocaleCore.Scales.ScheduleForNextHour, UserSession.HostName, nameof(ScalesUI));
        }
    }

    private void ScheduleEveryDays()
    {
        lock (_lockerDays)
        {
            if (Quartz is null)
                return;
            ActionMakeScreenShot();
			UserSession.ProductDate = DateTime.Now;
			//UserSession.DataAccess.LogInformation(LocaleCore.Scales.ScheduleForNextDay, UserSession.HostName, nameof(ScalesUI));
        }
    }

    #endregion

    #region Public and private methods - MouseHook

    private void KeyboardMouseSubscribe()
    {
        if (!IsKeyboardMouseEventsSubscribe)
        {
            KeyboardMouseEvents = Hook.GlobalEvents();
            KeyboardMouseEvents.MouseDownExt += MouseDownExt;
            //KeyboardMouseEvents.KeyUp += KeyUpExt;
        }
        IsKeyboardMouseEventsSubscribe = true;
    }

    private void KeyboardMouseUnsubscribe()
    {
        if (IsKeyboardMouseEventsSubscribe)
        {
            KeyboardMouseEvents.MouseDownExt -= MouseDownExt;
            //KeyboardMouseEvents.KeyUp += KeyUpExt;
            KeyboardMouseEvents.Dispose();
        }
        IsKeyboardMouseEventsSubscribe = false;
    }

    private void MouseDownExt(object sender, MouseEventExtArgs e)
    {
        if (e.Button == MouseButtons.Middle)
            ActionPrint(sender, e);
    }

    #endregion

    #region Public and private methods - Controls

    private void SetComboBoxItems(ComboBox comboBox, EventHandler eventHandler, List<string> sourceList, int selectedIndex = 0)
    {
        if (comboBox is null || sourceList is null || sourceList.Count == 0 || selectedIndex < 0)
            return;

        if (comboBox.InvokeRequired)
        {
            comboBox.Invoke((Action)delegate
            {
                SetComboBoxItemsWork(comboBox, eventHandler, sourceList, selectedIndex);
            });
        }
        else
            SetComboBoxItemsWork(comboBox, eventHandler, sourceList, selectedIndex);
    }

    private void SetComboBoxItemsWork(ComboBox comboBox, EventHandler eventHandler, List<string> sourceList, int selectedIndex = 0)
    {
        comboBox.SelectedIndexChanged -= eventHandler;

        int backupIndex = comboBox.SelectedIndex;
        comboBox.Items.Clear();
        if (sourceList.Any())
        {
            foreach (string source in sourceList)
            {
                comboBox.Items.Add(source);
            }
        }
        comboBox.SelectedIndex = selectedIndex <= 0
            ? backupIndex <= 0 ? 0 : backupIndex
            : selectedIndex < comboBox.Items.Count ? selectedIndex : 0;

        comboBox.SelectedIndexChanged += eventHandler;
        eventHandler.Invoke(comboBox, null);
    }

    private void FieldPrintManager_Click(object sender, EventArgs e)
    {
        using WpfPageLoader wpfPageLoader = new(PageEnum.MessageBox, false, FormBorderStyle.FixedDialog, 22, 16, 16) { Width = 700, Height = 450 };
        wpfPageLoader.Text = LocaleCore.Print.InfoCaption;
        wpfPageLoader.MessageBox.Caption = LocaleCore.Print.InfoCaption;
        wpfPageLoader.MessageBox.Message = GetPrintInfo(UserSession.ManagerControl.PrintMain, true);
        if (UserSession.Scale.IsShipping)
        {
            wpfPageLoader.MessageBox.Message += Environment.NewLine + Environment.NewLine +
                GetPrintInfo(UserSession.ManagerControl.PrintShipping, false);
            wpfPageLoader.Height = 700;
        }
        wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
        wpfPageLoader.MessageBox.VisibilitySettings.ButtonCustomVisibility = Visibility.Visible;
        wpfPageLoader.MessageBox.VisibilitySettings.ButtonCustomContent = LocaleCore.Print.ClearQueue;
        DialogResult result = wpfPageLoader.ShowDialog(this);
        wpfPageLoader.Close();
        if (result == DialogResult.Retry)
        {
            UserSession.ManagerControl.PrintMain.ClearPrintBuffer(1);
            if (UserSession.Scale.IsShipping)
                UserSession.ManagerControl.PrintShipping.ClearPrintBuffer(1);
        }
    }

    private string GetPrintInfo(ManagerPrint managerPrint, bool isMain)
    {
        string peeler = isMain
            ? UserSession.ManagerControl.PrintMain.ZebraPeelerStatus : UserSession.ManagerControl.PrintShipping.ZebraPeelerStatus;
        string printMode = isMain
            ? UserSession.ManagerControl.PrintMain.GetZebraPrintMode() :
            UserSession.ManagerControl.PrintShipping.GetZebraPrintMode();
        WmiWin32PrinterEntity wmiPrinter = managerPrint.TscWmiPrinter;
        return
            $"{managerPrint.GetDeviceName(isMain)}" + Environment.NewLine +
            $"{LocaleCore.Print.DeviceCommunication} ({managerPrint.Printer.Ip}): {managerPrint.Printer.PingStatus}" + Environment.NewLine +
            $"{LocaleCore.Print.PrinterStatus}: {managerPrint.GetDeviceStatus()}" + Environment.NewLine +
            Environment.NewLine +
            $"{LocaleCore.Print.Driver}: {wmiPrinter.DriverName}" + Environment.NewLine +
            $"{LocaleCore.Print.Port}: {wmiPrinter.PortName}" + Environment.NewLine +
            $"{LocaleCore.Print.StateCode}: {wmiPrinter.PrinterState}" + Environment.NewLine +
            $"{LocaleCore.Print.StatusCode}: {wmiPrinter.PrinterStatus}" + Environment.NewLine +
            $"{LocaleCore.Print.Status}: {managerPrint.GetPrinterStatusDescription(LocaleCore.Lang, wmiPrinter.PrinterStatus)}" + Environment.NewLine +
            $"{LocaleCore.Print.State} (ENG): {wmiPrinter.Status}" + Environment.NewLine +
            $"{LocaleCore.Print.State}: {WmiHelper.Instance.GetStatusDescription(LocaleCore.Lang, wmiPrinter.Status)}" + Environment.NewLine +
            $"{LocaleCore.Print.SensorPeeler}: {peeler}" + Environment.NewLine +
            $"{LocaleCore.Print.Mode}: {printMode}" + Environment.NewLine;
    }

    private void FieldSscc_Click(object sender, EventArgs e)
    {
        using WpfPageLoader wpfPageLoader = new(PageEnum.MessageBox, false, FormBorderStyle.FixedDialog, 26, 20, 18) { Width = 700, Height = 400 };
        wpfPageLoader.Text = LocaleCore.Scales.FieldSsccShort;
        wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.FieldSscc;
        wpfPageLoader.MessageBox.Message =
            $"{LocaleCore.Scales.FieldSscc}: {UserSession.ProductSeries.Sscc.Sscc}" + Environment.NewLine +
            $"{LocaleCore.Scales.FieldSsccGln}: {UserSession.ProductSeries.Sscc.Gln}" + Environment.NewLine +
            $"{LocaleCore.Scales.FieldSsccUnitId}: {UserSession.ProductSeries.Sscc.UnitId}" + Environment.NewLine +
            $"{LocaleCore.Scales.FieldSsccUnitType}: {UserSession.ProductSeries.Sscc.UnitType}" + Environment.NewLine +
            $"{LocaleCore.Scales.FieldSsccSynonym}: {UserSession.ProductSeries.Sscc.SynonymSscc}" + Environment.NewLine +
            $"{LocaleCore.Scales.FieldSsccControlNumber}: {UserSession.ProductSeries.Sscc.Check}";
        wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
        wpfPageLoader.MessageBox.VisibilitySettings.Localization();
        wpfPageLoader.ShowDialog(this);
        wpfPageLoader.Close();
    }

    private void FieldTasks_Click(object sender, EventArgs e)
    {
        string message = string.Empty;
        foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
        {
            message += $"{LocaleCore.Scales.ThreadId}: {thread.Id}. " +
                $"{LocaleCore.Scales.ThreadPriorityLevel}: {thread.PriorityLevel}. " +
                $"{LocaleCore.Scales.ThreadState}: {thread.ThreadState}. " +
                $"{LocaleCore.Scales.ThreadStartTime}: {thread.StartTime}. " + Environment.NewLine;
        }
        using WpfPageLoader wpfPageLoader = new(PageEnum.MessageBox, false, FormBorderStyle.FixedDialog,
            20, 14, 18, 0, 12) { Width = Width - 50, Height = Height - 50 };
        wpfPageLoader.Text = $@"{LocaleCore.Scales.ThreadsCount}: {Process.GetCurrentProcess().Threads.Count}";
        wpfPageLoader.MessageBox.Message = message;
        wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
        wpfPageLoader.MessageBox.VisibilitySettings.Localization();
        wpfPageLoader.ShowDialog(this);
        wpfPageLoader.Close();
    }

    private void FieldResolution_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (fieldResolution.Items[fieldResolution.SelectedIndex])
            {
                case "800x600":
                    WindowState = FormWindowState.Normal;
                    Size = new(800, 600);
                    break;
                case "1024x768":
                    WindowState = FormWindowState.Normal;
                    Size = new(1024, 768);
                    break;
                case "1366x768":
                    WindowState = FormWindowState.Normal;
                    Size = new(1366, 768);
                    break;
                case "1600x1024":
                    WindowState = FormWindowState.Normal;
                    Size = new(1600, 1024);
                    break;
                case "1920x1080":
                    WindowState = FormWindowState.Normal;
                    Size = new(1920, 1080);
                    break;
                default:
                    WindowState = FormWindowState.Maximized;
                    break;
            }
            CenterToScreen();
            FontsSettings.Transform(Width, Height);
            MainForm_FontsSet();
        }
        catch (Exception ex)
        {
	        ActionMakeScreenShot();
			GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private void FieldLang_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LocaleCore.Lang = LocaleData.Lang = fieldLang.SelectedIndex switch { 1 => LangEnum.English, _ => LangEnum.Russian, };
            string area = UserSession.Scale.WorkShop is null 
	            ? LocaleCore.Table.FieldNull : UserSession.Area.Name;
            MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonDevice, 
	            UserSession.Scale.Description + Environment.NewLine + area);
            MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPackage, UserSession.PluPackage.IdentityIsNew
				? LocaleCore.Table.FieldPackageIsNotSelected 
	            : UserSession.PluPackage.Name + Environment.NewLine + 
	              $"{LocaleCore.Table.PackageWeightKg}: {UserSession.PluPackage.Package.Weight}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonScalesTerminal, LocaleCore.Scales.ButtonRunScalesTerminal);
            MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonScalesInit, LocaleCore.Scales.ButtonScalesInitShort);
            MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonOrder, LocaleCore.Scales.ButtonSelectOrder);
            MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonNewPallet, LocaleCore.Scales.ButtonNewPallet);
            MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonKneading, LocaleCore.Scales.ButtonAddKneading);
            List<PluScaleModel> pluScales = UserSession.DataAccess.GetListPluScales(UserSession.Scale,
                false, false, false);
            MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPlu, LocaleCore.Scales.ButtonSelectPlu(pluScales.Count));
            MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonMore, LocaleCore.Scales.ButtonSetKneading);
            MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPrint, LocaleCore.Print.ActionPrint);
            SetComboBoxItems(fieldResolution, FieldResolution_SelectedIndexChanged, LocaleCore.Scales.ListResolutions,
                Debug.IsDebug ? 2 : LocaleCore.Scales.ListResolutions.Count - 1);
        }
        catch (Exception ex)
        {
	        ActionMakeScreenShot();
			GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    #endregion

    #region Public and private methods - Actions

    private void ActionClose(object sender, EventArgs e)
    {
        Close();
    }

    private void ActionDevice(object sender, EventArgs e)
    {
        try
        {
            UserSession.ManagerControl.Massa.Close();

            using WpfPageLoader wpfPageLoader = new(PageEnum.Device, false) { Width = 600, Height = 225 };
            DialogResult dialogResult = wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
            if (dialogResult == DialogResult.OK)
            {
                UserSession.Setup(wpfPageLoader.PageDevice.UserSession.Scale.Identity.Id, UserSession.Area.Name);
            }
            FieldLang_SelectedIndexChanged(sender, e);

            UserSession.ManagerControl.Massa.Open();
        }
        catch (Exception ex)
        {
	        ActionMakeScreenShot();
			GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private void ActionPackage(object sender, EventArgs e)
    {
        try
        {
	        if (!ActionPackageCheckPlu()) return;
            
	        UserSession.ManagerControl.Massa.Close();

			using WpfPageLoader wpfPageLoader = new(PageEnum.Package, false) { Width = 600, Height = 225 };
            DialogResult dialogResult = wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
            if (dialogResult == DialogResult.OK)
            {
                //UserSession.Setup(wpfPageLoader.PageDevice.Scale.Identity.Id);
            }
            FieldLang_SelectedIndexChanged(sender, e);

            UserSession.ManagerControl.Massa.Open();
        }
        catch (Exception ex)
        {
			ActionMakeScreenShot();
			GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private bool ActionPackageCheckPlu()
    {
	    switch (UserSession.PluScale.Plu.IdentityIsNew)
	    {
		    case true:
		    {
			    using WpfPageLoader wpfPageLoader = new(PageEnum.MessageBox, false, FormBorderStyle.FixedDialog, 22, 16, 16)
				    { Width = 700, Height = 450 };
			    wpfPageLoader.Text = LocaleCore.Action.ActionAccessDeny;
			    wpfPageLoader.MessageBox.Caption = LocaleCore.Table.FieldPluIsNotSelected;
			    wpfPageLoader.MessageBox.Message = LocaleCore.Table.FieldPluMustBeSelected;
			    wpfPageLoader.MessageBox.VisibilitySettings.ButtonCancelVisibility = Visibility.Visible;
			    _ = wpfPageLoader.ShowDialog(this);
			    wpfPageLoader.Close();
			    return false;
		    }
		    default:
			    return true;
	    }
    }

    private void ActionScalesTerminal(object sender, EventArgs e)
    {
        try
        {
	        DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(this,
		        $"{LocaleCore.Scales.QuestionRunApp} ScalesTerminal?",
		        true, LogTypeEnum.Question,
		        new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
		        UserSession.HostName, nameof(ScalesUI));
	        if (result is not DialogResult.Yes)
		        return;

	        // Run app.
            if (File.Exists(LocaleData.Paths.ScalesTerminal))
            {
                UserSession.ManagerControl.Massa.Close();
                Proc.Run(LocaleData.Paths.ScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
            }
            else
            {
                GuiUtils.WpfForm.ShowNewOperationControl(this,
                    LocaleCore.Scales.ProgramNotFound(
	                    LocaleData.Paths.ScalesTerminal), true, LogTypeEnum.Warning,
						new() { ButtonOkVisibility = Visibility.Visible },
						UserSession.HostName, nameof(ScalesUI));
            }
            UserSession.ManagerControl.Open();
        }
        catch (Exception ex)
        {
			ActionMakeScreenShot();
			GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private void ActionScalesInit(object sender, EventArgs e)
    {
        try
        {
            if (!UserSession.PluScale.Plu.IsCheckWeight)
            {
                GuiUtils.WpfForm.ShowNewOperationControl(this, 
                    LocaleCore.Scales.PluNotSelectWeight, true, LogTypeEnum.Warning,
                    new() { ButtonOkVisibility = Visibility.Visible },
                    UserSession.HostName, nameof(ScalesUI));
                return;
            }
            if (!UserSession.ManagerControl.Massa.MassaDevice.IsOpenPort)
            {
                GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.MassaIsNotRespond, true, LogTypeEnum.Warning,
                    new() { ButtonOkVisibility = Visibility.Visible },
                    UserSession.HostName, nameof(ScalesUI));
                return;
            }

            DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(this,
	            LocaleCore.Scales.QuestionPerformOperation, true, LogTypeEnum.Question,
	            new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
	            UserSession.HostName, nameof(ScalesUI));
            if (result is not DialogResult.Yes)
	            return;

            UserSession.ManagerControl.Massa.Close();

            // Fix negative weight.
            if (UserSession.ManagerControl.Massa.WeightNet < 0)
            {
                UserSession.ManagerControl.Massa.ResetMassa();
            }

            UserSession.CheckWeightMassaDeviceExists(this);
            UserSession.PluScale = new();

            UserSession.ManagerControl.Massa.Open();
            UserSession.ManagerControl.Massa.GetInit();
        }
        catch (Exception ex)
        {
			ActionMakeScreenShot();
			GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private void ActionOrder(object sender, EventArgs e)
    {
        throw new("Order is under construct!");
        //try
        //{
        //    UserSession.ManagerControl.Massa.Close();

        //    if (UserSession.Order is null)
        //    {
        //        using OrderListForm settingsForm = new();
        //        settingsForm.ShowDialog(this);
        //        settingsForm.Close();
        //        settingsForm.Dispose();
        //    }
        //    else
        //    {
        //        using OrderDetailForm settingsForm = new();
        //        DialogResult result = settingsForm.ShowDialog(this);
        //        settingsForm.Close();
        //        settingsForm.Dispose();
        //        if (result == DialogResult.Retry)
        //        {
        //            UserSession.Order = null;
        //        }
        //        if (result == DialogResult.OK)
        //        {
        //            //ws.Kneading = (int)settingsForm.currentKneading;
        //        }
        //        if (result == DialogResult.Cancel)
        //        {
        //            //ws.Kneading = (int)settingsForm.currentKneading;
        //        }
        //    }
        //    //if (UserSession.Order is not null)
        //    //{
        //    //    MDSoft.WinFormsUtils.InvokeProgressBar.SetMaximum(fieldPrintProgressMain, UserSession.Order.PlaneBoxCount);
        //    //    MDSoft.WinFormsUtils.InvokeProgressBar.SetMinimum(fieldPrintProgressMain, 0);
        //    //    MDSoft.WinFormsUtils.InvokeProgressBar.SetValue(fieldPrintProgressMain, UserSession.Order.FactBoxCount);
        //    //}

        //    UserSession.ManagerControl.Massa.Open();
        //}
        //catch (Exception ex)
        //{
        //    GuiUtils.WpfForm.CatchException(this, ex);
        //}
        //finally
        //{
        //    MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        //}
    }

    private void ActionNewPallet(object sender, EventArgs e)
    {
        try
        {
            UserSession.NewPallet();
        }
        catch (Exception ex)
        {
			ActionMakeScreenShot();
			GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private void ActionKneading(object sender, EventArgs e)
    {
        try
        {
            using NumberInputForm numberInputForm = new() { InputValue = 0 };
            DialogResult result = numberInputForm.ShowDialog(this);
            numberInputForm.Close();
            numberInputForm.Dispose();
            if (result == DialogResult.OK)
                UserSession.WeighingSettings.Kneading = (byte)numberInputForm.InputValue;
        }
        catch (Exception ex)
        {
            GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private void ActionPlu(object sender, EventArgs e)
    {
        try
        {
            KeyboardMouseUnsubscribe();
            UserSession.PluScale = new();
            if (UserSession.CheckWeightMassaDeviceExists(this))
            {
                if (!UserSession.CheckWeightIsNegative(this) || !UserSession.CheckWeightIsPositive(this))
                    return;
            }
            UserSession.ManagerControl.Massa.Close();

            // PLU form.
            using PlusForm pluListForm = new() { Owner = this };
            DialogResult result = pluListForm.ShowDialog(this);
            pluListForm.Close();
            pluListForm.Dispose();
            if (result == DialogResult.OK)
            {
                UserSession.WeighingSettings.Kneading = 1;
                UserSession.ProductDate = DateTime.Now;
                UserSession.NewPallet();
                //_mkDevice.SetTareWeight((int) (_sessionState.CurrentPLU.GoodsTareWeight * _sessionState.CurrentPLU.Scale.ScaleFactor));

                // форма с замесами, размерами паллет и прочее
                ActionMore(null, null);
            }
            else
            {
                UserSession.PluScale = new();
            }
            FieldLang_SelectedIndexChanged(sender, e);

            UserSession.ManagerControl.Massa.Open();
            KeyboardMouseSubscribe();
            UserSession.ManagerControl.Labels.SetControlsVisible();
        }
        catch (Exception ex)
        {
	        ActionMakeScreenShot();
            GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private void ActionMore(object sender, EventArgs e)
    {
        try
        {
            if (UserSession.PluScale.IdentityIsNew)
            {
                GuiUtils.WpfForm.ShowNewOperationControl(this, 
	                LocaleCore.Scales.PluNotSelect, true, LogTypeEnum.Warning,
	                new() { ButtonOkVisibility = Visibility.Visible },
                    UserSession.HostName, nameof(ScalesUI));
                return;
            }

            UserSession.ManagerControl.Massa.Close();

            using KneadingForm kneadingForm = new() { Owner = this };
            DialogResult result = kneadingForm.ShowDialog();
            kneadingForm.Close();
            kneadingForm.Dispose();
            if (result == DialogResult.OK)
            {
                //_sessionState.Kneading = settingsForm.CurrentKneading;
                //_sessionState.ProductDate = settingsForm.CurrentProductDate;
            }

            UserSession.ManagerControl.Massa.Open();
        }
        catch (Exception ex)
        {
	        ActionMakeScreenShot();
            GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        }
    }

    private void ActionPrint(object sender, EventArgs e)
    {
        try
        {
            UserSession.ManagerControl.PrintMain.IsPrintBusy = true;
            UserSession.ManagerControl.PrintShipping.IsPrintBusy = true;

			if (!UserSession.CheckPluIsEmpty(this)) return;
			//if (UserSession.PluScale.Plu.IsCheckWeight && UserSession.PluPackages.Count > 1 && UserSession.PluPackage.IdentityIsNew)
			if (UserSession.PluPackages.Count > 1 && UserSession.PluPackage.IdentityIsNew)
	            ActionPackage(sender, e);
			if (!UserSession.CheckPluPackageIsEmpty(this)) return;
            if (!UserSession.CheckWeightMassaDeviceExists(this)) return;
            if (!UserSession.CheckWeightMassaIsStable(this)) return;

			// Set fake data for PLU weighing.
			UserSession.SetPluWeighingFake(this);
            if (!UserSession.CheckWeightIsNegative(this)) return;
			if (!UserSession.CheckWeightThresholds(this)) return;
            UserSession.NewPluWeighing();

            // Check printers connections.
            if (!UserSession.CheckPrintIsConnect(this, UserSession.ManagerControl.PrintMain, true)) return;
            if (UserSession.Scale.IsShipping)
                if (!UserSession.CheckPrintIsConnect(this, UserSession.ManagerControl.PrintShipping, false)) return;
            // Check printers statuses.
            if (!UserSession.CheckPrintStatusReady(this, UserSession.ManagerControl.PrintMain, true)) return;
            if (UserSession.Scale.IsShipping)
                if (!UserSession.CheckPrintStatusReady(this, UserSession.ManagerControl.PrintShipping, false)) return;
            
            // Debug check.
            //if (Debug.IsDebug)
            //{
            //    DialogResult dialogResult = GuiUtils.WpfForm.ShowNewOperationControl(this, 
	           //     LocaleCore.Print.QuestionPrint, true, LogTypeEnum.Question,
            //        new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
            //        UserSession.HostName, nameof(WeightCore));
            //    if (dialogResult is not DialogResult.Yes) return;
            //}

            UserSession.PrintLabel(false);
            //UserSession.Manager.Massa.Open();
        }
        catch (Exception ex)
        {
	        ActionMakeScreenShot();
            if (UserSession.PluScale.IdentityIsNotNew)
                UserSession.DataAccess.LogError(new Exception(
                    $"{LocaleCore.Print.ErrorPlu(UserSession.PluScale.Plu.Number, UserSession.PluScale.Plu.Name)}"),
                    UserSession.HostName);
            GuiUtils.WpfForm.CatchException(this, ex);
        }
        finally
        {
	        UserSession.ManagerControl.Massa.IsWeightNetFake = false;
			UserSession.ManagerControl.PrintMain.IsPrintBusy = false;
            UserSession.ManagerControl.PrintShipping.IsPrintBusy = false;
            MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            //_sessionState.TaskManager.OpenPrintManager(CallbackPrintManagerClose, _sessionState,
            //_sessionState.PrintBrand, _sessionState.CurrentScale);
        }
    }

    private void ActionMakeScreenShot()
    {
	    try
	    {
		    UserSession.MakeScreenShot(this);
	    }
	    catch (Exception ex)
	    {
		    GuiUtils.WpfForm.CatchException(this, ex);
	    }
	    finally
	    {
		    MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
	    }
    }
    
    #endregion
}
