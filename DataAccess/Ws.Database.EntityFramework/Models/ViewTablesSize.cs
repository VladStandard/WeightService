namespace Ws.Database.EntityFramework.Models;

public partial class ViewTablesSize
{
    public Guid? Uid { get; set; }

    public string? Schema { get; set; }

    public string Table { get; set; } = null!;

    public long? RowsCount { get; set; }

    public long? UsedSpaceMb { get; set; }

    public string Filename { get; set; } = null!;
}
