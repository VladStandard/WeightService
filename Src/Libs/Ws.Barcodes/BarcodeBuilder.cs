using System.Reflection;
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

        foreach (var barcodeVar in barcodeVars)
        {
            PropertyInfo? propertyInfo = GetType().GetProperty(barcodeVar.Property);
            object value = propertyInfo?.GetValue(this) ?? barcodeVar.Property;

            if (!BarcodeVarUtils.TryFormat(value, barcodeVar.FormatStr, out var result))
                throw new FormatException($"{barcodeVar.Property}: not valid format - {barcodeVar.FormatStr}");

            barcodeBuilder.Append(result);
        }
        return new(barcodeBuilder.ToString());
    }
}