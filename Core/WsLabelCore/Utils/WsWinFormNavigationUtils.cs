// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Utils;
#nullable enable
/// <summary>
/// Утилиты навигации по контролам.
/// </summary>
public static class WsWinFormNavigationUtils
{
    #region Public and private fields, properties, constructor

    private static WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private static WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    private static WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;
    public static WsNavigationUserControl NavigationUserControl { get; } = new();
    public static WsMessageBoxUserControl MessageBoxUserControl { get; } = new();
    public static WsMoreUserControl KneadingUserControl { get; set; } = new();
    public static WsMoreUserControl MoreUserControl { get; set; } = new();
    public static WsLinesUserControl LinesUserControl { get; set; } = new();
    public static WsPlusLinesUserControl PlusLineUserControl { get; set; } = new();
    public static WsPlusNestingUserControl PlusNestingUserControl { get; set; } = new();
    public static WsWaitUserControl WaitUserControl { get; set; } = new();

    #endregion

    #region Public and private methods

    public static void ActionBackFromNavigation()
    {
        foreach (Control control in NavigationUserControl.Controls)
        {
            if (control is TableLayoutPanel tableLayoutPanel)
            {
                foreach (Control control2 in tableLayoutPanel.Controls)
                {
                    if (control2 is WsBaseUserControl userControl)
                    {
                        userControl.Visible = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Навигация.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="navigationPage"></param>
    /// <param name="title"></param>
    /// <param name="message"></param>
    public static void NavigateToControl(Action<WsBaseUserControl> showNavigation,
        WsNavigationPage navigationPage, string title, string message)
    {
        switch (navigationPage)
        {
            case WsNavigationPage.Kneading:
                showNavigation(KneadingUserControl);
                KneadingUserControl.Message = message;
                NavigationUserControl.SwitchUserControl(KneadingUserControl, LocaleCore.Scales.SwitchKneadingTitle);
                break;
            case WsNavigationPage.Lines:
                showNavigation(LinesUserControl);
                LinesUserControl.Message = message;
                NavigationUserControl.SwitchUserControl(LinesUserControl, LocaleCore.Scales.SwitchLineTitle);
                break;
            case WsNavigationPage.MessageBox:
                showNavigation(MessageBoxUserControl);
                NavigationUserControl.SwitchUserControl(MessageBoxUserControl, title);
                break;
            case WsNavigationPage.More:
                showNavigation(MoreUserControl);
                MoreUserControl.Message = message;
                NavigationUserControl.SwitchUserControl(MoreUserControl, LocaleCore.Scales.SwitchMoreTitle);
                break;
            case WsNavigationPage.PlusLine:
                showNavigation(PlusLineUserControl);
                PlusLineUserControl.Message = message;
                NavigationUserControl.SwitchUserControl(PlusLineUserControl, LocaleCore.Scales.SwitchPluTitle);
                break;
            case WsNavigationPage.PlusNesting:
                showNavigation(PlusNestingUserControl);
                PlusNestingUserControl.Message = message;
                NavigationUserControl.SwitchUserControl(PlusNestingUserControl, LocaleCore.Scales.SwitchPluNestingTitle);
                break;
            case WsNavigationPage.Wait:
                showNavigation(WaitUserControl);
                WaitUserControl.Message = message;
                NavigationUserControl.SwitchUserControl(WaitUserControl, LocaleCore.Scales.AppWaitLoad);
                break;
        }
    }

    /// <summary>
    /// Навигация в контрол сообщений.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="actionAbort"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionCustom"></param>
    /// <param name="actionIgnore"></param>
    /// <param name="actionNo"></param>
    /// <param name="actionOk"></param>
    /// <param name="actionRetry"></param>
    /// <param name="actionYes"></param>
    private static void NavigateToControlMessage(Action<WsBaseUserControl> showNavigation, string title,
        string message, Action actionAbort, Action actionCancel, Action actionCustom, Action actionIgnore, Action actionNo,
        Action actionOk, Action actionRetry, Action actionYes)
    {
        MessageBoxUserControl.ViewModel.Setup(message, actionAbort, actionCancel, actionCustom,
            actionIgnore, actionNo, actionOk, actionRetry, actionYes, ActionBackFromNavigation);
        NavigateToControl(showNavigation, WsNavigationPage.MessageBox, title, "");
    }

    /// <summary>
    /// Навигация в контрол сообщений.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="actionNo"></param>
    /// <param name="actionYes"></param>
    private static void NavigateToControlMessageNoYes(Action<WsBaseUserControl> showNavigation, string title,
        string message, Action actionNo, Action actionYes)
    {
        MessageBoxUserControl.ViewModel.SetupNoYes(message, actionNo, actionYes, ActionBackFromNavigation);
        NavigateToControl(showNavigation, WsNavigationPage.MessageBox, title, "");
    }

    /// <summary>
    /// Навигация в контрол сообщений.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="title"></param>
    /// <param name="message"></param>
    private static void NavigateToControlMessageOk(Action<WsBaseUserControl> showNavigation, string title,
        string message)
    {
        MessageBoxUserControl.ViewModel.SetupOk(message, ActionBackFromNavigation);
        NavigateToControl(showNavigation, WsNavigationPage.MessageBox, title, "");
    }

    /// <summary>
    /// Navigate to control.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    /// <param name="actionNo"></param>
    /// <param name="actionYes"></param>
    /// <returns></returns>
    public static void NavigateToOperationControlNoYes(Action<WsBaseUserControl> showNavigation,
        string message, bool isLog, WsEnumLogType logType, Action actionNo, Action actionYes)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        NavigateToControlMessageNoYes(showNavigation,
            LocaleCore.Scales.OperationControl, message, actionNo, actionYes);
    }

    /// <summary>
    /// Navigate to control.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    /// <returns></returns>
    public static void NavigateToOperationControlOk(Action<WsBaseUserControl> showNavigation,
        string message, bool isLog, WsEnumLogType logType)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        NavigateToControlMessageOk(showNavigation,
            LocaleCore.Scales.OperationControl, message);
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

    public static WsSqlDeviceModel SetNewDeviceWithQuestion(Action<WsBaseUserControl> showNavigation,
        WsSqlDeviceModel device, string ip, string mac)
    {
        if (device.IsNew)
        {
            NavigateToOperationControlNoYes(showNavigation,
                LocaleCore.Scales.HostNotFound(device.Name) + Environment.NewLine + LocaleCore.Scales.QuestionWriteToDb,
                false, WsEnumLogType.Information, () => { }, ActionYes);
            void ActionYes()
            {
                device = new()
                {
                    Name = device.Name,
                    PrettyName = device.Name,
                    Ipv4 = ip,
                    MacAddress = new(mac),
                    CreateDt = DateTime.Now,
                    ChangeDt = DateTime.Now,
                    LoginDt = DateTime.Now,
                    IsMarked = false,
                };
                AccessManager.AccessItem.Save(device);
            }
        }
        else
        {
            device.Ipv4 = ip;
            device.MacAddress = new(mac);
            device.ChangeDt = DateTime.Now;
            device.LoginDt = DateTime.Now;
            device.IsMarked = false;
            AccessManager.AccessItem.Update(device);
        }
        return device;
    }

    private static void CatchExceptionCore(Action<WsBaseUserControl> showNavigation, Exception ex, string filePath, int lineNumber, string memberName)
    {
        ContextManager.ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

        string message = ex.InnerException is null
            ? ex.Message
            : ex.Message + Environment.NewLine + ex.InnerException.Message;
        NavigateToControlMessageOk(showNavigation, LocaleCore.Scales.Exception,
            $"{LocaleCore.Scales.Method}: {memberName}." + Environment.NewLine +
            $"{LocaleCore.Scales.Line}: {lineNumber}." + Environment.NewLine + message);
    }

    private static void CatchExceptionSimpleCore(Exception ex, string filePath, int lineNumber, string memberName)
    {
        ContextManager.ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);
    }

    /// <summary>
    /// Show catch exception window.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="ex"></param>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static void CatchException(Action<WsBaseUserControl> showNavigation, Exception ex,
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

    private static void MakeScreenShot(IWin32Window win32Window, WsSqlScaleModel scale)
    {
        if (win32Window is not Form form) return;
        using MemoryStream memoryStream = new();
        using Bitmap bitmap = new(form.Width, form.Height);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.CopyFromScreen(form.Location.X, form.Location.Y, 0, 0, form.Size);
        using Image img = bitmap;
        img.Save(memoryStream, ImageFormat.Png);
        WsSqlScaleScreenShotModel scaleScreenShot = new() { Scale = scale, ScreenShot = memoryStream.ToArray() };
        AccessManager.AccessItem.Save(scaleScreenShot);
    }

    public static void ActionTryCatch(Action action, Action<WsBaseUserControl> showNavigation)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            CatchException(showNavigation, ex);
        }
    }

    public static void ActionTryCatchSimple(Action action)
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

    public static void ActionTryCatch(IWin32Window win32Window, Action<WsBaseUserControl> showNavigation, 
        Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            ActionMakeScreenShot(win32Window, LabelSession.Line);
            CatchException(showNavigation, ex);
        }
    }

    public static void ActionMakeScreenShot(IWin32Window win32Window, WsSqlScaleModel scale)
    {
        try
        {
            MakeScreenShot(win32Window, scale);
            PluginMemory.MemorySize.Execute();
            ContextManager.ContextItem.SaveLogMemory(PluginMemory.GetMemorySizeAppMb(), PluginMemory.GetMemorySizeFreeMb());
            GC.Collect();
        }
        catch (Exception ex)
        {
            CatchExceptionSimple(ex);
        }
    }
    
    #endregion
}