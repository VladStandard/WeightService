using Blazorise;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace DeviceControl.Features.Sections.Shared.Form;

public sealed partial class SectionFormInputSelect<TItem>: SectionFormInputBase, IDisposable where TItem: new()
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    [Parameter, EditorRequired] public IEnumerable<TItem> Items { get; set; } = new List<TItem>();
    [Parameter, EditorRequired] public TItem SelectedItem { get; set; } = new();
    [Parameter, EditorRequired] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;
    [Parameter, EditorRequired] public Func<TItem, string> ItemValue { get; set; } = item => item!.ToString()!;
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public EventCallback<TItem> SelectedItemChanged { get; set; }
    
    private string SelectedItemValue { get; set; } = string.Empty;
    private Dropdown Dropdown { get; set; } = new();
    private ElementReference DropdownWrapper { get; set; }
    private IJSObjectReference Module { get; set; } = null!;
    private string SearchString { get; set; } = string.Empty;
    
    protected override void OnParametersSet()
    {
        string newValue = ItemValue(SelectedItem);
        if (!SelectedItemValue.Equals(newValue))
            SelectedItemValue = newValue;
    }

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

    private async Task HandleSearchingChange(ChangeEventArgs e)
    {
        SearchString = e.Value?.ToString() ?? string.Empty;
        StateHasChanged();
        await Task.Delay(100);
    }

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
