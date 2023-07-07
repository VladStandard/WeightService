// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsDataCore.Utils;
using WsStorageCore.TableDiagModels.ScalesScreenshots;

namespace DeviceControl.Pages.Menu.Logs.ScalesScreenshots;

public sealed partial class ScalesScreenshots : SectionBase<WsSqlScaleScreenShotModel>
{
    #region Public and private fields, properties, constructor

    public ScalesScreenshots() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection.AddFilters(nameof(WsSqlScaleScreenShotModel.Scale), SqlItem);
        base.SetSqlSectionCast();
    }

    private static string GetByteLength(WsSqlScaleScreenShotModel screenshot) => 
        WsDataUtils.GetBytesLength(screenshot.ScreenShot, false);

    #endregion
}