// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusScales;
using WsStorageCore.TableScaleModels.Scales;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionPluScales;

public sealed partial class ItemPluScales : RazorComponentItemBase<WsSqlPluScaleModel>
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
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlPluScaleModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<WsSqlPluScaleModel>();
                ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlPluModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
	            ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlScaleModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
            }
        });
    }

    #endregion
}