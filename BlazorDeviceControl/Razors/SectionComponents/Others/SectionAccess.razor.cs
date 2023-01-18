// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Access;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionAccess : RazorComponentSectionBase<AccessModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionAccess() : base()
    {
        SqlCrudConfigSection.IsGuiShowFilterOnlyTop = false;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlSectionCast = DataContext.GetListNotNullable<AccessModel>(SqlCrudConfigSection);

            }
		});
    }

    #endregion
}