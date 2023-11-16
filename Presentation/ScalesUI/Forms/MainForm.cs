using Ws.DataCore.Utils;
using Ws.StorageCore.Entities.SchemaScale.Scales;
using Ws.StorageCore.Enums;
namespace ScalesUI.Forms;

public sealed partial class MainForm : Form
{
    #region Public and private fields, properties, constructor
    
    private DebugHelper Debug => DebugHelper.Instance;
    private FontsSettingsHelper FontsSettings => FontsSettingsHelper.Instance;
    private ActionSettingsModel ActionSettings { get; set; }
    private IKeyboardMouseEvents KeyboardMouseEvents { get; set; }
    private UserSessionHelper UserSession => UserSessionHelper.Instance;
    private PrintSessionHelper PrintSession => PrintSessionHelper.Instance;
    private SqlContextCacheHelper ContextCache => SqlContextCacheHelper.Instance;
    private LabelSessionHelper LabelSession => LabelSessionHelper.Instance;
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

    public MainForm()
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
        string message = LocaleCore.LabelPrint.RegistrationWarningLineNotFound(LabelSessionHelper.DeviceName);
        FormNavigationUtils.NavigateToNewDialog(ShowFormUserControl, message, true, LogTypeEnum.Error,
        EnumDialogType.Ok, new() { ActionCloseAfterNotLine });
        return true;
    }

    private bool IsAppExists()
    {
        _ = new Mutex(true, Application.ProductName, out bool isCreatedNew);
        if (isCreatedNew) return false;
        string message = $"{LocaleCore.Strings.Application} {Application.ProductName} {LocaleCore.LabelPrint.AlreadyRunning}!";
        FormNavigationUtils.DialogUserControl.ViewModel.SetupButtonsOk(message, ActionExit, 
        FormNavigationUtils.NavigationUserControl.Width);
        // Навигация в контрол диалога Ок.
        FormNavigationUtils.NavigateToNewDialog(ShowFormUserControl, message, true, LogTypeEnum.Error,
        EnumDialogType.Ok, new() { ActionFinally });
        ContextItem.SaveLogWarning(message);
        return true;
    }
     
    private void OnLoad(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            if (IsAppExists()) return;
            
            UserSession.StopwatchMain = Stopwatch.StartNew();
            LocaleCore.Lang = LocaleData.Lang = EnumLanguage.Russian;
            
            SetupNavigationUserControl();
            
            LabelSession.SetSessionForLabelPrint(ShowFormUserControl);
            
            if (IsNewLine()) return;
            
            LabelSession.Line.Version = AssemblyUtils.GetAppVersion(Assembly.GetExecutingAssembly());
            LabelSession.Line.ClickOnce = AssemblyUtils.GetClickOnceNetworkInstallDirectory();
            
            new SqlLineRepository().Update(LabelSession.Line);
            
            this.SetupResolution();
            MainFormLoadAtBackground();
            ActionFinally(); SaveStartLog();
        });
    }

    private void SaveStartLog()
    {
        UserSession.StopwatchMain.Stop();
         StringBuilder log = new();
         log.AppendLine($"{LocaleData.Program.IsLoaded}.")
             .AppendLine($"{LocaleCore.LabelPrint.ScreenResolution}: {Width} x {Height}.")
             .AppendLine($"Время загрузки: {UserSession.StopwatchMain.Elapsed}.");
         ContextItem.SaveLogInformation(log.ToString());
     }
    
    private void MainFormLoadAtBackground()
    {
        MouseSubscribe();
        SetupButtons();
        LoadFonts();
        LoadLocalizationStatic(EnumLanguage.Russian);
        
        LabelSession.NewPallet();
        SetupPlugins();
        LoadNavigationUserControl();
    }

    private static void ActionExit() => Application.Exit();
    
    private void SetupPlugins()
    {
        UserSession.PluginMassa.Init(new(1_000), new(0_150),
            new(0_150), fieldNettoWeight, fieldMassa, ResetWarning);
        PluginMassaExecute();

        MdInvokeControl.SetVisible(fieldPrintMain, true);
        // Основной принтер.
        switch (LabelSession.Line.Printer.Type)
        {

            case PrinterTypeEnum.Tsc:
                LabelSession.PluginPrintTscMain = new();
                LabelSession.PluginPrintTscMain.InitTsc(new(0_500),
                new(0_500), new(0_500),
                LabelSession.Line.Printer, fieldPrintMain);
                LabelSession.PluginPrintTscMain.Execute();
                break;
            case PrinterTypeEnum.Zebra:
                LabelSession.PluginPrintZebraMain = new();
                LabelSession.PluginPrintZebraMain.InitZebra(new(5000),
                new(0_250), new(0_250),
                LabelSession.Line.Printer, fieldPrintMain);
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
        TableLayoutPanel layoutPanelDevice = FormUtils.NewTableLayoutPanel(layoutPanelMain, nameof(layoutPanelDevice),
            1, 13, 2, 98);
        int rowCount = 0;

        if (ActionSettings.IsDevice)
        {
            ButtonLine = FormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonLine), 1, rowCount++);
            ButtonLine.Click += ActionSwitchLine;
        }
        if (ActionSettings.IsPlu)
        {
            ButtonPlu = FormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPlu), 1, rowCount++);
            ButtonPlu.Click += ActionSwitchPluLine;
        }
        if (ActionSettings.IsNesting)
        {
            ButtonPluNestingFk = FormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPluNestingFk), 1, rowCount++);
            ButtonPluNestingFk.Click += ActionSwitchPluNesting;
        }
        layoutPanelDevice.ColumnCount = 1;
        FormUtils.SetTableLayoutPanelColumnStyles(layoutPanelDevice);
        layoutPanelDevice.RowCount = rowCount;
        FormUtils.SetTableLayoutPanelRowStyles(layoutPanelDevice);
    }
    
    private void CreateButtonsActions()
    {
        TableLayoutPanel layoutPanelActions = FormUtils.NewTableLayoutPanel(layoutPanelMain, nameof(layoutPanelActions),
            3, 13, layoutPanelMain.ColumnCount - 3, 99);
        int columnCount = 0;

        if (ActionSettings.IsScalesTerminal)
        {
            ButtonScalesTerminal =
                FormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonScalesTerminal), columnCount++, 0);
            ButtonScalesTerminal.Click += ActionScalesTerminal;
        }
        if (ActionSettings.IsNewPallet)
        {
            ButtonNewPallet =
                FormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonNewPallet), columnCount++, 0);
            ButtonNewPallet.Click += ActionNewPallet;
        }
        if (ActionSettings.IsKneading)
        {
            ButtonKneading = 
                FormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonKneading), columnCount++, 0);
            ButtonKneading.Click += ActionKneading;
        }
        if (ActionSettings.IsPrint)
        {
            ButtonPrint =
                FormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonPrint), columnCount++, 0);
            ButtonPrint.Click += ActionPreparePrint;
            ButtonPrint.Focus();
        }

        layoutPanelActions.ColumnCount = columnCount;
        FormUtils.SetTableLayoutPanelColumnStyles(layoutPanelActions);
        layoutPanelActions.RowCount = 1;
        FormUtils.SetTableLayoutPanelRowStyles(layoutPanelActions);
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
    
    private void LoadLocalizationStatic(EnumLanguage lang)
    {
        LocaleCore.Lang = LocaleData.Lang = lang;
        MdInvokeControl.SetText(ButtonScalesTerminal, LocaleCore.LabelPrint.ButtonRunScalesTerminal());
        MdInvokeControl.SetText(ButtonNewPallet, LocaleCore.LabelPrint.ButtonNewPallet());
        MdInvokeControl.SetText(ButtonKneading, LocaleCore.LabelPrint.ButtonSetKneading);
        MdInvokeControl.SetText(ButtonPlu, LocaleCore.LabelPrint.ButtonPlu);
        MdInvokeControl.SetText(ButtonPrint, LocaleCore.Print.ActionPrint);
        MdInvokeControl.SetText(labelNettoWeight, LocaleCore.LabelPrint.FieldWeightNetto);
        MdInvokeControl.SetText(labelTareWeight, LocaleCore.LabelPrint.FieldWeightTare);
        MdInvokeControl.SetText(labelProductDate, LocaleCore.LabelPrint.FieldDate);
        MdInvokeControl.SetText(labelKneading, LocaleCore.LabelPrint.FieldKneading);
        
        MdInvokeControl.SetText(fieldTitle, $"{AppVersionHelper.Instance.AppTitle} {LabelSession.PublishDescription}");
        MdInvokeControl.SetText(this, AppVersionHelper.Instance.AppTitle);
        MdInvokeControl.SetText(fieldProductDate, string.Empty);
    }

    #endregion
}
