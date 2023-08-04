// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewDiagModels.LogsMemory;

public sealed class WsSqlViewLogMemoryRepository : IViewLogMemoryRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    // Если оставить прокси как есть, то будет падать, т.к. для вьюшки нет маппингов!
    public List<WsSqlViewLogMemoryModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => 
        GetList(sqlCrudConfig.SelectTopRowsCount);

    public List<WsSqlViewLogMemoryModel> GetListAppName(string appName) =>
        GetList(0, appName);

    public List<WsSqlViewLogMemoryModel> GetListToday(int topRecords = 0, string appName = "") =>
        GetList(WsSqlQueriesDiags.Views.GetViewLogsMemoryToday(topRecords, appName));

    public List<WsSqlViewLogMemoryModel> GetListMonth(int topRecords = 0, string appName = "") => 
        GetList(WsSqlQueriesDiags.Views.GetViewLogsMemoryMonth(topRecords, appName, (short)DateTime.Now.Month));

    public List<WsSqlViewLogMemoryModel> GetList(int topRecords = 0, string appName = "", DateTime? dtCreate = null) => 
        GetList(WsSqlQueriesDiags.Views.GetViewLogsMemory(topRecords, appName, dtCreate));

    public List<WsSqlViewLogMemoryModel> GetListDeviceControl() =>
        GetList(0, "DeviceControl");
    
    public List<WsSqlViewLogMemoryModel> GetListDeviceControlToday() =>
        GetListToday(0, "DeviceControl");
    
    public List<WsSqlViewLogMemoryModel> GetListDeviceControlMonth() =>
        GetListMonth(0, "DeviceControl");
    
    public List<WsSqlViewLogMemoryModel> GetList(string query)
    {
        List<WsSqlViewLogMemoryModel> result = new();
        //
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