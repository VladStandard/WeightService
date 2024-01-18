namespace Ws.Shared.TypeUtils;

public static class StrUtils
{
    public static string ToLen(string str, int len)
    {
        if (str.Length > len) str = str[..len];
        return str.PadLeft(len, '0');
    }
}