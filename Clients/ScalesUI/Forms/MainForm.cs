// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MDSoft.NetUtils;
using MDSoft.WinFormsUtils;
using MDSoft.Wmi.Helpers;
using MDSoft.Wmi.Models;

namespace ScalesUI.Forms;

/// <summary>
/// Main form.
/// </summary>
public partial class MainForm : Form
{
    #region Public and private fields, properties, constructor

    private ActionSettingsModel ActionSettings { get; set; }
    private AppVersionHelper AppVersion => AppVersionHelper.Instance;
    private DebugHelper Debug => DebugHelper.Instance;
    private FontsSettingsHelper FontsSettings => FontsSettingsHelper.Instance;
    private IKeyboardMouseEvents KeyboardMouseEvents { get; set; }
    private ProcHelper Proc => ProcHelper.Instance;
    private WsSchedulerHelper WsScheduler => WsSchedulerHelper.Instance;
    private WsUserSessionHelper UserSession => WsUserSessionHelper.Instance;
    private Button ButtonLine { get; set; }
    private Button ButtonPluNestingFk { get; set; }
    private Button ButtonKneading { get; set; }
    private Button ButtonMore { get; set; }
    private Button ButtonNewPallet { get; set; }
    private Button ButtonPlu { get; set; }
    private Button ButtonPrint { get; set; }
    private Button ButtonScalesInit { get; set; }
    private Button ButtonScalesTerminal { get; set; }
    private NavigationUserControl NavigationControl { get; set; }
    private PluUserControl PluControl { get; set; }
    private KneadingUserControl KneadingControl { get; set; }
    private WaitUserControl WaitControl { get; set; }
    private const bool IsReleaseForce = true;

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public MainForm()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods - MainForm

    private void ReturnBackLoad()
    {
        FormBorderStyle = Debug.IsDevelop ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
        TopMost = !Debug.IsDevelop;
        UserSession.NewPallet();

        MdInvokeControl.SetText(this, AppVersion.AppTitle);
        MdInvokeControl.SetText(fieldProductDate, string.Empty);
        LoadMainControls();
        LoadLocalizationStatic(Lang.Russian);

        // Quartz.
        WsScheduler.Load(this);

        ActionUtils.ActionMakeScreenShot(this, UserSession.Scale);
        UserSession.StopwatchMain.Stop();

        UserSession.ContextManager.ContextItem.SaveLogMemory(
            UserSession.PluginMemory.GetMemorySizeAppMb(), UserSession.PluginMemory.GetMemorySizeFreeMb());
        UserSession.ContextManager.ContextItem.SaveLogInformation(
            $"{LocaleData.Program.IsLoaded}. " + Environment.NewLine +
            $"{LocaleCore.Scales.ScreenResolution}: {Width} x {Height}." + Environment.NewLine +
            $"{nameof(LocaleData.Program.TimeSpent)}: {UserSession.StopwatchMain.Elapsed}.");
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
        this.SwitchResolution(Debug.IsDevelop ? WsScreenResolution.Value1366x768 : WsScreenResolution.FullScreen);
        CenterToScreen();
        LoadFonts();
    }

    private MdPrinterModel GetMdPrinter(PrinterModel scalePrinter) => new()
    {
        Name = scalePrinter.Name,
        Ip = scalePrinter.Ip,
        Port = scalePrinter.Port,
        Password = scalePrinter.Password,
        PeelOffSet = scalePrinter.PeelOffSet,
        DarknessLevel = scalePrinter.DarknessLevel,
        HttpStatusCode = scalePrinter.HttpStatusCode,
        PingStatus = scalePrinter.PingStatus,
        HttpStatusException = scalePrinter.HttpStatusException,
    };

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
        // Память.
        UserSession.PluginMemory.Init(new(1_000, 0_250), new(0_250, 0_250),
            new(0_250, 0_250), fieldMemory, fieldMemoryExt);
        UserSession.PluginMemory.Execute();
        MdInvokeControl.SetVisible(fieldMemoryExt, Debug.IsDevelop);

        // Весовая платформа Масса-К.
        UserSession.PluginMassa.Init(new(1_000, 1_000), new(0_100, 0_100),
            new(0_050, 0_100), fieldNettoWeight, fieldMassa, fieldMassaExt, ResetWarning);
        UserSession.PluginMassa.Execute();
        MdInvokeControl.SetVisible(fieldMassaExt, Debug.IsDevelop);

        // Шаблон.
        MdInvokeControl.SetVisible(fieldTemplateTitle, true);
        MdInvokeControl.SetVisible(fieldTemplateValue, true);

