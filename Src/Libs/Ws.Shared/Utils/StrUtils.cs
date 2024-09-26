using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.RegularExpressions;

namespace Ws.Shared.Utils;

public static partial class StrUtils
{
    [GeneratedRegex(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$")]
    private static partial Regex IsIpV4();

    public static bool TryParseToIpV4Address(string str, [NotNullWhen(true)] out IPAddress? ipAddress)
    {
        ipAddress = null;

        if (!string.IsNullOrWhiteSpace(str) && IsIpV4().IsMatch(str))
            return IPAddress.TryParse(str, out ipAddress);

        return false;
    }
}