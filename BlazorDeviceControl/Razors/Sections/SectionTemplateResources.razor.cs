// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionTemplateResources : RazorPageSectionBase<TemplateResourceModel>
{
    #region Public and private fields, properties, constructor

    public SectionTemplateResources()
    {
	    RazorPageConfig.IsShowFilterMarked = true;
	    CssStyleRadzenColumn.Width = "40%";
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsParametersSet(new()
        {
            () =>
            {
	            ItemsCast = AppSettings.DataAccess.GetListTemplateResources(RazorPageConfig.IsShowMarked, RazorPageConfig.IsShowOnlyTop);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
