// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents;

public partial class RazorSectionFieldIdentity<TItem> : RazorComponentSectionBase<TItem> where TItem : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	public RazorSectionFieldIdentity()
	{
		CssStyleRadzenColumn.IsShowLink = true;
		CssStyleRadzenColumn.Width = new TItem().Identity.Name switch
		{
			SqlFieldIdentityEnum.Id => "5%",
			SqlFieldIdentityEnum.Uid => "20%",
			_ => CssStyleRadzenColumn.Width
		};
	}

	#endregion

	#region Public and private methods

	//

	#endregion
}
