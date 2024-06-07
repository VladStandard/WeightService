namespace Ws.Domain.Models.Entities.Diag;

public sealed record DbFileSizeInfo
{
    public string FileName { get; set; } = string.Empty;
    public int SizeMb { get; set; }
    public int MaxSizeMb { get; set; }
    public double DbFillSize => Math.Round((double)SizeMb / MaxSizeMb * 100, 2);
    public List<TableSize> Tables { get; set; } = [];
}