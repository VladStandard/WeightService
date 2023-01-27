// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Versions;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionVersions : RazorComponentSectionBase<VersionModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionVersions() : base()
    {
        SqlCrudConfigSection.IsGuiShowFilterMarked = false;
        ButtonSettings = new(false, false, false, false, false, false, false);
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlSectionCast = DataContext.GetListNotNullable<VersionModel>(SqlCrudConfigSection);
                AutoShowFilterOnlyTopSetup();
            }
        });
    }

    #endregion
}