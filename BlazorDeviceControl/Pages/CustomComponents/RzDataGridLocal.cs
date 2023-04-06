// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen.Blazor;
using DataCore.Sql.Core.Helpers;

namespace BlazorDeviceControl.Pages.CustomComponents;

public class RzDataGridLocal<TItem> : RadzenDataGrid<TItem>
{
    public RzDataGridLocal()
    {
        AllowPaging = true;
        AllowSorting = true;
        Style = "DataGridSection";
        PagerPosition = PagerPosition.TopAndBottom;
        PagerHorizontalAlign = HorizontalAlign.Justify;
        PageSize = WsDataAccessHelper.Instance.JsonSettings.Local.SectionRowsCount;
    }
}
