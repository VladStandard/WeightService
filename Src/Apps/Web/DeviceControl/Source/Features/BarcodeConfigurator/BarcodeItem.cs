namespace DeviceControl.Source.Features.BarcodeConfigurator;

public class BarcodeTableItem
{
    public string? Name { get; set; }
    public BarcodeItemType Type { get; set; } = BarcodeItemType.Variable;
    public int Length { get; set; }
}
