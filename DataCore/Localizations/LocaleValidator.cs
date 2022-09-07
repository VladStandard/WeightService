// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleValidator
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleValidator _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleValidator Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string EqualsDefault => Lang == LangEnum.English ? "Equals default" : "Равно по умолчанию";
    public string Error => Lang == LangEnum.English ? "Error" : "Ошибка";
    public string FailedValidation => Lang == LangEnum.English ? "failed validation" : "неудачная валидация";
    public string Property => Lang == LangEnum.English ? "Property" : "Свойство";

    #endregion
}
