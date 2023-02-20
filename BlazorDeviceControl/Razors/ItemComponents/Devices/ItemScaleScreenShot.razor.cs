// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.ScalesScreenshots;

namespace BlazorDeviceControl.Razors.ItemComponents.Devices;

public partial class ItemScaleScreenShot : RazorComponentItemBase<ScaleScreenShotModel>
{
    #region Public and private fields, properties, constructor

    private string ImagePath { get; set; }

    public ItemScaleScreenShot() : base()
    {
	    ImagePath = string.Empty;
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
                SqlItemCast = DataContext.GetItemNotNullable<ScaleScreenShotModel>(IdentityUid);
                if (SqlItemCast.ScreenShot.Length > 1)
					ImagePath = "data:image/png;base64, " + Convert.ToBase64String(SqlItemCast.ScreenShot);
				if (SqlItemCast.IsNew)
                {
                    SqlItemCast = SqlItemNew<ScaleScreenShotModel>();
                }
            }
        });
    }

    #endregion
}
