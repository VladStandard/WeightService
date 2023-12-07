using Blazorise.DataGrid;

namespace DeviceControl.Features.Shared;

public class BlazorDataGrid<TItem> : DataGrid<TItem>
{
    public BlazorDataGrid()
    {
        PageSize = 15;
        ShowPager = true;
        PagerPosition = DataGridPagerPosition.Bottom;
        PagerOptions = new() { PaginationPosition = PagerElementPosition.Center };
        Class = "bg-white table-fixed";
    }
}

