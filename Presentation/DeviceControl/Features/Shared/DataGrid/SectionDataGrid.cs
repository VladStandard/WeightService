using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.DataGrid;

public class SectionDataGrid<TItem> : DataGrid<TItem>
{
    [Parameter] public int ItemsPerPage { get; set; } = 15;
    
    public SectionDataGrid()
    {
        PageSize = ItemsPerPage;
        ShowPager = true;
        PagerPosition = DataGridPagerPosition.Bottom;
        PagerOptions = new() { PaginationPosition = PagerElementPosition.Center };
    }

    protected override void OnParametersSet()
    {
        PageSize = ItemsPerPage;
    }
}
