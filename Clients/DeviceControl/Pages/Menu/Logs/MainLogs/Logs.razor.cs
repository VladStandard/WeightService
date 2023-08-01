// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        sqlCrudConfig.IsFillReferences = false;
        
        LogTypes = LogTypeRepository.GetList(sqlCrudConfig);
        Lines = LineRepository.GetList(sqlCrudConfig);
        
        IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultOrder = true;
        
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ViewLogRepository.GetListByLogTypeAndLineName(SqlCrudConfigSection, CurrentLogType, CurrentLine);
    }
}