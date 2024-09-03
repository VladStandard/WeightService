using System.Net;

namespace Ws.Shared.Constants;

public static class DefaultConsts
{
    public static readonly IPAddress IpLocal = IPAddress.Parse("127.0.0.1");
    public static readonly Guid GuidMax = Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");
}