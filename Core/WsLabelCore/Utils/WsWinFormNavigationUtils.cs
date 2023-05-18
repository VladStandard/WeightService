// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Conventions;
using WsLabelCore.Controls;

namespace WsLabelCore.Utils;

#nullable enable
public static class WsWinFormNavigationUtils
{
    #region Public and private fields, properties, constructor

    private static WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private static WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;
    public static WsNavigationUserControl NavigationUserControl { get; set; } = new();
    public static TableLayoutPanel LayoutPanel { get; set; } = new();
    public static WsMessageBoxUserControl MessageBoxUserControl { get; set; } = new();
    public static WsKneadingUserControl KneadingUserControl { get; set; } = new();
    public static WsMoreUserControl MoreUserControl { get; set; } = new();
    public static WsLinesUserControl LinesUserControl { get; set; } = new();
    public static WsPlusUserControl PlusUserControl { get; set; } = new();
    public static WsPlusNestingUserControl PlusNestingUserControl { get; set; } = new();
    public static WsWaitUserControl WaitUserControl { get; set; } = new();

    #endregion

    #region Public and private methods

    public static void ReturnBackDefault()
    {
        NavigationUserControl.Visible = false;
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
        LayoutPanel.Visible = true;
        System.Windows.Forms.Application.DoEvents();
    }

