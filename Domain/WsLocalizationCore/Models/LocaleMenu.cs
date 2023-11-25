namespace WsLocalizationCore.Models;

public sealed class LocaleMenu : LocalizationBase
{
    #region Public and private fields, properties, constructor
    
    public string MenuDbVersionHistory => Lang == EnumLanguage.English ? "DB version history" : "История версий БД";
    public string MenuReports => Lang == EnumLanguage.English ? "Diagnostic" : "Диагностика";

    #endregion
}