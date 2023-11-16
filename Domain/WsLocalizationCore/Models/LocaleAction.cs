using WsLocalizationCore.Common;
namespace WsLocalizationCore.Models;

public sealed class LocaleAction : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string ActionAccessAllow => Lang == EnumLanguage.English ? "Access to actions allowed" : "Доступ к действиям разрешён";
    public string ActionAccessDeny => Lang == EnumLanguage.English ? "Access to actions denied" : "Доступ к действиям запрещён";
    public string ActionAccessNone => Lang == EnumLanguage.English ? "No access to the actions" : "Доступ к действиям не предусмотрен";
    public string ActionDataControl => Lang == EnumLanguage.English ? "Data control" : "Контроль данных";
    public string ActionInfo => Lang == EnumLanguage.English ? "Information" : "Информация";
    public string ActionSaveSuccess => Lang == EnumLanguage.English ? "Saving was successful" : "Сохранение выполнено успешно";
    public string ActionDataControlField => Lang == EnumLanguage.English ? "Need to fill in the field" : "Необходимо заполнить поле";
    public string ActionIsShowMarked => Lang == EnumLanguage.English ? "Archive records" : "Архивные записи";
    public string ActionIsSelectTopRowsCount(int count) => Lang == EnumLanguage.English ? $"First {count} records" : $"Первые {count} записей";
    public string ActionIsShowActivePlu => Lang == EnumLanguage.English ? $"Active plu" : $"Активные плу";
    public string ActionMethod => Lang == EnumLanguage.English ? "Method" : "Метод";

    #endregion
}