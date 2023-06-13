using DeviceControl.Components.Section;
using WsBlazorCore.Settings;
using WsStorageCore.ViewScaleModels;

namespace DeviceControl.Pages.Menu.Logs.WebServiceLogs;

public sealed partial class WebServiceLogs : SectionBase<WsSqlViewWebLogModel>
{
    #region Public and private fields, properties, constructor

    public WebServiceLogs() : base()
    {
        SqlCrudConfigSection.IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ContextViewHelper.GetListViewWebLogs(SqlCrudConfigSection);
    }

    #endregion
}