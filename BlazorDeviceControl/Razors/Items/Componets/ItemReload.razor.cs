// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items.Componets;

public partial class ItemReload<T> : RazorPageItemBase<T> where T : DataCore.Sql.Tables.TableBase, new()
{
	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			//
		});
	}
}
