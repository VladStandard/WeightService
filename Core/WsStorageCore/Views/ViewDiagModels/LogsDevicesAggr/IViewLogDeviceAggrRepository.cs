// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.Views.ViewDiagModels.LogsDevicesAggr;

public interface IViewLogDeviceAggrRepository : IViewBaseRepository<WsSqlViewLogDeviceAggrModel>
{
    public List<WsSqlViewLogDeviceAggrModel> GetList(string appName, string deviceName, WsSqlEnumTimeInterval timeInterval, int maxRecords = 0);
    public List<WsSqlViewLogDeviceAggrModel> GetListForApp(string appName, WsSqlEnumTimeInterval timeInterval, int maxRecords = 0);
    public List<WsSqlViewLogDeviceAggrModel> GetListForDevice(string deviceName, WsSqlEnumTimeInterval timeInterval, int maxRecords = 0);
    public List<WsSqlViewLogDeviceAggrModel> GetList(int maxRecords);
}