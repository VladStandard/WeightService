// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlusScales : RazorComponentSectionBase<PluScaleModel, ScaleModel>
{
	#region Public and private fields, properties, constructor

	public SectionPlusScales()
	{
		RazorComponentConfig.IsShowItemsCount = true;
		RazorComponentConfig.IsShowFilterAdditional = true;
		RazorComponentConfig.IsShowFilterMarked = true;
		ButtonSettings = new(true, true, true, true, true, true, false);
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlItemsCast = BlazorAppSettings.DataAccess.GetListPluScales(ParentRazor?.SqlItem,
					RazorComponentConfig.IsShowMarked, RazorComponentConfig.IsShowOnlyTop, false);
			}
		});
	}

	#endregion
}
