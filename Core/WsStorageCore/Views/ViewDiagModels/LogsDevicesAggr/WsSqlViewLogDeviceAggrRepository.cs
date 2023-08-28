namespace WsStorageCore.Views.ViewDiagModels.LogsDevicesAggr;

public sealed class WsSqlViewLogDeviceAggrRepository : IViewLogDeviceAggrRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    // Если оставить прокси как есть, то будет падать, т.к. для вьюшки нет маппингов!
    public IList<WsSqlViewLogDeviceAggrModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) =>
        GetList(string.Empty, string.Empty, WsSqlEnumTimeInterval.All, sqlCrudConfig.SelectTopRowsCount);

    private IList<WsSqlViewLogDeviceAggrModel> GetListByQuery(string query)
    {
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        List<WsSqlViewLogDeviceAggrModel> result = new(objects.Length);
        foreach (object obj in objects)
        {
            int i = 1;
            if (obj is not object[] item || item.Length < 6) break;
            result.Add(new()
            {
                //CreateDt = Convert.ToDateTime(item[i++]),
                DeviceName = Convert.ToString(item[i++]),
                LineName = Convert.ToString(item[i++]),
                AppName = Convert.ToString(item[i++]),
                LogType = Convert.ToString(item[i++]),
                Count = Convert.ToUInt32(item[i]),
            });
        }
        return result;
    }

    public IList<WsSqlViewLogDeviceAggrModel> GetList(string appName, string deviceName, WsSqlEnumTimeInterval timeInterval, int maxRecords = 0) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsDevicesAggr(appName, deviceName, timeInterval, maxRecords));

    public IList<WsSqlViewLogDeviceAggrModel> GetListForApp(string appName, WsSqlEnumTimeInterval timeInterval, int maxRecords = 0) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsDevicesAggr(appName, string.Empty, timeInterval, maxRecords));

    public IList<WsSqlViewLogDeviceAggrModel> GetListForDevice(string deviceName, WsSqlEnumTimeInterval timeInterval, int maxRecords = 0) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsDevicesAggr(string.Empty, deviceName, timeInterval, maxRecords));

    public IList<WsSqlViewLogDeviceAggrModel> GetList(int maxRecords) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsDevicesAggr(
            string.Empty, string.Empty, WsSqlEnumTimeInterval.All, maxRecords));
}