using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.Xml;

namespace BlazorDeviceControl.Pages.SectionComponents.LogReports;

public partial class SectionLogTable : RazorComponentSectionBase<LogQuickModel>
{
    [Parameter] public LogTypeModel CurrentType { get; set; }
    public SectionLogTable() : base()
    {
        CurrentType = new();
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
            if (obj is object[] { Length: 12 } item)
            {
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
        }
        SqlSectionCast = items;
    }
    
}