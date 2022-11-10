// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Devices;

public partial class SectionScalesScreenShots : RazorComponentSectionBase<ScaleScreenShotModel, ScaleModel>
{
    #region Public and private fields, properties, constructor

    public SectionScalesScreenShots()
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
	            SqlCrudConfigSection.SetFilters(nameof(ScaleScreenShotModel.Scale), ParentRazor?.SqlItem);
				SqlSectionCast = DataContext.GetListNotNullable<ScaleScreenShotModel>(SqlCrudConfigSection);
            }
        });
    }

    #endregion
}
