// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLabelCore.Controls;

namespace ScalesUI.Forms;

public partial class WsMainForm : Form
{
    #region Public and private fields, properties, constructor

    private ActionSettingsModel ActionSettings { get; set; }
    private AppVersionHelper AppVersion => AppVersionHelper.Instance;
    private WsDebugHelper Debug => WsDebugHelper.Instance;
    private WsFontsSettingsHelper FontsSettings => WsFontsSettingsHelper.Instance;
    private IKeyboardMouseEvents KeyboardMouseEvents { get; set; }
    private WsProcHelper Proc => WsProcHelper.Instance;
    private WsSchedulerHelper WsScheduler => WsSchedulerHelper.Instance;
    private WsUserSessionHelper UserSession => WsUserSessionHelper.Instance;
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    private Button ButtonLine { get; set; }
    private Button ButtonPluNestingFk { get; set; }
    private Button ButtonKneading { get; set; }
    private Button ButtonNewPallet { get; set; }
    private Button ButtonPlu { get; set; }
    private Button ButtonPrint { get; set; }
    private Button ButtonScalesInit { get; set; }
    private Button ButtonScalesTerminal { get; set; }

    /// <summary>
    /// Отладочный флаг для сквозных тестов печати, без диалогов.
    /// </summary>
    private const bool IsSkipDialogs = false;
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

    /// <summary>
    /// Загрузить данные в фоне.
    /// </summary>
    private void MainFormLoadAtBackground()
    {
        // Хуки мышки.
        KeyboardMouseEvents = Hook.AppEvents();
        KeyboardMouseEvents.MouseDownExt += MouseDownExt;
        // Настройка кнопок.
        SetupButtons();
        // Загрузка шрифтов.
        LoadFonts();
        // Прочее.
        LabelSession.NewPallet();
        MdInvokeControl.SetText(this, AppVersion.AppTitle);
        MdInvokeControl.SetText(fieldProductDate, string.Empty);
        LoadMainControls();
        LoadLocalizationStatic(Lang.Russian);
        // Планировщик.
        WsScheduler.Load(this);
        // Загрузка остальных контролов.
        LoadNavigationUserControl();
    }

