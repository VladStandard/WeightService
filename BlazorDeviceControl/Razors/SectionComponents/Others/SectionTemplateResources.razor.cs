// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionTemplateResources : RazorComponentSectionBase<TemplateResourceModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionTemplateResources()
    {
	    RazorComponentConfig.IsShowFilterMarked = true;
	    CssStyleRadzenColumn.Width = "40%";
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlItemsCast = AppSettings.DataAccess.GetListTemplateResources(RazorComponentConfig.IsShowMarked, RazorComponentConfig.IsShowOnlyTop);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
