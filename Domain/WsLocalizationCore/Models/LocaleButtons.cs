using WsLocalizationCore.Common;
namespace WsLocalizationCore.Models;

public sealed class LocaleButtons : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string Abort => Lang == EnumLanguage.English ? "Abort" : "Прервать";
    public string Apply => Lang == EnumLanguage.English ? "Apply" : "Применить";
    public string Back => Lang == EnumLanguage.English ? "Back" : "Назад";
    public string Cancel => Lang == EnumLanguage.English ? "Cancel" : "Отмена";
    public string Clear => Lang == EnumLanguage.English ? "Clear" : "Очистить";
    public string Close => Lang == EnumLanguage.English ? "Close" : "Закрыть";
    public string Custom => Lang == EnumLanguage.English ? "Custom" : "Кастом";
    public string Enter => Lang == EnumLanguage.English ? "Enter" : "Ввод";
    public string Forward => Lang == EnumLanguage.English ? "Forward" : "Вперёд";
    public string Ignore => Lang == EnumLanguage.English ? "Ignore" : "Игнорировать";
    public string Next => Lang == EnumLanguage.English ? "Next" : "Следующие";
    public string No => Lang == EnumLanguage.English ? "No" : "Нет";
    public string Ok => Lang == EnumLanguage.English ? "Ok" : "Ок";
    public string Previous => Lang == EnumLanguage.English ? "Previous" : "Предыдущие";
    public string Retry => Lang == EnumLanguage.English ? "Retry" : "Повторить";
    public string Yes => Lang == EnumLanguage.English ? "Yes" : "Да";

    #endregion
}