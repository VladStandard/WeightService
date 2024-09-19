using System.Text;
using System.Text.RegularExpressions;
using Ws.Barcodes.Common;
using Ws.Shared.Extensions;

namespace Ws.Barcodes.Models;

public partial record BarcodeBuilder : IBarcodeVariables
{
    #region Regex

    [GeneratedRegex(@"[^0-9\(\)]")]
    private static partial Regex NotFriendlyChars();

    [GeneratedRegex(@"[^0-9]")]
    private static partial Regex NonDigitRegex();

    [GeneratedRegex(@"[^#\d]+")]
    private static partial Regex NonFriendlyCharsForZpl();

    #endregion

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
            object? value = GetType().GetProperty(barcodeVar.Property)?.GetValue(this);

            if (value is decimal)
                value = Convert.ToUInt16(Convert.ToDecimal(value).ToSepStr());

            string formatString = string.IsNullOrWhiteSpace(barcodeVar.FormatStr) ? "{0}" : barcodeVar.FormatStr;

            switch (value)
            {
                case null when barcodeVar.Property.IsDigitsOnly():
                    barcodeBuilder.Append(string.Format(formatString, barcodeVar.Property));
                    continue;
                case DateTime or string or uint or ushort:
                    barcodeBuilder.Append(string.Format(formatString, value));
                    break;
                default:
                    throw new NotImplementedException($"Not supported type of variable: {barcodeVar.Property}");
            }
        }
        string initialBarcode = barcodeBuilder.ToString();

        string cleanBarcode = NonDigitRegex().Replace(initialBarcode, string.Empty);
        string friendlyBarcode = NotFriendlyChars().Replace(initialBarcode, string.Empty);
        string zplBarcode = NonFriendlyCharsForZpl().Replace(initialBarcode, string.Empty);

        return new(cleanBarcode, friendlyBarcode, zplBarcode);
    }
}