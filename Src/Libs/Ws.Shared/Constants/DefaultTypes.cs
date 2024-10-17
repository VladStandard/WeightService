using Ws.Shared.ValueTypes;

namespace Ws.Shared.Constants;

public static class DefaultTypes
{
    public static readonly IPAddress IpLocal = IPAddress.Parse("127.0.0.1");
    public static readonly Guid GuidMax = Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");
    public static readonly Fio Fio = new(string.Empty, string.Empty, string.Empty);
}