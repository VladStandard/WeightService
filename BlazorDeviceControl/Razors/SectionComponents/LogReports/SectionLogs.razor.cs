// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.Xml;

namespace BlazorDeviceControl.Razors.SectionComponents.LogReports;

public partial class SectionLogs : RazorComponentSectionBase<LogQuickModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionLogs() : base()
    {
        ButtonSettings = new(false, true,  false, false, false, false, false);
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                Guid logTypeUid = Guid.Empty;
                if (SqlItem is LogTypeModel logType)
                    logTypeUid = logType.IdentityValueUid;

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
        });
    }

    #endregion
}