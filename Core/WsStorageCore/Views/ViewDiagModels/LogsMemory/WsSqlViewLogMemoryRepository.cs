namespace WsStorageCore.Views.ViewDiagModels.LogsMemory;

public sealed class WsSqlViewLogMemoryRepository : IViewLogMemoryRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    // Если оставить прокси как есть, то будет падать, т.к. для вьюшки нет маппингов!
    public IList<WsSqlViewLogMemoryModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => 
        GetList(string.Empty, string.Empty, WsSqlEnumTimeInterval.All, sqlCrudConfig.SelectTopRowsCount);

    private IList<WsSqlViewLogMemoryModel> GetListByQuery(string query)
    {
        object[] objects = SqlCore.GetArrayObjects(query);
        IList<WsSqlViewLogMemoryModel> result = new List<WsSqlViewLogMemoryModel>(objects.Length);
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
                SizeFreeMb = Convert.ToInt16(item[i])
            });
        }
        return result;
    }
    
    public IList<WsSqlViewLogMemoryModel> GetList(string appName, string deviceName,  WsSqlEnumTimeInterval timeInterval, int records = 0) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsMemory(appName, deviceName, timeInterval, records));
    
    public IList<WsSqlViewLogMemoryModel> GetList(int records) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsMemory(
            string.Empty, string.Empty, WsSqlEnumTimeInterval.All, records));
}