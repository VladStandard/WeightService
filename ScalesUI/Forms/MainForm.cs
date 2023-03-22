// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using MDSoft.BarcodePrintUtils.Wmi.Models;
using System;

namespace ScalesUI.Forms;

/// <summary>
/// Main form.
/// </summary>
public partial class MainForm : Form
{
    #region Public and private fields, properties, constructor

    private AppVersionHelper AppVersion => AppVersionHelper.Instance;
    private DebugHelper Debug => DebugHelper.Instance;
    private ProcHelper Proc => ProcHelper.Instance;
    private QuartzHelper Quartz => QuartzHelper.Instance;
    private UserSessionHelper UserSession => UserSessionHelper.Instance;
    private FontsSettingsHelper FontsSettings => FontsSettingsHelper.Instance;
    private IKeyboardMouseEvents KeyboardMouseEvents { get; set; }
    private ActionSettingsModel ActionSettings { get; set; }
    private Button ButtonDevice { get; set; }
    private Button ButtonPluNestingFk { get; set; }
    private Button ButtonKneading { get; set; }
    private Button ButtonMore { get; set; }
    private Button ButtonNewPallet { get; set; }
    private Button ButtonPlu { get; set; }
    private Button ButtonPrint { get; set; }
    private Button ButtonScalesInit { get; set; }
    private Button ButtonScalesTerminal { get; set; }
    private readonly object _lockerHours = new();
    private readonly object _lockerDays = new();
    private NavigationUserControl NavigationControl { get; set; }
    private PluUserControl PluControl { get; set; }
    private KneadingUserControl KneadingControl { get; set; }
    private WaitUserControl WaitControl { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public MainForm()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods - MainForm

    private void ReturnBackLoad()
    {
        FormBorderStyle = Debug.IsDebug ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
        TopMost = !Debug.IsDebug;
        UserSession.NewPallet();

        MDSoft.WinFormsUtils.InvokeControl.SetText(this, AppVersion.AppTitle);
        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldProductDate, string.Empty);
        LoadMainControls();
        LoadLocalizationStatic(Lang.Russian);

        // Quartz.
        LoadSchedule();

        ActionUtils.ActionMakeScreenShot(this, UserSession.Scale);
        UserSession.StopwatchMain.Stop();

        UserSession.DataAccess.LogInformation(
            $"{LocaleData.Program.IsLoaded}. " + Environment.NewLine +
            $"{LocaleCore.Scales.ScreenResolution}: {Width} x {Height}." + Environment.NewLine +
            $"{nameof(LocaleData.Program.Elapsed)}: {UserSession.StopwatchMain.Elapsed}.");
    }

