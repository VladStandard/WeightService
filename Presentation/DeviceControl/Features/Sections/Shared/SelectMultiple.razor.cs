using Blazorise;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace DeviceControl.Features.Sections.Shared;

public sealed partial class SelectMultiple<TItem>: ComponentBase, IDisposable where TItem: new()
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    [Parameter, EditorRequired] public IEnumerable<TItem> Items { get; set; } = new List<TItem>();
    [Parameter, EditorRequired] public IEnumerable<TItem> SelectedItems { get; set; } = [];
    [Parameter] public Func<TItem, string> ItemDisplayName { get; set; } = item => item!.ToString()!;
    [Parameter] public EventCallback<IEnumerable<TItem>> SelectedItemsChanged { get; set; }
    
    private Dropdown Dropdown { get; set; } = new();
    private ElementReference DropdownWrapper { get; set; }
    private IJSObjectReference Module { get; set; } = null!;
    private string SearchString { get; set; } = string.Empty;
    private SelectVisibility SelectVisibility { get; set; } = SelectVisibility.All;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./libs/selectResize.js");
        await Module.InvokeVoidAsync("initializeResizeSelect", DropdownWrapper);
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
            searchingList.Where(item => ItemDisplayName(item).Contains(SearchString, StringComparison.OrdinalIgnoreCase));
    }
    
    private async Task HandleSearchingChange(ChangeEventArgs e)
    {
        SearchString = e.Value?.ToString() ?? string.Empty;
        StateHasChanged();
        await Task.Delay(100);
    }

    private void SwitchSelectVisibility()
    {
        SelectVisibility = SelectVisibility == SelectVisibility.All ? SelectVisibility.Selected : SelectVisibility.All;
        StateHasChanged();
    }

    private async Task ToggleSelectedItems()
    {
        SelectedItems = GetIsAllSelectedInFilteredList() ? SelectedItems.Except(GetFilteredList()).ToList() :
            SelectedItems.Union(GetFilteredList().Except(SelectedItems)).ToList();
        await SelectedItemsChanged.InvokeAsync(SelectedItems);
        StateHasChanged();
    }

    private bool GetIsAllSelectedInFilteredList() => !GetFilteredList().Except(SelectedItems).Any();
    
    private bool IsSelectedItem(TItem item) => SelectedItems.Any(i => i != null && i.Equals(item));
    
    private bool IsButtonSwitcherDisabled() => SelectedItems.Count() == Items.Count();

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