namespace ScalesUI.Forms;

public sealed partial class WsMainForm : Form
{
    #region Public and private fields, properties, constructor

    private ActionSettingsModel ActionSettings { get; set; }
    private WsDebugHelper Debug => WsDebugHelper.Instance;
    private WsFontsSettingsHelper FontsSettings => WsFontsSettingsHelper.Instance;
    private IKeyboardMouseEvents KeyboardMouseEvents { get; set; }
    private WsSchedulerHelper WsScheduler => WsSchedulerHelper.Instance;
    private WsUserSessionHelper UserSession => WsUserSessionHelper.Instance;
    private WsPrintSessionHelper PrintSession => WsPrintSessionHelper.Instance;
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    private Button ButtonLine { get; set; } = new();
    private Button ButtonPluNestingFk { get; set; } = new();
    private Button ButtonKneading { get; set; } = new();
    private Button ButtonNewPallet { get; set; } = new();
    private Button ButtonPlu { get; set; } = new();
    private Button ButtonPrint { get; set; } = new();
    private Button ButtonScalesTerminal { get; set; } = new();
    
    /// <summary>
    /// Магический флаг закрытия после нажатия кнопки OK.
    /// </summary>
    private bool IsMagicClose { get; set; }

    public WsMainForm()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods - MainForm

