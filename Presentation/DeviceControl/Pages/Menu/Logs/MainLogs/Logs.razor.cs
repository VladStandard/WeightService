using WsStorageCore.Enums;
namespace DeviceControl.Pages.Menu.Logs.MainLogs;

public sealed partial class Logs : SectionBase<WsSqlLogEntity>
{
    private LogTypeEnum? CurrentLogType { get; set; }
    private WsSqlScaleEntity? CurrentLine { get; set; }
    private List<WsSqlScaleEntity> Lines { get; set; }
    private WsSqlLogRepository LogRepository { get; } = new();
    private WsSqlLineRepository LineRepository { get; } = new();

    public Logs() : base()
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudActual();
        
        Lines = LineRepository.GetEnumerable(sqlCrudConfig).ToList();
        
        IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultOrder = true;

        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = LogRepository.GetListByLogTypeAndLineName(SqlCrudConfigSection, CurrentLogType, CurrentLine).ToList();
    }
}