    private MdPrinterModel GetMdPrinter(WsSqlPrinterModel scalePrinter) => new()
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
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            UserSession.StopwatchMain = Stopwatch.StartNew();
            UserSession.StopwatchMain.Restart();
            // Загрузка контрола ожидания.
            LoadNavigationWaitUserControl();
            // Проверка линии.
            LabelSession.SetSessionForLabelPrint(ShowFormUserControl);
            if (LabelSession.DeviceScaleFk.IsNew)
            {
                string message = LocaleCore.Scales.RegistrationWarningLineNotFound(LabelSession.DeviceName);
                WsFormNavigationUtils.DialogUserControl.Page.ViewModel.SetupButtonsOk(
                    message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin,
                    ActionExit, WsFormNavigationUtils.NavigationUserControl.Width);
                // Навигация в контрол диалога Ок.
                WsFormNavigationUtils.NavigateToMessageUserControlOk(ShowFormUserControl, message, true, WsEnumLogType.Error);
                ContextManager.ContextItem.SaveLogError(new Exception(message));
                return;
            }
            // Проверка повторного запуска.
            _ = new Mutex(true, System.Windows.Forms.Application.ProductName, out bool isCreatedNew);
            if (!isCreatedNew)
            {
                string message = $"{LocaleCore.Strings.Application} {System.Windows.Forms.Application.ProductName} {LocaleCore.Scales.AlreadyRunning}!";
                WsFormNavigationUtils.DialogUserControl.Page.ViewModel.SetupButtonsOk(message, ActionExit, 
                    WsFormNavigationUtils.NavigationUserControl.Width);
                // Навигация в контрол диалога Ок.
                WsFormNavigationUtils.NavigateToMessageUserControlOk(ShowFormUserControl, message, true, WsEnumLogType.Error);
                ContextManager.ContextItem.SaveLogWarning(message);
                return;
            }
            // Навигация в контрол ожидания.
            WsFormNavigationUtils.NavigateToWaitUserControl(ShowFormUserControl,  LocaleCore.Scales.AppLoad, LocaleCore.Scales.AppLoadDescription);
            // Загрузка фоном.
            MainFormLoadAtBackground();
            // Авто-возврат из контрола на главную форму.
            WsFormNavigationUtils.WaitUserControl.Page.ViewModel.CmdCustom.Relay();
            // Логи.
            UserSession.StopwatchMain.Stop();
            ContextManager.ContextItem.SaveLogMemory(
                UserSession.PluginMemory.GetMemorySizeAppMb(), UserSession.PluginMemory.GetMemorySizeFreeMb());
            ContextManager.ContextItem.SaveLogInformation(
                $"{LocaleData.Program.IsLoaded}. " + Environment.NewLine +
                $"{LocaleCore.Scales.ScreenResolution}: {Width} x {Height}." + Environment.NewLine +
                $"{nameof(LocaleData.Program.TimeSpent)}: {UserSession.StopwatchMain.Elapsed}.");
        });
    }

    private static void ActionExit() => System.Windows.Forms.Application.Exit();

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

        // Основной принтер.
        if (LabelSession.Line.PrinterMain is not null)
        {
            LabelSession.PluginPrintMain.Init(new(0_500, 0_250), new(0_250, 0_250),
                new(0_250, 0_250),
                LabelSession.PrintBrandMain, GetMdPrinter(LabelSession.Line.PrinterMain), fieldPrintMain, fieldPrintMainExt, true);
            MdInvokeControl.SetVisible(fieldPrintMain, true);
            MdInvokeControl.SetVisible(fieldPrintMainExt, Debug.IsDevelop);
            LabelSession.PluginPrintMain.Execute();
            LabelSession.PluginPrintMain.SetOdometorUserLabel(1);
        }

        // Транспортный принтер.
        if (LabelSession.Line.IsShipping)
        {
            if (LabelSession.Line.PrinterShipping is not null)
            {
                LabelSession.PluginPrintShipping.Init(new(0_500, 0_250),
                    new(0_250, 0_250), new(0_250, 0_250),
                    LabelSession.PrintBrandShipping, GetMdPrinter(LabelSession.Line.PrinterShipping), fieldPrintShipping, fieldPrintShippingExt, false);
                MdInvokeControl.SetVisible(fieldPrintShipping, true);
                MdInvokeControl.SetVisible(fieldPrintShippingExt, Debug.IsDevelop);
                LabelSession.PluginPrintShipping.Execute();
                LabelSession.PluginPrintShipping.SetOdometorUserLabel(1);
            }
        }

        // Метки.
        UserSession.PluginLabels.Init(
            new(0_250, 0_250), new(0_250, 0_250),
            new(0_250, 0_250), fieldPlu, fieldProductDate, fieldKneading);
        UserSession.PluginLabels.Execute();
        MdInvokeControl.SetText(fieldTitle, $"{AppVersionHelper.Instance.AppTitle}. {LabelSession.PublishDescription}.");
        MdInvokeControl.SetBackColor(fieldTitle, Color.Transparent);
    }

    /// <summary>
    /// Загрузить шрифты.
    /// </summary>
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
        ButtonPrint.Font = FontsSettings.FontButtons;
        ButtonPrint.BackColor = ColorTranslator.FromHtml("#ff7f50");
    }

    /// <summary>
    /// Настроить кнопки.
    /// </summary>
    private void SetupButtons()
    {
        ActionSettings = new()
        {
            // Устройства.
            IsDevice = true,
            IsPlu = true,
            IsNesting = true,
            // Действия.
            IsKneading = true,
            IsNewPallet = false,
            IsOrder = LabelSession.Line.IsOrder,
            IsPrint = true,
            IsScalesInit = false,
            IsScalesTerminal = true,
        };

        CreateButtonsDevices();
        CreateButtonsActions();
    }

    /// <summary>
    /// Создать кнопки устройств.
    /// </summary>
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
        else ButtonLine = new();

        if (ActionSettings.IsPlu)
        {
            ButtonPlu = WsFormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPlu), 1, rowCount++);
            ButtonPlu.Click += ActionSwitchPlu;
        }
        else ButtonPlu = new();

        if (ActionSettings.IsNesting)
        {
            ButtonPluNestingFk = WsFormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPluNestingFk), 1, rowCount++);
            ButtonPluNestingFk.Click += ActionSwitchPluNesting;
        }
        else ActionSettings = new();

        layoutPanelDevice.ColumnCount = 1;
        WsFormUtils.SetTableLayoutPanelColumnStyles(layoutPanelDevice);
        layoutPanelDevice.RowCount = rowCount;
        WsFormUtils.SetTableLayoutPanelRowStyles(layoutPanelDevice);
    }

    /// <summary>
    /// Создать кнопки действий.
    /// </summary>
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
        else
        {
            ButtonScalesTerminal = new();
        }

        if (ActionSettings.IsScalesInit)
        {
            ButtonScalesInit =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonScalesInit), columnCount++, 0);
            ButtonScalesInit.Click += ActionScalesInit;
        }
        else
        {
            ButtonScalesInit = new();
        }

        if (ActionSettings.IsNewPallet)
        {
            ButtonNewPallet =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonNewPallet), columnCount++, 0);
            ButtonNewPallet.Click += ActionNewPallet;
        }
        else
        {
            ButtonNewPallet = new();
        }

        if (ActionSettings.IsKneading)
        {
            ButtonKneading = 
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonKneading), columnCount++, 0);
            ButtonKneading.Click += ActionKneading;
        }
        else
        {
            ButtonKneading = new();
        }

        if (ActionSettings.IsPrint)
        {
            ButtonPrint =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonPrint), columnCount++, 0);
            ButtonPrint.Click += ActionPreparePrint;
            ButtonPrint.Focus();
        }
        else
        {
            ButtonPrint = new();
        }

        layoutPanelActions.ColumnCount = columnCount;
        WsFormUtils.SetTableLayoutPanelColumnStyles(layoutPanelActions);
        layoutPanelActions.RowCount = 1;
        WsFormUtils.SetTableLayoutPanelRowStyles(layoutPanelActions);
    }

    #endregion

    #region Public and private methods - Controls

    private void MouseDownExt(object sender, MouseEventExtArgs e)
    {
        if (e.Button == MouseButtons.Middle)
            ActionPreparePrint(sender, e);
    }

    /// <summary>
    /// Загрузить локализацию.
    /// </summary>
    /// <param name="lang"></param>
    private void LoadLocalizationStatic(Lang lang)
    {
        LocaleCore.Lang = LocaleData.Lang = lang;
        MdInvokeControl.SetText(ButtonScalesTerminal, LocaleCore.Scales.ButtonRunScalesTerminal);
        MdInvokeControl.SetText(ButtonScalesInit, LocaleCore.Scales.ButtonScalesInitShort);
        MdInvokeControl.SetText(ButtonNewPallet, LocaleCore.Scales.ButtonNewPallet);
        MdInvokeControl.SetText(ButtonKneading, LocaleCore.Scales.ButtonSetKneading);
        MdInvokeControl.SetText(ButtonPlu, LocaleCore.Scales.ButtonPlu);
        MdInvokeControl.SetText(ButtonPrint, LocaleCore.Print.ActionPrint);
        MdInvokeControl.SetText(labelNettoWeight, LocaleCore.Scales.FieldWeightNetto);
        MdInvokeControl.SetText(labelPackageWeight, LocaleCore.Scales.FieldWeightTare);
        MdInvokeControl.SetText(labelProductDate, LocaleCore.Scales.FieldDate);
        MdInvokeControl.SetText(labelKneading, LocaleCore.Scales.FieldKneading);
    }

    #endregion
}