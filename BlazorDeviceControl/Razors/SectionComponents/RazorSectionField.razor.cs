// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.SectionComponents;

public partial class RazorSectionField<TItem> : RazorComponentSectionBase<TItem, SqlTableBase> where TItem : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	public RazorSectionField()
	{
		CssStyleRadzenColumn.Width = "12%";
	}

	#endregion

	#region Public and private methods

	//

	#endregion
}
