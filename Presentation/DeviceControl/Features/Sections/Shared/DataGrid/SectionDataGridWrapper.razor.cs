using System.Drawing;
using Blazor.Heroicons;
using Blazorise.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.Shared.DataGrid;

[CascadingTypeParameter(nameof(TItem))]
public sealed partial class SectionDataGridWrapper<TItem> : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

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
    [Parameter] public bool IsBorderless { get; set; }
    [Parameter] public bool IsGroupable { get; set; }
    [Parameter] public bool IsCollapsed { get; set; }

    private DataGrid<TItem>? DataGrid { get; set; }
    private bool IsVisibleContextMenu { get; set; }
    private bool IsLoading { get; set; } = true;
    private TItem? SelectedItem { get; set; }
    private Point ContextMenuPos { get; set; }
    private TItem? ContextMenuItem { get; set; }
    private IEnumerable<ContextMenuEntry> ContextMenuEntries { get; set; } = new List<ContextMenuEntry>();
    
    private ElementReference ContextMenuRef { get; set; }

    protected override void OnInitialized() => InitializeContextMenu();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        
        if (GetGridData.HasDelegate)
            await UpdateDataGridData();
        
        IsLoading = false;
        StateHasChanged();
    }

    private void ContextMenuClickedOutsideAction()
    {
        SelectedItem = default;
        IsVisibleContextMenu = false;
        StateHasChanged();
    }

    private void InitializeContextMenu()
    {
        AddContextMenuEntryIfDelegateExists(ReadAction, "ContextMenuOpen",
            HeroiconName.ArrowTopRightOnSquare, OnContextItemOpenClicked);
        AddContextMenuEntryIfDelegateExists(OpenInTabAction, "ContextMenuOpenInNewTab",
            HeroiconName.ArrowTopRightOnSquare, OnContextItemOpenInNewTabClicked);
        AddContextMenuEntryIfDelegateExists(DeleteAction, "ContextMenuDelete", 
            HeroiconName.Trash, OnContextItemDeleteClicked, "hover:bg-red-200 hover:text-red-600");
    }

    private void AddContextMenuEntryIfDelegateExists(EventCallback<TItem> actionDelegate, string nameLocalizationKey,
        string iconName, Func<Task> clickAction, string customClass = "")
    {
        if (!actionDelegate.HasDelegate) return;

        ContextMenuEntries = ContextMenuEntries.Append(new()
        {
            Name = Localizer[nameLocalizationKey],
            IconName = iconName,
            OnClickAction = EventCallback.Factory.Create(this, clickAction),
            CustomClass = customClass
        });
    }
    
    private static void CustomRowStyling(TItem item, DataGridRowStyling styling) =>
        styling.Class = "transition-colors !border-y !border-black/[.1] hover:bg-sky-100";
    
    
    private static DataGridRowStyling CustomHeaderRowStyling() =>
        new() { Class = "bg-sky-200 text-black [&_th]:truncate" };
    
    
    private static void CustomCellStyling(TItem item, DataGridColumn<TItem> gridItem, DataGridCellStyling styling) =>
        styling.Class = "truncate";
    
    private string GetSpinnerStyle() => $"h-5 w-5 text-white {(IsLoading ? "animate-spin" : "")}";

    private string GetTableStyle() =>
        $"table-fixed {(!GetIsPagerNeeded() ? "[&>tbody>tr:last-child]:!border-b-0 !mb-0" : "")}";

    private bool GetIsPagerNeeded() => !IsGroupable && (GridData.Count() > ItemsPerPage || IsBorderless);
    
    private async Task OnRowContextMenu(DataGridRowMouseEventArgs<TItem> eventArgs)
    {
        await ContextMenuRef.FocusAsync();
        IsVisibleContextMenu = true;
        ContextMenuItem = eventArgs.Item;
        SelectedItem = ContextMenuItem;
        ContextMenuPos = eventArgs.MouseEventArgs.Client;
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
        if (DataGrid != null && ContextMenuItem != null) 
            await DataGrid.Delete(ContextMenuItem);
        await DeleteAction.InvokeAsync(ContextMenuItem);
        IsVisibleContextMenu = false;
    }

    private async Task OnDoubleClick(DataGridRowMouseEventArgs<TItem> eventArgs)
    {
        SelectedItem = default;
        await ReadAction.InvokeAsync(eventArgs.Item);
    }

    private async Task UpdateDataGridData()
    {
        await GetGridData.InvokeAsync();
        if (DataGrid != null) await DataGrid.Reload();
        await OnGetGridDataAction();
    }

    public async Task ReloadData()
    {
        if (IsLoading) return;

        IsLoading = true;
        StateHasChanged();

        await UpdateDataGridData();
        
        IsLoading = false;
        StateHasChanged();
    }

    private async Task OnGetGridDataAction()
    {
        if (!IsGroupable || IsCollapsed || DataGrid == null) return;
        await DataGrid.ExpandAllGroups();
    }
}

public class ContextMenuEntry
{
    public string Name { get; init; } = string.Empty;
    public EventCallback OnClickAction { get; init; } = EventCallback.Empty;
    public string IconName { get; init; } = string.Empty;
    public string CustomClass { get; init; } = string.Empty;
}