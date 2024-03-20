namespace Ws.Database.EntityFramework.Models;

public partial class Line
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ChangeDt { get; set; }

    public string Name { get; set; } = null!;

    public string Version { get; set; } = null!;

    public int Counter { get; set; }

    public int Number { get; set; }

    public Guid WarehouseUid { get; set; }

    public Guid PrinterUid { get; set; }

    public string PcName { get; set; } = null!;

    public string? Type { get; set; }

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();

    public virtual ICollection<PlusLine> PlusLines { get; set; } = new List<PlusLine>();

    // public virtual Printer PrinterU { get; set; } = null!;
    //
    // public virtual Warehouse WarehouseU { get; set; } = null!;
}
