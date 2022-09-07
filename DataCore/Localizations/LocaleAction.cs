// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleAction
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleAction _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleAction Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string ActionAccessAllow => Lang == LangEnum.English ? "Access to actions allowed" : "Доступ к действиям разрешён";
    public string ActionAccessDeny => Lang == LangEnum.English ? "Access to actions denied" : "Доступ к действиям запрещён";
    public string ActionAccessNone => Lang == LangEnum.English ? "No access to the actions" : "Доступ к действиям не предусмотрен";
    public string ActionDataControl => Lang == LangEnum.English ? "Data control" : "Контроль данных";
    public string ActionInfo => Lang == LangEnum.English ? "Information" : "Информация";
    public string ActionSaveSuccess => Lang == LangEnum.English ? "Saving was successful" : "Сохранение выполнено успешно";
    public string ActionDataControlField => Lang == LangEnum.English ? "Need to fill in the field" : "Необходимо заполнить поле";
    public string ActionIsShowMarked => Lang == LangEnum.English ? "Archive records" : "Архивные записи";
    public string ActionIsSelectTopRowsCount(int count) => Lang == LangEnum.English ? $"First {count} records" : $"Первые {count} записей";
    public string ActionMethod => Lang == LangEnum.English ? "Method" : "Метод";

    #endregion
}
