// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.Scales;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlusScales : RazorComponentSectionBase<PluScaleModel, ScaleModel>
{
	#region Public and private fields, properties, constructor

	public bool HideNoneActivePlu { get; set; }
    public SectionPlusScales() : base()
    {
        HideNoneActivePlu = true;
        SqlCrudConfigSection.IsGuiShowFilterAdditional = true;
		SqlCrudConfigSection.IsResultOrder = true;
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
                if (HideNoneActivePlu)
                    SqlCrudConfigSection.AddFilters(new SqlFieldFilterModel(nameof(PluScaleModel.IsActive), SqlFieldComparerEnum.Equal, true));
				else
                    SqlCrudConfigSection.RemoveFilters(new SqlFieldFilterModel(nameof(PluScaleModel.IsActive), SqlFieldComparerEnum.Equal, true));

                SqlCrudConfigSection.AddFilters(nameof(PluScaleModel.Scale), ParentRazor?.SqlItem);
                SqlSectionCast = DataContext.GetListNotNullable<PluScaleModel>(SqlCrudConfigSection);
                AutoShowFilterOnlyTopSetup();
            }
		});
	}

	private string GetPluPackagesCount(PluModel plu)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(plu, nameof(PluScaleModel.Plu));
		return DataContext.GetListNotNullable<PluBundleFkModel>(sqlCrudConfig).Count.ToString();
	}

	#endregion
}