    /// <summary>
    /// Навигация.
    /// </summary>
    /// <param name="navigationPage"></param>
    /// <param name="message"></param>
    public static void NavigateToControl(WsNavigationPage navigationPage, string message = "")
    {
        LayoutPanel.Visible = false;
        switch (navigationPage)
        {
            case WsNavigationPage.Kneading:
                KneadingUserControl.Message = message;
                NavigationUserControl.AddUserControl(KneadingUserControl);
                KneadingUserControl.RefreshAction();
                if (!KneadingUserControl.ViewModel.ActionReturnFinally.IsAny())
                    KneadingUserControl.ViewModel.ActionReturnFinally = NavigationUserControl.ViewModel.ActionReturnFinally;
                break;
            case WsNavigationPage.Lines:
                LinesUserControl.Message = message;
                NavigationUserControl.AddUserControl(LinesUserControl);
                LinesUserControl.RefreshAction();
                if (!LinesUserControl.ViewModel.ActionReturnFinally.IsAny())
                    LinesUserControl.ViewModel.ActionReturnFinally = NavigationUserControl.ViewModel.ActionReturnFinally;
                break;
            case WsNavigationPage.MessageBox:
                MessageBoxUserControl.Message = message;
                NavigationUserControl.AddUserControl(MessageBoxUserControl);
                MessageBoxUserControl.RefreshAction();
                if (!MessageBoxUserControl.ViewModel.ActionReturnFinally.IsAny())
                    MessageBoxUserControl.ViewModel.ActionReturnFinally = NavigationUserControl.ViewModel.ActionReturnFinally;
                break;
            case WsNavigationPage.More:
                MoreUserControl.Message = message;
                NavigationUserControl.AddUserControl(MoreUserControl);
                MoreUserControl.RefreshAction();
                if (!MoreUserControl.ViewModel.ActionReturnFinally.IsAny())
                    MoreUserControl.ViewModel.ActionReturnFinally = NavigationUserControl.ViewModel.ActionReturnFinally;
                break;
            case WsNavigationPage.Plus:
                PlusUserControl.Message = message;
                NavigationUserControl.AddUserControl(PlusUserControl);
                PlusUserControl.RefreshAction();
                if (!PlusUserControl.ViewModel.ActionReturnFinally.IsAny())
                    PlusUserControl.ViewModel.ActionReturnFinally = NavigationUserControl.ViewModel.ActionReturnFinally;
                break;
            case WsNavigationPage.PlusNesting:
                PlusNestingUserControl.Message = message;
                NavigationUserControl.AddUserControl(PlusNestingUserControl);
                PlusNestingUserControl.RefreshAction();
                if (!PlusNestingUserControl.ViewModel.ActionReturnFinally.IsAny())
                    PlusNestingUserControl.ViewModel.ActionReturnFinally = NavigationUserControl.ViewModel.ActionReturnFinally;
                break;
            case WsNavigationPage.Wait:
                WaitUserControl.Message = message;
                NavigationUserControl.AddUserControl(WaitUserControl);
                WaitUserControl.RefreshAction();
                if (!WaitUserControl.ViewModel.ActionReturnFinally.IsAny())
                    WaitUserControl.ViewModel.ActionReturnFinally = NavigationUserControl.ViewModel.ActionReturnFinally;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(navigationPage), navigationPage.ToString());
        }
    }

    /// <summary>
    /// Навигация в контрол ожидания.
    /// </summary>
    /// <param name="message"></param>
    public static void NavigateToControlWait(string message = "") => NavigateToControl(WsNavigationPage.Wait, message);

    /// <summary>
    /// Навигация в контрол смены ПЛУ.
    /// </summary>
    public static void NavigateToControlPlus() => NavigateToControl(WsNavigationPage.Plus);

    /// <summary>
    /// Навигация в контрол смены вложенности ПЛУ.
    /// </summary>
    public static void NavigateToControlPlusNesting() => NavigateToControl(WsNavigationPage.PlusNesting);

    /// <summary>
    /// Навигация в контрол смены линии.
    /// </summary>
    public static void NavigateToControlLines() => NavigateToControl(WsNavigationPage.Lines);

    /// <summary>
    /// Навигация в контрол смены замеса.
    /// </summary>
    public static void NavigateToControlKneading() => NavigateToControl(WsNavigationPage.Kneading);

    /// <summary>
    /// Навигация в контрол смены ещё.
    /// </summary>
    public static void NavigateToControlMore() => NavigateToControl(WsNavigationPage.More);

    /// <summary>
    /// Навигация в контрол сообщений.
    /// </summary>
    /// <param name="caption"></param>
    /// <param name="message"></param>
    /// <param name="buttonVisibility"></param>
    /// <param name="actionOk"></param>
    /// <param name="actionCancel"></param>
    public static void NavigateToControlMessage(string caption, string message, WsButtonVisibilityModel buttonVisibility,
        Action? actionOk, Action? actionCancel)
    {
        WsMessageBoxViewModel viewModel = new();
        MessageBoxUserControl.SetupViewModel(viewModel);
        viewModel.Caption = caption;
        viewModel.Message = message;
        viewModel.ButtonVisibility = buttonVisibility;
        viewModel.ButtonVisibility.Localization();
        if (actionOk is not null)
        {
            viewModel.ActionReturnOk = actionOk;
            viewModel.ActionReturnOk += ReturnBackDefault;
        }
        else
            viewModel.ActionReturnOk = ReturnBackDefault;
        if (actionCancel is not null)
        {
            viewModel.ActionReturnCancel = actionCancel;
            viewModel.ActionReturnCancel += ReturnBackDefault;
        }
        else
            viewModel.ActionReturnCancel = ReturnBackDefault;

        NavigateToControl(WsNavigationPage.MessageBox);
    }

    public static void NavigateToControlRegistration(string message)
    {
        WsMessageBoxViewModel viewModel = new();
        MessageBoxUserControl.SetupViewModel(viewModel);
        viewModel.Caption = LocaleCore.Scales.Registration;
        viewModel.Message = message;
        viewModel.ButtonVisibility.ButtonOkVisibility = Visibility.Visible;
        viewModel.ButtonVisibility.Localization();

        NavigateToControl(WsNavigationPage.MessageBox);
    }

    /// <summary>
    /// Show new OperationControl form.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    /// <param name="buttonVisibility"></param>
    /// <param name="actionOk"></param>
    /// <param name="actionCancel"></param>
    /// <returns></returns>
    public static void NavigateToOperationControl(string message, bool isLog, WsEnumLogType logType, 
        WsButtonVisibilityModel buttonVisibility, Action actionOk, Action actionCancel)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        NavigateToControlMessage(LocaleCore.Scales.OperationControl, message, buttonVisibility, actionOk, actionCancel);
    }

    private static void ShowNewOperationControlLogType(string message, WsEnumLogType logType,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        switch (logType)
        {
            case WsEnumLogType.Error:
                ContextManager.ContextItem.SaveLogErrorWithInfo(message, filePath, lineNumber, memberName);
                break;
            case WsEnumLogType.Question:
                ContextManager.ContextItem.SaveLogQuestion(message);
                break;
            case WsEnumLogType.Warning:
                ContextManager.ContextItem.SaveLogWarning(message);
                break;
            case WsEnumLogType.None:
            case WsEnumLogType.Information:
                ContextManager.ContextItem.SaveLogInformation(message);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(logType), logType.ToString());
        }
    }

    public static WsSqlDeviceModel SetNewDeviceWithQuestion(WsSqlDeviceModel device, string ip, string mac)
    {
        if (device.IsNew)
        {
            NavigateToOperationControl(
                LocaleCore.Scales.HostNotFound(device.Name) + Environment.NewLine + LocaleCore.Scales.QuestionWriteToDb,
                false, WsEnumLogType.Information,
                new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
                ActionOk, () => { });
            void ActionOk()
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

    private static void CatchExceptionCore(Exception ex, string filePath, int lineNumber, string memberName)
    {
        ContextManager.ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

        string message = ex.InnerException is null
            ? ex.Message
            : ex.Message + Environment.NewLine + ex.InnerException.Message;
        NavigateToControlMessage(LocaleCore.Scales.Exception,
            $"{LocaleCore.Scales.Method}: {memberName}." + Environment.NewLine +
            $"{LocaleCore.Scales.Line}: {lineNumber}." + Environment.NewLine + message,
            new() { ButtonOkVisibility = Visibility.Visible }, null, null);
    }

    /// <summary>
    /// Show catch exception window..
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static void CatchException(Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        CatchExceptionCore(ex, filePath, lineNumber, memberName);

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

    public static void ActionTryCatch(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            CatchException(ex);
        }
    }

    public static void ActionTryCatch(IWin32Window win32Window, Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            ActionMakeScreenShot(win32Window, WsUserSessionHelper.Instance.Scale);
            CatchException(ex);
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
            CatchException(ex);
        }
    }
    
    #endregion
}