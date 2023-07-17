namespace WsStorageCore.Views.ViewScaleModels.Logs;

public class WsSqlViewLogRepository : IViewLogRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewLogRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewLogRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewLogModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewLogModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetLogs(sqlCrudConfig.SelectTopRowsCount, null, null);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 11 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
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
                Message = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
    
    public List<WsSqlViewLogModel> GetListByLogTypeAndLineName(WsSqlCrudConfigModel sqlCrudConfig, string? logType, string? line)
    {
        List<WsSqlViewLogModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetLogs(sqlCrudConfig.SelectTopRowsCount, logType, line);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 11 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
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
                Message = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }


}