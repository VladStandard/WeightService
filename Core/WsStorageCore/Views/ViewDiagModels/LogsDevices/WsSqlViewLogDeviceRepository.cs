// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewDiagModels.LogsDevices;

public sealed class WsSqlViewLogDeviceRepository : IViewLogDeviceRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    // Если оставить прокси как есть, то будет падать, т.к. для вьюшки нет маппингов!
    public List<WsSqlViewLogDeviceModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) =>
        GetList(string.Empty, string.Empty, WsSqlEnumTimeInterval.All, sqlCrudConfig.SelectTopRowsCount);

    private List<WsSqlViewLogDeviceModel> GetListByQuery(string query)
    {
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        List<WsSqlViewLogDeviceModel> result = new(objects.Length);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 11) break;
            result.Add(new()
            {
                IdentityValueUid = Guid.Parse(Convert.ToString(item[i++])),
                CreateDt = Convert.ToDateTime(item[i++]),
                LineName = Convert.ToString(item[i++]),
                DeviceName = Convert.ToString(item[i++]),
                AppName = Convert.ToString(item[i++]),
                Version = Convert.ToString(item[i++]),
                FileName = Convert.ToString(item[i++]),
                CodeLine = Convert.ToUInt16(item[i++]),
                Member = Convert.ToString(item[i++]),
                LogType = Convert.ToString(item[i++]),
                Message = Convert.ToString(item[i]),
            });
        }
        return result;
    }

    public List<WsSqlViewLogDeviceModel> GetList(string appName, string deviceName, WsSqlEnumTimeInterval timeInterval, int records = 0) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsDevices(appName, deviceName, timeInterval, records));

    public List<WsSqlViewLogDeviceModel> GetList(int records) =>
        GetListByQuery(WsSqlQueriesDiags.Views.GetViewLogsDevices(
            string.Empty, string.Empty, WsSqlEnumTimeInterval.All, records));
}