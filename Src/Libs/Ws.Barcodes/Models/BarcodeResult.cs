namespace Ws.Barcodes.Models;

public sealed record BarcodeResult(string Barcode)
{
    private string Barcode { get; } = Barcode;
    public string Clean => string.Concat(Barcode.Where(char.IsDigit));
    public string Zpl => string.Concat(Barcode.Where("0123456789#".Contains));
    public string Friendly => string.Concat(Barcode.Where("0123456789()".Contains));
};