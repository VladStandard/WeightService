namespace Ws.DataCore.Models;

[DebuggerDisplay("{ToString()}")]
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
        $"{nameof(GetTemplateLanguagesRus)}: {GetTemplateLanguagesRus().Count}";
    
    public List<string> GetTemplateCategories() =>
        new()
        {
            "NaN",
            "203 dpi",
            "203 dpi tsc",
            "300 dpi",
            "300 dpi tsc",
            "608 dpi",
            "608 dpi tsc",
            "zpl"
        };

    public List<EnumTypeModel<EnumLanguage>> GetTemplateLanguages() => LocaleCore.Lang switch
    {
        EnumLanguage.English => GetTemplateLanguagesEng(),
        EnumLanguage.Russian => GetTemplateLanguagesRus(),
        _ => new()
    };

    private List<EnumTypeModel<EnumLanguage>> GetTemplateLanguagesEng() => new()
    {
        new($"{EnumLanguage.English}", EnumLanguage.English),
        new($"{EnumLanguage.Russian}", EnumLanguage.Russian)
    };

    private List<EnumTypeModel<EnumLanguage>> GetTemplateLanguagesRus() => new()
    {
        new("Английский", EnumLanguage.English),
        new("Русский", EnumLanguage.Russian)
    };

    #endregion
}