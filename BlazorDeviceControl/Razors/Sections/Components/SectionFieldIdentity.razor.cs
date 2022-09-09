// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections.Components;

public partial class SectionFieldIdentity<T> : RazorPageSectionBase<T> where T : TableBase, new()
{
	#region Public and private fields, properties, constructor

	public SectionFieldIdentity()
	{
		T item = new();
		CssStyleRadzenColumn.Width = item.Identity.Name switch
		{
			SqlFieldIdentityEnum.Id => "7.5%",
			SqlFieldIdentityEnum.Uid => "20%",
			_ => CssStyleRadzenColumn.Width
		};
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
