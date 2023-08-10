// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Components.Common;

public class RazorComponentBase : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    #region Inject

    [Inject] protected DialogService DialogService { get; set; } = default!;
    [Inject] protected NotificationService NotificationService { get; set; } = default!;
    [Inject] protected WsUserService UserService { get; set; } = default!;

    #endregion

    protected static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;

    protected ClaimsPrincipal? User { get; set; }

    #endregion

    #region Public and private methods - Actions

    protected bool SqlItemValidateWithMsg<T>(T? item, bool isCheckIdentity) where T : WsSqlTableBase, new()
    {
        string detailAddition = string.Empty;
        bool result = WsSqlValidationUtils.IsValidation(item, ref detailAddition, isCheckIdentity);
        switch (result)
        {
            case false:
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Warning,
                    Summary = WsLocaleCore.Action.ActionDataControl,
                    Detail = detailAddition,
                    Duration = BlazorAppSettingsHelper.DelayError
                };
                NotificationService.Notify(msg);
                return false;
            }
            default:
                return true;
        }
    }

    protected bool SqlItemValidate<T>(T item) where T : WsSqlTableBase, new()
    {
        string detailAddition = string.Empty;
        return WsSqlValidationUtils.IsValidation(item, ref detailAddition, !item.IsNew);
    }
    
    protected static TItem SqlItemNewEmpty<TItem>() where TItem : WsSqlTableBase, new()
    {
        return ContextManager.SqlCore.GetItemNewEmpty<TItem>();
    }

    protected void SqlItemSave<T>(T? item) where T : WsSqlTableBase, new()
    {
        if (item is null || !SqlItemValidate(item)) 
            return;
        if (item.IsNew)
            ContextManager.SqlCore.Save(item);
        else 
            ContextManager.SqlCore.Update(item);
    }

    protected void SqlItemsSave<T>(List<T>? items) where T : WsSqlTableBase, new()
    {
        if (items is null) return;

        foreach (T item in items)
            SqlItemSave(item);
    }
    
    private static ConfirmOptions GetConfirmOptions()
    {
        return new()
        {
            OkButtonText = WsLocaleCore.Dialog.DialogButtonYes,
            CancelButtonText = WsLocaleCore.Dialog.DialogButtonCancel,
            CloseDialogOnEsc = true,
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
                WsLocaleCore.Dialog.DialogResultSuccess, BlazorAppSettingsHelper.DelayInfo
                );
        }
        catch (Exception ex)
        {
            NotificationException(title, ex);
        }
    }

    protected void RunAction(string title, Action action) => RunAction(title, new List<Action> { action });
    
    protected void RunActionsWithQuestion(string title, string message, Action action)
    {
        try
        {
            string question = string.IsNullOrEmpty(message) ? WsLocaleCore.Dialog.DialogQuestion : message;
            Task<bool?> dialog = DialogService.Confirm(question, title, GetConfirmOptions());
            if (dialog.Result == true)
                RunAction(title, action);
        }
        catch (Exception ex)
        {
            NotificationException(title, ex);
        }
    }
    
    private void NotificationException(string title, Exception ex)
    {
        if (string.IsNullOrEmpty(title))
            title = WsLocaleCore.Dialog.DialogResultFail;
        
        string msg = ex.Message;
        if (!string.IsNullOrEmpty(ex.InnerException?.Message))
            msg += $"\r\n{ex.InnerException.Message}";

        NotificationService.Notify(
            NotificationSeverity.Error,
            title, 
            msg,
            BlazorAppSettingsHelper.DelayError
            );
        ContextManager.ContextItem.SaveLogError(ex);
    }
    
    #endregion
    
    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
        await base.OnInitializedAsync();
    }
}