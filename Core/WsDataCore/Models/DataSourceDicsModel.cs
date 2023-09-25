namespace WsDataCore.Models;

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

    public List<WsEnumTypeModel<WsEnumLanguage>> GetTemplateLanguages() => WsLocaleCore.Lang switch
    {
        WsEnumLanguage.English => GetTemplateLanguagesEng(),
        WsEnumLanguage.Russian => GetTemplateLanguagesRus(),
        _ => new()
    };

    private List<WsEnumTypeModel<WsEnumLanguage>> GetTemplateLanguagesEng() => new()
    {
        new($"{WsEnumLanguage.English}", WsEnumLanguage.English),
        new($"{WsEnumLanguage.Russian}", WsEnumLanguage.Russian)
    };

    private List<WsEnumTypeModel<WsEnumLanguage>> GetTemplateLanguagesRus() => new()
    {
        new("Английский", WsEnumLanguage.English),
        new("Русский", WsEnumLanguage.Russian)
    };

    #endregion
}