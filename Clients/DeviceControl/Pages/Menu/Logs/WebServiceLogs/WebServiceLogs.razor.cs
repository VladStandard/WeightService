using WsStorageCore.Tables.TableDiagModels.LogsWebs;

namespace DeviceControl.Pages.Menu.Logs.WebServiceLogs;

public sealed partial class WebServiceLogs : SectionBase<WsSqlLogWebModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlViewWebLogRepository ViewWebLogRepository { get; } = new();

    public WebServiceLogs() : base()
    {
        IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
        
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        WsSqlCrudConfigModel crud = WsSqlCrudConfigFactory.GetCrudAll();
        SqlSectionCast = new WsSqlLogWebRepository().GetList(crud).ToList();
    }

    #endregion
}
