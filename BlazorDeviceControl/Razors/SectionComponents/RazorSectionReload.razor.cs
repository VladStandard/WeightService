// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using BlazorDeviceControl.Razors.SectionComponents.Plus;

namespace BlazorDeviceControl.Razors.SectionComponents;

public partial class RazorSectionReload<TItem, TItemFilter> : RazorComponentSectionBase<TItem, TItemFilter> 
	where TItem : SqlTableBase, new() where TItemFilter : SqlTableBase, new()
{
	#region Public and private fields, properties, constructor

	//

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				//if (ParentRazor is not null && ParentRazor is SectionPlusPackages sectionPlusPackages)
				//{
				//	//sectionPlusPackages.SqlItemFilter = (PluModel)SqlItemFilter;
				//}
			}
		});
	}

	#endregion
}
