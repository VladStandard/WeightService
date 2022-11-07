// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPluWeighings : RazorComponentSectionBase<PluWeighingModel, SqlTableBase>
{
	#region Public and private fields, properties, constructor

	public SectionPluWeighings()
	{
		SqlCrudConfigList.IsGuiShowItemsCount = true;
		SqlCrudConfigList.IsGuiShowFilterMarked = true;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlItemsCast = DataAccess.GetListPluWeighings(SqlCrudConfigList);

				ButtonSettings = new(false, true, false, true, false, false, false);
			}
		});
	}

	#endregion
}