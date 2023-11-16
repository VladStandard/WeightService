using Ws.StorageCore.Enums;

namespace DeviceControl.Pages.Menu.Logs.MainLogs;

public sealed partial class Logs : SectionBase<SqlLogEntity>
{
    private LogTypeEnum? CurrentLogType { get; set; }
    private SqlScaleEntity? CurrentLine { get; set; }
    private List<SqlScaleEntity> Lines { get; set; }
    private SqlLogRepository LogRepository { get; } = new();
    private SqlLineRepository LineRepository { get; } = new();

    public Logs() : base()
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudActual();
        
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
