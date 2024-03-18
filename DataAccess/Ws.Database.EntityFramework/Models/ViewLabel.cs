namespace Ws.Database.EntityFramework.Models;

public partial class ViewLabel
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ProdDt { get; set; }

    public string? Line { get; set; }

    public string? PluName { get; set; }

    public int? PluNumber { get; set; }

    public string? Warehouse { get; set; }

    public bool? IsCheckWeight { get; set; }

    public string BarcodeTop { get; set; } = null!;

    public string BarcodeBottom { get; set; } = null!;

    public string BarcodeRight { get; set; } = null!;
}
