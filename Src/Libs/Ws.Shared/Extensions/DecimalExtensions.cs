using System.Globalization;

namespace Ws.Shared.Extensions;

public static class DecimalExtensions
{
    public static string ToSepStr(this decimal d, string? separator = null)
    {
        if (!string.IsNullOrEmpty(separator))
            return d.ToString(new NumberFormatInfo { NumberDecimalSeparator = separator });

        string systemDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        return $"{d}".Replace(systemDecimalSeparator, string.Empty);
    }
}