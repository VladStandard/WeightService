using Blazorise.DataGrid;

namespace DeviceControl.Features.Shared;

public class BlazorDataGridColumn<TItem>: DataGridColumn<TItem>
{
    public BlazorDataGridColumn()
    {
        // CellClass = _ => "truncate !w-[98%]";
        HeaderCellClass = "bg-sky-200 text-black overflow-hidden";
    }    
}