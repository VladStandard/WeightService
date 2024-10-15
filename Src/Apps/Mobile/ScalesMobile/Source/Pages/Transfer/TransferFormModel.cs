namespace ScalesMobile.Source.Pages.Transfer;

public record TransferFormModel
{
    public string DocumentNumber { get; set; } = string.Empty;
    public List<string> Pallets { get; set; } = [];
    public Guid WarehouseId { get; set; } = Guid.Empty;
}