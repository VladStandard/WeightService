// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        SqlSectionCast = new WsSqlScaleScreenshotRepository().GetList(SqlCrudConfigSection);
    }

    private static string GetByteLength(WsSqlScaleScreenShotModel screenshot) => 
        WsDataUtils.GetBytesLength(screenshot.ScreenShot, false);

    #endregion
}
