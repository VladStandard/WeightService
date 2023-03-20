// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.Scales;

namespace BlazorDeviceControl.Pages.SectionComponents.Admins;

public partial class SectionPlusScales : RazorComponentSectionBase<PluScaleModel>
{
	#region Public and private fields, properties, constructor
    [Parameter] public ScaleModel Scale { get; set; }
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

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection.ClearFilters();
        if (HideNoneActivePlu)
            SqlCrudConfigSection.AddFilters(new SqlFieldFilterModel(nameof(PluScaleModel.IsActive), SqlFieldComparerEnum.Equal, true));
        SqlCrudConfigSection.AddFilters(nameof(PluScaleModel.Scale), Scale);
        SqlSectionCast.Clear();
        base.SetSqlSectionCast();
    }
    
    private string GetPluPackagesCount(PluModel plu)
	{
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(plu, nameof(PluScaleModel.Plu));
		return DataContext.GetListNotNullable<PluBundleFkModel>(sqlCrudConfig).Count.ToString();
	}

    protected override async Task OnSqlSectionSaveAsync()
    {
        await base.OnSqlSectionSaveAsync();
        SetSqlSectionCast();
    }

    #endregion
}
