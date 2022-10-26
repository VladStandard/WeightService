// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Devices;

public partial class SectionScalesScreenShots : RazorComponentSectionBase<ScaleScreenShotModel, ScaleModel>
{
    #region Public and private fields, properties, constructor

    public SectionScalesScreenShots()
    {
        RazorComponentConfig.IsShowItemsCount = true;
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
                SqlItemsCast = AppSettings.DataAccess.GetListScalesScreenShots(ParentRazor?.SqlItem,
                    RazorComponentConfig.IsShowMarked, RazorComponentConfig.IsShowOnlyTop, true);
            }
        });
    }

    #endregion
}
