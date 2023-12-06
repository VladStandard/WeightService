using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Models;

public class BlazorDataGrid<TItem>: DataGrid<TItem>
{
    public BlazorDataGrid()
    {
        PageSize = 7;
        ShowPager = true;
        PagerPosition = DataGridPagerPosition.Bottom;
        PagerOptions = new() { PaginationPosition = PagerElementPosition.Center };
        Class="text-xl !overflow-hidden !text-black table-fixed";
    }
}