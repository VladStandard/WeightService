using System.Drawing;
using Blazor.Heroicons;
using Blazorise.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Ws.StorageCore.Common;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Shared.DataGrid;

[CascadingTypeParameter(nameof(TItem))]
public sealed partial class SectionDataGridWrapper<TItem> : ComponentBase, IDisposable where TItem : SqlEntityBase, new()
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter] public RenderFragment ChildContent { get; set; } = null!;
    [Parameter] public IEnumerable<TItem> GridData { get; set; } = new List<TItem>();
    [Parameter] public EventCallback GetGridData { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public int ItemsPerPage { get; set; } = 13;
    [Parameter] public EventCallback AddAction { get; set; }
    [Parameter] public EventCallback<TItem> DeleteAction { get; set; }
    [Parameter] public EventCallback<TItem> ReadAction { get; set; }
    [Parameter] public bool IsFilterable { get; set; }
    [Parameter] public bool IsLoading { get; set; } = true;
    [Parameter] public bool IsBorderless { get; set; }

    private DataGrid<TItem> DataGrid { get; set; } = null!;
    private bool IsVisibleContextMenu { get; set; } = false;
    private Point ContextMenuPos { get; set; } = new();
    private TItem ContextMenuItem { get; set; } = new();
    private IEnumerable<ContextMenuEntry> ContextMenuEntries { get; set; } = new List<ContextMenuEntry>();
    private IJSObjectReference Module { get; set; } = null!;
    private TItem? SelectedItem { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        InitializeContextMenu();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./Features/Shared/DataGrid/SectionDataGridWrapper.razor.js");
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
        ContextMenuEntries = 
            ContextMenuEntries.Append(new()
            {
                Name = "Open",
                IconName = HeroiconName.ArrowTopRightOnSquare,
                OnClickAction = EventCallback.Factory.Create(this, OnContextItemOpenClicked)
            });
        // ContextMenuEntries = 
        //     ContextMenuEntries.Append(new()
        //     {
        //         Name = "Open in new tab",
        //         IconName = HeroiconName.ArrowTopRightOnSquare,
        //         OnClickAction = EventCallback.Factory.Create(this, OnContextItemCheckClicked)
        //     });
        if (DeleteAction.HasDelegate) 
            ContextMenuEntries = ContextMenuEntries.Append(new()
            {
                Name = "Delete",
                IconName = HeroiconName.Trash,
                OnClickAction = EventCallback.Factory.Create(this, OnContextItemDeleteClicked)
            });
    }
    
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
        await ReadAction.InvokeAsync(ContextMenuItem);
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
    }
    
    public async void Dispose()
    {
        await Module.InvokeVoidAsync("removeClickOutsideListener", "dataGridContextMenu");
        await Module.DisposeAsync();
    }
}

public class ContextMenuEntry
{
    public string Name { get; init; } = string.Empty;
    public EventCallback OnClickAction { get; init; } = EventCallback.Empty;
    public string IconName { get; init; } = string.Empty; 
}