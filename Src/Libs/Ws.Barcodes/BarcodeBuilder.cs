using System.Text;
using Ws.Barcodes.Common;
using Ws.Barcodes.Models;
using Ws.Barcodes.Utils;

namespace Ws.Barcodes;

public record BarcodeBuilder : IBarcodeVariables
{
    #region Variables

    public required uint LineNumber { get; init; }
    public required uint LineCounter { get; init; }

    public required string PluGtin { get; init; }
    public required string PluEan13 { get; init; }
    public required ushort PluNumber { get; init; }

    public required DateTime ProductDt { get; init; }
    public required DateTime ExpirationDt { get; init; }
    public required ushort ExpirationDay { get; init; }

    public required ushort Kneading { get; init; }
    public required ushort BundleCount { get; init; }
    public required decimal WeightNet { get; init; }

    #endregion

    public BarcodeResult Build(List<BarcodeVar> barcodeVars)
    {
        StringBuilder barcodeBuilder = new();

        foreach (BarcodeVar barcodeVar in barcodeVars)
        {
            object value = GetType().GetProperty(barcodeVar.Property)?.GetValue(this) ?? barcodeVar.Property;
            string formatString = string.IsNullOrWhiteSpace(barcodeVar.FormatStr) ? "{0}" : barcodeVar.FormatStr;

            string valueStr = BarcodeVarUtils.GetVariableResult(value, formatString);
            barcodeBuilder.Append(valueStr);
        }

        string initialBarcode = barcodeBuilder.ToString();

        string zplBarcode = BarcodeRegexUtils.GetZplChars(initialBarcode);
        string cleanBarcode = BarcodeRegexUtils.GetOnlyDigits(initialBarcode);
        string friendlyBarcode = BarcodeRegexUtils.GetFriendlyChars(initialBarcode);

        return new(cleanBarcode, friendlyBarcode, zplBarcode);
    }
}