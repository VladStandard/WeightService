namespace Ws.Shared.TypeUtils;

public static class IntUtils
{
    public static string ToStringToLen(int number, int len) => StrUtils.ToLen(number.ToString(), len);
}