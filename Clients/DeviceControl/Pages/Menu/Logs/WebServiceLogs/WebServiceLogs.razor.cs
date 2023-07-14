using WsBlazorCore.Settings;
using WsStorageCore.ViewScaleModels;
using WsStorageCore.ViewScaleModels.WebLogs;

namespace DeviceControl.Pages.Menu.Logs.WebServiceLogs;

public sealed partial class WebServiceLogs : SectionBase<WsSqlViewWebLogModel>
{
    #region Public and private fields, properties, constructor
    
    private WsSqlViewWebLogRepository ViewWebLogRepository = WsSqlViewWebLogRepository.Instance;
    public WebServiceLogs() : base()
    {
        IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ViewWebLogRepository.GetList(SqlCrudConfigSection);
    }

    #endregion
}