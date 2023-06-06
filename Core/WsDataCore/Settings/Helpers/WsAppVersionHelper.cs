// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;

namespace WsDataCore.Settings.Helpers;

public sealed class WsAppVersionHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsAppVersionHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsAppVersionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public string App { get; private set; }
    public string AppTitle { get; private set; }
    public string Version { get; private set; }

    #endregion

    #region Constructor and destructor

    public WsAppVersionHelper()
    {
        App = string.Empty;
        AppTitle = string.Empty;
        Version = string.Empty;
    }

    #endregion

    #region Public and private methods

    private string GetCurrentVersion(Assembly assembly, WsEnumAppVerCountDigits countDigits, List<WsEnumAppVerStringFormat>? stringFormats = null)
    {
        if (stringFormats is null || stringFormats.Count is 0)
            stringFormats = new() {
                WsEnumAppVerStringFormat.Use1, WsEnumAppVerStringFormat.Use2, WsEnumAppVerStringFormat.Use2 };

        WsEnumAppVerStringFormat formatMajor = stringFormats.First();
        WsEnumAppVerStringFormat formatMinor = WsEnumAppVerStringFormat.AsString;
        WsEnumAppVerStringFormat formatBuild = WsEnumAppVerStringFormat.AsString;
        WsEnumAppVerStringFormat formatRevision = WsEnumAppVerStringFormat.AsString;
        if (stringFormats.Count > 1)
            formatMinor = stringFormats[1];
        if (stringFormats.Count > 2)
            formatBuild = stringFormats[2];
        if (stringFormats.Count > 3)
            formatRevision = stringFormats[3];

        Version version = assembly.GetName().Version;
        string major = GetCurrentVersionFormat(version.Major, formatMajor);
        string minor = GetCurrentVersionFormat(version.Minor, formatMinor);
        string build = GetCurrentVersionFormat(version.Build, formatBuild);
        string revision = GetCurrentVersionFormat(version.Revision, formatRevision);
        string version4 = $"{major}.{minor}.{build}.{revision}";
        string version3 = $"{major}.{minor}.{build}";
        string version2 = $"{major}.{minor}";
        string version1 = $"{major}";

        return countDigits == WsEnumAppVerCountDigits.Use1
            ? version1 : countDigits == WsEnumAppVerCountDigits.Use2
            ? version2 : countDigits == WsEnumAppVerCountDigits.Use3
            ? version3 : version4;
    }

    public string GetCurrentVersionSubString(string input)
    {
        string result = string.Empty;
        int idx = input.LastIndexOf('.');
        if (idx >= 0)
            result = input[..idx];
        return result;
    }

    private string GetCurrentVersionFormat(int input, WsEnumAppVerStringFormat format)
    {
        return format switch
        {
            WsEnumAppVerStringFormat.Use1 => $"{input:D}",
            WsEnumAppVerStringFormat.Use2 => $"{input:D}",
            WsEnumAppVerStringFormat.Use3 => $"{input:D}",
            WsEnumAppVerStringFormat.Use4 => $"{input:D}",
            _ => $"{input:D}"
        };
    }

    public string GetDescription(Assembly assembly)
    {
        string result = string.Empty;
        object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
        if (attributes.Length > 0)
        {
            if (attributes[0] is AssemblyDescriptionAttribute attribute)
                result = attribute.Description;
        }
        return result;
    }

    private string GetTitle(Assembly assembly)
    {
        string result = string.Empty;
        object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        if (attributes.Length > 0)
        {
            if (attributes[0] is AssemblyTitleAttribute attribute)
                result = attribute.Title;
        }
        return result;
    }

    public void Setup(Assembly assembly, string appTitle = "")
    {
        AppTitle = string.IsNullOrEmpty(appTitle)
            ? $"{GetTitle(assembly)} {GetCurrentVersion(assembly, WsEnumAppVerCountDigits.Use3)}"
            : $"{appTitle} {GetCurrentVersion(assembly, WsEnumAppVerCountDigits.Use3)}";
        if (AppTitle.Split(' ').Length > 1)
        {
            App = AppTitle.Split(' ').First();
            Version = AppTitle.Split(' ').Last();
        }
    }

    #endregion
}