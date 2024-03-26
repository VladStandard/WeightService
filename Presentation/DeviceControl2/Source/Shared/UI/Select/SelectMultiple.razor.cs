using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DeviceControl2.Source.Shared.UI.Select;

public sealed partial class SelectMultiple<TItem> : ComponentBase, IDisposable where TItem : new()
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter, EditorRequired] public IEnumerable<TItem> Items { get; set; } = new List<TItem>();
    [Parameter, EditorRequired] public IEnumerable<TItem> SelectedItems { get; set; } = [];
    [Parameter] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;
    [Parameter] public EventCallback<IEnumerable<TItem>> SelectedItemsChanged { get; set; }
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
        TItem? existingItem = SelectedItems.SingleOrDefault(i => i != null && i.Equals(item));
        SelectedItems = existingItem == null ? SelectedItems.Append(item) :
            SelectedItems.Where(i => i != null && !i.Equals(existingItem)).ToList();
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

    private bool IsSelectedItem(TItem item) => SelectedItems.Any(i => i != null && i.Equals(item));

    public async void Dispose()
    {
        try
        {
            await Module.InvokeVoidAsync("removeResizeEvent", DropdownWrapper);
            await Module.DisposeAsync();
        }
        catch (Exception ex) when (ex is JSDisconnectedException or ArgumentNullException)
        {
            // pass error
        }
    }
}

public enum SelectVisibility
{
    All,
    Selected
}
