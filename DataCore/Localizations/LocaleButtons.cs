// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleButtons
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleButtons _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleButtons Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string Abort => Lang == LangEnum.English ? "Abort" : "Прервать";
    public string Apply => Lang == LangEnum.English ? "Apply" : "Применить";
    public string Cancel => Lang == LangEnum.English ? "Cancel" : "Отмена";
    public string Clear => Lang == LangEnum.English ? "Clear" : "Очистить";
    public string Close => Lang == LangEnum.English ? "Close" : "Закрыть";
    public string Custom => Lang == LangEnum.English ? "Custom" : "Кастом";
    public string Enter => Lang == LangEnum.English ? "Enter" : "Ввод";
    public string Ignore => Lang == LangEnum.English ? "Ignore" : "Игнорировать";
    public string Next => Lang == LangEnum.English ? "Next" : "Следующие";
    public string No => Lang == LangEnum.English ? "No" : "Нет";
    public string Ok => Lang == LangEnum.English ? "Ok" : "Ок";
    public string Previous => Lang == LangEnum.English ? "Previous" : "Предыдущие";
    public string Retry => Lang == LangEnum.English ? "Retry" : "Повторить";
    public string Yes => Lang == LangEnum.English ? "Yes" : "Да";
    
    #endregion
}
