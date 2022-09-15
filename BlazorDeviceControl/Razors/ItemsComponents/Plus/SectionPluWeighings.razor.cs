// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemsComponents.Plus;

public partial class SectionPluWeighings : RazorPageSectionBase<PluWeighingModel>
{
	#region Public and private fields, properties, constructor

	public SectionPluWeighings()
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
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, 0, RazorPageConfig.IsShowMarked, RazorPageConfig.IsShowOnlyTop);
				ItemsCast = AppSettings.DataAccess.GetList<PluWeighingModel>(sqlCrudConfig);

				ButtonSettings = new(true, true, true, true, true, false, false);
			}
		});
	}

	#endregion
}