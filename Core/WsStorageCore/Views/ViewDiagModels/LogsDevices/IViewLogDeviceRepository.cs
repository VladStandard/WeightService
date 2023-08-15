// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.Views.ViewDiagModels.LogsDevices;

public interface IViewLogDeviceRepository : IWsSqlViewBaseRepository<WsSqlViewLogDeviceModel>
{
    public List<WsSqlViewLogDeviceModel> GetList(string appName, string deviceName, WsSqlEnumTimeInterval timeInterval, int records = 0);
    public List<WsSqlViewLogDeviceModel> GetList(int records);
}