using System.Text.RegularExpressions;

namespace Ws.Barcodes.Utils;

public static partial class BarcodeRegexUtils
{
    #region Regex

    [GeneratedRegex(@"[^0-9\(\)]")]
    private static partial Regex NotFriendlyChars();

    [GeneratedRegex(@"[^0-9]")]
    private static partial Regex NonDigitRegex();

    [GeneratedRegex(@"[^#\d]+")]
    private static partial Regex NonFriendlyCharsForZpl();

    #endregion

    public static string GetFriendlyChars(string barcode) =>
        NotFriendlyChars().Replace(barcode, string.Empty);

    internal static string GetOnlyDigits(string barcode) =>
        NonDigitRegex().Replace(barcode, string.Empty);

    internal static string GetZplChars(string barcode) =>
        NonFriendlyCharsForZpl().Replace(barcode, string.Empty);
}