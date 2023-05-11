// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewDiagModels;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlViewLogMemoryModel
{
    #region Public and private fields, properties, constructor

    public DateTime CreateDt { get; init; }
    public string AppName { get; init; }
    public string DeviceName { get; init; }
    public string ScaleName { get; init; }
    public short SizeAppMb { get; init; }
    public short SizeFreeMb { get; init; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public WsSqlViewLogMemoryModel() : this(DateTime.MinValue)
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="createDt"></param>
    /// <param name="appName"></param>
    /// <param name="deviceName"></param>
    /// <param name="scaleName"></param>
    /// <param name="sizeAppMb"></param>
    /// <param name="sizeFreeMb"></param>
    public WsSqlViewLogMemoryModel(DateTime createDt, string appName = "", string deviceName = "", string scaleName = "",
        short sizeAppMb = 0, short sizeFreeMb = 0)
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