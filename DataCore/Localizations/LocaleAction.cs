// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Localizations
{
    public class LocaleAction
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LocaleAction _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LocaleAction Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        #region Public and private fields, properties, constructor

        public string ActionAccessAllow => Lang == ShareEnums.Lang.English ? "Access to actions allowed" : "Доступ к действиям разрешён";
        public string ActionAccessDeny => Lang == ShareEnums.Lang.English ? "Access to actions denied" : "Доступ к действиям запрещён";
        public string ActionAccessNone => Lang == ShareEnums.Lang.English ? "No access to the actions" : "Доступ к действиям не предусмотрен";
        public string ActionDataControl => Lang == ShareEnums.Lang.English ? "Data control" : "Контроль данных";
        public string ActionInfo => Lang == ShareEnums.Lang.English ? "Information" : "Информация";
        public string ActionSaveSuccess => Lang == ShareEnums.Lang.English ? "Saving was successful" : "Сохранение выполнено успешно";
        public string ActionDataControlField => Lang == ShareEnums.Lang.English ? "Need to fill in the field" : "Необходимо заполнить поле";
        public string ActionIsShowMarked => Lang == ShareEnums.Lang.English ? "Archive records" : "Архивные записи";
        public string ActionIsSelectTopRowsCount(int count) => Lang == ShareEnums.Lang.English ? $"First {count} records" : $"Первые {count} записей";
        public string ActionMethod => Lang == ShareEnums.Lang.English ? "Method" : "Метод";

        #endregion
    }
}
