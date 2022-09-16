// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemsComponents.Components;

public partial class SectionField<TItems, TItemFilter> : RazorPageSectionBase<TItems, TItemFilter>
	where TItems : SqlTableBase, new() where TItemFilter : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	public SectionField()
	{
		CssStyleRadzenColumn.Width = "12%";
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			//
		});
	}

	#endregion
}
