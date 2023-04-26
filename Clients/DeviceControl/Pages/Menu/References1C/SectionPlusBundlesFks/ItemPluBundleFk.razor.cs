// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusBundlesFks;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionPlusBundlesFks;

public sealed partial class ItemPluBundleFk : RazorComponentItemBase<PluBundleFkModel>
{
    #region Public and private fields, properties, constructor

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSetJustOne(
            () =>
            {
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<PluBundleFkModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<PluBundleFkModel>();
            }
        );
    }

    #endregion
}