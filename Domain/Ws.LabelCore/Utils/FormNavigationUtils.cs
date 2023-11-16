using System.Windows.Forms;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Enums;

namespace Ws.LabelCore.Utils;

/// <summary>
/// WinForms навигация по контролам.
/// </summary>
#nullable enable
public static class FormNavigationUtils
{
    #region Public and private fields, properties, constructor

    private static SqlContextItemHelper ContextItem => SqlContextItemHelper.Instance;
    
    /// <summary>
    /// Пользовательская сессия.
    /// </summary>
    private static LabelSessionHelper LabelSession => LabelSessionHelper.Instance;
    /// <summary>
    /// SQL-менеджер прямого доступа к данным БД (используется ядром фреймворка).
    /// </summary>
    private static SqlCoreHelper SqlCore => SqlCoreHelper.Instance;

    /// <summary>
    /// WinForms-контрол диалога.
    /// </summary>
    public static XamlDialogUserControl DialogUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол ввода цифр.
    /// </summary>
    public static XamlDigitsUserControl DigitsUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол смены линии.
    /// </summary>
    public static XamlLinesUserControl LinesUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол замеса.
    /// </summary>
    public static XamlKneadingUserControl KneadingUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол навигации.
    /// </summary>
    public static FormNavigationUserControl NavigationUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол смены плу линии.
    /// </summary>
    public static XamlPlusLinesUserControl PlusLineUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол смены вложенности ПЛУ.
    /// </summary>
    public static XamlPlusNestingUserControl PlusNestingUserControl { get; } = new();
    /// <summary>
    /// Тег временных диалогов.
    /// </summary>
    private const string DialogTempTag = "DIALOG_TEMP";
    /// <summary>
    /// Флаг загрузки WinForms-контрола смены замеса.
    /// </summary>
    public static bool IsLoadKneading{ get; set; }
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
                    if (control2 is FormBaseUserControl formUserControl)
                    {
                        MdInvokeControl.SetVisible(formUserControl, false);
                    }
                }
            }
        }
        MdInvokeControl.SetVisible(NavigationUserControl, false);
    }
    
    /// <summary>
    /// Навигация в WinForms-контрол смены линии.
    /// </summary>
    public static void NavigateToExistsLines(Action<FormBaseUserControl, string> showNavigation)
    {
        // Загрузка из сессии пользователя.
        LinesUserControl.ViewModel.ProductionSites = LabelSessionHelper.ContextCache.Areas;
        LinesUserControl.ViewModel.Lines = LabelSessionHelper.ContextCache.Lines;
        LinesUserControl.ViewModel.ProductionSite = LabelSession.Area;
        LinesUserControl.ViewModel.Line = LabelSession.Line;

        LinesUserControl.ViewModel.UpdateCommandsFromActions();
        LinesUserControl.ViewModel.SetupButtonsCancelYes(NavigationUserControl.Width);
        showNavigation(LinesUserControl, LocaleCore.LabelPrint.SwitchLine);
        NavigationUserControl.SwitchUserControl(LinesUserControl);
    }
    
    /// <summary>
    /// Навигация в новый WinForms-контрол диалога.
    /// </summary>
    public static void NavigateToNewDialog(Action<FormBaseUserControl, string> showNavigation,
        string message, bool isLog, LogTypeEnum logType, EnumDialogType dialogType, List<Action> actions)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        XamlDialogUserControl dialog = new();
        dialog.SetupUserControl();
        dialog.Tag = DialogTempTag;
        dialog.SetupActions(dialogType, actions);
        dialog.SetupButtons(dialogType, actions, message, NavigationUserControl.Width);
        showNavigation(dialog, LocaleCore.LabelPrint.OperationControl);
        NavigationUserControl.SwitchUserControl(dialog);
    }

    /// <summary>
    /// Очистить новые диалоги.
    /// </summary>
    public static void ClearNewDialogs()
    {
        foreach (Control control in NavigationUserControl.Controls)
        {
            if (control is not TableLayoutPanel tableLayoutPanel)
                continue;
            foreach (Control control2 in tableLayoutPanel.Controls)
            {
                if (control2 is not XamlDialogUserControl dialogUserControl)
                    continue;
                if (dialogUserControl.Tag is not null && dialogUserControl.Tag.Equals(DialogTempTag))
                    dialogUserControl.Dispose();
            }
        }
        GC.Collect();
    }

    /// <summary>
    /// Навигация в WinForms-контрол диалога Ок.
    /// </summary>
    private static void NavigateToExistsDialogOk(Action<FormBaseUserControl, string> showNavigation, 
        string message, bool isLog, LogTypeEnum logType)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        DialogUserControl.ViewModel.SetupButtonsOk(message, ActionBackFromNavigation, NavigationUserControl.Width);
        showNavigation(DialogUserControl, LocaleCore.LabelPrint.OperationControl);
        NavigationUserControl.SwitchUserControl(DialogUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол ввода цифр.
    /// </summary>
    public static void NavigateToExistsDigitsUserControl(Action<FormBaseUserControl, string> showNavigation,
        string message, bool isLog, LogTypeEnum logType, Action actionCancel, Action actionYes)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        DigitsUserControl.ViewModel.SetupButtonsCancelYes(message, actionCancel, actionYes, ActionBackFromNavigation, NavigationUserControl.Width);
        showNavigation(DigitsUserControl, LocaleCore.LabelPrint.OperationControl);
        NavigationUserControl.SwitchUserControl(DigitsUserControl);
    }
    /// <summary>
    /// Навигация в WinForms-контрол замеса.
    /// </summary>
    public static void NavigateToExistsKneading(Action<FormBaseUserControl, string> showNavigation)
    {
        KneadingUserControl.ViewModel.UpdateCommandsFromActions();
        KneadingUserControl.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(KneadingUserControl,
            LabelSession.PluLine.Plu.IsCheckWeight
            ? $"{LocaleCore.LabelPrint.SwitchKneading} {LocaleCore.LabelPrint.PluWeight} | {LabelSession.PluLine.Plu.Number} | {LabelSession.PluLine.Plu.Name}"
            : $"{LocaleCore.LabelPrint.SwitchKneading} {LocaleCore.LabelPrint.PluCount} | {LabelSession.PluLine.Plu.Number} | {LabelSession.PluLine.Plu.Name}");
        NavigationUserControl.SwitchUserControl(KneadingUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол смены ПЛУ линии.
    /// </summary>
    public static void NavigateToExistsPlusLine(Action<FormBaseUserControl, string> showNavigation)
    {
        PlusLineUserControl.ViewModel.UpdateCommandsFromActions();
        PlusLineUserControl.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(PlusLineUserControl, LocaleCore.LabelPrint.SwitchPluLine);
        NavigationUserControl.SwitchUserControl(PlusLineUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол смены вложенности ПЛУ.
    /// </summary>
    public static void NavigateToExistsPlusNesting(Action<FormBaseUserControl, string> showNavigation)
    {
        // Загрузка из сесси пользователя.
        PlusNestingUserControl.ViewModel.PlusNestings = 
            new SqlViewPluNestingRepository().GetEnumerable((ushort)LabelSession.PluLine.Plu.Number).ToList();
        PlusNestingUserControl.ViewModel.PluNesting = LabelSession.ViewPluNesting;

        PlusNestingUserControl.ViewModel.UpdateCommandsFromActions();
        PlusNestingUserControl.ViewModel.SetupButtonsCancelYes(NavigationUserControl.Width);
        showNavigation(PlusNestingUserControl, LocaleCore.LabelPrint.SwitchPluNesting);
        NavigationUserControl.SwitchUserControl(PlusNestingUserControl);
    }

    private static void ShowNewOperationControlLogType(string message, LogTypeEnum logType,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        switch (logType)
        {
            case LogTypeEnum.Error:
                ContextItem.SaveLogErrorWithInfo(message, filePath, lineNumber, memberName);
                break;
            case LogTypeEnum.Info:
                ContextItem.SaveLogInformation(message);
                break;
            case LogTypeEnum.Warning:
                ContextItem.SaveLogWarning(message);
                break;
            default:
                ContextItem.SaveLogWarning(message);
                break;
        }
    }

    public static SqlHostEntity SetNewDeviceWithQuestion(Action<FormBaseUserControl, string> showNavigation,
        SqlHostEntity host, string ip)
    {
        if (host.IsNew)
        {
            NavigateToNewDialog(showNavigation,
                LocaleCore.LabelPrint.HostNotFound(host.Name) + Environment.NewLine + LocaleCore.LabelPrint.QuestionWriteToDb,
                false, LogTypeEnum.Info, EnumDialogType.CancelYes, new() { () => { }, ActionYes });
            void ActionYes()
            {
                host = new()
                {
                    Name = host.Name,
                    Ip = ip,
                    CreateDt = DateTime.Now,
                    ChangeDt = DateTime.Now,
                    LoginDt = DateTime.Now,
                    IsMarked = false,
                };
                SqlCore.Save(host);
            }
        }
        else
        {
            host.Ip = ip;
            host.ChangeDt = DateTime.Now;
            host.LoginDt = DateTime.Now;
            host.IsMarked = false;
            SqlCore.Update(host);
        }
        return host;
    }

    private static void CatchExceptionCore(Action<FormBaseUserControl, string> showNavigation, Exception ex, 
        string filePath, int lineNumber, string memberName)
    {
        ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

        string message = ex.InnerException is null
            ? ex.Message
            : ex.Message + Environment.NewLine + ex.InnerException.Message;
        // Навигация в WinForms-контрол диалога Ок.
        NavigateToExistsDialogOk(showNavigation, 
            $"{LocaleCore.LabelPrint.Method}: {memberName}." + Environment.NewLine +
            $"{LocaleCore.LabelPrint.Line}: {lineNumber}." + Environment.NewLine + message, true, LogTypeEnum.Error);
    }

    private static void CatchExceptionSimpleCore(Exception ex, string filePath, int lineNumber, string memberName) => 
        ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

    /// <summary>
    /// Show catch exception window.
    /// </summary>
    private static void CatchException(Action<FormBaseUserControl, string> showNavigation, Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        CatchExceptionCore(showNavigation, ex, filePath, lineNumber, memberName);

    /// <summary>
    /// Show catch exception window.
    /// </summary>
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

    public static void ActionTryCatch(IWin32Window win32Window, Action<FormBaseUserControl, string> showNavigation, 
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