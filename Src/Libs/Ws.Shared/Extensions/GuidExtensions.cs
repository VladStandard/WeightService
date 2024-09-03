using Ws.Shared.Constants;

namespace Ws.Shared.Extensions;

public static class GuidExtensions
{
    public static bool IsMax(this Guid guid) => guid == DefaultConsts.GuidMax;
}