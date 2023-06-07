// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen;
using WsBlazorCore.Settings;

namespace WsBlazorCore.Razors;

public partial class RazorComponentBase
{
    #region Public and private methods - Run actions

    private static ConfirmOptions GetConfirmOptions()
    {
        return new ConfirmOptions
        {
            OkButtonText = LocaleCore.Dialog.DialogButtonYes,
            CancelButtonText = LocaleCore.Dialog.DialogButtonCancel
        };
    }

    protected void RunAction(string title, List<Action> actions)
    {
        try
        {
            foreach (Action action in actions)
                action.Invoke();
            
            if (string.IsNullOrEmpty(title)) return;

            NotificationService.Notify(
                NotificationSeverity.Success,
                title,
                LocaleCore.Dialog.DialogResultSuccess, BlazorAppSettingsHelper.Delay
                );
        }
        catch (Exception ex)
        {
            CatchException(title, ex);
        }
    }

    protected void RunAction(string title, Action action) => 
        RunAction(title, new List<Action> { action });
    
    protected void RunActionsWithQuestion(string title, string message, Action action)
    {
        try
        {

            string question = string.IsNullOrEmpty(message) ? LocaleCore.Dialog.DialogQuestion : message;
            Task<bool?> dialog = DialogService.Confirm(question, title, GetConfirmOptions());
            if (dialog.Result == true)
                RunAction(title, action);
        }
        catch (Exception ex)
        {
            CatchException(title, ex);
        }
    }
    
    private void CatchException(string title, Exception ex)
    {
        if (string.IsNullOrEmpty(title))
            title = LocaleCore.Dialog.DialogResultFail;
        
        string msg = ex.Message;
        if (!string.IsNullOrEmpty(ex.InnerException?.Message))
            msg += $"\r\n{ex.InnerException.Message}";

        NotificationService.Notify(
            NotificationSeverity.Error,
            title, 
            msg,
            BlazorAppSettingsHelper.Delay + 2000
            );
        ContextManager.ContextItem.SaveLogError(ex);
    }

    #endregion
}