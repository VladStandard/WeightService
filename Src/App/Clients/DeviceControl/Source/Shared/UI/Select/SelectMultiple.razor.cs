using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DeviceControl.Source.Shared.UI.Select;

public sealed partial class SelectMultiple<TItem> : ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    [Parameter, EditorRequired] public ISet<TItem> Items { get; set; } = new HashSet<TItem>();
    [Parameter, EditorRequired] public ISet<TItem> SelectedItems { get; set; } = new HashSet<TItem>();
    [Parameter] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;
    [Parameter] public EventCallback<ISet<TItem>> SelectedItemsChanged { get; set; }
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public bool IsFilterable { get; set; }
    [Parameter] public string SearchPlaceholder { get; set; } = string.Empty;
    [Parameter] public string EmptyPlaceholder { get; set; } = string.Empty;

    private ElementReference DropdownWrapper { get; set; }
    private bool IsOpen { get; set; }
    private IJSObjectReference Module { get; set; } = null!;
    private string SearchString { get; set; } = string.Empty;
    private SelectVisibility SelectVisibility { get; set; } = SelectVisibility.All;
    private string Id { get; } = $"id-{Guid.NewGuid()}";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./libs/select-resize.js");
        await Module.InvokeVoidAsync("initializeResizeSelect", DropdownWrapper);
    }

    private async Task OpenDropdown()
    {
        if (IsDisabled) return;
        IsOpen = !IsOpen;
        await Task.Delay(10);
        await Module.InvokeVoidAsync("updateDropdownWidth", DropdownWrapper);
    }

    private async Task SwitchSelectedItem(TItem item)
    {
        if (!SelectedItems.Add(item)) SelectedItems.Remove(item);
        await SelectedItemsChanged.InvokeAsync(SelectedItems);
    }

    private IEnumerable<TItem> GetFilteredList()
    {
        IEnumerable<TItem> searchingList = SelectVisibility == SelectVisibility.Selected ? SelectedItems : Items;
        return string.IsNullOrWhiteSpace(SearchString) ? searchingList :
            searchingList.Where(
                item => ItemDisplayName(item).Contains(SearchString, StringComparison.OrdinalIgnoreCase));
    }

    private async Task HandleSearchingChange()
    {
        StateHasChanged();
        await Task.Delay(100);
    }

    private bool GetIsAllSelectedInFilteredList() => !GetFilteredList().Except(SelectedItems).Any();

    private bool IsSelectedItem(TItem item) => SelectedItems.Contains(item);

    public async ValueTask DisposeAsync()
    {
        try
        {
            await Module.InvokeVoidAsync("removeResizeEvent", DropdownWrapper);
            await Module.DisposeAsync();
        }
        catch
        {
            // pass
        }
    }
}

public enum SelectVisibility
{
    All,
    Selected
}