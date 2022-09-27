// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionAccess : RazorComponentSectionBase<AccessModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionAccess()
    {
		RazorComponentConfig.IsShowItemsCount = true;
        RazorComponentConfig.IsShowFilterMarked = true;
	    RazorComponentConfig.IsShowFilterOnlyTop = true;
	}

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemsCast = AppSettings.DataAccess.GetListAcesses(RazorComponentConfig.IsShowMarked, RazorComponentConfig.IsShowOnlyTop);

				ButtonSettings = new(true, true, true, true, true, false, false);
            }
		});
    }

    #endregion
}
