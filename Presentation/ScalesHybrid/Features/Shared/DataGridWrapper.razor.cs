using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using Ws.Domain.Models.Common;

namespace ScalesHybrid.Features.Shared;

[CascadingTypeParameter(nameof(TItem))]
public sealed partial class DataGridWrapper<TItem>: ComponentBase where TItem: EntityBase, new()
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public IEnumerable<TItem> GridData { get; set; } = [];
    [Parameter] public EventCallback<TItem> OnItemSelect { get; set; }
    [Parameter] public int ItemsPerPage { get; set; } = 7;
    [Parameter] public bool IsMultiSelect { get; set; }
    [Parameter] public List<TItem> SelectedItems { get; set; } = [];
    [Parameter] public Action<List<TItem>>? SelectedItemsChanged { get; set; }
    [Parameter] public bool IsDesktop { get; set; }

    public DataGrid<TItem> DataGrid { get; set; } = null!;

    private void OnSelectedItemsChanged() => SelectedItemsChanged?.Invoke(SelectedItems);
    
    private static void CustomRowStyling(TItem item, DataGridRowStyling styling) =>
        styling.Class = "transition-colors hover:bg-blue-100";
    
    
    private static DataGridRowStyling CustomHeaderRowStyling() =>
        new() { Class = "truncate text-lg xl:text-xl" };
    
    
    private static void CustomCellStyling(TItem item, DataGridColumn<TItem> gridItem, DataGridCellStyling styling) =>
        styling.Class = "truncate text-black !py-2 font-light text-lg xl:!py-3 xl:text-xl";

    private string CustomPaginationButtonStyle() => $"h-4 w-4 m-2 {(!IsDesktop ? "xl:h-6 xl:w-6" : "")}";
    
    private string CustomPaginationPageStyle() => $"h-8 w-8 text-lg {(!IsDesktop ? "xl:h-10 xl:w-10 xl:text-xl" : "")}";
}