// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewDiagModels.LogsDevicesAggr;

[DebuggerDisplay("{ToString()}")]
public sealed class WsSqlViewLogDeviceAggrShortModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    public string DeviceName { get; init; }
    public string LineName { get; init; }
    public string AppName { get; init; }
    public string LogType { get; init; }
    public uint Count { get; init; }

    public WsSqlViewLogDeviceAggrShortModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        DeviceName = string.Empty;
        LineName = string.Empty;
        AppName = string.Empty;
        LogType = string.Empty;
        Count = 0;
    }

    public WsSqlViewLogDeviceAggrShortModel(WsSqlViewLogDeviceAggrShortModel item) : base(WsSqlEnumFieldIdentity.Uid)
    {
        DeviceName = item.DeviceName;
        LineName = item.LineName;
        AppName = item.AppName;
        LogType = item.LogType;
        Count = item.Count;
    }

    public WsSqlViewLogDeviceAggrShortModel(WsSqlViewLogDeviceAggrModel item) : base(WsSqlEnumFieldIdentity.Uid)
    {
        DeviceName = item.DeviceName;
        LineName = item.LineName;
        AppName = item.AppName;
        LogType = item.LogType;
        Count = item.Count;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{LineName} | {DeviceName} | {AppName} | {LogType} | {Count}";

    #endregion
}