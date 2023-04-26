// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusScales;
using WsStorageCore.TableScaleModels.Scales;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionPluScales;

public sealed partial class ItemPluScales : RazorComponentItemBase<PluScaleModel>
{
    #region Public and private fields, properties, constructor

    //

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new List<Action>
        {
            () =>
            {
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<PluScaleModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<PluScaleModel>();
                ContextManager.AccessManager.AccessList.GetListNotNullable<PluModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
	            ContextManager.AccessManager.AccessList.GetListNotNullable<ScaleModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
            }
        });
    }

    #endregion
}