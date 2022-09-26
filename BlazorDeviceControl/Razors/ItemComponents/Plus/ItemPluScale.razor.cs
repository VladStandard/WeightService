// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluScale : RazorComponentItemBase<PluScaleModel>
{
    #region Public and private fields, properties, constructor

    private List<ScaleModel> Scales { get; set; }
    private List<PluModel> Plus { get; set; }

    public ItemPluScale() : base()
    {
        Scales = new();
        Plus = new();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = AppSettings.DataAccess.GetItemByUidNotNull<PluScaleModel>(IdentityUid);
                if (SqlItemCast.Identity.IsNew())
	                SqlItem = SqlItemNew<PluScaleModel>();
                Plus = AppSettings.DataAccess.GetListPlus(false, false, true);
	            Scales = AppSettings.DataAccess.GetListScales(false, false, true);

	            ButtonSettings = new(false, false, false, false, false, true, true);
            }
        });
    }

    #endregion
}