        // Основной принтер.
        if (UserSession.Scale.PrinterMain is not null)
        {
            UserSession.PluginPrintMain.Init(new(0_500, 0_250), new(0_250, 0_250),
                new(0_250, 0_250),
                UserSession.PrintBrandMain, GetMdPrinter(UserSession.Scale.PrinterMain), fieldPrintMain, fieldPrintMainExt, true);
            MdInvokeControl.SetVisible(fieldPrintMain, true);
            MdInvokeControl.SetVisible(fieldPrintMainExt, Debug.IsDevelop);
            UserSession.PluginPrintMain.Execute();
            UserSession.PluginPrintMain.SetOdometorUserLabel(1);
        }

        // Транспортный принтер.
        if (UserSession.Scale.IsShipping)
        {
            if (UserSession.Scale.PrinterShipping is not null)
            {
                UserSession.PluginPrintShipping.Init(new(0_500, 0_250),
                    new(0_250, 0_250), new(0_250, 0_250),
                    UserSession.PrintBrandShipping, GetMdPrinter(UserSession.Scale.PrinterShipping), fieldPrintShipping, fieldPrintShippingExt, false);
                MdInvokeControl.SetVisible(fieldPrintShipping, true);
                MdInvokeControl.SetVisible(fieldPrintShippingExt, Debug.IsDevelop);
                UserSession.PluginPrintShipping.Execute();
                UserSession.PluginPrintShipping.SetOdometorUserLabel(1);
            }
        }

