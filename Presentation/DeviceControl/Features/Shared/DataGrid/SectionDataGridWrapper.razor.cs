using Blazorise.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Shared.DataGrid;

public sealed partial class SectionDataGridWrapper<TItem>: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    [Parameter] public RenderFragment ChildContent { get; set; } = null!;
    [Parameter] public IEnumerable<TItem> GridData { get; set; } = new List<TItem>();
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public bool IsFilterable { get; set; }
    [Parameter] public EventCallback GetGridData { get; set; }
    [Parameter] public EventCallback<DataGridRowMouseEventArgs<TItem>> OnDoubleClick { get; set; }
    [Parameter] public bool IsLoading { get; set; } = true;
    [Parameter] public int ItemsPerPage { get; set; } = 15;
    [Parameter] public bool IsBorderless { get; set; }
    [Parameter] public bool IsCreatable { get; set; }
    [Parameter] public EventCallback AddDataAction { get; set; }

    private DataGrid<TItem> DataGrid { get; set; } = null!;

    public async Task ReloadData()
    {
        await GetGridData.InvokeAsync();
        await DataGrid.Reload();
    }
}