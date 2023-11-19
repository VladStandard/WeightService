namespace DeviceControl.Components.Common;

public class RazorComponentBase : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    #region Inject

    [Inject] protected DialogService DialogService { get; set; } = default!;
    [Inject] protected NotificationService NotificationService { get; set; } = default!;
    [Inject] protected UserService UserService { get; set; } = default!;
    private SqlContextItemHelper ContextItem => SqlContextItemHelper.Instance;
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;

    #endregion
    
    protected ClaimsPrincipal? User { get; set; }

    #endregion

    #region Public and private methods - Actions

    protected bool SqlItemValidateWithMsg<T>(T? item, bool isCheckIdentity) where T : SqlEntityBase, new()
    {
        string detailAddition = string.Empty;
        bool result = SqlValidationUtils.IsValidation(item, ref detailAddition, isCheckIdentity);
        switch (result)
        {
            case false:
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Warning,
                    Summary = LocaleCore.Action.ActionDataControl,
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

    protected bool SqlItemValidate<T>(T item) where T : SqlEntityBase, new()
    {
        string detailAddition = string.Empty;
        return SqlValidationUtils.IsValidation(item, ref detailAddition, !item.IsNew);
    }
    
    protected TItem SqlItemNewEmpty<TItem>() where TItem : SqlEntityBase, new()
    {
        return SqlCore.GetItemNewEmpty<TItem>();
    }

    protected void SqlItemSave<T>(T? item) where T : SqlEntityBase, new()
    {
        if (item is null || !SqlItemValidate(item)) 
            return;
        if (item.IsNew)
            SqlCore.Save(item);
        else 
            SqlCore.Update(item);
    }

    protected void SqlItemsSave<T>(List<T>? items) where T : SqlEntityBase, new()
    {
        if (items is null) return;

        foreach (T item in items)
            SqlItemSave(item);
    }
    
    private static ConfirmOptions GetConfirmOptions()
    {
        return new()
        {
            OkButtonText = LocaleCore.Dialog.DialogButtonYes,
            CancelButtonText = LocaleCore.Dialog.DialogButtonCancel,
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
                LocaleCore.Dialog.DialogResultSuccess, BlazorAppSettingsHelper.DelayInfo
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
            string question = string.IsNullOrEmpty(message) ? LocaleCore.Dialog.DialogQuestion : message;
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
            title = LocaleCore.Dialog.DialogResultFail;
        
        string msg = ex.Message;
        if (!string.IsNullOrEmpty(ex.InnerException?.Message))
            msg += $"\r\n{ex.InnerException.Message}";

        NotificationService.Notify(
            NotificationSeverity.Error,
            title, 
            msg,
            BlazorAppSettingsHelper.DelayError
            );
        ContextItem.SaveLogError(ex);
    }
    
    #endregion
    
    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
    }
}
