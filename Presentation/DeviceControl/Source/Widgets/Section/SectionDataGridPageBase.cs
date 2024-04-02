using DeviceControl.Source.Shared.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Ws.Domain.Models.Common;

namespace DeviceControl.Source.Widgets.Section;

public class SectionDataGridPageBase<TItem> : ComponentBase, IAsyncDisposable where TItem : EntityBase, new()
{
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    [Parameter] public string SearchingSectionItemId { get; set; } = string.Empty;

    protected IEnumerable<TItem> SectionItems { get; set; } = [];
    protected bool IsLoading { get; set; } = true;
    private bool IsFirstLoading { get; set; } = true;
    private IJSObjectReference? Module { get; set; }
    protected DialogParameters DialogParameters { get; private set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await GetSectionData();
        IsLoading = false;
        DialogParameters = new()
        {
            OnDialogClosing = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
                {
                    if (Module == null) return;
                    await Module.InvokeVoidAsync("animateDialogClosing", instance.Id);
                }
            ),
            OnDialogOpened = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
                {
                    if (Module == null) return;
                    await Module.InvokeVoidAsync("animateDialogOpening", instance.Id);
                }
            ),
            OnDialogResult = DialogService.CreateDialogCallback(this, HandleDialogCallback)
        };
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./libs/dialog-animation.js");
    }

    protected virtual IEnumerable<TItem> SetSqlSectionCast() =>
        throw new NotImplementedException();

    protected virtual IEnumerable<TItem> SetSqlSearchingCast() =>
        throw new NotImplementedException();

    protected virtual Task OpenDataGridEntityModal(TItem item) =>
        throw new NotImplementedException();

    protected virtual Task OpenSectionCreateForm() =>
        throw new NotImplementedException();

    protected virtual Task OpenItemInNewTab(TItem item) =>
        throw new NotImplementedException();

    protected virtual Task DeleteItemAction(TItem item) =>
        throw new NotImplementedException();

    protected async Task OpenLinkInNewTab(string url) =>
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");

    protected async Task ContextFuncWrapper(TItem? item, EventCallback onComplete, Func<TItem, Task> action)
    {
        await onComplete.InvokeAsync();
        if (item == null) return;
        await action(item);
    }

    protected async Task OpenSectionModal<T>(TItem sectionEntity) where T : SectionDialogBase<TItem> =>
        await DialogService.ShowDialogAsync<T>(new SectionDialogContent<TItem> { Item = sectionEntity },
            DialogParameters);


    private Task HandleDialogCallback(DialogResult result)
    {
        if (result.Cancelled || result.Data is not SectionDialogContent<TItem> dialogResult) return Task.CompletedTask;
        TItem item = dialogResult.Item;

        switch (dialogResult.DataAction)
        {
            case SectionDialogResultEnum.Delete:
                SectionItems = SectionItems.Where(entity => entity.Uid != item.Uid);
                break;
            case SectionDialogResultEnum.Update:
                SectionItems = SectionItems.Where(entity => entity.Uid != item.Uid).Concat(new[] { item });
                break;
            case SectionDialogResultEnum.Create:
                SectionItems = SectionItems.Concat(new[] { item });
                break;
            case SectionDialogResultEnum.Cancel:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return Task.CompletedTask;
    }

    protected async Task UpdateData()
    {
        if (IsLoading) return;

        IsLoading = true;
        StateHasChanged();

        await GetSectionData();

        IsLoading = false;
        StateHasChanged();
    }

    protected async Task DeleteItem(TItem item)
    {
        try
        {
            await DeleteItemAction(item);
            SectionItems = SectionItems.Where(entity => !entity.Equals(item));
            ToastService.ShowSuccess(Localizer["ToastDeleteItem"]);
            StateHasChanged();
        }
        catch
        {
            ToastService.ShowError("Неизвестная ошибка. Попробуйте позже");
        }
    }

    private async Task GetSectionData()
    {
        if (!string.IsNullOrEmpty(SearchingSectionItemId) && IsFirstLoading)
        {
            SectionItems = await GetItemsAsync(SetSqlSearchingCast);
            IsFirstLoading = false;
            SectionItems = SectionItems.Where(i => !i.IsNew).ToList();
            if (SectionItems.Any()) await OpenDataGridEntityModal(SectionItems.First());
            return;
        }

        SectionItems = await GetItemsAsync(SetSqlSectionCast);
    }

    private async Task<IEnumerable<TItem>> GetItemsAsync(Func<IEnumerable<TItem>> sectionItemsProvider)
    {
        try
        {
            return await Task.Run(sectionItemsProvider);
        }
        catch
        {
            ToastService.ShowError(Localizer["ToastErrorGettingData"]);
            return [];
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (Module == null) return;
            await Module.DisposeAsync();
        }
        catch (Exception ex) when (ex is JSDisconnectedException or ArgumentNullException)
        {
            // pass error
        }
    }
}