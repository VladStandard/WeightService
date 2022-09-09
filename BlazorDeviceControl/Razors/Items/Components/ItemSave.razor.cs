// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items.Components;

/// <summary>
/// Actions save.
/// </summary>
public partial class ItemSave<T> : RazorPageItemBase<T> where T : TableBase, new()
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
