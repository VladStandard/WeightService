namespace Ws.DataCore.Utils;

public static class StrUtils
{
    #region Public and private methods

    public static string FormatDtLong => "{0:dd.MM.yy HH:mm:ss}";
    
    public static string FormatDtDefault => "{0:dd.MM.yy HH:mm}";
    
    public static string FormatDtShort => "{0:dd.MM.yy}";

    public static void SetStringValueTrim(ref string value, int length, bool isGetFileName = false)
    {
        if (string.IsNullOrEmpty(value))
            return;
        if (isGetFileName)
            value = Path.GetFileName(value);
        if (value.Length > length)
            value = value[..length];
    }
    
    #endregion
}