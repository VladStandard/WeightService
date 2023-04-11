// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Models;

public enum DbFileType
{
    MasterDataFile,
    LogDataFile
}

public sealed record SqlDbFileSizeInfoModel
{
    #region Public and private fields, properties, constructor

    public DbFileType Type { get; init; }
    public string FileName { get; init; }
    public ushort SizeMb { get; init; }
    public ushort MaxSizeMb { get; init; }
    public string DisplayName => $"{SizeMb} из {MaxSizeMb} MB";
    public double DbFillSize => ((double)SizeMb / MaxSizeMb) * 100;
    
    public SqlDbFileSizeInfoModel(byte type, string fileName, ushort sizeMb, ushort maxSizeMb)
    {
        Type = type == 0 ? DbFileType.MasterDataFile : DbFileType.LogDataFile;
        FileName = fileName;
        SizeMb = sizeMb;
        MaxSizeMb = maxSizeMb;
    }

    #endregion
}