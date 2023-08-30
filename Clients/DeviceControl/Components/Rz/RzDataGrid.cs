namespace DeviceControl.Components.Rz;

public class RzDataGrid<TItem> : RadzenDataGrid<TItem>
{
    public RzDataGrid()
    {
        AllowPaging = true;
        AllowSorting = true;
        PagerPosition = PagerPosition.TopAndBottom;
        PagerHorizontalAlign = HorizontalAlign.Center;
        GridLines = DataGridGridLines.Horizontal;
        // PageSize = WsSqlContextManagerHelper.Instance.JsonSettings.Local.SectionRowsCount;
    }
}
