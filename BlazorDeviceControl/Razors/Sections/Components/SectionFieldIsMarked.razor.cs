// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.Sections.Components;

public partial class SectionFieldIsMarked<TItem> : RazorPageSectionBase<TItem> where TItem : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	public SectionFieldIsMarked()
	{
		CssStyleRadzenColumn.Width = "5%";
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			//
		});
	}

	#endregion
}
