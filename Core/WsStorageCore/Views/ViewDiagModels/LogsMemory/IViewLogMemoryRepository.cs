// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.Views.ViewDiagModels.LogsMemory;

public interface IViewLogMemoryRepository : IViewBaseRepository<WsSqlViewLogMemoryModel>
{
    public List<WsSqlViewLogMemoryModel> GetList(int topRecords = 0, string appName = "", DateTime? dtCreate = null);
    public List<WsSqlViewLogMemoryModel> GetList(string query);
    public List<WsSqlViewLogMemoryModel> GetListAppName(string appName);
    public List<WsSqlViewLogMemoryModel> GetListDeviceControl();
    public List<WsSqlViewLogMemoryModel> GetListDeviceControlToday();
    public List<WsSqlViewLogMemoryModel> GetListDeviceControlMonth();
    public List<WsSqlViewLogMemoryModel> GetListMonth(int topRecords = 0, string appName = "");
    public List<WsSqlViewLogMemoryModel> GetListToday(int topRecords = 0, string appName = "");
}