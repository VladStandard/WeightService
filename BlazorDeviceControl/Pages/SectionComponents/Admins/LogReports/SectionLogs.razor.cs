// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.Xml;

namespace BlazorDeviceControl.Pages.SectionComponents.Admins.LogReports;

public sealed partial class SectionLogs : RazorComponentSectionBase<LogQuickModel>
{
    private static LogTypeModel LogTypeNew => new();
    private LogTypeModel CurrentType { get; set; }
    private List<LogTypeModel> LogTypes { get; set; }
    public SectionLogs() : base()
    {
        CurrentType = new();
        LogTypes = DataAccess.GetListNotNullable<LogTypeModel>(new SqlCrudConfigModel());
        SqlCrudConfigSection.IsGuiShowFilterMarked = false;
        SqlCrudConfigSection.IsResultShowMarked = true;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings = new(false, false,  false, false, false, false, false);
    }
    protected override void SetSqlSectionCast()
    {
        Guid logTypeUid = CurrentType.IdentityValueUid;
        string query = SqlQueries.DbServiceManaging.Tables.Logs.GetLogs(SqlCrudConfigSection.IsResultShowOnlyTop
            ? DataAccess.JsonSettings.Local.SelectTopRowsCount : 0, SqlCrudConfigSection.IsResultShowMarked, logTypeUid);
        object[] objects = DataAccess.GetArrayObjectsNotNullable(query);
        List<LogQuickModel> items = new();
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 12 } item)
                continue;

            if (Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
            {
                items.Add(new()
                {
                    IdentityValueUid = uid,
                    CreateDt = Convert.ToDateTime(item[1]),
                    Scale = item[2] as string ?? string.Empty,
                    Host = item[4] as string ?? string.Empty,
                    App = item[5] as string ?? string.Empty,
                    Version = item[6] as string ?? string.Empty,
                    File = item[7] as string ?? string.Empty,
                    Line = Convert.ToInt32(item[8]),
                    Member = item[9] as string ?? string.Empty,
                    Icon = item[10] as string ?? string.Empty,
                    Message = item[11] as string ?? string.Empty,
                });
            }
        }
        SqlSectionCast = items;
    }

    private void OnSelectTypeChanged(LogTypeModel logType)
    {
        CurrentType = logType;
        SqlCrudConfigSection.IsResultShowOnlyTop = true;
        GetSectionData();
    }
}