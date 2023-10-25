// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.Views.ViewDiagModels.LogsDevicesAggr;

public interface IViewLogDeviceAggrRepository : IWsSqlViewBaseRepository<WsSqlViewLogDeviceAggrModel>
{
    public IList<WsSqlViewLogDeviceAggrModel> GetList(string appName, string deviceName, WsSqlEnumTimeInterval timeInterval, int maxRecords = 0);
    public IList<WsSqlViewLogDeviceAggrModel> GetListForApp(string appName, WsSqlEnumTimeInterval timeInterval, int maxRecords = 0);
    public IList<WsSqlViewLogDeviceAggrModel> GetListForDevice(string deviceName, WsSqlEnumTimeInterval timeInterval, int maxRecords = 0);
    public IList<WsSqlViewLogDeviceAggrModel> GetList(int maxRecords = 0);
}