// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionPlusPackages : RazorComponentSectionBase<PluPackageModel>
{
    #region Public and private fields, properties, constructor

    public SectionPlusPackages()
    {
	    RazorComponentConfig.IsShowFilterAdditional = true;
		RazorComponentConfig.IsShowFilterMarked = true;
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
				SqlItemsCast = AppSettings.DataAccess.GetListPluPackages(RazorComponentConfig.IsShowMarked, RazorComponentConfig.IsShowOnlyTop, SqlItem);
            }
        });
    }

    #endregion
}
