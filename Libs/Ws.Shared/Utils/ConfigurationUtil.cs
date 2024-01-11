using Ws.Shared.Enums;

namespace Ws.Shared.Utils;

public static class ConfigurationUtil
{
    public static bool IsDevelop => Config switch
    {
        EnumConfiguration.DevelopVs => true,
        EnumConfiguration.ReleaseVs => false,
        _ => throw new ArgumentOutOfRangeException(nameof(IsDevelop), IsDevelop.ToString())
    };
    
    public static EnumConfiguration Config =>
#if RELEASEVS
        EnumConfiguration.ReleaseVs;
#else
        EnumConfiguration.DevelopVs;
#endif
    
}