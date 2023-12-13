namespace Ws.Shared.TypeUtils;

public static class StrUtils
{
    public static string FormatDtLong => "{0:dd.MM.yy HH:mm:ss}";
    
    public static string FormatDtDefault => "{0:dd.MM.yy HH:mm}";
    
    public static string FormatDtShort => "{0:dd.MM.yy}";
    
    public static string ToLen(string str, int len) => str.PadLeft(len, '0');
}