// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PlusGroups;

namespace BlazorDeviceControl.Razors.SectionComponents.References1C;

public partial class SectionNomenclaturesGroups : RazorComponentSectionBase<PluGroupModel, SqlTableBase>
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
                SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigSection.CloneCast();
                sqlCrudConfig.AddFilters(new SqlFieldFilterModel($"{nameof(PluGroupModel.IsGroup)}", false));
                //sqlCrudConfig.SetOrders(new(nameof(SqlTableBase.Name), SqlFieldOrderEnum.Asc));
                sqlCrudConfig.IsResultOrder = true;
                SqlSectionCast = DataContext.GetListNotNullable<PluGroupModel>(sqlCrudConfig);
                AutoShowFilterOnlyTopSetup();
            }
        });
    }

    #endregion
}