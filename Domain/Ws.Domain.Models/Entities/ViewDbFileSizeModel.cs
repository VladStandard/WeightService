using Ws.Domain.Models.Entities.Diag;

namespace Ws.Domain.Models.Entities;

public sealed record DbFileSizeInfoEntity
{
    public string FileName { get; set; } = string.Empty;
    public int SizeMb { get; set; }
    public int MaxSizeMb { get; set; }
    public double DbFillSize => Math.Round((double)SizeMb / MaxSizeMb * 100, 2);
    public List<TableSizeEntity> Tables { get; set; } = [];
}