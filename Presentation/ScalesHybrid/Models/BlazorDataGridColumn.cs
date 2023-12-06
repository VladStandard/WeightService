using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Models;

public class BlazorDataGridColumn<TItem>: DataGridColumn<TItem>
{
    public BlazorDataGridColumn()
    {
        CellClass = _ => "truncate !w-[98%]";
        HeaderCellClass = "text-xl text-black overflow-hidden";
    }
}