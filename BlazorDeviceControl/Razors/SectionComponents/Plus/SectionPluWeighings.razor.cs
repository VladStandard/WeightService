// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.PlusWeighings;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPluWeighings : RazorComponentSectionBase<PluWeighingModel, SqlTableBase>
{
	#region Public and private fields, properties, constructor

	public SectionPluWeighings()
	{
		SqlCrudConfigSection.IsGuiShowItemsCount = true;
		SqlCrudConfigSection.IsGuiShowFilterMarked = true;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlSectionCast = DataContext.GetListNotNullable<PluWeighingModel>(SqlCrudConfigSection);
				
				ButtonSettings = new(false, true, false, true, false, false, false);
			}
		});
	}

	#endregion
}