namespace WsLocalizationCore.Models;

public sealed class LocaleValidator : LocalizationBase
{
    #region Public and private fields, properties, constructor
    
    public string Error => Lang == EnumLanguage.English ? "Error" : "Ошибка";
    public string FailedValidation => Lang == EnumLanguage.English ? "failed validation" : "неудачная валидация";
    public string Property => Lang == EnumLanguage.English ? "Property" : "Свойство";

    #endregion
}