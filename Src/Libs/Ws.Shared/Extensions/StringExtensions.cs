using System.Globalization;
using System.Text.RegularExpressions;

namespace Ws.Shared.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex(@"^\d+$")]
    private static partial Regex DigitsOnlyRegex();

    [GeneratedRegex(@"^(?!.*(\b(?:yy|MM|dd|HH|ss|mm)\b).*\1)(?=(?:yy|MM|dd|HH|ss|mm)+$).+$")]
    private static partial Regex DateFormatRegex();

    public static bool IsDigitsOnly(this string str) => DigitsOnlyRegex().IsMatch(str);
    public static bool IsDateFormat(this string str) => DateFormatRegex().IsMatch(str);
    public static string Capitalize(this string str) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
}