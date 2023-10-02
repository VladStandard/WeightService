namespace DeviceControl.Pages.Menu.Logs.MainLogs;

public sealed partial class Logs : SectionBase<WsSqlViewLogModel>
{
    private string? CurrentLogType { get; set; }
    private string? CurrentLine { get; set; }
    private List<WsSqlLogTypeModel> LogTypes { get; set; }
    private List<WsSqlScaleModel> Lines { get; set; }

    private WsSqlViewLogRepository ViewLogRepository { get; } = new();
    private WsSqlLogTypeRepository LogTypeRepository  { get; } = new();
    private WsSqlLineRepository LineRepository { get; } = new();

    public Logs() : base()
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudActual();
        
        LogTypes = LogTypeRepository.GetEnumerable(sqlCrudConfig).ToList();
        Lines = LineRepository.GetEnumerable(sqlCrudConfig).ToList();
        
        IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultOrder = true;

        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ViewLogRepository.GetListByLogTypeAndLineName(SqlCrudConfigSection, CurrentLogType, CurrentLine).ToList();
    }
}
