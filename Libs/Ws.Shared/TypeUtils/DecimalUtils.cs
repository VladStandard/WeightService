using System.Globalization;

namespace Ws.Shared.TypeUtils;

public static class DecimalUtils
{
    public static string ToStrToLen(decimal number, int len)
    {
        string numStr = number.ToString(CultureInfo.InvariantCulture).Replace(".", "")
            .Replace(",", "");
        return StrUtils.ToLen(numStr, len);
    }
    
    public static string ToStrWithSep(decimal number, string separator)
    {
        NumberFormatInfo formatInfo = new() { NumberDecimalSeparator = separator};
        return number.ToString(formatInfo);
    }
}