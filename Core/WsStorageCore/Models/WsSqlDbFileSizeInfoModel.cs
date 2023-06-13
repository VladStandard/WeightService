// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

public enum DbFileType
{
    MasterDataFile,
    LogDataFile
}

public sealed record WsSqlDbFileSizeInfoModel
{
    #region Public and private fields, properties, constructor

    public DbFileType Type { get; init; }
    public string FileName { get; init; }
    public ushort SizeMb { get; init; }
    public ushort MaxSizeMb { get; init; }
    public string DisplayName => $"{SizeMb} из {MaxSizeMb} MB";
    public double DbFillSize => Math.Round(((double)SizeMb / MaxSizeMb) * 100, 2);
    public List<WsSqlViewTableSizeModel> Tables { get; set; }
    
    public WsSqlDbFileSizeInfoModel(byte type, string fileName, ushort sizeMb, ushort maxSizeMb)
    {
        Type = type == 0 ? DbFileType.MasterDataFile : DbFileType.LogDataFile;
        FileName = fileName;
        SizeMb = sizeMb;
        MaxSizeMb = maxSizeMb;
        Tables = new();
    }

    #endregion
}