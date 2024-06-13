using System.Security.Claims;
using Force.DeepCloner;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Services.Exceptions;

namespace DeviceControl.Source.Widgets.Section;

public abstract class SectionFormBase<TItem> : ComponentBase where TItem : new()
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private AuthenticationStateProvider AuthProvider { get; set; } = default!;

    [CascadingParameter(Name = "DialogItem")] protected TItem DialogItem { get; set; } = new();
    [CascadingParameter] protected FluentDialog Dialog { get; set; } = default!;

    protected ClaimsPrincipal UserPrincipal { get; private set; } = new();
    protected bool IsForceSubmit { get; set; }
    protected TItem DialogItemCopy { get; private set; } = new();

    protected override void OnInitialized() => DialogItemCopy = DialogItem.DeepClone();

    protected override async Task OnInitializedAsync() =>
        UserPrincipal = (await AuthProvider.GetAuthenticationStateAsync()).User;

    protected virtual Task DeleteItemAction(TItem item) =>
        throw new NotImplementedException();

    protected virtual TItem UpdateItemAction(TItem item) =>
        throw new NotImplementedException();

    protected virtual TItem CreateItemAction(TItem item) =>
        throw new NotImplementedException();

    protected async Task OnCancelAction() => await Dialog.CancelAsync();

    protected void ResetAction()
    {
        DialogItem = DialogItemCopy.DeepClone();
        ToastService.ShowInfo(Localizer["ToastResetItem"]);
    }

    protected async Task CreateItem()
    {
        try
        {
            TItem createdItem = CreateItemAction(DialogItem.DeepClone());
            ToastService.ShowSuccess(Localizer["ToastCreateItem"]);
            await Dialog.CloseAsync(new SectionDialogContent<TItem>
            { Item = createdItem, DataAction = SectionDialogResultEnum.Create });
        }
        catch (ValidateException ex)
        {
            foreach (string error in ex.Errors.Keys)
                ToastService.ShowWarning(ex.Errors[error]);
        }
        catch (DbServiceException)
        {
            ToastService.ShowError(Localizer["UnknownError"]);
        }
    }

    protected async Task UpdateItem()
    {
        if (DialogItem != null && DialogItem.Equals(DialogItemCopy))
        {
            await Dialog.CancelAsync();
            return;
        }

        try
        {
            TItem updatedItem = UpdateItemAction(DialogItem.DeepClone());
            ToastService.ShowSuccess(Localizer["ToastUpdateItem"]);
            await Dialog.CloseAsync(new SectionDialogContent<TItem>
            { Item = updatedItem, DataAction = SectionDialogResultEnum.Update });
        }
        catch (ValidateException ex)
        {
            foreach (string error in ex.Errors.Keys)
                ToastService.ShowWarning(ex.Errors[error]);
        }
        catch (DbServiceException)
        {
            ToastService.ShowError(Localizer["UnknownError"]);
        }
    }

    protected async Task DeleteItem()
    {
        try
        {
            await DeleteItemAction(DialogItem.DeepClone());
            ToastService.ShowSuccess(Localizer["ToastDeleteItem"]);
            await Dialog.CloseAsync(new SectionDialogContent<TItem>
            { Item = DialogItem, DataAction = SectionDialogResultEnum.Delete });
        }
        catch (DbServiceException)
        {
            ToastService.ShowError(Localizer["UnknownError"]);
        }
    }
}