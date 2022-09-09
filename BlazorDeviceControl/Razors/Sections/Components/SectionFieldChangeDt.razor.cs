// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections.Components;

public partial class SectionFieldChangeDt<T> : RazorPageSectionBase<T> where T : TableBase, new()
{
	#region Public and private fields, properties, constructor

	public SectionFieldChangeDt()
	{
		CssStyleRadzenColumn.Width = "12%";
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