    private bool IsNewLine()
    {
        if (LabelSession.Line.IsExists)
        {
            return false;
        }
        string message = WsLocaleCore.LabelPrint.RegistrationWarningLineNotFound(WsLabelSessionHelper.DeviceName);
        WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl, message, true, WsEnumLogType.Error,
        WsEnumDialogType.Ok, new() { ActionCloseAfterNotLine });
        return true;
    }

    private bool IsAppExists()
    {
        _ = new Mutex(true, Application.ProductName, out bool isCreatedNew);
        if (isCreatedNew) return false;
        string message = $"{WsLocaleCore.Strings.Application} {Application.ProductName} {WsLocaleCore.LabelPrint.AlreadyRunning}!";
        WsFormNavigationUtils.DialogUserControl.ViewModel.SetupButtonsOk(message, ActionExit, 
        WsFormNavigationUtils.NavigationUserControl.Width);
        // Навигация в контрол диалога Ок.
        WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl, message, true, WsEnumLogType.Error,
        WsEnumDialogType.Ok, new() { ActionFinally });
        ContextManager.ContextItem.SaveLogWarning(message);
        return true;
    }
     
    private void OnLoad(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            if (IsAppExists()) return;
            
            UserSession.StopwatchMain = Stopwatch.StartNew();
            WsLocaleCore.Lang = WsLocaleData.Lang = WsEnumLanguage.Russian;
            
            SetupNavigationUserControl();
            
            LabelSession.SetSessionForLabelPrint(ShowFormUserControl);
            if (IsNewLine()) return;
            
            MainFormLoadAtBackground();
            ActionFinally();
            ReturnOkFromDeviceSettings();
            SaveStartLog();
        });
    }

    private void SaveStartLog()
    {
        UserSession.StopwatchMain.Stop();
         StringBuilder log = new();
         log.AppendLine($"{WsLocaleData.Program.IsLoaded}.")
             .AppendLine($"{WsLocaleCore.LabelPrint.ScreenResolution}: {Width} x {Height}.")
             .AppendLine($"Время загрузки: {UserSession.StopwatchMain.Elapsed}.");
         ContextManager.ContextItem.SaveLogInformation(log);
     }
    
    private void MainFormLoadAtBackground()
    {
        MouseSubscribe();
        SetupButtons();
        LoadFonts();
        LoadLocalizationStatic(WsEnumLanguage.Russian);
        
        LabelSession.NewPallet();
        SetupPlugins();
        WsScheduler.Load(this);
        LoadNavigationUserControl();
    }

    private static void ActionExit() => Application.Exit();
    
    private void SetupPlugins()
    {
        UserSession.PluginMassa.Init(new(1_000), new(0_150),
            new(0_150), fieldNettoWeight, fieldMassa, ResetWarning);
        PluginMassaExecute();

        MdInvokeControl.SetVisible(fieldPrintMain, true);
        MdInvokeControl.SetVisible(fieldPrintMainExt, Debug.IsDevelop);
        // Основной принтер.
        switch (LabelSession.PrintModelMain)
        {

            case WsEnumPrintModel.Tsc:
                LabelSession.PluginPrintTscMain = new();
                LabelSession.PluginPrintTscMain.InitTsc(new(0_500),
                new(0_500), new(0_500),
                LabelSession.Line.PrinterMain, fieldPrintMain);
                LabelSession.PluginPrintTscMain.Execute();
                break;
            case WsEnumPrintModel.Zebra:
                LabelSession.PluginPrintZebraMain = new();
                LabelSession.PluginPrintZebraMain.InitZebra(new(5000),
                new(0_250), new(0_250),
                LabelSession.Line.PrinterMain, fieldPrintMain);
                LabelSession.PluginPrintZebraMain.Execute();
                // LabelSession.PluginPrintZebraMain.SetOdometorUserLabel(1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        UserSession.PluginLabels.Init(
            new(0_500), new(0_500),
            new(0_500), fieldPlu, fieldProductDate, fieldKneading);
        UserSession.PluginLabels.Execute();
    }
    
    private void LoadFonts()
    {
        fieldTitle.Font = FontsSettings.FontLabelsTitle;

        fieldNettoWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldTareWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldPlu.Font = FontsSettings.FontLabelsMaximum;
        fieldProductDate.Font = FontsSettings.FontLabelsMaximum;

        fieldPrintMain.Font = FontsSettings.FontLabelsGray;
        fieldPrintShipping.Font = FontsSettings.FontLabelsGray;
        fieldMassa.Font = FontsSettings.FontLabelsGray;
        fieldPrintMainExt.Font = FontsSettings.FontLabelsGray;
        fieldPrintShippingExt.Font = FontsSettings.FontLabelsGray;
        fieldWarning.Font = FontsSettings.FontLabelsBlack;
        labelNettoWeight.Font = FontsSettings.FontLabelsBlack;
        labelTareWeight.Font = FontsSettings.FontLabelsBlack;
        labelKneading.Font = FontsSettings.FontLabelsBlack;
        fieldKneading.Font = FontsSettings.FontLabelsBlack;
        labelProductDate.Font = FontsSettings.FontLabelsBlack;

        ButtonLine.Font = FontsSettings.FontButtonsSmall;
        ButtonPlu.Font = FontsSettings.FontButtonsSmall;
        ButtonPluNestingFk.Font = FontsSettings.FontButtonsSmall;

        ButtonScalesTerminal.Font = FontsSettings.FontButtons;
        ButtonNewPallet.Font = FontsSettings.FontButtons;
        ButtonKneading.Font = FontsSettings.FontButtons;
        ButtonPrint.Font = FontsSettings.FontButtons;
        ButtonPrint.BackColor = ColorTranslator.FromHtml("#ff7f50");
    }
    
    private void SetupButtons()
    {
        ActionSettings = new()
        {
            IsDevice = true,
            IsPlu = true,
            IsNesting = true,
            IsKneading = true,
            IsNewPallet = false,
            IsPrint = true,
            IsScalesTerminal = true,
        };

        CreateButtonsDevices();
        CreateButtonsActions();
    }
    
    private void CreateButtonsDevices()
    {
        TableLayoutPanel layoutPanelDevice = WsFormUtils.NewTableLayoutPanel(layoutPanelMain, nameof(layoutPanelDevice),
            1, 13, 2, 98);
        int rowCount = 0;

        if (ActionSettings.IsDevice)
        {
            ButtonLine = WsFormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonLine), 1, rowCount++);
            ButtonLine.Click += ActionSwitchLine;
        }
        if (ActionSettings.IsPlu)
        {
            ButtonPlu = WsFormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPlu), 1, rowCount++);
            ButtonPlu.Click += ActionSwitchPluLine;
        }
        if (ActionSettings.IsNesting)
        {
            ButtonPluNestingFk = WsFormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPluNestingFk), 1, rowCount++);
            ButtonPluNestingFk.Click += ActionSwitchPluNesting;
        }
        layoutPanelDevice.ColumnCount = 1;
        WsFormUtils.SetTableLayoutPanelColumnStyles(layoutPanelDevice);
        layoutPanelDevice.RowCount = rowCount;
        WsFormUtils.SetTableLayoutPanelRowStyles(layoutPanelDevice);
    }
    
    private void CreateButtonsActions()
    {
        TableLayoutPanel layoutPanelActions = WsFormUtils.NewTableLayoutPanel(layoutPanelMain, nameof(layoutPanelActions),
            3, 13, layoutPanelMain.ColumnCount - 3, 99);
        int columnCount = 0;

        if (ActionSettings.IsScalesTerminal)
        {
            ButtonScalesTerminal =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonScalesTerminal), columnCount++, 0);
            ButtonScalesTerminal.Click += ActionScalesTerminal;
        }
        if (ActionSettings.IsNewPallet)
        {
            ButtonNewPallet =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonNewPallet), columnCount++, 0);
            ButtonNewPallet.Click += ActionNewPallet;
        }
        if (ActionSettings.IsKneading)
        {
            ButtonKneading = 
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonKneading), columnCount++, 0);
            ButtonKneading.Click += ActionKneading;
        }
        if (ActionSettings.IsPrint)
        {
            ButtonPrint =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonPrint), columnCount++, 0);
            ButtonPrint.Click += ActionPreparePrint;
            ButtonPrint.Focus();
        }

        layoutPanelActions.ColumnCount = columnCount;
        WsFormUtils.SetTableLayoutPanelColumnStyles(layoutPanelActions);
        layoutPanelActions.RowCount = 1;
        WsFormUtils.SetTableLayoutPanelRowStyles(layoutPanelActions);
    }

    #endregion

    #region Public and private methods - Controls
    
    private void MouseSubscribe()
    {
        KeyboardMouseEvents = Hook.AppEvents();
        KeyboardMouseEvents.MouseDownExt += MouseDownExt;
    }
    
    private void MouseUnsubscribe()
    {
        KeyboardMouseEvents.MouseDownExt -= MouseDownExt;
        KeyboardMouseEvents.Dispose();
    }
    
    private void MouseDownExt(object sender, MouseEventExtArgs e)
    {
        if (!e.Button.Equals(MouseButtons.Middle))
            return;
        e.Handled = true;
        ActionPreparePrint(sender, e);
    }
    
    private void LoadLocalizationStatic(WsEnumLanguage lang)
    {
        WsLocaleCore.Lang = WsLocaleData.Lang = lang;
        MdInvokeControl.SetText(ButtonScalesTerminal, WsLocaleCore.LabelPrint.ButtonRunScalesTerminal());
        MdInvokeControl.SetText(ButtonNewPallet, WsLocaleCore.LabelPrint.ButtonNewPallet());
        MdInvokeControl.SetText(ButtonKneading, WsLocaleCore.LabelPrint.ButtonSetKneading);
        MdInvokeControl.SetText(ButtonPlu, WsLocaleCore.LabelPrint.ButtonPlu);
        MdInvokeControl.SetText(ButtonPrint, WsLocaleCore.Print.ActionPrint);
        MdInvokeControl.SetText(labelNettoWeight, WsLocaleCore.LabelPrint.FieldWeightNetto);
        MdInvokeControl.SetText(labelTareWeight, WsLocaleCore.LabelPrint.FieldWeightTare);
        MdInvokeControl.SetText(labelProductDate, WsLocaleCore.LabelPrint.FieldDate);
        MdInvokeControl.SetText(labelKneading, WsLocaleCore.LabelPrint.FieldKneading);
        
        MdInvokeControl.SetText(fieldTitle, $"{WsAppVersionHelper.Instance.AppTitle} {LabelSession.PublishDescription}");
        MdInvokeControl.SetText(this, WsAppVersionHelper.Instance.AppTitle);
        MdInvokeControl.SetText(fieldProductDate, string.Empty);
    }

    #endregion
}
