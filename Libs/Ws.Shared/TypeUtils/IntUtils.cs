namespace Ws.Shared.TypeUtils;

public static class IntUtils
{
    public static string ToStringToLen(int number, int len) =>
        StrUtils.ToLen(number.ToString(), len);

    public static int ConvertStrToIntOrMin(string? value) =>
        int.TryParse(value, out int parsed) ? parsed : default;
}