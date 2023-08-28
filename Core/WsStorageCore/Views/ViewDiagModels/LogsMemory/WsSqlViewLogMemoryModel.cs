namespace WsStorageCore.Views.ViewDiagModels.LogsMemory;

[DebuggerDisplay("{ToString()}")]
public sealed class WsSqlViewLogMemoryModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    public string AppName { get; init; }
    public string DeviceName { get; init; }
    public string ScaleName { get; init; }
    public short SizeAppMb { get; init; }
    public short SizeFreeMb { get; init; }
    public int TotalMemoryMb => SizeFreeMb + SizeAppMb;
    public double FillSizePercent => Math.Round(((double)SizeAppMb / TotalMemoryMb) * 100, 2);
    
    public WsSqlViewLogMemoryModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        AppName = string.Empty;
        DeviceName = string.Empty;
        ScaleName = string.Empty;
        SizeAppMb = 0;
        SizeFreeMb = 0;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        string.IsNullOrEmpty(ScaleName)
            ? $"{AppName} | {DeviceName} | {CreateDt:yyyy-MM-dd} | {SizeAppMb} | {SizeFreeMb}"
            : $"{AppName} | {DeviceName} | {ScaleName} | {CreateDt:yyyy-MM-dd} | {SizeAppMb} | {SizeFreeMb}";

    #endregion
}