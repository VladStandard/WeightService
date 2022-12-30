// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.NestingFks;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;

public partial class SectionNestingFks : RazorComponentSectionBase<NestingFkModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionNestingFks()
    {
        SqlCrudConfigSection.IsGuiShowItemsCount = true;
        SqlCrudConfigSection.IsGuiShowFilterMarked = true;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlSectionCast = DataContext.GetListNotNullable<NestingFkModel>(SqlCrudConfigSection);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}