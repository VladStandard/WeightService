using System.Net;
using System.Text.RegularExpressions;
using Ws.Shared.Constants;

namespace Ws.Shared.Parsers;

public static partial class IpAddressParser
{
    [GeneratedRegex(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$")]
    private static partial Regex MyRegex();

    public static IPAddress Parse(string newIp, IPAddress oldIp)
    {
        bool isIp = MyRegex().IsMatch(newIp);
        if (!isIp) return oldIp;
        return IPAddress.TryParse(newIp, out IPAddress? ip) ?  ip : DefaultConsts.IpLocal;
    }
}