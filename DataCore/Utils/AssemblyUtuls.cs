// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Utils;

public static class AssemblyUtuls
{
    #region Public and private methods

    public static string GetAppVersion(System.Reflection.Assembly executingAssembly)
    {
        FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
        string result = fieVersionInfo.FileVersion;
        if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
            result = result[..result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase)];
        return result;
    }

    public static string GetLibVersion()
    {
        FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
        string result = fieVersionInfo.FileVersion;
        if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
            result = result[..result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase)];
        return result;
    }

    #endregion
}
