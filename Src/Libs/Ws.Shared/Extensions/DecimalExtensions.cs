using System.Globalization;

namespace Ws.Shared.Extensions;

public static class DecimalExtensions
{
    public static string ToSepStr(this decimal d, char? separator = null)
    {
        string systemDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        return d.ToString("0.000")
            .Replace(systemDecimalSeparator, separator != null ? separator.ToString() : string.Empty);
    }
}