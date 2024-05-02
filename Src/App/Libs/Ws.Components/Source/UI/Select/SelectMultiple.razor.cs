using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ws.Components.Source.UI.Select;

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
    private string SearchString { get; set; } = string.Empty;
    private SelectVisibility SelectVisibility { get; set; } = SelectVisibility.All;
    private string Id { get; } = $"id-{Guid.NewGuid()}";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await JsRuntime.InvokeVoidAsync("subscribeElementResize", DropdownWrapper);
    }

    private async Task OpenDropdown()
    {
        if (IsDisabled) return;
        IsOpen = !IsOpen;
        await Task.Delay(10);
        await JsRuntime.InvokeVoidAsync("updateElementSize", DropdownWrapper);
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

    private bool IsSelectedItem(TItem item) => SelectedItems.Contains(item);

    public async ValueTask DisposeAsync()
    {
        try
        {
            await JsRuntime.InvokeVoidAsync("unsubscribeElementResize", DropdownWrapper);
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