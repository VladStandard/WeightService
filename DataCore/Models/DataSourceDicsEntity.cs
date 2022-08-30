// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Models;

/// <summary>
/// Data source dictionaries.
/// </summary>
public class DataSourceDicsHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static DataSourceDicsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static DataSourceDicsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
            $"{nameof(GetTemplateCategories)}: {GetTemplateCategories().Count}. " +
            $"{nameof(GetTemplateLanguagesEng)}: {GetTemplateLanguagesEng().Count}. " +
            $"{nameof(GetTemplateLanguagesRus)}: {GetTemplateLanguagesRus().Count}. " +
            $"{nameof(GetTemplateIsDebug)}: {GetTemplateIsDebug().Count}. ";
    }

    public List<TypeEntity<string>> GetTemplateCategories()
    {
        return new()
        {
            new("", ""),
            new("NaN", "NaN"),
            new("203 dpi", "203 dpi"),
            new("203 dpi tsc", "203 dpi tsc"),
            new("300 dpi", "300 dpi"),
            new("300 dpi tsc", "300 dpi tsc"),
            new("608 dpi", "608 dpi"),
            new("608 dpi tsc", "608 dpi tsc"),
            new("zpl", "zpl"),
        };
    }

    public List<TypeEntity<ShareEnums.AccessRights>> GetTemplateAccessRights(byte? accessRights = null)
    {
        List<TypeEntity<ShareEnums.AccessRights>> result = new();
        result.Add(new($"{ShareEnums.AccessRights.None}", ShareEnums.AccessRights.None));
        result.Add(new($"{ShareEnums.AccessRights.Read}", ShareEnums.AccessRights.Read));
        result.Add(new($"{ShareEnums.AccessRights.Write}", ShareEnums.AccessRights.Write));
        if (accessRights >= (byte)ShareEnums.AccessRights.Admin)
            result.Add(new($"{ShareEnums.AccessRights.Admin}", ShareEnums.AccessRights.Admin));
        return result;
    }

    public List<TypeEntity<ShareEnums.Lang>> GetTemplateLanguages()
    {
        return LocaleCore.Lang switch
        {
            ShareEnums.Lang.English => GetTemplateLanguagesEng(),
            ShareEnums.Lang.Russian => GetTemplateLanguagesRus(),
            _ => new()
        };
    }

    private List<TypeEntity<ShareEnums.Lang>> GetTemplateLanguagesEng()
    {
        return new()
        {
            new($"{ShareEnums.Lang.English}", ShareEnums.Lang.English),
            new($"{ShareEnums.Lang.Russian}", ShareEnums.Lang.Russian),
        };
    }

    private List<TypeEntity<ShareEnums.Lang>> GetTemplateLanguagesRus()
    {
        return new()
        {
            new("Английский", ShareEnums.Lang.English),
            new("Русский", ShareEnums.Lang.Russian),
        };
    }

    public List<TypeEntity<bool>> GetTemplateIsDebug()
    {
        return new()
        {
            new("Enable", true),
            new("Disable", false),
        };
    }

    #endregion
}
