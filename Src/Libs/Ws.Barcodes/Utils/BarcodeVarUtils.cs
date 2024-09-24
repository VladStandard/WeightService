using System.Text.RegularExpressions;
using Ws.Barcodes.Formatters;
using Ws.Barcodes.Models;

namespace Ws.Barcodes.Utils;


public static partial class BarcodeVarUtils
{
    #region Public

    public static List<BarcodeVarInfo> BarcodeVarInfos => VariablesInfo.Value;

    public static List<string> BarcodeVarConstantsFormats => ["({0:C})", "#({0:C})", "{0:C}"];

    #endregion

    #region Private

    private static readonly Lazy<List<BarcodeVarInfo>> VariablesInfo = new(GenerateVariablesInfo);

    [GeneratedRegex(@"^(?=.*\d)([0-9#]*\([0-9#]+\)[0-9#]*|[0-9#]+)$")]
    private static partial Regex AllowedChars();

    private static List<BarcodeVarInfo> GenerateVariablesInfo()
    {
        return
        [
            CreateVariable(nameof(BarcodeBuilder.LineNumber), "{0:D5}", (uint)12345),
            CreateVariable(nameof(BarcodeBuilder.LineCounter), "{0:D6}", (uint)999999),
            CreateVariable(nameof(BarcodeBuilder.PluNumber), "{0:D3}", (ushort)100),
            CreateVariable(nameof(BarcodeBuilder.PluGtin), "{0:D14}", "14607100238871"),
            CreateVariable(nameof(BarcodeBuilder.PluEan13), "{0:D13}", "4607100238874"),
            CreateVariable(nameof(BarcodeBuilder.Kneading), "{0:D3}", (ushort)1),
            CreateVariable(nameof(BarcodeBuilder.WeightNet), "{0:D5}", 1m),
            CreateVariable(nameof(BarcodeBuilder.BundleCount), "{0:D2}", (ushort)10),
            CreateVariable(nameof(BarcodeBuilder.ExpirationDay), "{0:D3}", (ushort)1),
            CreateVariable(nameof(BarcodeBuilder.ProductDt), "{0:yyMMdd}", DateTime.Now, true),
            CreateVariable(nameof(BarcodeBuilder.ExpirationDt), "{0:yyMMdd}",  DateTime.Now,true)
        ];
    }

    private static BarcodeVarInfo CreateVariable(string property, string mask, object example, bool isRepeatable = false)
    {
        List<Type> typesWhiteList = [typeof(uint), typeof(ushort), typeof(string), typeof(decimal), typeof(DateTime)];

        Type? propertyType = typeof(BarcodeBuilder).GetProperty(property)?.PropertyType;

        if (typesWhiteList.All(i => i != propertyType))
            throw new ArgumentException($"Barcode variable type not supported: {propertyType}");

        if (propertyType != example.GetType())
            throw new ArgumentException($"Barcode example var not supported: {example.GetType()}");

        return new(propertyType, property, mask, example, isRepeatable);
    }

    internal static string GetVariableResult(object? value, string format)
    {
        string result = string.Format(BarcodeFormatter.Default, format, value);
        return AllowedChars().IsMatch(result) ? result : throw new FormatException(result);
    }

    #endregion
}