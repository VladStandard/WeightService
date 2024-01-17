using System.Drawing;
using Blazor.Heroicons;
using Blazorise.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Ws.StorageCore.Common;

namespace DeviceControl.Features.Sections.Shared.DataGrid;

[CascadingTypeParameter(nameof(TItem))]
public sealed partial class SectionDataGridWrapper<TItem> : ComponentBase, IDisposable where TItem : SqlEntityBase, new()
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter] public RenderFragment ChildContent { get; set; } = null!;
    [Parameter] public RenderFragment? DataGridButtons { get; set; }
    [Parameter] public IEnumerable<TItem> GridData { get; set; } = new List<TItem>();
    [Parameter] public EventCallback GetGridData { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public int ItemsPerPage { get; set; } = 13;
    [Parameter] public EventCallback AddAction { get; set; }
    [Parameter] public EventCallback<TItem> DeleteAction { get; set; }
    [Parameter] public EventCallback<TItem> ReadAction { get; set; }
    [Parameter] public EventCallback<TItem> OpenInTabAction { get; set; }
    [Parameter] public bool IsFilterable { get; set; }
    [Parameter] public bool IsLoading { get; set; } = true;
    [Parameter] public bool IsBorderless { get; set; }
    [Parameter] public bool IsGroupable { get; set; }
    [Parameter] public bool IsCollapsed { get; set; }

    private DataGrid<TItem> DataGrid { get; set; } = null!;
    private bool IsVisibleContextMenu { get; set; }
    private Point ContextMenuPos { get; set; }
    private TItem ContextMenuItem { get; set; } = new();
    private IEnumerable<ContextMenuEntry> ContextMenuEntries { get; set; } = new List<ContextMenuEntry>();
    private IJSObjectReference Module { get; set; } = null!;
    private TItem? SelectedItem { get; set; }

    protected override void OnInitialized()
    {
        InitializeContextMenu();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await InitializeJsModule();
    }

    private async Task InitializeJsModule()
    {
        Module = await JsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./Features/Sections/Shared/DataGrid/SectionDataGridWrapper.razor.js");
        await Module.InvokeVoidAsync("addClickOutsideListener", 
            "dataGridContextMenu", DotNetObjectReference.Create(this));
    }
    
    [JSInvokable]
    public void ContextMenuClickedOutsideAction()
    {
        SelectedItem = null;
        IsVisibleContextMenu = false;
        StateHasChanged();
    }

    private void InitializeContextMenu()
    {
        if (ReadAction.HasDelegate)
            ContextMenuEntries = 
                ContextMenuEntries.Append(new()
                {
                    Name = Localizer["ContextMenuOpen"],
                    IconName = HeroiconName.ArrowTopRightOnSquare,
                    OnClickAction = EventCallback.Factory.Create(this, OnContextItemOpenClicked)
                });
        if (OpenInTabAction.HasDelegate)
            ContextMenuEntries = 
                ContextMenuEntries.Append(new()
                {
                    Name = Localizer["ContextMenuOpenInNewTab"],
                    IconName = HeroiconName.ArrowTopRightOnSquare,
                    OnClickAction = EventCallback.Factory.Create(this, OnContextItemOpenInNewTabClicked)
                });
        if (DeleteAction.HasDelegate) 
            ContextMenuEntries = ContextMenuEntries.Append(new()
            {
                Name = Localizer["ContextMenuDelete"],
                IconName = HeroiconName.Trash,
                OnClickAction = EventCallback.Factory.Create(this, OnContextItemDeleteClicked),
                CustomClass = "hover:bg-red-200 hover:text-red-600"
            });
    }
    
    private static void CustomRowStyling(TItem item, DataGridRowStyling styling) =>
        styling.Class = "transition-colors !border-y !border-black/[.1] hover:bg-sky-100";
    
    
    private static DataGridRowStyling CustomHeaderRowStyling() =>
        new() { Class = "bg-sky-200 truncate text-black overflow-hidden" };
    
    
    private static void CustomCellStyling(TItem item, DataGridColumn<TItem> gridItem, DataGridCellStyling styling) =>
        styling.Class = "truncate";
    
    private Task OnRowContextMenu(DataGridRowMouseEventArgs<TItem> eventArgs)
    {
        IsVisibleContextMenu = true;
        ContextMenuItem = eventArgs.Item;
        SelectedItem = ContextMenuItem;
        ContextMenuPos = eventArgs.MouseEventArgs.Client;
        return Task.CompletedTask;
    }
    
    private async Task OnContextItemOpenClicked()
    {
        await ReadAction.InvokeAsync(ContextMenuItem);
        IsVisibleContextMenu = false;
    }
    
    private async Task OnContextItemOpenInNewTabClicked()
    {
        await OpenInTabAction.InvokeAsync(ContextMenuItem);
        IsVisibleContextMenu = false;
    }
    
    private async Task OnContextItemDeleteClicked()
    {
        await DataGrid.Delete(ContextMenuItem);
        await DeleteAction.InvokeAsync(ContextMenuItem);
        IsVisibleContextMenu = false;
    }

    private Task OnDoubleClick(DataGridRowMouseEventArgs<TItem> eventArgs)
    {
        SelectedItem = null;
        ReadAction.InvokeAsync(eventArgs.Item);
        return Task.CompletedTask;
    }

    public async Task ReloadData()
    {
        await GetGridData.InvokeAsync();
        await DataGrid.Reload();
        await OnGetGridDataAction();
    }

    private async Task OnGetGridDataAction()
    {
        if (IsGroupable && !IsCollapsed)
            await DataGrid.ExpandAllGroups();
    }
    
    public async void Dispose()
    {
        try
        {
            await Module.InvokeVoidAsync("removeClickOutsideListener", "dataGridContextMenu");
            await Module.DisposeAsync();
        }
        catch (Exception ex) when (ex is JSDisconnectedException or ArgumentNullException)
        {
            // pass error
        }
    }
}

public class ContextMenuEntry
{
    public string Name { get; init; } = string.Empty;
    public EventCallback OnClickAction { get; init; } = EventCallback.Empty;
    public string IconName { get; init; } = string.Empty;
    public string CustomClass { get; init; } = string.Empty;
}