// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Scales;

namespace BlazorDeviceControl.Razors.SectionComponents.Devices;

public partial class SectionScales : RazorComponentSectionBase<ScaleModel, SqlTableBase>
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
                SqlCrudConfigSection.AddOrders(new(nameof(ScaleModel.Description), SqlFieldOrderEnum.Asc));
                SqlSectionCast = DataContext.GetListNotNullable<ScaleModel>(SqlCrudConfigSection);
                AutoShowFilterOnlyTopSetup();
            }
        });
    }

    #endregion
}
