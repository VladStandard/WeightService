// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluScale : RazorComponentItemBase<PluScaleModel>
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
                SqlItemCast = DataContext.GetItemNotNull<PluScaleModel>(IdentityUid);
                if (SqlItemCast.IdentityIsNew)
	                SqlItem = SqlItemNew<PluScaleModel>();
                DataContext.GetListNotNull<PluModel>(false, false, true);
	            DataContext.GetListNotNull<ScaleModel>(false, false, true);

	            ButtonSettings = new(false, false, false, false, false, true, true);
            }
        });
    }

    #endregion
}
