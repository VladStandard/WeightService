using Ws.Domain.Models.Entities.Diag;

namespace Ws.Domain.Models.Entities;

public enum DbFileType
{
    MasterDataFile,
    LogDataFile
}

public sealed record DbFileSizeInfoEntity
{
    public DbFileType Type { get; init; }
    public string FileName { get; init; }
    public ushort SizeMb { get; init; }
    public ushort MaxSizeMb { get; init; }
    public double DbFillSize => Math.Round(((double)SizeMb / MaxSizeMb) * 100, 2);
    public List<TableSizeEntity> Tables { get; set; }
    
    public DbFileSizeInfoEntity(byte type, string fileName, ushort sizeMb, ushort maxSizeMb)
    {
        Type = type == 0 ? DbFileType.MasterDataFile : DbFileType.LogDataFile;
        FileName = fileName;
        SizeMb = sizeMb;
        MaxSizeMb = maxSizeMb;
        Tables = [];
    }
}