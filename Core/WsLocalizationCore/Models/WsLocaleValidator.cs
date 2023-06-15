// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class WsLocaleValidator : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public string EqualsDefault => Lang == WsEnumLanguage.English ? "Equals default" : "Равно по умолчанию";
    public string Error => Lang == WsEnumLanguage.English ? "Error" : "Ошибка";
    public string FailedValidation => Lang == WsEnumLanguage.English ? "failed validation" : "неудачная валидация";
    public string Property => Lang == WsEnumLanguage.English ? "Property" : "Свойство";

    #endregion
}