using System.Reflection;

namespace DeviceControl.Utils;

public static class BlazorCoreUtils
{
    public static string GetLibVersion()
    {
        AssemblyInformationalVersionAttribute? versionAttribute = Assembly.GetEntryAssembly()?
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>();

        if (versionAttribute == null) return "undefined";

        string fullVersion = versionAttribute.InformationalVersion;
        int endIndex = fullVersion.LastIndexOf(".0", StringComparison.InvariantCultureIgnoreCase);
        return endIndex != -1 ? fullVersion[..endIndex] : fullVersion;
    }
}