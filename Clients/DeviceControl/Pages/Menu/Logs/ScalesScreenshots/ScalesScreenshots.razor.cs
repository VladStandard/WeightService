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
