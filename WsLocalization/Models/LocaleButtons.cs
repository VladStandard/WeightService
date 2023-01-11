// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalization.Models;

public class LocaleButtons
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleButtons _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleButtons Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

    #region Public and private fields, properties, constructor

    public string Abort => Lang == Lang.English ? "Abort" : "Прервать";
    public string Apply => Lang == Lang.English ? "Apply" : "Применить";
    public string Cancel => Lang == Lang.English ? "Cancel" : "Отмена";
    public string Clear => Lang == Lang.English ? "Clear" : "Очистить";
    public string Close => Lang == Lang.English ? "Close" : "Закрыть";
    public string Custom => Lang == Lang.English ? "Custom" : "Кастом";
    public string Enter => Lang == Lang.English ? "Enter" : "Ввод";
    public string Ignore => Lang == Lang.English ? "Ignore" : "Игнорировать";
    public string Next => Lang == Lang.English ? "Next" : "Следующие";
    public string No => Lang == Lang.English ? "No" : "Нет";
    public string Ok => Lang == Lang.English ? "Ok" : "Ок";
    public string Previous => Lang == Lang.English ? "Previous" : "Предыдущие";
    public string Retry => Lang == Lang.English ? "Retry" : "Повторить";
    public string Yes => Lang == Lang.English ? "Yes" : "Да";

    #endregion
}