namespace WsDataCore.Utils;

public static class StrUtils
{
    #region Public and private methods

    public static string FormatDtLong => "{0:dd.MM.yy HH:mm:ss}";
    
    public static string FormatDtDefault => "{0:dd.MM.yy HH:mm}";
    
    public static string FormatDtShort => "{0:dd.MM.yy}";

    public static string FormatDtRus(DateTime dt, bool isShowSeconds) =>
        isShowSeconds
            ? $"{dt.Day:D2}.{dt.Month:D2}.{dt.Year:D4} {dt.Hour:D2}:{dt.Minute:D2}.{dt.Second:D2}"
            : $"{dt.Day:D2}.{dt.Month:D2}.{dt.Year:D4} {dt.Hour:D2}:{dt.Minute:D2}";
    
    public static string FormatDtEng(DateTime dt, bool isShowSeconds) =>
        isShowSeconds
            ? $"{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}.{dt.Second:D2}"
            : $"{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}";

    public static void SetStringValueTrim(ref string value, int length, bool isGetFileName = false)
    {
        if (string.IsNullOrEmpty(value))
            return;
        if (isGetFileName)
            value = Path.GetFileName(value);
        if (value.Length > length)
            value = value[..length];
    }
    
    public static decimal GetDecimalValue(string value)
    {
        //System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.InvariantCulture;
        string convert = value.Replace(',', '.');
        if (decimal.TryParse(convert, out decimal result))
            return result;
        return 0M;
    }

    private static int NextInt32(Random random)
    {
        try
        {
            int firstBits = random.Next(0, 1 << 4) << 28;
            int lastBits = random.Next(0, 1 << 28);
            return firstBits | lastBits;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static decimal NextDecimal(decimal min, decimal max)
    {
        Random random = new();
        decimal result = min;
        if (max == min && min == 0)
        {
            return random.Next(0, 10);
        }

        try
        {
            byte scale = (byte)random.Next(29);
            bool sign = random.Next(2) == 1;
            result = new(NextInt32(random), NextInt32(random), NextInt32(random), sign, scale);
        }
        catch (Exception)
        {
            //
        }

        if (result < min)
            result = min;
        if (result > max)
            result = max;
        return result;
    }

    #endregion
}