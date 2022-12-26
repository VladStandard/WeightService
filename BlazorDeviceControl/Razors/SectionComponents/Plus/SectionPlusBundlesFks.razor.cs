// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Plus;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlusBundlesFks : RazorComponentSectionBase<PluBundleFkModel, PluModel>
{
    #region Public and private fields, properties, constructor

    public SectionPlusBundlesFks()
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
	            SqlCrudConfigSection.SetFilters(nameof(PluBundleFkModel.Plu), ParentRazor?.SqlItem);
				SqlSectionCast = DataContext.GetListNotNullable<PluBundleFkModel>(SqlCrudConfigSection);
            }
        });
    }

    #endregion
}
