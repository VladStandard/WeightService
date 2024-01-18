using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Ws.StorageCore.Common;

namespace ScalesHybrid.Features.Shared;

[CascadingTypeParameter(nameof(TItem))]
public sealed partial class DataGridWrapper<TItem>: ComponentBase where TItem: SqlEntityBase, new()
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public IEnumerable<TItem> GridData { get; set; } = [];
    [Parameter] public EventCallback<TItem> OnItemSelect { get; set; }
    [Parameter] public int ItemsPerPage { get; set; } = 7;
    [Parameter] public string Title { get; set; } = string.Empty;

    public DataGrid<TItem> DataGrid { get; set; } = null!;

    private async Task CloseCurrentDialog() => await ModalService.Hide();
}