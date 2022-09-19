// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlusScales : RazorComponentSectionBase<PluScaleModel>
{
	#region Public and private fields, properties, constructor

	public SectionPlusScales()
	{
		RazorComponentConfig.IsShowFilterAdditional = true;
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
				SqlItemsCast = AppSettings.DataAccess.GetListPluScales(RazorComponentConfig.IsShowMarked, RazorComponentConfig.IsShowOnlyTop, SqlItem);
			}
		});
	}

	#endregion
}
