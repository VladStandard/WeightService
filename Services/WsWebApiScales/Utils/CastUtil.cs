using System.Globalization;
namespace WsWebApiScales.Utils;

public static class CastUtil
{
    public static decimal StringToDecimal(string str)
    {
        CultureInfo culture = new("en-US")
        {
            NumberFormat =
            {
                NumberDecimalSeparator = ".",
                CurrencyDecimalSeparator = ","
            }
        };
        return decimal.TryParse(str, NumberStyles.Any, culture, out decimal result) ? result : default;
    }
}