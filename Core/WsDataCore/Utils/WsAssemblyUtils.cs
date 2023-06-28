// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Deployment.Application;

namespace WsDataCore.Utils;

/// <summary>
/// Утилиты сборок.
/// </summary>
public static class WsAssemblyUtils
{
    #region Public and private methods

    /// <summary>
    /// Get application version.
    /// </summary>
    /// <param name="executingAssembly"></param>
    /// <returns></returns>
    public static string GetAppVersion(Assembly executingAssembly)
    {
        FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
        string result = fieVersionInfo.FileVersion;
        if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
            result = result[..result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase)];
        return result;
    }

    /// <summary>
    /// Get ClickOnce network install directory.
    /// </summary>
    /// <returns></returns>
    public static string GetClickOnceNetworkInstallDirectory()
    {
        string? directory = null;
        if (ApplicationDeployment.IsNetworkDeployed)
            directory = Path.GetDirectoryName(ApplicationDeployment.CurrentDeployment.UpdateLocation.AbsolutePath);
        if (directory is not null && directory.StartsWith("\\") && !directory.StartsWith("\\\\"))
            directory = string.Join("\\", directory);
        return directory ?? "This application is not deployed using ClickOnce!";
    }

    /// <summary>
    /// Get run directory.
    /// </summary>
    /// <returns></returns>
    public static string GetRunDirectory()
    {
        string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        UriBuilder uri = new(codeBase);
        string path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path) ?? string.Empty;
    }

    /// <summary>
    /// Get library version.
    /// </summary>
    /// <returns></returns>
    public static string GetLibVersion()
    {
        FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        string result = fieVersionInfo.FileVersion;
        if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
            result = result[..result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase)];
        return result;
    }

    #endregion
}