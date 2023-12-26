using Blazorise.DataGrid;

namespace ScalesHybrid.Models;

public class BlazorDataGridColumn<TItem>: DataGridColumn<TItem>
{
    public BlazorDataGridColumn()
    {
        CellClass = _ => "!py-2.5 truncate !w-[98%] xl:!py-4";
        HeaderCellClass = "text-lg text-black overflow-hidden xl:text-xl";
    }
}