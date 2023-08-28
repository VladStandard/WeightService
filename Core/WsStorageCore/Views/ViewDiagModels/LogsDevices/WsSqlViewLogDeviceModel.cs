namespace WsStorageCore.Views.ViewDiagModels.LogsDevices;

[DebuggerDisplay("{ToString()}")]
public sealed class WsSqlViewLogDeviceModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    public string LineName { get; init; }
    public string DeviceName { get; init; }
    public string AppName { get; init; }
    public string Version { get; init; }
    public string FileName { get; init; }
    public ushort CodeLine { get; init; }
    public string Member { get; init; }
    public string LogType { get; init; }
    public string Message { get; init; }

    public WsSqlViewLogDeviceModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        LineName = string.Empty;
        DeviceName = string.Empty;
        AppName = string.Empty;
        Version = string.Empty;
        FileName = string.Empty;
        CodeLine = 0;
        Member = string.Empty;
        LogType = string.Empty;
        Message = string.Empty;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{LineName} | {AppName} | {DeviceName} | " +
        $"{CreateDt:yyyy-MM-dd} | {Version} | {CodeLine} | {Member} | {LogType} | {Message} | {FileName}";

    #endregion
}