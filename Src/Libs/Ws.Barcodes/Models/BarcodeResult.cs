using Ws.Barcodes.Utils;

namespace Ws.Barcodes.Models;

public sealed record BarcodeResult(string Barcode)
{
    private string Barcode { get; } = Barcode;
    public string Clean => BarcodeRegexUtils.GetOnlyDigits(Barcode);
    public string Zpl => BarcodeRegexUtils.GetZplChars(Barcode);
    public string Friendly =>  BarcodeRegexUtils.GetFriendlyChars(Barcode);
};