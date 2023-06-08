// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsStorageCore.ViewDiagModels;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlViewLogMemoryModel : WsSqlViewRecordBase
{
    #region Public and private fields, properties, constructor

    public DateTime CreateDt { get; init; }
    public string AppName { get; init; }
    public string DeviceName { get; init; }
    public string ScaleName { get; init; }
    public short SizeAppMb { get; init; }
    public short SizeFreeMb { get; init; }

    public WsSqlViewLogMemoryModel() : this(Guid.Empty, DateTime.MinValue, string.Empty, 
        string.Empty, string.Empty) { }
    
    public WsSqlViewLogMemoryModel(Guid uid, DateTime createDt, string appName = "", string deviceName = "", string scaleName = "",
        short sizeAppMb = 0, short sizeFreeMb = 0) : base(uid)
    {
        CreateDt = createDt;
        AppName = appName;
        DeviceName = deviceName;
        ScaleName = scaleName;
        SizeAppMb = sizeAppMb;
        SizeFreeMb = sizeFreeMb;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        string.IsNullOrEmpty(ScaleName)
            ? $"{AppName} | {DeviceName} | " +
              $"{CreateDt:yyyy-MM-dd} | {SizeAppMb} | {SizeFreeMb}"
            : $"{AppName} | {DeviceName} | {ScaleName} | " +
              $"{CreateDt:yyyy-MM-dd} | {SizeAppMb} | {SizeFreeMb}";

    #endregion
}