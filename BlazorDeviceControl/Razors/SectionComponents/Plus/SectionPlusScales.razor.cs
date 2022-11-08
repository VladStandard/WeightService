// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.Fields;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlusScales : RazorComponentSectionBase<PluScaleModel, ScaleModel>
{
	#region Public and private fields, properties, constructor

	public SectionPlusScales()
	{
		SqlCrudConfigSection.IsGuiShowItemsCount = true;
		SqlCrudConfigSection.IsGuiShowFilterAdditional = true;
		SqlCrudConfigSection.IsGuiShowFilterMarked = true;
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
				SqlCrudConfigSection.SetFilters(nameof(PluScaleModel.Scale), ParentRazor?.SqlItem, EnumFilterAction.Add);
				SqlSectionCast = DataContext.GetListNotNull<PluScaleModel>(SqlCrudConfigSection);
			}
		});
	}

	private string GetPluPackagesCount(PluModel plu)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(plu, nameof(PluScaleModel.Plu));
		return DataContext.GetListNotNull<PluPackageModel>(sqlCrudConfig).Count.ToString();
	}

	#endregion
}
