namespace DeviceControl.Utils;

public static class BlazorCoreUtils
{
    public static string GetLibVersion()
    {
        FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        if (string.IsNullOrEmpty(fieVersionInfo.FileVersion))
            return string.Empty;
        
        string result = fieVersionInfo.FileVersion;
        int endIndex = result.LastIndexOf(".0", StringComparison.InvariantCultureIgnoreCase);
        return endIndex != -1 ? result[..endIndex] : result;
    }
}
