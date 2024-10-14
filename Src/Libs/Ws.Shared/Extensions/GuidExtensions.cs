using Ws.Shared.Constants;

namespace Ws.Shared.Extensions;

public static class GuidExtensions
{
    public static bool IsMax(this Guid guid) => guid == DefaultTypes.GuidMax;
    public static bool IsEmpty(this Guid guid) => guid == Guid.Empty;
}