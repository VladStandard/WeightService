using System.Diagnostics;
using System.Reflection;

namespace Ws.Shared.Utils;

public static class AssemblyUtils
{
    #region Public and private methods
    
    public static String? GetApVersion(Assembly executingAssembly)
    {
        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
        String? result = fileVersionInfo.FileVersion;

        if (string.IsNullOrEmpty(result) || !result.EndsWith(".0"))
        {
            return result;
        }
        
        int lastIndex = result.LastIndexOf(".0", StringComparison.Ordinal);
        if (lastIndex > 0)
        {
            result = result.Substring(0, lastIndex);
        }

        return result;
    }
    
    public static String? GetLibVersion()
    {
        FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        String? result = fieVersionInfo.FileVersion;
        if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
            result = result[..result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase)];
        return result;
    }

    #endregion
}