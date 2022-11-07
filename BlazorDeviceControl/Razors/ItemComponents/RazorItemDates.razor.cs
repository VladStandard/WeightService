// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents;

public partial class RazorItemDates<TItem> : RazorComponentItemBase<TItem> where TItem : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	private string CreateDt { get; set; }
	private string ChangeDt { get; set; }

	public RazorItemDates()
	{
		CreateDt = string.Empty;
		ChangeDt = string.Empty;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				if (SqlItem is not null)
				{
					CreateDt = StringUtils.FormatDtRus(SqlItem.CreateDt, true);
					ChangeDt = StringUtils.FormatDtRus(SqlItem.ChangeDt, true);
				}
			}
		});
	}

	#endregion
}
