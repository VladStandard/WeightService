using System.Windows.Forms;

namespace WsLabelCore.Utils;

/// <summary>
/// WinForms навигация по контролам.
/// </summary>
#nullable enable
public static class WsFormNavigationUtils
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Пользовательская сессия.
    /// </summary>
    private static WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    /// <summary>
    /// SQL-менеджер прямого доступа к данным БД (используется ядром фреймворка).
    /// </summary>
    private static WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    /// <summary>
    /// SQL-менеджер доступа к данным БД (используется клиентами).
    /// </summary>
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    /// <summary>
    /// WinForms-контрол диалога.
    /// </summary>
    public static WsXamlDialogUserControl DialogUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол ввода цифр.
    /// </summary>
    public static WsXamlDigitsUserControl DigitsUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол настроек устройства.
    /// </summary>
    public static WsXamlDeviceSettingsUserControl DeviceSettingsUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол смены линии.
    /// </summary>
    public static WsXamlLinesUserControl LinesUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол замеса.
    /// </summary>
    public static WsXamlKneadingUserControl KneadingUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол навигации.
    /// </summary>
    public static WsFormNavigationUserControl NavigationUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол смены плу линии.
    /// </summary>
    public static WsXamlPlusLinesUserControl PlusLineUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол смены вложенности ПЛУ.
    /// </summary>
    public static WsXamlPlusNestingUserControl PlusNestingUserControl { get; } = new();
    /// <summary>
    /// Тег временных диалогов.
    /// </summary>
    private const string DialogTempTag = "DIALOG_TEMP";
    /// <summary>
    /// Флаг загрузки WinForms-контрола смены замеса.
    /// </summary>
    public static bool IsLoadKneading{ get; set; }
    /// <summary>
    /// Флаг загрузки WinForms-контрола настройки устройства.
    /// </summary>
    public static bool IsLoadDeviceSettings { get; set; }
    /// <summary>
    /// Флаг загрузки WinForms-контрола смены линии.
    /// </summary>
    public static bool IsLoadLines { get; set; }
    /// <summary>
    /// Флаг загрузки WinForms-контрола смены ПЛУ линии.
    /// </summary>
    public static bool IsLoadPlusLine { get; set; }
    /// <summary>
    /// Флаг загрузки WinForms-контрола смены вложенности ПЛУ.
    /// </summary>
    public static bool IsLoadPlusNesting { get; set; }
    
    #endregion

    #region Public and private methods

    /// <summary>
    /// Возврат из навигации.
    /// </summary>
    public static void ActionBackFromNavigation()
    {
        foreach (Control control in NavigationUserControl.Controls)
        {
            if (control is TableLayoutPanel tableLayoutPanel)
            {
                foreach (Control control2 in tableLayoutPanel.Controls)
                {
                    if (control2 is WsFormBaseUserControl formUserControl)
                    {
                        MdInvokeControl.SetVisible(formUserControl, false);
                    }
                }
            }
        }
        MdInvokeControl.SetVisible(NavigationUserControl, false);
    }

    /// <summary>
    /// Навигация в WinForms-контрол настроек устройства.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToExistsDeviceSettings(Action<WsFormBaseUserControl, string> showNavigation)
    {
        // Загрузка из сессии пользователя.
        DeviceSettingsUserControl.ViewModel.Line = LabelSession.Line;
        DeviceSettingsUserControl.ViewModel.Device = LabelSession.Line.Device;
        DeviceSettingsUserControl.ViewModel.Devices =
            WsSqlContextManagerHelper.Instance.DeviceRepository.GetEnumerable(WsSqlCrudConfigFactory.GetCrudAll()).ToList();
        DeviceSettingsUserControl.ViewModel.DeviceSettingsFks = new(
            WsSqlContextManagerHelper.Instance.DeviceSettingsFksRepository.GetEnumerableByDevice(DeviceSettingsUserControl.ViewModel.Device));

        DeviceSettingsUserControl.ViewModel.UpdateCommandsFromActions();
        DeviceSettingsUserControl.ViewModel.SetupButtonsCancelYes(NavigationUserControl.Width);
        showNavigation(DeviceSettingsUserControl, WsLocaleCore.LabelPrint.SwitchDeviceSettings);
        NavigationUserControl.SwitchUserControl(DeviceSettingsUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол смены линии.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToExistsLines(Action<WsFormBaseUserControl, string> showNavigation)
    {
        // Загрузка из сессии пользователя.
        LinesUserControl.ViewModel.ProductionSites = LabelSession.ContextCache.Areas;
        LinesUserControl.ViewModel.Lines = LabelSession.ContextCache.Lines;
        LinesUserControl.ViewModel.ProductionSite = LabelSession.Area;
        LinesUserControl.ViewModel.Line = LabelSession.Line;

        LinesUserControl.ViewModel.UpdateCommandsFromActions();
        LinesUserControl.ViewModel.SetupButtonsCancelYes(NavigationUserControl.Width);
        showNavigation(LinesUserControl, WsLocaleCore.LabelPrint.SwitchLine);
        NavigationUserControl.SwitchUserControl(LinesUserControl);
    }

    /// <summary>
    /// Навигация в существующий WinForms-контрол диалога Отмена/Да.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    public static void NavigateToExistsDialogCancelYes(Action<WsFormBaseUserControl, string> showNavigation,
        string message, bool isLog, WsEnumLogType logType, Action actionCancel, Action actionYes)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        DialogUserControl.ViewModel.SetupButtonsCancelYes(message, actionCancel, actionYes, ActionBackFromNavigation, NavigationUserControl.Width);
        showNavigation(DialogUserControl, WsLocaleCore.LabelPrint.OperationControl);
        NavigationUserControl.SwitchUserControl(DialogUserControl);
    }

    /// <summary>
    /// Навигация в новый WinForms-контрол диалога.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    /// <param name="dialogType"></param>
    /// <param name="actions"></param>
    public static void NavigateToNewDialog(Action<WsFormBaseUserControl, string> showNavigation,
        string message, bool isLog, WsEnumLogType logType, WsEnumDialogType dialogType, List<Action> actions)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        WsXamlDialogUserControl dialog = new();
        dialog.SetupUserControl();
        dialog.Tag = DialogTempTag;
        dialog.SetupActions(dialogType, actions);
        dialog.SetupButtons(dialogType, actions, message, NavigationUserControl.Width);
        showNavigation(dialog, WsLocaleCore.LabelPrint.OperationControl);
        NavigationUserControl.SwitchUserControl(dialog);
    }

    /// <summary>
    /// Очистить новые диалоги.
    /// </summary>
    public static void ClearNewDialogs()
    {
        foreach (Control control in NavigationUserControl.Controls)
        {
            if (control is TableLayoutPanel tableLayoutPanel)
            {
                foreach (Control control2 in tableLayoutPanel.Controls)
                {
                    if (control2 is WsXamlDialogUserControl dialogUserControl)
                    {
                        if (dialogUserControl.Tag is not null && dialogUserControl.Tag.Equals(DialogTempTag))
                            dialogUserControl.Dispose();
                    }
                }
            }
        }
        GC.Collect();
    }

    /// <summary>
    /// Навигация в WinForms-контрол диалога Ок.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    public static void NavigateToExistsDialogOk(Action<WsFormBaseUserControl, string> showNavigation, 
        string message, bool isLog, WsEnumLogType logType)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        DialogUserControl.ViewModel.SetupButtonsOk(message, ActionBackFromNavigation, NavigationUserControl.Width);
        showNavigation(DialogUserControl, WsLocaleCore.LabelPrint.OperationControl);
        NavigationUserControl.SwitchUserControl(DialogUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол ввода цифр.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    public static void NavigateToExistsDigitsUserControl(Action<WsFormBaseUserControl, string> showNavigation,
        string message, bool isLog, WsEnumLogType logType, Action actionCancel, Action actionYes)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        DigitsUserControl.ViewModel.SetupButtonsCancelYes(message, actionCancel, actionYes, ActionBackFromNavigation, NavigationUserControl.Width);
        showNavigation(DigitsUserControl, WsLocaleCore.LabelPrint.OperationControl);
        NavigationUserControl.SwitchUserControl(DigitsUserControl);
    }
    /// <summary>
    /// Навигация в WinForms-контрол замеса.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToExistsKneading(Action<WsFormBaseUserControl, string> showNavigation)
    {
        KneadingUserControl.ViewModel.UpdateCommandsFromActions();
        KneadingUserControl.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(KneadingUserControl,
            LabelSession.PluLine.Plu.IsCheckWeight
            ? $"{WsLocaleCore.LabelPrint.SwitchKneading} {WsLocaleCore.LabelPrint.PluWeight} | {LabelSession.PluLine.Plu.Number} | {LabelSession.PluLine.Plu.Name}"
            : $"{WsLocaleCore.LabelPrint.SwitchKneading} {WsLocaleCore.LabelPrint.PluCount} | {LabelSession.PluLine.Plu.Number} | {LabelSession.PluLine.Plu.Name}");
        NavigationUserControl.SwitchUserControl(KneadingUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол смены ПЛУ линии.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToExistsPlusLine(Action<WsFormBaseUserControl, string> showNavigation)
    {
        PlusLineUserControl.ViewModel.UpdateCommandsFromActions();
        PlusLineUserControl.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(PlusLineUserControl, WsLocaleCore.LabelPrint.SwitchPluLine);
        NavigationUserControl.SwitchUserControl(PlusLineUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол смены вложенности ПЛУ.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToExistsPlusNesting(Action<WsFormBaseUserControl, string> showNavigation)
    {
        // Загрузка из сесси пользователя.
        PlusNestingUserControl.ViewModel.PlusNestings = 
            new WsSqlViewPluNestingRepository().GetEnumerable((ushort)LabelSession.PluLine.Plu.Number).ToList();
        PlusNestingUserControl.ViewModel.PluNesting = LabelSession.ViewPluNesting;

        PlusNestingUserControl.ViewModel.UpdateCommandsFromActions();
        PlusNestingUserControl.ViewModel.SetupButtonsCancelYes(NavigationUserControl.Width);
        showNavigation(PlusNestingUserControl, WsLocaleCore.LabelPrint.SwitchPluNesting);
        NavigationUserControl.SwitchUserControl(PlusNestingUserControl);
    }

    private static void ShowNewOperationControlLogType(string message, WsEnumLogType logType,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        switch (logType)
        {
            case WsEnumLogType.Error:
                ContextManager.ContextItem.SaveLogErrorWithInfo(message, filePath, lineNumber, memberName);
                break;
            case WsEnumLogType.Information:
            case WsEnumLogType.None:
                ContextManager.ContextItem.SaveLogInformation(message);
                break;
            case WsEnumLogType.Question:
                ContextManager.ContextItem.SaveLogQuestion(message);
                break;
            case WsEnumLogType.Warning:
                ContextManager.ContextItem.SaveLogWarning(message);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(logType), logType.ToString());
        
        }
    }

    public static WsSqlDeviceModel SetNewDeviceWithQuestion(Action<WsFormBaseUserControl, string> showNavigation,
        WsSqlDeviceModel device, string ip, string mac)
    {
        if (device.IsNew)
        {
            // Навигация в WinForms-контрол диалога Отмена/Да.
            NavigateToNewDialog(showNavigation,
                WsLocaleCore.LabelPrint.HostNotFound(device.Name) + Environment.NewLine + WsLocaleCore.LabelPrint.QuestionWriteToDb,
                false, WsEnumLogType.Information, WsEnumDialogType.CancelYes, new() { () => { }, ActionYes });
            void ActionYes()
            {
                device = new()
                {
                    Name = device.Name,
                    Ipv4 = ip,
                    MacAddress = new(mac),
                    CreateDt = DateTime.Now,
                    ChangeDt = DateTime.Now,
                    LoginDt = DateTime.Now,
                    IsMarked = false,
                };
                SqlCore.Save(device);
            }
        }
        else
        {
            device.Ipv4 = ip;
            device.MacAddress = new(mac);
            device.ChangeDt = DateTime.Now;
            device.LoginDt = DateTime.Now;
            device.IsMarked = false;
            SqlCore.Update(device);
        }
        return device;
    }

    private static void CatchExceptionCore(Action<WsFormBaseUserControl, string> showNavigation, Exception ex, 
        string filePath, int lineNumber, string memberName)
    {
        ContextManager.ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

        string message = ex.InnerException is null
            ? ex.Message
            : ex.Message + Environment.NewLine + ex.InnerException.Message;
        // Навигация в WinForms-контрол диалога Ок.
        NavigateToExistsDialogOk(showNavigation, 
            $"{WsLocaleCore.LabelPrint.Method}: {memberName}." + Environment.NewLine +
            $"{WsLocaleCore.LabelPrint.Line}: {lineNumber}." + Environment.NewLine + message, true, WsEnumLogType.Error);
    }

    private static void CatchExceptionSimpleCore(Exception ex, string filePath, int lineNumber, string memberName) => 
        ContextManager.ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

    /// <summary>
    /// Show catch exception window.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="ex"></param>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static void CatchException(Action<WsFormBaseUserControl, string> showNavigation, Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        CatchExceptionCore(showNavigation, ex, filePath, lineNumber, memberName);

    /// <summary>
    /// Show catch exception window.
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static void CatchExceptionSimple(Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        CatchExceptionSimpleCore(ex, filePath, lineNumber, memberName);
    
    public static void ActionTryCatch(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            CatchExceptionSimple(ex);
        }
    }

    public static void ActionTryCatch(IWin32Window win32Window, Action<WsFormBaseUserControl, string> showNavigation, 
        Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            // ActionMakeScreenShot(win32Window, LabelSession.Line);
            CatchException(showNavigation, ex);
        }
    }
    
    #endregion
}