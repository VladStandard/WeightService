// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsStorageCore.ViewDiagModels;

namespace DeviceControl.Pages.Menu.Admins;

public sealed partial class LogsMemory : SectionBase<WsSqlViewLogMemoryModel>
{
    #region Public and private methods

    public LogsMemory() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ContextViewHelper.GetListViewLogsMemory(SqlCrudConfigSection);
    }

    #endregion
}