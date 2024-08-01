using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ws.Components.Source.UI.Select;

public abstract class SelectBase<TItem, TValue> : ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }
    [Parameter] public Func<TItem, string> ItemDisplayName { get; set; } = item => item?.ToString() ?? string.Empty;
    [Parameter] public bool IgnoreWidth { get; set; }
    [Parameter] public string HtmlId { get; set; } = $"id-{Guid.NewGuid()}";

    internal Dictionary<Guid, SelectItem<TItem, TValue>> SelectItems { get; } = new();
    internal double TriggerWidth { get; private set; }
    public bool IsDropdownOpened { get; set; }
    public string SearchString { get; set; } = string.Empty;

    private DotNetObjectReference<SelectBase<TItem, TValue>> DotNetObjRef { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        DotNetObjRef = DotNetObjectReference.Create(this);
        await InitializeDropdownAsync();
    }

    private async Task InitializeDropdownAsync()
    {
        await SetDropdownWidthAsync();
        await JsRuntime.InvokeVoidAsync("addDotNetEventListener", "resize", DotNetObjRef, "HandleResize");
    }

    [JSInvokable("HandleResize")]
    public async Task HandleResize() => await SetDropdownWidthAsync();

    public void ToggleDropdown() => IsDropdownOpened = !IsDropdownOpened;

    private async Task SetDropdownWidthAsync()
    {
        double width = await JsRuntime.InvokeAsync<double>("getElementWidthById", HtmlId);
        if (Math.Abs(width - TriggerWidth) < 5) return;
        TriggerWidth = width;
        StateHasChanged();
    }

    internal void SetSearchingValue(string search)
    {
        SearchString = search;
        StateHasChanged();
    }

    internal void Register(SelectItem<TItem, TValue> item) => SelectItems.Add(item.Id, item);

    internal void Unregister(SelectItem<TItem, TValue> item) => SelectItems.Remove(item.Id);

    protected internal abstract Task SetValue(TItem item, bool withClose = true);
    protected internal abstract bool IsItemSelected(TItem item);

    public async ValueTask DisposeAsync()
    {
        try
        {
            await JsRuntime.InvokeVoidAsync("removeDotNetEventListener", "resize", DotNetObjRef, "HandleResize");
            DotNetObjRef.Dispose();
        }
        catch
        {
            // pass
        }
    }
}