    private void PreLoadControls()
    {
        // Mouse hook.
        KeyboardMouseEvents = Hook.AppEvents();
        KeyboardMouseEvents.MouseDownExt += MouseDownExt;
        // NavigationControl.
        NavigationControl = new() { ReturnBackAction = ReturnBackDefault };
        NavigationControl.Parent = this;
        NavigationControl.Dock = DockStyle.Fill;
        WaitControl = new();
        PluControl = new();
        PluControl.RefreshAction();
        KneadingControl = new();
        KneadingControl.RefreshAction();
        // Buttons.
        SetButtonsSettings();
        // Form properties: resolution, position, fonts.
        this.SwitchResolution(Debug.IsDebug ? Resolution.Value1366x768 : Resolution.FullScreen);
        CenterToScreen();
        LoadFonts();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                UserSession.StopwatchMain = Stopwatch.StartNew();
                UserSession.StopwatchMain.Restart();
                // Load controls.
                PreLoadControls();
                // Navigate to wait control.
                NavigateToControl(WaitControl, ReturnBackLoad, true, LocaleCore.Scales.AppWaitLoad);
                // Return back.
                WaitControl.ReturnBackAction();
            }, FinallyAction);
    }

    private void LoadMainControls()
    {
        // Memory.
        UserSession.PluginMemory.Init(new(1_000, 0_250), new(0_250, 0_250),
            new(0_250, 0_250), fieldMemory, fieldMemoryExt);
        _ = Task.Run(async () => {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            UserSession.PluginMemory.Execute();
        }).ConfigureAwait(false);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMemoryExt, Debug.IsDebug);

        // Massa.
        UserSession.PluginMassa.Init(new(1_000, 1_000), new(0_100, 0_100), 
            new(0_050, 0_100), fieldNettoWeight, fieldMassa, fieldMassaExt);
        _ = Task.Run(async () => {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            UserSession.PluginMassa.Execute();
        }).ConfigureAwait(false);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMassaExt, Debug.IsDebug);

        // Template.
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldTemplateTitle, true);
        MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldTemplateValue, true);

        // PrintMain.
        if (UserSession.Scale.PrinterMain is not null)
        {
            UserSession.PluginPrintMain.Init(new(0_500, 0_250), new(0_250, 0_250), 
                new(0_250, 0_250),
                UserSession.PrintBrandMain, UserSession.Scale.PrinterMain, fieldPrintMain, fieldPrintMainExt, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldPrintMain, true);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldPrintMainExt, Debug.IsDebug);
            _ = Task.Run(async () => {
                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                UserSession.PluginPrintMain.Execute();
            }).ConfigureAwait(false);
            UserSession.PluginPrintMain.SetOdometorUserLabel(1);
        }

        // PrintShipping.
        if (UserSession.Scale.IsShipping)
        {
            if (UserSession.Scale.PrinterShipping is not null)
            {
                UserSession.PluginPrintShipping.Init(new(0_500, 0_250), 
                    new(0_250, 0_250), new(0_250, 0_250),
                    UserSession.PrintBrandShipping, UserSession.Scale.PrinterShipping, fieldPrintShipping, fieldPrintShippingExt, false);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldPrintShipping, true);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldPrintShippingExt, Debug.IsDebug);
                _ = Task.Run(async () => {
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                    UserSession.PluginPrintShipping.Execute();
                }).ConfigureAwait(false);
                UserSession.PluginPrintShipping.SetOdometorUserLabel(1);
            }
        }

        // Labels.
        UserSession.PluginLabels.Init(
            new(0_250, 0_250), new(0_250, 0_250), 
            new(0_250, 0_250), fieldPlu, fieldSscc, fieldProductDate, fieldKneading);
        _ = Task.Run(async () => {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            UserSession.PluginLabels.Execute();
        }).ConfigureAwait(false);
        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, $"{AppVersionHelper.Instance.AppTitle}. {UserSession.PublishDescription}.");
        MDSoft.WinFormsUtils.InvokeControl.SetBackColor(fieldTitle, Color.Transparent);
    }

    private void ReturnBackExit()
    {
        //
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                UserSession.StopwatchMain.Restart();
                ActionUtils.ActionMakeScreenShot(this, UserSession.Scale);
                // Wait control.
                NavigateToControl(WaitControl, ReturnBackExit, true, LocaleCore.Scales.AppWaitExit);
                // Quartz.
                if (Quartz is not null)
                {
                    Quartz.Close();
                    Quartz.Dispose();
                }
                UserSession.PluginsClose();
                // Mouse unhook.
                KeyboardMouseEvents.MouseDownExt -= MouseDownExt;
                KeyboardMouseEvents.Dispose();
            },
            () =>
            {
                UserSession.StopwatchMain.Stop();
                UserSession.DataAccess.LogInformation(
                    LocaleData.Program.IsClosed + Environment.NewLine +
                    $"{LocaleData.Program.Elapsed}: {UserSession.StopwatchMain.Elapsed}.");
            }
        );
    }

    private void LoadFonts()
    {
        fieldTitle.Font = FontsSettings.FontLabelsTitle;

        fieldNettoWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldPackageWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldPlu.Font = FontsSettings.FontLabelsMaximum;
        fieldProductDate.Font = FontsSettings.FontLabelsMaximum;

        fieldSscc.Font = FontsSettings.FontLabelsGray;
        fieldMemoryExt.Font = FontsSettings.FontLabelsGray;
        fieldPrintMain.Font = FontsSettings.FontLabelsGray;
        fieldPrintShipping.Font = FontsSettings.FontLabelsGray;
        fieldMassaExt.Font = FontsSettings.FontLabelsGray;
        fieldMassa.Font = FontsSettings.FontLabelsGray;
        fieldPrintMainExt.Font = FontsSettings.FontLabelsGray;
        fieldPrintShippingExt.Font = FontsSettings.FontLabelsGray;
        fieldMemory.Font = FontsSettings.FontLabelsGray;
        fieldTemplateTitle.Font = FontsSettings.FontLabelsGray;
        fieldTemplateValue.Font = FontsSettings.FontLabelsGray;

        labelNettoWeight.Font = FontsSettings.FontLabelsBlack;
        labelPackageWeight.Font = FontsSettings.FontLabelsBlack;
        labelKneading.Font = FontsSettings.FontLabelsBlack;
        fieldKneading.Font = FontsSettings.FontLabelsBlack;
        labelProductDate.Font = FontsSettings.FontLabelsBlack;

        ButtonDevice.Font = FontsSettings.FontButtonsSmall;
        ButtonPlu.Font = FontsSettings.FontButtonsSmall;
        ButtonPluNestingFk.Font = FontsSettings.FontButtonsSmall;

        ButtonScalesTerminal.Font = FontsSettings.FontButtons;
        ButtonScalesInit.Font = FontsSettings.FontButtons;
        ButtonNewPallet.Font = FontsSettings.FontButtons;
        ButtonKneading.Font = FontsSettings.FontButtons;
        ButtonMore.Font = FontsSettings.FontButtons;
        ButtonPrint.Font = FontsSettings.FontButtons;
    }

    private void SetButtonsSettings()
    {
        ActionSettings = new()
        {
            // Device.
            IsDevice = true,
            IsPlu = true,
            IsNesting = true,
            // Actions.
            IsKneading = false,
            IsMore = true,
            IsNewPallet = false,
            IsOrder = UserSession.Scale.IsOrder,
            IsPrint = true,
            IsScalesInit = false,
            IsScalesTerminal = true,
        };

        CreateButtonsDevices();
        CreateButtonsActions();
    }

    private void CreateButtonsDevices()
    {
        TableLayoutPanel layoutPanelDevice = GuiUtils.WinForm.NewTableLayoutPanel(layoutPanel, nameof(layoutPanelDevice),
            1, 14, 2, 98);
        int rowCount = 0;

        if (ActionSettings.IsDevice)
        {
            ButtonDevice = GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonDevice), 1, rowCount++);
            ButtonDevice.Click += ActionDevice;
        }
        else
        {
            ButtonDevice = new();
        }

        if (ActionSettings.IsPlu)
        {
            ButtonPlu = GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPlu), 1, rowCount++);
            ButtonPlu.Click += ActionPlu;
        }
        else
        {
            ButtonPlu = new();
        }

        if (ActionSettings.IsNesting)
        {
            ButtonPluNestingFk = GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPluNestingFk), 1, rowCount++);
            ButtonPluNestingFk.Click += ActionPluNestingFk;
        }
        else
        {
            ActionSettings = new();
        }

        layoutPanelDevice.ColumnCount = 1;
        GuiUtils.WinForm.SetTableLayoutPanelColumnStyles(layoutPanelDevice);
        layoutPanelDevice.RowCount = rowCount;
        GuiUtils.WinForm.SetTableLayoutPanelRowStyles(layoutPanelDevice);
    }

    private void CreateButtonsActions()
    {
        TableLayoutPanel layoutPanelActions = GuiUtils.WinForm.NewTableLayoutPanel(layoutPanel, nameof(layoutPanelActions),
            3, 14, layoutPanel.ColumnCount - 3, 99);
        int columnCount = 0;

        if (ActionSettings.IsScalesTerminal)
        {
            ButtonScalesTerminal =
                GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonScalesTerminal), columnCount++, 0);
            ButtonScalesTerminal.Click += ActionScalesTerminal;
        }
        else
        {
            ButtonScalesTerminal = new();
        }

        if (ActionSettings.IsScalesInit)
        {
            ButtonScalesInit =
                GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonScalesInit), columnCount++, 0);
            ButtonScalesInit.Click += ActionScalesInit;
        }
        else
        {
            ButtonScalesInit = new();
        }

        if (ActionSettings.IsNewPallet)
        {
            ButtonNewPallet =
                GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonNewPallet), columnCount++, 0);
            ButtonNewPallet.Click += ActionNewPallet;
        }
        else
        {
            ButtonNewPallet = new();
        }

        if (ActionSettings.IsKneading)
        {
            ButtonKneading =
                GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonKneading), columnCount++, 0);
            ButtonKneading.Click += ActionKneading;
        }
        else
        {
            ButtonKneading = new();
        }

        if (ActionSettings.IsMore)
        {
            ButtonMore = GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonMore), columnCount++, 0);
            ButtonMore.Click += ActionMore;
        }
        else
        {
            ButtonMore = new();
        }

        if (ActionSettings.IsPrint)
        {
            ButtonPrint =
                GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonPrint), columnCount++, 0);
            ButtonPrint.Click += ActionPrint;
            ButtonPrint.Focus();
        }
        else
        {
            ButtonPrint = new();
        }

        layoutPanelActions.ColumnCount = columnCount;
        GuiUtils.WinForm.SetTableLayoutPanelColumnStyles(layoutPanelActions);
        layoutPanelActions.RowCount = 1;
        GuiUtils.WinForm.SetTableLayoutPanelRowStyles(layoutPanelActions);
    }

    #endregion

    #region Public and private methods - Schedule

    private void LoadSchedule()
    {
        Quartz.AddJob(QuartzUtils.CronExpression.EveryHours(), ScheduleEveryHours,
            $"job{nameof(ScheduleEveryHours)}", $"trigger{nameof(ScheduleEveryHours)}",
            $"triggerGroup{nameof(ScheduleEveryHours)}");
        Quartz.AddJob(QuartzUtils.CronExpression.EveryDays(), ScheduleEveryDays,
            $"job{nameof(ScheduleEveryDays)}", $"trigger{nameof(ScheduleEveryDays)}",
            $"triggerGroup{nameof(ScheduleEveryDays)}");
    }

    private void ScheduleEveryHours()
    {
        lock (_lockerHours)
        {
            ActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
                {
                    if (Quartz is null) return;
                    ActionUtils.ActionMakeScreenShot(this, UserSession.Scale);
                }
            );
        }
    }

    private void ScheduleEveryDays()
    {
        lock (_lockerDays)
        {
            if (Quartz is null) return;
            UserSession.ProductDate = DateTime.Now;
            ActionUtils.ActionMakeScreenShot(this, UserSession.Scale);
        }
    }

    #endregion

    #region Public and private methods - Controls

    private void MouseDownExt(object sender, MouseEventExtArgs e)
    {
        if (e.Button == MouseButtons.Middle)
            ActionPrint(sender, e);
    }

    private void FieldPrintManager_Click(object sender, EventArgs e)
    {
        using WpfPageLoader wpfPageLoader = new(PageEnum.MessageBox, false, FormBorderStyle.FixedDialog, 22, 16, 16) { Width = 700, Height = 450 };
        wpfPageLoader.Text = LocaleCore.Print.InfoCaption;
        wpfPageLoader.MessageBox.Caption = LocaleCore.Print.InfoCaption;
        wpfPageLoader.MessageBox.Message = GetPrintInfo(UserSession.PluginPrintMain, true);
        if (UserSession.Scale.IsShipping)
        {
            wpfPageLoader.MessageBox.Message += Environment.NewLine + Environment.NewLine +
                GetPrintInfo(UserSession.PluginPrintShipping, false);
            wpfPageLoader.Height = 700;
        }
        wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
        wpfPageLoader.MessageBox.VisibilitySettings.ButtonCustomVisibility = Visibility.Visible;
        wpfPageLoader.MessageBox.VisibilitySettings.ButtonCustomContent = LocaleCore.Print.ClearQueue;
        DialogResult result = wpfPageLoader.ShowDialog(this);
        wpfPageLoader.Close();
        if (result == DialogResult.Retry)
        {
            UserSession.PluginPrintMain.ClearPrintBuffer(1);
            if (UserSession.Scale.IsShipping)
                UserSession.PluginPrintShipping.ClearPrintBuffer(1);
        }
    }

    private string GetPrintInfo(PluginPrintModel pluginPrint, bool isMain)
    {
        string peeler = isMain
            ? UserSession.PluginPrintMain.ZebraPeelerStatus : UserSession.PluginPrintShipping.ZebraPeelerStatus;
        string printMode = isMain
            ? UserSession.PluginPrintMain.GetZebraPrintMode() :
            UserSession.PluginPrintShipping.GetZebraPrintMode();
        PrintBrand printBrand = isMain ? UserSession.PluginPrintMain.PrintBrand : UserSession.PluginPrintShipping.PrintBrand;
        WmiWin32PrinterModel wmiPrinter = pluginPrint.TscWmiPrinter;
        return
            $"{UserSession.WeighingSettings.GetPrintName(isMain, printBrand)}" + Environment.NewLine +
            $"{LocaleCore.Print.DeviceCommunication} ({pluginPrint.Printer.Ip}): {pluginPrint.Printer.PingStatus}" + Environment.NewLine +
            $"{LocaleCore.Print.PrinterStatus}: {pluginPrint.GetDeviceStatus()}" + Environment.NewLine +
            Environment.NewLine +
            $"{LocaleCore.Print.Name}: {wmiPrinter.Name}" + Environment.NewLine +
            $"{LocaleCore.Print.Driver}: {wmiPrinter.DriverName}" + Environment.NewLine +
            $"{LocaleCore.Print.Port}: {wmiPrinter.PortName}" + Environment.NewLine +
            $"{LocaleCore.Print.StateCode}: {wmiPrinter.PrinterState}" + Environment.NewLine +
            $"{LocaleCore.Print.StatusCode}: {wmiPrinter.PrinterStatus}" + Environment.NewLine +
            $"{LocaleCore.Print.Status}: {pluginPrint.GetPrinterStatusDescription(LocaleCore.Lang, wmiPrinter.PrinterStatus)}" + Environment.NewLine +
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
            20, 14, 18, 0, 12)
        { Width = Width - 50, Height = Height - 50 };
        wpfPageLoader.Text = $@"{LocaleCore.Scales.ThreadsCount}: {Process.GetCurrentProcess().Threads.Count}";
        wpfPageLoader.MessageBox.Message = message;
        wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
        wpfPageLoader.MessageBox.VisibilitySettings.Localization();
        wpfPageLoader.ShowDialog(this);
        wpfPageLoader.Close();
    }

    private void LoadLocalizationStatic(Lang lang)
    {
        LocaleCore.Lang = LocaleData.Lang = lang;
        MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonScalesTerminal, LocaleCore.Scales.ButtonRunScalesTerminal);
        MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonScalesInit, LocaleCore.Scales.ButtonScalesInitShort);
        MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonNewPallet, LocaleCore.Scales.ButtonNewPallet);
        MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonKneading, LocaleCore.Scales.ButtonAddKneading);
        MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPlu, LocaleCore.Scales.ButtonPlu);
        MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonMore, LocaleCore.Scales.ButtonSetKneading);
        MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPrint, LocaleCore.Print.ActionPrint);
        MDSoft.WinFormsUtils.InvokeControl.SetText(labelNettoWeight, LocaleCore.Scales.FieldWeightNetto);
        MDSoft.WinFormsUtils.InvokeControl.SetText(labelPackageWeight, LocaleCore.Scales.FieldWeightTare);
        MDSoft.WinFormsUtils.InvokeControl.SetText(labelProductDate, LocaleCore.Scales.FieldDate);
        MDSoft.WinFormsUtils.InvokeControl.SetText(labelKneading, LocaleCore.Scales.FieldKneading);
        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTemplateTitle, $"{LocaleCore.Print.Template}");
    }

    private void LoadLocalizationDynamic(Lang lang)
    {
        LocaleCore.Lang = LocaleData.Lang = lang;
        string area = UserSession.Scale.WorkShop is null
            ? LocaleCore.Table.FieldEmpty : UserSession.ProductionFacility.Name;
        MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonDevice, UserSession.Scale.Description + Environment.NewLine + area);
        MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPluNestingFk, UserSession.PluNestingFk.IsNew
            ? LocaleCore.Table.FieldPackageIsNotSelected
            : $"{UserSession.PluNestingFk.WeightTare} {LocaleCore.Scales.WeightUnitKg} | {UserSession.PluNestingFk.Name}");
        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPackageWeight,
            UserSession.PluScale.IsNotNew
                ? $"{UserSession.PluNestingFk.WeightTare:0.000} {LocaleCore.Scales.WeightUnitKg}"
                : $"0,000 {LocaleCore.Scales.WeightUnitKg}");
        TemplateModel template = UserSession.DataAccess.GetItemTemplateNotNullable(UserSession.PluScale);
        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTemplateValue, template.Title);
    }

    #endregion

    #region Public and private methods - UI

    private void FinallyAction()
    {
        MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
        LoadLocalizationDynamic(Lang.Russian);
    }

    private void NavigateToControl(UserControlBase userControlBase, Action returnBack, bool isJoinReturnBackAction, string message = "")
    {
        if (userControlBase is null) return;

        layoutPanel.Visible = false;
        userControlBase.Visible = false;
        NavigationControl.UserControl = userControlBase;
        NavigationControl.Visible = true;
        userControlBase.ReturnBackAction = returnBack;
        if (isJoinReturnBackAction)
            userControlBase.ReturnBackAction += NavigationControl.ReturnBackAction;
        userControlBase.Message = message;
        userControlBase.RefreshAction();
        userControlBase.Visible = true;
    }

    private void ReturnBackDefault()
    {
        NavigationControl.Visible = false;
        layoutPanel.Visible = true;
    }

    #endregion

    #region Public and private methods - Actions

    private void ActionClose(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
            {
                DialogResult result = WpfUtils.ShowNewOperationControl(this,
                    $"{LocaleCore.Scales.QuestionCloseApp}?",
                    true, LogType.Question,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                FinallyAction();
                if (result is not DialogResult.Yes)
                    return;
                // See the MainForm_FormClosing() method.
                Close();
            }
        );
    }

    private void ActionDevice(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                using WpfPageLoader wpfPageLoader = new(PageEnum.Device, false) { Width = 800, Height = 400 };
                DialogResult dialogResult = wpfPageLoader.ShowDialog(this);
                wpfPageLoader.Close();
                // Here is another instance of wpfPageLoader.PageDevice.UserSession.
                switch (dialogResult)
                {
                    case DialogResult.OK:
                        if (wpfPageLoader.PageDevice is not null)
                            UserSession.SetMain(wpfPageLoader.PageDevice.UserSession.Scale.IdentityValueId, UserSession.ProductionFacility.Name);
                        break;
                    case DialogResult.Cancel:
                        if (wpfPageLoader.PageDevice is not null)
                            UserSession.SetMain(wpfPageLoader.PageDevice.UserSession.Scale.IdentityValueId, string.Empty);
                        break;
                }
            }, FinallyAction);
    }

    private void ActionPluNestingFk(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                if (!ActionCheckPluIdentityIsNew()) return;

                using WpfPageLoader wpfPageLoader = new(PageEnum.PluBundleFk, false) { Width = 800, Height = 300 };
                DialogResult dialogResult = wpfPageLoader.ShowDialog(this);
                wpfPageLoader.Close();
                // Here is another instance of wpfPageLoader.PagePluNestingFk.UserSession.
                switch (dialogResult)
                {
                    case DialogResult.OK:
                        if (wpfPageLoader.PagePluNestingFk is not null)
                            UserSession.SetBundleFk(wpfPageLoader.PagePluNestingFk.UserSession.PluNestingFk.IdentityValueUid);
                        break;
                    case DialogResult.Cancel:
                        if (wpfPageLoader.PagePluNestingFk is not null)
                            UserSession.SetBundleFk(null);
                        break;
                }
            }, FinallyAction);
    }

    private bool ActionCheckPluIdentityIsNew()
    {
        if (UserSession.PluScale.Plu.IsNew)
        {
            using WpfPageLoader wpfPageLoader = new(
                PageEnum.MessageBox, false, FormBorderStyle.FixedDialog, 22, 16, 16)
            { Width = 700, Height = 450 };
            wpfPageLoader.Text = LocaleCore.Action.ActionAccessDeny;
            wpfPageLoader.MessageBox.Caption = LocaleCore.Table.FieldPluIsNotSelected;
            wpfPageLoader.MessageBox.Message = LocaleCore.Table.FieldPluMustBeSelected;
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonCancelVisibility = Visibility.Visible;
            _ = wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
            return false;
        }
        return true;
    }

    private void ActionScalesTerminal(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                DialogResult result = WpfUtils.ShowNewOperationControl(this,
                    $"{LocaleCore.Scales.QuestionRunApp} ScalesTerminal?",
                    true, LogType.Question,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (result is not DialogResult.Yes)
                    return;

                // Run app.
                if (File.Exists(LocaleData.Paths.ScalesTerminal))
                {
                    UserSession.PluginMassa.Close();
                    Proc.Run(LocaleData.Paths.ScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
                    UserSession.PluginMassa.Execute();
                }
                else
                {
                    WpfUtils.ShowNewOperationControl(this,
                        LocaleCore.Scales.ProgramNotFound(
                            LocaleData.Paths.ScalesTerminal), true, LogType.Warning,
                        new() { ButtonOkVisibility = Visibility.Visible });
                }
            }, FinallyAction);
    }

    private void ActionScalesInit(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                if (!UserSession.PluScale.Plu.IsCheckWeight)
                {
                    WpfUtils.ShowNewOperationControl(this,
                        LocaleCore.Scales.PluNotSelectWeight, true, LogType.Warning,
                        new() { ButtonOkVisibility = Visibility.Visible });
                    return;
                }
                if (!UserSession.PluginMassa.MassaDevice.IsOpenPort)
                {
                    WpfUtils.ShowNewOperationControl(this, LocaleCore.Scales.MassaIsNotRespond, true, LogType.Warning,
                        new() { ButtonOkVisibility = Visibility.Visible });
                    return;
                }

                DialogResult result = WpfUtils.ShowNewOperationControl(this,
                    LocaleCore.Scales.QuestionPerformOperation, true, LogType.Question,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (result is not DialogResult.Yes)
                    return;

                // Fix negative weight.
                //if (UserSession.PluginMassa.WeightNet < 0)
                //{
                //    UserSession.PluginMassa.ResetMassa();
                //}

                UserSession.CheckWeightMassaDeviceExists();
                UserSession.PluScale = new();

                UserSession.PluginMassa.Execute();
                UserSession.PluginMassa.GetInit();
            }, FinallyAction);
    }

    private void ActionNewPallet(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                UserSession.NewPallet();
            }, FinallyAction);
    }

    private void ReturnBackKneading()
    {
        using NumberInputForm numberInputForm = new() { InputValue = 0 };
        DialogResult result = numberInputForm.ShowDialog(this);
        numberInputForm.Close();
        if (result == DialogResult.OK)
            UserSession.WeighingSettings.Kneading = (byte)numberInputForm.InputValue;
    }

    private void ActionKneading(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
        {
            NavigateToControl(KneadingControl, ReturnBackKneading, true);
        }, FinallyAction);
    }

    private void ReturnBackPlu()
    {
        if (PluControl.Result == DialogResult.OK)
        {
            UserSession.WeighingSettings.Kneading = 1;
            UserSession.ProductDate = DateTime.Now;
            UserSession.NewPallet();
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(labelNettoWeight, UserSession.PluScale.Plu.IsCheckWeight);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldNettoWeight, UserSession.PluScale.Plu.IsCheckWeight);

            ActionMore(null, null);
        }
        else
        {
            UserSession.PluScale = UserSession.DataAccess.GetItemNewEmpty<PluScaleModel>();
        }
    }

    private void ActionPlu(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(labelNettoWeight, false);
                MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldNettoWeight, false);
                UserSession.PluScale = UserSession.DataAccess.GetItemNewEmpty<PluScaleModel>();
                //if (UserSession.CheckWeightMassaDeviceExists())
                //{
                //    if (!UserSession.CheckWeightIsNegative(this) || !UserSession.CheckWeightIsPositive(this))
                //        return;
                //}
                NavigateToControl(PluControl, ReturnBackPlu, false);
            }, FinallyAction);
    }

    private void ReturnBackMore()
    {
        UserSession.PluginMassa.Execute();
    }

    private void ActionMore(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale,
            () =>
            {
                if (UserSession.PluScale.IsNew)
                {
                    WpfUtils.ShowNewOperationControl(this,
                        LocaleCore.Scales.PluNotSelect, true, LogType.Warning,
                        new() { ButtonOkVisibility = Visibility.Visible });
                    return;
                }

                NavigateToControl(KneadingControl, ReturnBackMore, true);
            }, FinallyAction);
    }

    private void ActionPrint(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                UserSession.AddScaleCounter();

                if (!UserSession.CheckPluIsEmpty(this)) return;
                //if (UserSession.PluScale.Plu.IsCheckWeight && UserSession.PluPackages.Count > 1 && UserSession.PluPackage.IsNew)
                // Maybe tare didn't need.
                //if (UserSession.PluBundlesFks.Count > 1 && UserSession.PluBundleFk.IsNew)
                //    ActionPluBundleFk(sender, e);
                if (!UserSession.CheckPluBundleFkIsEmpty(this)) return;
                if (!UserSession.CheckWeightMassaDeviceExists()) return;
                if (!UserSession.CheckWeightMassaIsStable(this)) return;
                if (!UserSession.CheckPluGtin(this)) return;

                // Set fake data for PLU weighing.
                UserSession.SetPluWeighingFake(this);
                if (!UserSession.CheckWeightIsNegative(this)) return;
                UserSession.NewPluWeighing();
                if (!UserSession.CheckWeightThresholds(this)) return;

                // Check printers connections.
                bool isSkipPrintCheckAccess = false;
                if (Debug.IsDebug)
                {
                    DialogResult dialogResult = WpfUtils.ShowNewOperationControl(
                        LocaleCore.Print.QuestionPrintCheckAccess, true, LogType.Question,
                        new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                    isSkipPrintCheckAccess = dialogResult != DialogResult.Yes;
                }
                if (!isSkipPrintCheckAccess)
                {
                    if (!UserSession.CheckPrintIsConnect(this, UserSession.PluginPrintMain, true)) return;
                    if (UserSession.Scale.IsShipping)
                        if (!UserSession.CheckPrintIsConnect(this, UserSession.PluginPrintShipping, false)) return;
                    // Check printers statuses.
                    if (!UserSession.CheckPrintStatusReady(this, UserSession.PluginPrintMain, true)) return;
                    if (UserSession.Scale.IsShipping)
                        if (!UserSession.CheckPrintStatusReady(this, UserSession.PluginPrintShipping, false)) return;
                }

                UserSession.PrintLabel(this, false);
            },
            () =>
            {
                FinallyAction();
                UserSession.PluginMassa.IsWeightNetFake = false;
            });
    }

    #endregion
}