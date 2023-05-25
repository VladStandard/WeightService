// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Utils;
using WsStorageCore.TableDiagModels.ScalesScreenshots;

namespace DeviceControl.Pages.Menu.Logs.SectionScalesScreenShots;

public sealed partial class ScalesScreenShots : RazorComponentSectionBase<WsSqlScaleScreenShotModel>
{
    #region Public and private fields, properties, constructor

    public ScalesScreenShots() : base()
    {
        ButtonSettings = new(true, true, true, true, true, true, false);
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection.AddFilters(nameof(WsSqlScaleScreenShotModel.Scale), SqlItem);
        base.SetSqlSectionCast();
    }

    private string GetByteLength(WsSqlScaleScreenShotModel screenshot) =>
        DataUtils.GetBytesLength(screenshot.ScreenShot, false);

    #endregion
}
