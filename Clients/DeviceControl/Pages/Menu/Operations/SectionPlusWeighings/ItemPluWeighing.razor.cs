// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusWeighingsFks;

namespace BlazorDeviceControl.Pages.Menu.Operations.SectionPlusWeighings;

public sealed partial class ItemPluWeighing : RazorComponentItemBase<WsSqlPluWeighingModel>
{
    #region Public and private fields, properties, constructor

    public ItemPluWeighing() : base()
    {
        ButtonSettings = new(false, false, false, false, false, false, true);
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlPluWeighingModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                {
                    SqlItemCast = SqlItemNew<WsSqlPluWeighingModel>();
                }
            }
        });
    }

    #endregion
}
