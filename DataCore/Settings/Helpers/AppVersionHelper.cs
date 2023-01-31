// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Settings.Helpers;

public class AppVersionHelper
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

    private string GetCurrentVersion(Assembly assembly, AppVerCountDigitsEnum countDigits, List<AppVerStringFormatEnum>? stringFormats = null)
    {
        if (stringFormats is null || stringFormats.Count is 0)
            stringFormats = new() {
                AppVerStringFormatEnum.Use1, AppVerStringFormatEnum.Use2, AppVerStringFormatEnum.Use2 };

        AppVerStringFormatEnum formatMajor = stringFormats.First();
        AppVerStringFormatEnum formatMinor = AppVerStringFormatEnum.AsString;
        AppVerStringFormatEnum formatBuild = AppVerStringFormatEnum.AsString;
        AppVerStringFormatEnum formatRevision = AppVerStringFormatEnum.AsString;
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

        return countDigits == AppVerCountDigitsEnum.Use1
            ? version1 : countDigits == AppVerCountDigitsEnum.Use2
            ? version2 : countDigits == AppVerCountDigitsEnum.Use3
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

    private string GetCurrentVersionFormat(int input, AppVerStringFormatEnum format)
    {
        return format switch
        {
            AppVerStringFormatEnum.Use1 => $"{input:D}",
            AppVerStringFormatEnum.Use2 => $"{input:D}",
            AppVerStringFormatEnum.Use3 => $"{input:D}",
            AppVerStringFormatEnum.Use4 => $"{input:D}",
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
            ? $"{GetTitle(assembly)} {GetCurrentVersion(assembly, AppVerCountDigitsEnum.Use3)}"
            : $"{appTitle} {GetCurrentVersion(assembly, AppVerCountDigitsEnum.Use3)}";
        if (AppTitle.Split(' ').Length > 1)
        {
            App = AppTitle.Split(' ').First();
            Version = AppTitle.Split(' ').Last();
        }
    }

    #endregion
}