using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ScalesDesktop.Source.Shared.UI;

public sealed partial class SelectSingle<TItem> : ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    
    [Parameter, EditorRequired] public IEnumerable<TItem> Items { get; set; } = [];
    [Parameter, EditorRequired] public TItem? SelectedItem { get; set; }
    [Parameter] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;
    [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
    [Parameter] public EventCallback<IEnumerable<TItem>> ItemsChanged { get; set; }
    [Parameter] public bool IsFilterable { get; set; }
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public string SearchPlaceholder { get; set; } = string.Empty;
    [Parameter] public string EmptyPlaceholder { get; set; } = string.Empty;

    private Dropdown Dropdown { get; set; } = new();
    private ElementReference DropdownWrapper { get; set; }
    private IJSObjectReference Module { get; set; } = null!;
    private string SearchString { get; set; } = string.Empty;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./libs/selectResize.js");
        await Module.InvokeVoidAsync("initializeResizeSelect", DropdownWrapper);
    }

    private async Task SetSelectedItem(TItem item)
    {
        SelectedItem = item;
        await SelectedItemChanged.InvokeAsync(SelectedItem);
        SearchString = string.Empty;
        await Dropdown.Hide();
    }

    private IEnumerable<TItem> GetFilteredList() => string.IsNullOrWhiteSpace(SearchString) ? Items :
        Items.Where(item => ItemDisplayName(item).Contains(SearchString, StringComparison.OrdinalIgnoreCase));

    private bool IsSelectedItem(TItem item) => SelectedItem != null && SelectedItem.Equals(item);

    private string GetToggleText() => SelectedItem != null ? ItemDisplayName(SelectedItem) : Placeholder;

    private async Task HandleSearchingChange(ChangeEventArgs e)
    {
        SearchString = e.Value?.ToString() ?? string.Empty;
        StateHasChanged();
        await Task.Delay(100);
    }

    public async ValueTask DisposeAsync()
    {
        await Module.InvokeVoidAsync("removeResizeEvent", DropdownWrapper);
        await Module.DisposeAsync();
    }
}