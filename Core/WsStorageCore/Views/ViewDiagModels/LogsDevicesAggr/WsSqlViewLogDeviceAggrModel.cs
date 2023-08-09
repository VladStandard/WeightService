// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewDiagModels.LogsDevicesAggr;

[DebuggerDisplay("{ToString()}")]
public sealed class WsSqlViewLogDeviceAggrModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    public string DeviceName { get; init; }
    public string LineName { get; init; }
    public string AppName { get; init; }
    public string Version { get; init; }
    public string LogType { get; init; }
    public uint Count { get; init; }

    public WsSqlViewLogDeviceAggrModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        CreateDt = DateTime.MinValue;
        DeviceName = string.Empty;
        LineName = string.Empty;
        AppName = string.Empty;
        Version = string.Empty;
        LogType = string.Empty;
        Count = 0;
    }

    public WsSqlViewLogDeviceAggrModel(WsSqlViewLogDeviceAggrModel item) : this(item, item.CreateDt)
    {
        //
    }

    public WsSqlViewLogDeviceAggrModel(WsSqlViewLogDeviceAggrModel item, DateTime createDt) : base(WsSqlEnumFieldIdentity.Uid)
    {
        CreateDt = createDt;
        DeviceName = item.DeviceName;
        LineName = item.LineName;
        AppName = item.AppName;
        Version = item.Version;
        LogType = item.LogType;
        Count = item.Count;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => 
        $"{CreateDt:yyyy-MM-dd} | {LineName} | {DeviceName} | {AppName} | {Version} | {LogType} | {Count}";

    #endregion
}