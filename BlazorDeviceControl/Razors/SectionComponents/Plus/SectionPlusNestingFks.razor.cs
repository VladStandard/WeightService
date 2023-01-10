// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.Plus;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlusNestingFks : RazorComponentSectionBase<PluNestingFkModel, PluModel>
{
    #region Public and private fields, properties, constructor

    public SectionPlusNestingFks()
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
	            SqlCrudConfigSection.AddFilters(nameof(PluBundleFkModel.Plu), ParentRazor?.SqlItem);
				SqlSectionCast = DataContext.GetListNotNullable<PluNestingFkModel>(SqlCrudConfigSection);
            }
        });
    }

    #endregion
}
