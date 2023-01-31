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

    public override string ToString() =>
        $"{nameof(GetTemplateCategories)}: {GetTemplateCategories().Count}. " +
        $"{nameof(GetTemplateLanguagesEng)}: {GetTemplateLanguagesEng().Count}. " +
        $"{nameof(GetTemplateLanguagesRus)}: {GetTemplateLanguagesRus().Count}. " +
        $"{nameof(GetTemplateIsDebug)}: {GetTemplateIsDebug().Count}. ";

    public List<TypeModel<string>> GetTemplateCategories() =>
        new()
        {
            new("", ""),
            new("NaN", "NaN"),
            new("203 dpi", "203 dpi"),
            new("203 dpi tsc", "203 dpi tsc"),
            new("300 dpi", "300 dpi"),
            new("300 dpi tsc", "300 dpi tsc"),
            new("608 dpi", "608 dpi"),
            new("608 dpi tsc", "608 dpi tsc"),
            new("zpl", "zpl")
        };

    public List<TypeModel<AccessRightsEnum>> GetTemplateAccessRights(byte? accessRights = null)
    {
        List<TypeModel<AccessRightsEnum>> result = new()
        {
            new($"{AccessRightsEnum.None}", AccessRightsEnum.None),
            new($"{AccessRightsEnum.Read}", AccessRightsEnum.Read),
            new($"{AccessRightsEnum.Write}", AccessRightsEnum.Write)
        };
        if (accessRights >= (byte)AccessRightsEnum.Admin)
            result.Add(new($"{AccessRightsEnum.Admin}", AccessRightsEnum.Admin));
        return result;
    }

    public List<TypeModel<Lang>> GetTemplateLanguages() => LocaleCore.Lang switch
    {
        Lang.English => GetTemplateLanguagesEng(),
        Lang.Russian => GetTemplateLanguagesRus(),
        _ => new()
    };

    private List<TypeModel<Lang>> GetTemplateLanguagesEng() => new()
    {
        new($"{Lang.English}", Lang.English),
        new($"{Lang.Russian}", Lang.Russian)
    };

    private List<TypeModel<Lang>> GetTemplateLanguagesRus() => new()
    {
        new("Английский", Lang.English),
        new("Русский", Lang.Russian)
    };

    public List<TypeModel<bool>> GetTemplateIsDebug() => new()
    {
        new("Enable", true),
        new("Disable", false)
    };

    #endregion
}
