// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsStorageCore.TableDiagModels.LogsTypes;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.ViewScaleModels;
using WsStorageCore.ViewScaleModels.Logs;

namespace DeviceControl.Pages.Menu.Logs.MainLogs;

public sealed partial class Logs : SectionBase<WsSqlViewLogModel>
{
    private string? CurrentLogType { get; set; }
    private string? CurrentLine { get; set; }
    private List<WsSqlLogTypeModel> LogTypes { get; set; }
    private List<WsSqlScaleModel> Lines { get; set; }
    
    private WsSqlViewLogRepository ViewLogRepository = WsSqlViewLogRepository.Instance;
    
    public Logs() : base()
    {
        LogTypes = ContextManager.SqlCore.GetListNotNullable<WsSqlLogTypeModel>(
            new WsSqlCrudConfigModel()
        );
        Lines = ContextManager.SqlCore.GetListNotNullable<WsSqlScaleModel>(new WsSqlCrudConfigModel());
        Lines = (from item in Lines orderby item.Description select item).ToList();
        
        IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultOrder = true;
        
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ViewLogRepository.GetList(SqlCrudConfigSection, CurrentLogType, CurrentLine);
    }
}