        // Метки.
        UserSession.PluginLabels.Init(
            new(0_250, 0_250), new(0_250, 0_250),
            new(0_250, 0_250), fieldPlu, fieldProductDate, fieldKneading);
        UserSession.PluginLabels.Execute();
        MdInvokeControl.SetText(fieldTitle, $"{AppVersionHelper.Instance.AppTitle}. {UserSession.PublishDescription}.");
        MdInvokeControl.SetBackColor(fieldTitle, Color.Transparent);
    }

    private void ReturnBackExit()
    {
        //
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                UserSession.ContextManager.ContextItem.SaveLogMemory(
                    UserSession.PluginMemory.GetMemorySizeAppMb(), UserSession.PluginMemory.GetMemorySizeFreeMb());
                UserSession.StopwatchMain.Restart();
                ActionUtils.ActionMakeScreenShot(this, UserSession.Scale);
                // Wait control.
                NavigateToControl(WaitControl, ReturnBackExit, true, LocaleCore.Scales.AppWaitExit);
                // Quartz.
                WsScheduler.Close();
                UserSession.PluginsClose();
            },
            () =>
            {
                UserSession.StopwatchMain.Stop();
                UserSession.ContextManager.ContextItem.SaveLogInformation(
                    LocaleData.Program.IsClosed + Environment.NewLine +
                    $"{LocaleData.Program.TimeSpent}: {UserSession.StopwatchMain.Elapsed}.");
            }
        );
        // Mouse unhook.
        KeyboardMouseEvents.MouseDownExt -= MouseDownExt;
        KeyboardMouseEvents.Dispose();
    }

    private void LoadFonts()
    {
        fieldTitle.Font = FontsSettings.FontLabelsTitle;

        fieldNettoWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldPackageWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldPlu.Font = FontsSettings.FontLabelsMaximum;
        fieldProductDate.Font = FontsSettings.FontLabelsMaximum;

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

        fieldWarning.Font = FontsSettings.FontLabelsBlack;
        labelNettoWeight.Font = FontsSettings.FontLabelsBlack;
        labelPackageWeight.Font = FontsSettings.FontLabelsBlack;
        labelKneading.Font = FontsSettings.FontLabelsBlack;
        fieldKneading.Font = FontsSettings.FontLabelsBlack;
        labelProductDate.Font = FontsSettings.FontLabelsBlack;

        ButtonLine.Font = FontsSettings.FontButtonsSmall;
        ButtonPlu.Font = FontsSettings.FontButtonsSmall;
        ButtonPluNestingFk.Font = FontsSettings.FontButtonsSmall;

        ButtonScalesTerminal.Font = FontsSettings.FontButtons;
        ButtonScalesInit.Font = FontsSettings.FontButtons;
        ButtonNewPallet.Font = FontsSettings.FontButtons;
        ButtonKneading.Font = FontsSettings.FontButtons;
        ButtonMore.Font = FontsSettings.FontButtons;
        ButtonPrint.Font = FontsSettings.FontButtons;
        ButtonPrint.BackColor = ColorTranslator.FromHtml("#ff7f50");
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
            1, 13, 2, 98);
        int rowCount = 0;

        if (ActionSettings.IsDevice)
        {
            ButtonLine = GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonLine), 1, rowCount++);
            ButtonLine.Click += ActionSwitchLine;
        }
        else
        {
            ButtonLine = new();
        }

        if (ActionSettings.IsPlu)
        {
            ButtonPlu = GuiUtils.WinForm.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPlu), 1, rowCount++);
            ButtonPlu.Click += ActionSwitchPlu;
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
            3, 13, layoutPanel.ColumnCount - 3, 99);
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
            ButtonPrint.Click += ActionPreparePrint;
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

    #region Public and private methods - Controls

    private void MouseDownExt(object sender, MouseEventExtArgs e)
    {
        if (e.Button == MouseButtons.Middle)
            ActionPreparePrint(sender, e);
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
        MdWmiWinPrinterModel wmiPrinter = pluginPrint.TscWmiPrinter;
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
            $"{LocaleCore.Print.State}: {MdWmiHelper.Instance.GetStatusDescription(
                LocaleCore.Lang == Lang.English ? MDSoft.Wmi.Enums.MdLang.English : MDSoft.Wmi.Enums.MdLang.Russian, wmiPrinter.Status)}" + Environment.NewLine +
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
        MdInvokeControl.SetText(ButtonScalesTerminal, LocaleCore.Scales.ButtonRunScalesTerminal);
        MdInvokeControl.SetText(ButtonScalesInit, LocaleCore.Scales.ButtonScalesInitShort);
        MdInvokeControl.SetText(ButtonNewPallet, LocaleCore.Scales.ButtonNewPallet);
        MdInvokeControl.SetText(ButtonKneading, LocaleCore.Scales.ButtonAddKneading);
        MdInvokeControl.SetText(ButtonPlu, LocaleCore.Scales.ButtonPlu);
        MdInvokeControl.SetText(ButtonMore, LocaleCore.Scales.ButtonSetKneading);
        MdInvokeControl.SetText(ButtonPrint, LocaleCore.Print.ActionPrint);
        MdInvokeControl.SetText(labelNettoWeight, LocaleCore.Scales.FieldWeightNetto);
        MdInvokeControl.SetText(labelPackageWeight, LocaleCore.Scales.FieldWeightTare);
        MdInvokeControl.SetText(labelProductDate, LocaleCore.Scales.FieldDate);
        MdInvokeControl.SetText(labelKneading, LocaleCore.Scales.FieldKneading);
        MdInvokeControl.SetText(fieldTemplateTitle, $"{LocaleCore.Print.Template}");
    }

    private void LoadLocalizationDynamic(Lang lang)
    {
        LocaleCore.Lang = LocaleData.Lang = lang;
        string area = UserSession.Scale.WorkShop is null
            ? LocaleCore.Table.FieldEmpty : UserSession.ProductionFacility.Name;
        MdInvokeControl.SetText(ButtonLine, $"{UserSession.Scale.Description} | {UserSession.Scale.Number}" +
            Environment.NewLine + area);
        MdInvokeControl.SetText(ButtonPluNestingFk, UserSession.PluNestingFk.IsNew
            ? LocaleCore.Table.FieldPackageIsNotSelected
            : $"{UserSession.PluNestingFk.WeightTare} {LocaleCore.Scales.WeightUnitKg} | {UserSession.PluNestingFk.Name}");
        MdInvokeControl.SetText(fieldPackageWeight,
            UserSession.PluScale.IsNotNew
                ? $"{UserSession.PluNestingFk.WeightTare:0.000} {LocaleCore.Scales.WeightUnitKg}"
                : $"0,000 {LocaleCore.Scales.WeightUnitKg}");
        TemplateModel template = UserSession.ContextManager.ContextItem.GetItemTemplateNotNullable(UserSession.PluScale);
        MdInvokeControl.SetText(fieldTemplateValue, template.Title);
    }

    #endregion

    #region Public and private methods - UI

    private void FinallyAction()
    {
        MdInvokeControl.Select(ButtonPrint);
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

    /// <summary>
    /// Закрыть программу.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionClose(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
            {
                // Сброс предупреждения.
                ResetWarning();
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

    /// <summary>
    /// Сменить линию.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchLine(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                // Сброс предупреждения.
                ResetWarning();
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

    /// <summary>
    /// Сменить вложенность ПЛУ.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionPluNestingFk(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                // Сброс предупреждения.
                ResetWarning();
                // Проверить наличие ПЛУ.
                if (!ActionCheckPluIdentityIsNew()) return;

                using WpfPageLoader wpfPageLoader = new(PageEnum.PluNestingFk, false) { Width = 800, Height = 300 };
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

    /// <summary>
    /// Проверить наличие ПЛУ.
    /// </summary>
    /// <returns></returns>
    private bool ActionCheckPluIdentityIsNew()
    {
        if (UserSession.PluScale.Plu.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Table.FieldPluIsNotSelected);
            UserSession.ContextManager.ContextItem.SaveLogError(LocaleCore.Table.FieldPluIsNotSelected);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Запустить ПО.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionScalesTerminal(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                // Сброс предупреждения.
                ResetWarning();
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

    /// <summary>
    /// Инициализировать весовую платформу Масса-К.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionScalesInit(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                // Сброс предупреждения.
                ResetWarning();
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
                // Проверить наличие весовой платформы Масса-К.
                if (IsReleaseForce || Debug.IsRelease)
                    UserSession.CheckWeightMassaDeviceExists();
                UserSession.PluScale = new();

                UserSession.PluginMassa.Execute();
                UserSession.PluginMassa.GetInit();
            }, FinallyAction);
    }

    /// <summary>
    /// Новая паллета.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionNewPallet(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                // Сброс предупреждения.
                ResetWarning();
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

    /// <summary>
    /// Сменить замес.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
            MdInvokeControl.SetVisible(labelNettoWeight, UserSession.PluScale.Plu.IsCheckWeight);
            MdInvokeControl.SetVisible(fieldNettoWeight, UserSession.PluScale.Plu.IsCheckWeight);

            ActionMore(null, null);
        }
        else
        {
            UserSession.PluScale = UserSession.AccessManager.AccessItem.GetItemNewEmpty<PluScaleModel>();
        }
    }

    /// <summary>
    /// Сменить ПЛУ.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionSwitchPlu(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
            {
                // Сброс предупреждения.
                ResetWarning();
                MdInvokeControl.SetVisible(labelNettoWeight, false);
                MdInvokeControl.SetVisible(fieldNettoWeight, false);
                UserSession.PluScale = UserSession.AccessManager.AccessItem.GetItemNewEmpty<PluScaleModel>();
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

    /// <summary>
    /// Ещё.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionMore(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale,
            () =>
            {
                // Сброс предупреждения.
                ResetWarning();
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

    /// <summary>
    /// Сброс предупреждения.
    /// </summary>
    private void ResetWarning()
    {
        MdInvokeControl.SetVisible(fieldWarning, false);
        MdInvokeControl.SetText(fieldWarning, string.Empty);
    }

    /// <summary>
    /// Подготовка к печати.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionPreparePrint(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, () =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Инкремент счётчика этикеток.
            UserSession.AddScaleCounter();
            // Проверить наличие ПЛУ.
            if (!UserSession.CheckPluIsEmpty(fieldWarning)) return;
            // Проверить наличие вложенности ПЛУ.
            if (!UserSession.CheckPluNestingFkIsEmpty(fieldWarning)) return;
            // Проверить наличие весовой платформы Масса-К.
            if (IsReleaseForce || Debug.IsRelease)
                if (!UserSession.CheckWeightMassaDeviceExists()) return;
            // Проверить стабилизацию весовой платформы Масса-К.
            if (IsReleaseForce || Debug.IsRelease)
                if (!UserSession.CheckWeightMassaIsStable(fieldWarning)) return;
            // Проверить ГТИН ПЛУ.
            if (!UserSession.CheckPluGtin(fieldWarning)) return;
            // Задать фейк данные веса ПЛУ для режима разработки.
            if (!IsReleaseForce && Debug.IsDevelop)
                UserSession.SetPluWeighingFakeForDevelop(this);
            // Проверить отрицательный вес.
            if (!UserSession.CheckWeightIsNegative(fieldWarning)) return;
            // Создать новое взвешивание ПЛУ.
            UserSession.NewPluWeighing();
            // Проверить границы веса.
            if (!UserSession.CheckWeightThresholds(fieldWarning)) return;
            // Проверить подключение принтера.
            bool isSkipPrintCheckAccess = false;
            if (!IsReleaseForce && Debug.IsDevelop)
            {
                DialogResult dialogResult = WpfUtils.ShowNewOperationControl(
                    LocaleCore.Print.QuestionPrintCheckAccess, true, LogType.Question,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                isSkipPrintCheckAccess = dialogResult != DialogResult.Yes;
            }
            if (!isSkipPrintCheckAccess)
            {
                // Проверить подключение и готовность основного принтера.
                if (!UserSession.CheckPrintIsConnectAndReady(fieldWarning, UserSession.PluginPrintMain, true)) return;
                // Проверить подключение и готовность транспортного принтера.
                if (UserSession.Scale.IsShipping)
                    if (!UserSession.CheckPrintIsConnectAndReady(fieldWarning, UserSession.PluginPrintShipping, false)) return;
            }
            // Печать этикетки.
            UserSession.PrintLabel(fieldWarning, false);
        },
            () =>
            {
                FinallyAction();
                UserSession.PluginMassa.IsWeightNetFake = false;
            });
    }

    #endregion
}