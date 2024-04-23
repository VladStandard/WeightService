using System.Globalization;

namespace Ws.Labels.Service.Extensions;

internal static class DigitUtils
{
    internal static string ToStrLenWithZero(this int number, int len) => $"{number}".ToLenWithZero(len);
    internal static string ToStrLenWithZero(this short number, int len) => $"{number}".ToLenWithZero(len);
    internal static string ToStrWithLen(this decimal number, int len)
    {
        string numStr = number.ToString(CultureInfo.InvariantCulture).Replace(".", "")
            .Replace(",", "");
        return numStr.ToLenWithZero(len);
    }
    internal static string ToStrWithSep(this decimal number, string separator)
    {
        NumberFormatInfo formatInfo = new() { NumberDecimalSeparator = separator };
        return number.ToString(formatInfo);
    }
}