namespace WsLocalizationCore.Utils;

public static class WsLocalizationUtils
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// ПО Печать этикеток.
    /// </summary>
    public static string AppLabelPrint => nameof(AppLabelPrint);
    public const string AppScalesTerminal = "C:\\Program Files (x86)\\Massa-K\\ScalesTerminal 100\\ScalesTerminal.exe";
    public const decimal MassaThresholdPositive = 0.050M;
    public const decimal MassaThresholdValue = 0.010M;
    private const string LocalDirectoryLocales = @"Locales";
    private const string RemoteDirectoryLocales = @"\\palych\install\VSSoft\Locales\";
    public static string Tests => nameof(Tests);
    public static string DeviceControlAppName = "DeviceControl";

    #endregion

    #region Public and private methods

    /// <summary>
    /// Получить список языков.
    /// </summary>
    /// <returns></returns>
    public static List<string> GetListLanguages() =>
        (from object lang in Enum.GetValues(typeof(WsEnumLanguage)) select lang.ToString()).ToList();

    /// <summary>
    /// Проверить каталог и файлы локализации.
    /// </summary>
    public static void CheckDirectoryWithFiles()
    {
        if (!Directory.Exists(RemoteDirectoryLocales))
            throw new($"Directory `{RemoteDirectoryLocales}` not exists!");
        if (!Directory.Exists(LocalDirectoryLocales))
            Directory.CreateDirectory(LocalDirectoryLocales);

        foreach (string remoteFile in Directory.GetFiles(RemoteDirectoryLocales))
        {
            if (!remoteFile.EndsWith(".loc.json"))
                throw new($"File `{remoteFile}` is wrong! Contact with administrator!");
            string remoteFileShortName = Path.GetFileName(remoteFile);
            string? localFile = Directory.GetFiles(LocalDirectoryLocales).FirstOrDefault(file => 
                Path.GetFileName(file).Equals(remoteFileShortName));
            if (!string.IsNullOrEmpty(localFile))
            {
                FileInfo localFileInfo = new(localFile);
                FileInfo remoteFileInfo = new(remoteFile);
                if (!localFileInfo.Length.Equals(remoteFileInfo.Length))
                {
                    CopyFile(localFile, remoteFile, remoteFileShortName);
                }
            } else
                CopyFile(string.Empty, remoteFile, remoteFileShortName);
        }
    }

    /// <summary>
    /// Копировать файл.
    /// </summary>
    /// <param name="localFile"></param>
    /// <param name="remoteFile"></param>
    /// <param name="remoteFileShortName"></param>
    private static void CopyFile(string localFile, string remoteFile, string remoteFileShortName)
    {
        if (!string.IsNullOrEmpty(localFile))
            File.Delete(localFile);
        File.Copy(Path.Combine(RemoteDirectoryLocales, remoteFile),
            Path.Combine(LocalDirectoryLocales, remoteFileShortName));
    }

    #endregion
}