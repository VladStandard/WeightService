using Ws.Mobile.Models.Features.Warehouses;

namespace ScalesMobile.Source.Widgets.TransferForm;

public record TransferFormModel
{
    public string DocumentNumber { get; set; } = string.Empty;
    public List<string> Pallets { get; set; } = [];
    public WarehouseDto Warehouse { get; set; } = new()
    {
        Id = Guid.Empty, WarehouseName = string.Empty, ProductionSiteName = string.Empty
    };
}