// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewDiagModels.LogsDevicesAggr;

public sealed class WsSqlViewLogDeviceAggrRepository : IViewLogDeviceAggrRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    // Если оставить прокси как есть, то будет падать, т.к. для вьюшки нет маппингов!
    public List<WsSqlViewLogDeviceAggrModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) =>
        GetList(string.Empty, string.Empty, WsSqlEnumTimeInterval.All, sqlCrudConfig.SelectTopRowsCount);

    private List<WsSqlViewLogDeviceAggrModel> GetListByQuery(string query)
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

    public List<WsSqlViewLogDeviceAggrModel> GetList(string appName, string deviceName, WsSqlEnumTimeInterval timeInterval, int records = 0) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsDevicesAggr(appName, deviceName, timeInterval, records));

    public List<WsSqlViewLogDeviceAggrModel> GetListForApp(string appName, WsSqlEnumTimeInterval timeInterval, int records = 0) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsDevicesAggr(appName, string.Empty, timeInterval, records));

    public List<WsSqlViewLogDeviceAggrModel> GetListForDevice(string deviceName, WsSqlEnumTimeInterval timeInterval, int records = 0) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsDevicesAggr(string.Empty, deviceName, timeInterval, records));

    public List<WsSqlViewLogDeviceAggrModel> GetList(int records) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsDevicesAggr(
            string.Empty, string.Empty, WsSqlEnumTimeInterval.All, records));
}