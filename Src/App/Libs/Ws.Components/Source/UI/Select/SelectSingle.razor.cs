using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ws.Components.Source.UI.Select;

public sealed partial class SelectSingle<TItem> : ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    [Parameter, EditorRequired] public IEnumerable<TItem> Items { get; set; } = [];
    [Parameter, EditorRequired] public TItem? SelectedItem { get; set; }
    [Parameter] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;
    [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
    [Parameter] public bool IsFilterable { get; set; }
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public string SearchPlaceholder { get; set; } = string.Empty;
    [Parameter] public string EmptyPlaceholder { get; set; } = string.Empty;
    [Parameter] public string Class { get; set; } = string.Empty;

    private ElementReference DropdownWrapper { get; set; }
    private string Id { get; } = $"id-{Guid.NewGuid()}";
    private string SearchString { get; set; } = string.Empty;
    private bool IsOpen { get; set; }

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

    private async Task SetSelectedItem(TItem item)
    {
        SelectedItem = item;
        await SelectedItemChanged.InvokeAsync(SelectedItem);
        SearchString = string.Empty;
        IsOpen = false;
    }

    private IEnumerable<TItem> GetFilteredList => string.IsNullOrWhiteSpace(SearchString) ? Items :
        Items.Where(item => ItemDisplayName(item).Contains(SearchString, StringComparison.OrdinalIgnoreCase));

    private bool IsSelectedItem(TItem item) => SelectedItem != null && SelectedItem.Equals(item);

    private string GetToggleText =>
        SelectedItem != null ? ItemDisplayName(SelectedItem) : Placeholder;

    private async Task HandleSearchingChange(ChangeEventArgs e)
    {
        SearchString = e.Value?.ToString() ?? string.Empty;
        StateHasChanged();
        await Task.Delay(100);
    }

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