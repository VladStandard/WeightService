// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Common;

namespace WsLocalizationCore.Models;

public sealed class LocaleButtons : WsLocalizationBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleButtons _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleButtons Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public string Abort => Lang == WsEnumLanguage.English ? "Abort" : "Прервать";
    public string Apply => Lang == WsEnumLanguage.English ? "Apply" : "Применить";
    public string Back => Lang == WsEnumLanguage.English ? "Back" : "Назад";
    public string Cancel => Lang == WsEnumLanguage.English ? "Cancel" : "Отмена";
    public string Clear => Lang == WsEnumLanguage.English ? "Clear" : "Очистить";
    public string Close => Lang == WsEnumLanguage.English ? "Close" : "Закрыть";
    public string Custom => Lang == WsEnumLanguage.English ? "Custom" : "Кастом";
    public string Enter => Lang == WsEnumLanguage.English ? "Enter" : "Ввод";
    public string Forward => Lang == WsEnumLanguage.English ? "Forward" : "Вперёд";
    public string Ignore => Lang == WsEnumLanguage.English ? "Ignore" : "Игнорировать";
    public string Next => Lang == WsEnumLanguage.English ? "Next" : "Следующие";
    public string No => Lang == WsEnumLanguage.English ? "No" : "Нет";
    public string Ok => Lang == WsEnumLanguage.English ? "Ok" : "Ок";
    public string Previous => Lang == WsEnumLanguage.English ? "Previous" : "Предыдущие";
    public string Retry => Lang == WsEnumLanguage.English ? "Retry" : "Повторить";
    public string Yes => Lang == WsEnumLanguage.English ? "Yes" : "Да";

    #endregion
}