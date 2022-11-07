// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlusPackages : RazorComponentSectionBase<PluPackageModel, PluModel>
{
    #region Public and private fields, properties, constructor

    public SectionPlusPackages()
    {
	    SqlCrudConfigList.IsGuiShowItemsCount = true;
	    SqlCrudConfigList.IsGuiShowFilterAdditional = true;
	    SqlCrudConfigList.IsGuiShowFilterMarked = true;
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
	            SqlCrudConfigList.SetFilters(nameof(PluPackageModel.Plu), ParentRazor?.SqlItem, EnumFilterAction.Add);
				SqlItemsCast = DataContext.GetListNotNull<PluPackageModel>(SqlCrudConfigList);
            }
        });
    }

    #endregion
}
