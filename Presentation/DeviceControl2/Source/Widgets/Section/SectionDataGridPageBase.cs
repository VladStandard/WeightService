using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Ws.Domain.Models.Common;

namespace DeviceControl2.Source.Widgets.Section;

public class SectionDataGridPageBase<TItem> : ComponentBase, IAsyncDisposable where TItem : EntityBase, new()
{
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    [Parameter] public string SearchingSectionItemId { get; set; } = string.Empty;

    protected IEnumerable<TItem> SectionItems { get; set; } = [];
    protected bool IsLoading { get; set; } = true;
    private bool IsFirstLoading { get; set; } = true;
    private IJSObjectReference? Module { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetSectionData();
        IsLoading = false;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
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

    protected async Task OpenLinkInNewTab(string url) =>
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");
    
    protected async Task ContextFuncWrapper(TItem? item, EventCallback onComplete, Func<TItem, Task> action)
    {
        await onComplete.InvokeAsync();
        if (item == null) return;
        await action(item);
    }

    protected async Task OpenSectionModal<T>(TItem sectionEntity) where T : SectionDialogBase<TItem> =>
        await DialogService.ShowDialogAsync<T>(sectionEntity, new()
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
            });
    

    private async Task HandleDialogCallback(DialogResult result)
    {
        if (result is { Cancelled: false })
            await UpdateData();
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

    private async Task GetSectionData()
    {
        if (!string.IsNullOrEmpty(SearchingSectionItemId) && IsFirstLoading)
        {
            SectionItems = await Task.Run(SetSqlSearchingCast);
            IsFirstLoading = false;
            SectionItems = SectionItems.Where(i => !i.IsNew).ToList();
            if (SectionItems.Any()) await OpenDataGridEntityModal(SectionItems.First());
            return;
        }

        SectionItems = await Task.Run(SetSqlSectionCast);
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