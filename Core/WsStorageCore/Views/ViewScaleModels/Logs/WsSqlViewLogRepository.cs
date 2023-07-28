// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewScaleModels.Logs;

public class WsSqlViewLogRepository : IViewLogRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    private List<WsSqlViewLogModel> ReadQuery(string query)
    {
        List<WsSqlViewLogModel> result = new();
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 11 || !Guid.TryParse(Convert.ToString(item[i++]), out Guid uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[i++]),
                Line = item[i++] as string ?? string.Empty,
                Host = item[i++] as string ?? string.Empty,
                App = item[i++] as string ?? string.Empty,
                Version = item[i++] as string ?? string.Empty,
                File = item[i++] as string ?? string.Empty,
                CodeLine = Convert.ToInt32(item[i++]),
                Member = item[i++] as string ?? string.Empty,
                LogType = item[i++] as string ?? string.Empty,
                Message = item[i] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewLogModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        return ReadQuery(WsSqlQueriesDiags.Views.GetLogs(sqlCrudConfig.SelectTopRowsCount, null, null));
    }
    
    public List<WsSqlViewLogModel> GetListByLogTypeAndLineName(WsSqlCrudConfigModel sqlCrudConfig, string? logType, string? line)
    {
        return ReadQuery(WsSqlQueriesDiags.Views.GetLogs(sqlCrudConfig.SelectTopRowsCount, logType, line));
    }
}