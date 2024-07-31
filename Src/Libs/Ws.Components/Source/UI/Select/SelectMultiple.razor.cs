using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ws.Components.Source.UI.Select;

public sealed partial class SelectMultiple<TItem> : ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    /// <summary>
    /// The collection of items to be displayed in the dropdown.
    /// </summary>
    [Parameter, EditorRequired] public ISet<TItem> Items { get; set; } = new HashSet<TItem>();

    /// <summary>
    /// The currently selected item from the dropdown.
    /// </summary>
    [Parameter, EditorRequired] public ISet<TItem> SelectedItems { get; set; } = new HashSet<TItem>();

    /// <summary>
    /// Event called whenever the selection changed.
    /// </summary>
    [Parameter] public EventCallback<ISet<TItem>> SelectedItemsChanged { get; set; }

    /// <summary>
    /// A function to determine the display text for each item in the dropdown.
    /// </summary>
    [Parameter] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;

    /// <summary>
    /// Determines whether the select trigger is disabled.
    /// </summary>
    [Parameter] public bool IsDisabled { get; set; }

    /// <summary>
    /// Determines whether the dropdown allows filtering of items based on user input.
    /// </summary>
    [Parameter] public bool IsFilterable { get; set; }

    /// <summary>
    /// The placeholder text displayed in the search input when filtering is enabled.
    /// </summary>
    [Parameter] public string SearchPlaceholder { get; set; } = string.Empty;

    /// <summary>
    /// The placeholder text displayed when no items match the filter criteria.
    /// </summary>
    [Parameter] public string EmptyPlaceholder { get; set; } = string.Empty;

    /// <summary>
    /// The custom CSS class to apply to the select trigger.
    /// </summary>
    [Parameter] public string Class { get; set; } = string.Empty;

    /// <summary>
    /// The html id for forms
    /// </summary>
    [Parameter] public string HtmlId { get; set; } = string.Empty;

    private ElementReference DropdownWrapper { get; set; }
    private bool IsOpen { get; set; }
    private string SearchString { get; set; } = string.Empty;
    private string AnchorId { get; } = $"id-{Guid.NewGuid()}";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await JsRuntime.InvokeVoidAsync("subscribeElementResize", DropdownWrapper);
    }

    private async Task OpenDropdown()
    {
        if (IsDisabled) return;
        IsOpen = !IsOpen;
        await Task.Yield();
        await JsRuntime.InvokeVoidAsync("updateElementSize", DropdownWrapper);
    }

    private async Task SwitchSelectedItem(TItem item)
    {
        if (!SelectedItems.Add(item)) SelectedItems.Remove(item);
        await SelectedItemsChanged.InvokeAsync(SelectedItems);
    }

    private IEnumerable<TItem> GetFilteredList() =>
        string.IsNullOrWhiteSpace(SearchString) ? Items :
            Items.Where(item => ItemDisplayName(item).Contains(SearchString, StringComparison.OrdinalIgnoreCase));

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
