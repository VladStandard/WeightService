namespace DeviceControl.Pages.Menu.Logs;

public sealed partial class LogsMemory : SectionBase<WsSqlViewLogMemoryModel>
{
    #region Public and private methods

    private WsSqlViewLogMemoryRepository LogMemoryRepository { get; } = new();
    
    public LogsMemory() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = LogMemoryRepository.GetList(SqlCrudConfigSection.SelectTopRowsCount).ToList();
    }

    #endregion
}
