// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen.Blazor;
using WsStorageCore.Helpers;

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
        PageSize = WsSqlContextManagerHelper.Instance.JsonSettings.Local.SectionRowsCount;
    }
}