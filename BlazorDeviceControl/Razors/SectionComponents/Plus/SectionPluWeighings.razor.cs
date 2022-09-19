// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPluWeighings : RazorComponentSectionBase<PluWeighingModel>
{
	#region Public and private fields, properties, constructor

	public SectionPluWeighings()
	{
		RazorComponentConfig.IsShowFilterMarked = true;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, 0, RazorComponentConfig.IsShowMarked, RazorComponentConfig.IsShowOnlyTop);
				SqlItemsCast = AppSettings.DataAccess.GetList<PluWeighingModel>(sqlCrudConfig);

				ButtonSettings = new(true, true, true, true, true, false, false);
			}
		});
	}

	#endregion
}