// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Boxes;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemBox : RazorComponentItemBase<BoxModel>
{
    #region Public and private fields, properties, constructor

    //

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = DataAccess.GetItemNotNullable<BoxModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                {
                    SqlItemCast = SqlItemNew<BoxModel>();
                }

                ButtonSettings = new(false, false, false, false, false, true, true);
            }
        });
    }

    #endregion
}
