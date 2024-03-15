using DeviceControl2.Source.Widgets.Tests;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DeviceControl2.Source.Shared.UI.DataGrid;

[CascadingTypeParameter(nameof(TGridItem))]
public sealed partial class FluentUIDataGrid<TGridItem> : ComponentBase
{
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; } = default!;
    [Parameter] public IQueryable<TGridItem> Items { get; set; } = new List<TGridItem>().AsQueryable();
    [Parameter] public string GridTemplate { get; set; } = "1fr";
    
    private PaginationState Pagination { get; } = new() { ItemsPerPage = 10 };

    protected override void OnInitialized() =>
        Pagination.TotalItemCountChanged += (_, _) => StateHasChanged();
    
    private async Task HandleOnRowFocus(FluentDataGridRow<TGridItem> row) =>
        await DialogService.ShowDialogAsync<SimpleDialog>(new());
}