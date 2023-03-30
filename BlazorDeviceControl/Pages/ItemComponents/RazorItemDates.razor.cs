// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Pages.ItemComponents;

public partial class RazorItemDates : LayoutComponentBase
{
	#region Public and private fields, properties, constructor
    [Parameter] public SqlTableBase? SqlItem { get; set; }
	private string CreateDt =>  SqlItem == null ? "" : StrUtils.FormatDtRus(SqlItem.CreateDt, true);
	private string ChangeDt =>  SqlItem == null ? "" : StrUtils.FormatDtRus(SqlItem.ChangeDt, true);
    
	#endregion

	#region Public and private methods

    #endregion
}
