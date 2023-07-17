namespace WsStorageCore.Views.ViewDiagModels.LogsMemory;

public class WsSqlViewLogMemoryRepository : IViewLogMemoryRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewLogMemoryRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewLogMemoryRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewLogMemoryModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewLogMemoryModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewLogsMemory(sqlCrudConfig.SelectTopRowsCount);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 7) break;
            result.Add(new()
            {
                IdentityValueUid = Guid.Parse(Convert.ToString(item[i++])),
                CreateDt = Convert.ToDateTime(item[i++]),
                AppName = Convert.ToString(item[i++]),
                DeviceName = Convert.ToString(item[i++]),
                ScaleName = Convert.ToString(item[i++]),
                SizeAppMb = Convert.ToInt16(item[i++]),
                SizeFreeMb = Convert.ToInt16(item[i++])
            });
        }
        return result;
    }
}