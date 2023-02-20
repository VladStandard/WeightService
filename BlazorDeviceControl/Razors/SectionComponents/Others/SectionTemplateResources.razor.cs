// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.TemplatesResources;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionTemplateResources : RazorComponentSectionBase<TemplateResourceModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionTemplateResources() : base()
    {
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
	            SqlSectionCast = DataContext.GetListNotNullable<TemplateResourceModel>(SqlCrudConfigSection);
                AutoShowFilterOnlyTopSetup();
            }
        });
    }

    #endregion
}
