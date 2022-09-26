// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents;

public partial class RazorSectionFieldIsMarked<TItem> : RazorComponentSectionBase<TItem, SqlTableBase> where TItem : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	public RazorSectionFieldIsMarked()
	{
		CssStyleRadzenColumn.Width = "5%";
	}

	#endregion

	#region Public and private methods

	//

	#endregion
}
