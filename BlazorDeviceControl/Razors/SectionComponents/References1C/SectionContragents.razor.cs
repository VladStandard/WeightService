// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Contragents;

namespace BlazorDeviceControl.Razors.SectionComponents.References1C;

public partial class SectionContragents : RazorComponentSectionBase<ContragentModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlSectionCast = DataContext.GetListNotNullable<ContragentModel>(SqlCrudConfigSection);
                AutoShowFilterOnlyTopSetup();
            }
        });
    }

    #endregion
}
