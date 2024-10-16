using Ws.Shared.Constants;

namespace Ws.Shared.Extensions;

public static class GuidExtensions
{
    [Pure]
    public static bool IsMax(this Guid guid) => guid == DefaultTypes.GuidMax;

    [Pure]
    public static bool IsEmpty(this Guid guid) => guid == Guid.Empty;
}