namespace WsStorageCore.Views.ViewScaleModels.WebLogs;

public class WsSqlViewWebLogRepository : IViewWebLogRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewWebLogRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewWebLogRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewWebLogModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewWebLogModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetWebLogs(sqlCrudConfig.SelectTopRowsCount);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[i++]),
                RequestUrl = item[i++] as string ?? string.Empty,
                RequestCountAll = Convert.ToInt32(item[i++]),
                ResponseCountSuccess = Convert.ToInt32(item[i++]),
                ResponseCountError = Convert.ToInt32(item[i++]),
                AppVersion = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
    
}