namespace WsDataCore.Settings.Helpers;

public sealed class AppVersionHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static AppVersionHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static AppVersionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public string App { get; private set; }
    public string AppTitle { get; private set; }
    public string Version { get; private set; }

    #endregion

    #region Constructor and destructor

    public AppVersionHelper()
    {
        App = string.Empty;
        AppTitle = string.Empty;
        Version = string.Empty;
    }

    #endregion

    #region Public and private methods

    public string GetCurrentVersion(Assembly assembly, EnumAppVerCountDigits countDigits)
    {
        Version version = assembly.GetName().Version;
        string major = GetCurrentVersionFormat(version.Major);
        string minor = GetCurrentVersionFormat(version.Minor);
        string build = GetCurrentVersionFormat(version.Build);
        string revision = GetCurrentVersionFormat(version.Revision);

        return countDigits switch
        {
            EnumAppVerCountDigits.Use1 => major,
            EnumAppVerCountDigits.Use2 => $"{major}.{minor}",
            EnumAppVerCountDigits.Use3 => $"{major}.{minor}.{build}",
            _ => $"{major}.{minor}.{build}.{revision}"
        };
    }

    public static string GetCurrentVersionSubString(string input)
    {
        int idx = input.LastIndexOf('.');
        string result = idx >= 0 ? input[..idx] : string.Empty;
        return result;
    }

    private static string GetCurrentVersionFormat(int input) => $"{input:D}";

    public string GetDescription(Assembly assembly)
    {
        string result = string.Empty;
        object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
        if (attributes.Length <= 0)
            return result;

        if (attributes[0] is AssemblyDescriptionAttribute attribute)
            result = attribute.Description;
        return result;
    }

    private static string GetTitle(Assembly assembly)
    {
        string result = string.Empty;
        object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        if (attributes.Length <= 0)
            return result;
        if (attributes[0] is AssemblyTitleAttribute attribute)
            result = attribute.Title;
        return result;
    }

    public void Setup(Assembly assembly, string appTitle = "")
    {
        AppTitle = string.IsNullOrEmpty(appTitle)
            ? $"{GetTitle(assembly)} {GetCurrentVersion(assembly, EnumAppVerCountDigits.Use3)}"
            : $"{appTitle} {GetCurrentVersion(assembly, EnumAppVerCountDigits.Use3)}";

        if (AppTitle.Split(' ').Length <= 1)
            return;

        App = AppTitle.Split(' ').First();
        Version = AppTitle.Split(' ').Last();
    }

    #endregion
}