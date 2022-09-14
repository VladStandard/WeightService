// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.Sections.Plus;

public partial class SectionPlus : RazorPageSectionBase<PluModel>
{
	#region Public and private fields, properties, constructor

	public SectionPlus()
	{
		RazorPageConfig.IsShowFilterMarked = true;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			() =>
			{
				ItemsCast = AppSettings.DataAccess.GetListPlus(RazorPageConfig.IsShowMarked, RazorPageConfig.IsShowOnlyTop, false);

				ButtonSettings = new(false, false, true, true, false, false, false);
			}
		});
	}

	#endregion
}
