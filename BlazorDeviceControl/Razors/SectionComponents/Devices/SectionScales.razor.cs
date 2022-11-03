// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Devices;

public partial class SectionScales : RazorComponentSectionBase<ScaleModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionScales()
    {
	    SqlCrudConfigSection.IsShowItemsCount = true;
	    SqlCrudConfigSection.IsShowFilterMarked = true;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemsCast = DataContext.GetListNotNull<ScaleModel>(SqlCrudConfigSection);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
