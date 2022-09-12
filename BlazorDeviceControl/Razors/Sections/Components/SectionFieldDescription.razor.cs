// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections.Components;

public partial class SectionFieldDescription<T> : RazorPageSectionBase<T> where T : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	public SectionFieldDescription()
	{
		CssStyleRadzenColumn.Width = "30%";
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
