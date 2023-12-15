using Blazorise.DataGrid;

namespace DeviceControl.Features.Shared.DataGrid;

public class SectionDataGridColumn<TItem>: DataGridColumn<TItem>
{
    public SectionDataGridColumn()
    {
        CellClass = _ => "truncate";
        HeaderCellClass = "bg-sky-200 text-black overflow-hidden";
    }    
}