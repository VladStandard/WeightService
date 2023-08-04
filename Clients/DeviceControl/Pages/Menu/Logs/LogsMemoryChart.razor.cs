// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Views.ViewDiagModels.LogsMemory;

namespace DeviceControl.Pages.Menu.Logs;

public sealed partial class LogsMemoryChart : SectionBase<WsSqlViewLogMemoryModel>
{
    #region Public and private methods

    private WsSqlViewLogMemoryRepository LogMemoryRepository { get; } = new();
    
    public LogsMemoryChart() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = LogMemoryRepository.GetList(SqlCrudConfigSection);
    }

    #endregion
}