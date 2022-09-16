// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemsComponents;

public partial class SectionTemplates : RazorPageSectionBase<TemplateModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionTemplates()
    {
	    RazorPageConfig.IsShowFilterMarked = true;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                ItemsCast = AppSettings.DataAccess.GetListTemplates(RazorPageConfig.IsShowMarked, RazorPageConfig.IsShowOnlyTop, false);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
