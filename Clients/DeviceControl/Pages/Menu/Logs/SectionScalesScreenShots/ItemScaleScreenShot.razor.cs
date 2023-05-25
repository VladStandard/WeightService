// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsStorageCore.TableDiagModels.ScalesScreenshots;

namespace DeviceControl.Pages.Menu.Logs.SectionScalesScreenShots;

public sealed partial class ItemScaleScreenShot : RazorComponentItemBase<WsSqlScaleScreenShotModel>
{
    #region Public and private fields, properties, constructor

    private string ImagePath { get; set; }

    public ItemScaleScreenShot() : base()
    {
        ImagePath = string.Empty;
        ButtonSettings = new ButtonSettingsModel(false, false, false, false, false, false, true);
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast =
            ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlScaleScreenShotModel>(IdentityUid);
        if (SqlItemCast.ScreenShot.Length > 1)
            ImagePath = "data:image/png;base64, " + Convert.ToBase64String(SqlItemCast.ScreenShot);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNew<WsSqlScaleScreenShotModel>();
    }

    #endregion
}
