// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemsComponents;

public partial class SectionBarCodes : RazorPageSectionBase<BarCodeModel>
{
	#region Public and private fields, properties, constructor

	public SectionBarCodes()
	{
		RazorPageConfig.IsShowFilterMarked = true;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				ItemsCast = AppSettings.DataAccess.GetListBarCodes(RazorPageConfig.IsShowMarked, RazorPageConfig.IsShowOnlyTop);

				ButtonSettings = new(true, true, true, true, true, false, false);
			}
		});
	}

	#endregion
}
