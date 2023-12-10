using Blazorise.DataGrid;

namespace DeviceControl.Features.Shared.DataGrid;

public class BlazorDataGridColumn<TItem>: DataGridColumn<TItem>
{
    public BlazorDataGridColumn()
    {
        CellClass = _ => "truncate";
        HeaderCellClass = "bg-sky-200 text-black overflow-hidden";
    }    
}