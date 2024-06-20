using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DeviceControl.Source.Widgets.Section;

public abstract class SectionDataGridPageBase<TItem> : ComponentBase where TItem : new()
{
    #region Inject

    [Inject] protected IDialogService DialogService { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    #endregion
    [Parameter] public string SearchingSectionItemId { get; set; } = string.Empty;

    protected IEnumerable<TItem> SectionItems { get; set; } = [];
    protected DialogParameters DialogParameters { get; private set; } = new();

    protected bool IsLoading { get; set; } = true;
    private bool IsFirstLoading { get; set; } = true;

    protected override void OnInitialized()
    {
        DialogParameters = new()
        {
            OnDialogClosing = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
                await JsRuntime.InvokeVoidAsync("animateDialogClosing", instance.Id)
            ),
            OnDialogOpened = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
                await JsRuntime.InvokeVoidAsync("animateDialogOpening", instance.Id)
            ),
            OnDialogResult = DialogService.CreateDialogCallback(this, HandleDialogCallback)
        };
    }

    protected override async Task OnInitializedAsync()
    {
        await GetSectionData();
        IsLoading = false;
    }

    protected abstract IEnumerable<TItem> SetSqlSectionCast();

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

    private async Task HandleDialogCallback(DialogResult result)
    {
        if (result.Cancelled || result.Data is not SectionDialogContent<TItem> dialogResult) return;
        TItem item = dialogResult.Item;

        switch (dialogResult.DataAction)
        {
            case SectionDialogResultEnum.Delete:
                SectionItems = SectionItems.Where(entity => entity != null && !entity.Equals(item));
                break;
            case SectionDialogResultEnum.Update:
                await UpdateData();
                break;
            case SectionDialogResultEnum.Create:
                SectionItems = SectionItems.Concat(new[] { item });
                break;
            case SectionDialogResultEnum.Cancel:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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
            SectionItems = SectionItems.Where(entity => entity != null && !entity.Equals(item));
            ToastService.ShowSuccess(Localizer["ToastDeleteItem"]);
            StateHasChanged();
        }
        catch
        {
            ToastService.ShowError(Localizer["ToastDeleteItemError"]);
        }
    }

    private async Task GetSectionData()
    {
        if (!string.IsNullOrEmpty(SearchingSectionItemId) && IsFirstLoading)
        {
            SectionItems = await GetItemsAsync(SetSqlSearchingCast);
            IsFirstLoading = false;
            TItem? firstItem = SectionItems.FirstOrDefault();
            if (firstItem != null) await OpenDataGridEntityModal(firstItem);
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
}