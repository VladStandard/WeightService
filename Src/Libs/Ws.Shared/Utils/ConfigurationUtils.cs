using Ws.Shared.Enums;

namespace Ws.Shared.Utils;

public static class ConfigurationUtils
{
    [Pure]
    public static bool IsDevelop => Config switch
    {
        ConfigurationType.DevelopVs => true,
        ConfigurationType.ReleaseVs => false,
        _ => throw new ArgumentOutOfRangeException(nameof(IsDevelop), IsDevelop.ToString())
    };

    [Pure]
    public static ConfigurationType Config =>
#if RELEASEVS
        ConfigurationType.ReleaseVs;
#else
        ConfigurationType.DevelopVs;
#endif

}