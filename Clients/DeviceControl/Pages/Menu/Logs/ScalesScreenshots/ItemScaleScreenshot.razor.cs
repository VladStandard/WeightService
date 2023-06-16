// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Item;
using WsBlazorCore.Settings;
using WsStorageCore.TableDiagModels.ScalesScreenshots;

namespace DeviceControl.Pages.Menu.Logs.ScalesScreenshots;

public sealed partial class ItemScaleScreenshot : ItemBase<WsSqlScaleScreenShotModel>
{
    #region Public and private fields, properties, constructor

    private string ImagePath { get; set; }

    public ItemScaleScreenshot() : base()
    {
        ImagePath = string.Empty;
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast =
            ContextManager.SqlCore.GetItemNotNullable<WsSqlScaleScreenShotModel>(Uid);
        if (SqlItemCast.ScreenShot.Length > 1)
            ImagePath = "data:image/png;base64, " + Convert.ToBase64String(SqlItemCast.ScreenShot);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNewEmpty<WsSqlScaleScreenShotModel>();
    }

    #endregion
}