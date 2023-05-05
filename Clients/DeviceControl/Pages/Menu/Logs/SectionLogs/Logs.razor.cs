// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableDiagModels.LogsTypes;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.ViewScaleModels;

namespace BlazorDeviceControl.Pages.Menu.Logs.SectionLogs;

public sealed partial class Logs : RazorComponentSectionBase<LogView>
{
    private string? CurrentLogType { get; set; }
    private string? CurrentLine { get; set; }
    private List<WsSqlLogTypeModel> LogTypes { get; set; }
    private List<WsSqlScaleModel> Lines { get; set; }


    public Logs() : base()
    {
        LogTypes = ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlLogTypeModel>(new SqlCrudConfigModel());
        Lines = ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlScaleModel>(new SqlCrudConfigModel());
        Lines = (from item in Lines orderby item.Description select item).ToList();
        SqlCrudConfigSection.IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings = new(false, false,  false, false, false, false, false);
    }

    protected override void SetSqlSectionCast()
    {
        var query = WsSqlQueriesDiags.Tables.Views.GetLogs(SqlCrudConfigSection.IsResultShowOnlyTop
            ? ContextManager.JsonSettings.Local.SelectTopRowsCount
            : 0, CurrentLogType, CurrentLine);
        object[] objects = ContextManager.AccessManager.AccessList.GetArrayObjectsNotNullable(query);
        List<LogView> items = new();
        foreach (var obj in objects)
        {
            if (obj is not object[] { Length: 11 } item)
                continue;

            if (Guid.TryParse(Convert.ToString(item[0]), out var uid))
            {
                items.Add(new LogView
                {
                    IdentityValueUid = uid,
                    CreateDt = Convert.ToDateTime(item[1]),
                    Line = item[2] as string ?? string.Empty,
                    Host = item[3] as string ?? string.Empty,
                    App = item[4] as string ?? string.Empty,
                    Version = item[5] as string ?? string.Empty,
                    File = item[6] as string ?? string.Empty,
                    CodeLine = Convert.ToInt32(item[7]),
                    Member = item[8] as string ?? string.Empty,
                    LogType = item[9] as string ?? string.Empty,
                    Message = item[10] as string ?? string.Empty
                });
            }
        }
        SqlSectionCast = items;
    }

    private void OnSelectTypeChanged()
    {
        SqlCrudConfigSection.IsResultShowOnlyTop = true;
        GetSectionData();
    }
}
