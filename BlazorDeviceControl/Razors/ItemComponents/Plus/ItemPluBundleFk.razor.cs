// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Boxes;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluBundleFk : RazorComponentItemBase<PluBundleFkModel>
{
    #region Public and private fields, properties, constructor

    //

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSetJustOne(
            () => 
            {
                DataContext.GetListNotNullable<BoxModel>(SqlCrudConfigList);
                SqlItemCast = DataAccess.GetItemNotNullable<PluBundleFkModel>(IdentityUid);
                    if (SqlItemCast.IdentityIsNew)
                    SqlItemCast = SqlItemNew<PluBundleFkModel>();

                ButtonSettings = new(false, false, false, false, false, true, true);
            }
        );
    }

    #endregion
}