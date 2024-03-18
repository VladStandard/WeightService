namespace Ws.Database.EntityFramework.Models;

public partial class ViewPallet
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ProdDt { get; set; }

    public int Counter { get; set; }

    public string? PalletMan { get; set; }

    public string Barcode { get; set; } = null!;

    public string? PluName { get; set; }

    public string? LineName { get; set; }

    public string? WarehouseName { get; set; }

    public decimal? WeightNetto { get; set; }

    public decimal? WeightBrut { get; set; }

    public int? LabelCount { get; set; }
}
