using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Ws.Barcodes.Formatters;
using Ws.Barcodes.Models;

namespace Ws.Barcodes.Utils;


public static partial class BarcodeVarUtils
{
    #region Public

    public static List<BarcodeVarInfo> BarcodeVarInfos => VariablesInfo.Value;

    public static List<string> BarcodeVarConstantsFormats => ["{0:C}", "({0:C})", "#({0:C})"];

    public static string GetVariableResult(object? value, string format)
    {
        string result = string.Format(BarcodeFormatter.Default, format, value);
        return AllowedChars().IsMatch(result) ? result : throw new FormatException(result);
    }

    #endregion

    #region Private

    private static readonly Lazy<List<BarcodeVarInfo>> VariablesInfo = new(GenerateVariablesInfo);

    [GeneratedRegex(@"^(?=.*\d)([0-9#]*\([0-9#]+\)[0-9#]*|[0-9#]+)$")]
    private static partial Regex AllowedChars();

    private static List<BarcodeVarInfo> GenerateVariablesInfo()
    {
        BarcodeBuilder barcodeBuilder = new()
        {
            LineNumber = 12345,
            LineCounter = 123456,
            PluGtin = "14607100238871",
            PluEan13 = "4607100238874",
            PluNumber = 301,
            ProductDt = DateTime.Now,
            ExpirationDt = DateTime.Now,
            ExpirationDay = 364,
            Kneading = 105,
            BundleCount = 20,
            WeightNet = 20.135m
        };
        return
        [
            CreateVariable(() => barcodeBuilder.LineNumber, "{0:D5}"),
            CreateVariable(() => barcodeBuilder.LineCounter, "{0:D6}"),
            CreateVariable(() => barcodeBuilder.PluNumber, "{0:D3}"),
            CreateVariable(() => barcodeBuilder.PluGtin, "{0:D14}"),
            CreateVariable(() => barcodeBuilder.PluEan13, "{0:D13}"),
            CreateVariable(() => barcodeBuilder.Kneading, "{0:D3}"),
            CreateVariable(() => barcodeBuilder.WeightNet, "{0:D5}"),
            CreateVariable(() => barcodeBuilder.BundleCount, "{0:D2}"),
            CreateVariable(() => barcodeBuilder.ExpirationDay, "{0:D3}"),
            CreateVariable(() => barcodeBuilder.ProductDt, "{0:yyMMdd}"),
            CreateVariable(() => barcodeBuilder.ExpirationDt, "{0:yyMMdd}")
        ];
    }

    private static BarcodeVarInfo CreateVariable<T>(Expression<Func<T>> propertyExpression, string mask, bool isRepeatable = false)
    {
        List<Type> typesWhiteList = [typeof(uint), typeof(ushort), typeof(string), typeof(decimal), typeof(DateTime)];

        if (propertyExpression.Body is not MemberExpression memberExpression)
            throw new ArgumentException($"Barcode variable expression is not a property");

        string propertyName = memberExpression.Member.Name;

        T propertyValue = propertyExpression.Compile().Invoke()!;

        if (typesWhiteList.All(i => i != typeof(T)))
            throw new ArgumentException($"Barcode variable type not supported: {typeof(T)}");

        return new(typeof(T), propertyName, mask, propertyValue, isRepeatable);
    }

    #endregion
}