// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;

namespace BlazorDeviceControl.Razors.SectionComponents.Plus;
        
public partial class SectionPlusNestingFks : RazorComponentSectionBase<PluNestingFkModel, PluModel>
{
    #region Public and private fields, properties, constructor

    public SectionPlusNestingFks() : base()
    {
        ButtonSettings = new(true, true, true,
            true, true, true, false);
        SqlCrudConfigSection.NativeQuery = SqlQueries.DbScales.Tables.PluNestingFks.GetList(true);
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlCrudConfigSection.NativeParameters = new()
                {
                    new("P_UID", SqlItem?.IdentityValueUid),
                };
                SqlSectionCast = DataContext.GetListNotNullable<PluNestingFkModel>(SqlCrudConfigSection);
                AutoShowFilterOnlyTopSetup();
            }
        });
    }

    #endregion
}