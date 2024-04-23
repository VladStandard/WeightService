namespace Ws.Labels.Service.Extensions;

internal static class StrUtils
{
    internal static string ToLenWithZero(this string str, int len) =>
        $"{(str.Length > len ? str[..len] : str)}".PadLeft(len, '0');
}