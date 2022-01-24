// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;

namespace DataShareCore
{
    public static class LocalizationCore
    {
        public static ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        public static class Strings
        {
            public static class Main
            {
                public static string PageError => Lang == ShareEnums.Lang.English ? @"Sorry, there's nothing at this address." : @"Извините, по этому адресу ничего нет.";
                public static string Company => Lang == ShareEnums.Lang.English ? @"Vladimir Standard" : @"Владимирский стандарт";
                public static string Index => Lang == ShareEnums.Lang.English ? @"Inside resources" : @"Внутренние ресурсы";
                public static string IndexDescription => Lang == ShareEnums.Lang.English ?
                    @"The site was created to help you navigate through the company's internal resources" :
                    @"Сайт создан для помощи в навигации по внутренним ресурсам компании";
                public static string CallbackTitle => Lang == ShareEnums.Lang.English ? @"Feedback" : @"Обратная связь";
                public static string DataLoading => Lang == ShareEnums.Lang.English ? @"Loading data..." : @"Загрузка данных...";
                public static string DataLoadComplete => Lang == ShareEnums.Lang.English ? @"Data downloaded successfully." : @"Данные загружены успешно.";
                public static string DataLoadError => Lang == ShareEnums.Lang.English ? @"Error loading data!" : @"Ошибка загрузки данных!";
                public static string IdentityError => Lang == ShareEnums.Lang.English ? @"User error!" : @"Ошибка пользователя";
                public static string NotLoad => Lang == ShareEnums.Lang.English ? @"Not load!" : @"Не загружено";
                public static string DebugMode => Lang == ShareEnums.Lang.English ? @"Debug mode" : @"Режим отладки";
                public static string ItemsCount => Lang == ShareEnums.Lang.English ? @"Count of records" : @"Количество записей";
                public static string ButtonWidth => Lang == ShareEnums.Lang.English ? @"Buttons width" : @"Ширина кнопок";
                public static string ButtonHeight => Lang == ShareEnums.Lang.English ? @"Buttons height" : @"Высота кнопок";
                public static string ServerDevelop => Lang == ShareEnums.Lang.English ? @"Debug server" : @"Сервер разработки";
                public static string ServerRelease => Lang == ShareEnums.Lang.English ? @"Debug release" : @"Промышленный сервер";
                public static string Authorization => Lang == ShareEnums.Lang.English ? @"Authorization" : @"Авторизация";
                public static string Doc => Lang == ShareEnums.Lang.English ? @"Documentation" : @"Документация";
                public static string ProgramVer => Lang == ShareEnums.Lang.English ? @"Program version" : @"Версия программы";
                public static string CoreVer => Lang == ShareEnums.Lang.English ? @"Core version" : @"Версия ядра";
                public static string AppSettings => Lang == ShareEnums.Lang.English ? @"App settings" : @"Настройки приложения";
                public static string DbInfo => Lang == ShareEnums.Lang.English ? @"Information about the DB" : @"Информация о БД";
                public static string UserSettings => Lang == ShareEnums.Lang.English ? @"User settings" : @"Пользовательские настройки";
                public static string SettingName => Lang == ShareEnums.Lang.English ? @"Setting name" : @"Название настройки";
                public static string SettingValue => Lang == ShareEnums.Lang.English ? @"Setting value" : @"Значение настройки";
                public static string RecordName => Lang == ShareEnums.Lang.English ? @"Setting name" : @"Название записи";
                public static string RecordValue => Lang == ShareEnums.Lang.English ? @"Setting value" : @"Значение записи";
                public static string HostName => Lang == ShareEnums.Lang.English ? @"Host name" : @"Имя хоста";
                public static string Language => Lang == ShareEnums.Lang.English ? @"Language" : @"Язык";
                public static string LanguageDetect => Lang == ShareEnums.Lang.English ? @"English" : @"Русский";
                public static string IsYes(bool isTrue) => Lang == ShareEnums.Lang.English ? isTrue ? @"Yes" : @"No" : isTrue ? @"Да" : @"Нет";
                public static string IsEnableIt(bool isTrue) => Lang == ShareEnums.Lang.English ? isTrue ? @"Enable" : @"Disable" : isTrue ? @"Включено" : @"Отключено";
                public static string IsEnableShe(bool isTrue) => Lang == ShareEnums.Lang.English ? isTrue ? @"Enable" : @"Disable" : isTrue ? @"Включена" : @"Отключена";
                public static string IsEnableHe(bool isTrue) => Lang == ShareEnums.Lang.English ? isTrue ? @"Enable" : @"Disable" : isTrue ? @"Включен" : @"Отключен";
                public static string AuthorizingProcess => Lang == ShareEnums.Lang.English ? "Authorizing ..." : "Авторизация ...";
                public static string AuthorizingSuccess => Lang == ShareEnums.Lang.English ? "Success authorized" : "Успешная авторизация";
                public static string AuthorizingNot => Lang == ShareEnums.Lang.English ? "Not authorized!" : "Авторизация провалена!";
                public static string AuthorizingUserName => Lang == ShareEnums.Lang.English ? "User name" : "Имя пользователя";
                public static string AuthorizingAccessLevel => Lang == ShareEnums.Lang.English ? "Access level" : "Уровень доступа";
                public static string AuthorizingAccessLevelNull => Lang == ShareEnums.Lang.English ? "No access" : "Нет доступа";
                public static string AuthorizingAccessLevelFalse => Lang == ShareEnums.Lang.English ? "Read only" : "Только чтение";
                public static string AuthorizingAccessLevelTrue => Lang == ShareEnums.Lang.English ? "Read and write" : "Чтение и запись";
            }
            #region Menu
            public static string MenuReferences => Lang == ShareEnums.Lang.English ? @"References" : @"Справочники";
            public static string FileChoose => Lang == ShareEnums.Lang.English ? @"Select a file" : @"Выбрать файл";
            public static string FileUpload => Lang == ShareEnums.Lang.English ? @"Upload a file" : @"Загрузить файл";
            public static string FileDownload => Lang == ShareEnums.Lang.English ? @"Download a file" : @"Скачать файл";
            public static string FileDialog => Lang == ShareEnums.Lang.English ? @"File dialog" : @"Файловый диалог";
            public static string FileSaveDialog => Lang == ShareEnums.Lang.English ? @"Specify the file name to save" : @"Указать имя файла для сохранения";
            public static string ServerResponse => Lang == ShareEnums.Lang.English ? @"Server response" : @"Ответ сервера";
            public static string MenuSecurity => Lang == ShareEnums.Lang.English ? @"Security" : @"Безопасность";
            public static string Login => Lang == ShareEnums.Lang.English ? @"Login" : @"Логин";
            public static string MenuAccess => Lang == ShareEnums.Lang.English ? @"Menu access" : @"Доступ к меню";
            public static string MenuAccessDeny => Lang == ShareEnums.Lang.English ? @"Menu access denied" : @"Доступ к меню запрещён";
            public static string MenuAccessAllow => Lang == ShareEnums.Lang.English ? @"Menu access allowed" : @"Доступ к меню разрешён";
            public static string MenuInfo => Lang == ShareEnums.Lang.English ? @"Info" : @"Информация";
            #endregion
            #region Action
            public static string ActionAccessNone => Lang == ShareEnums.Lang.English ? @"No access to the actions" : @"Доступ к действиям не предусмотрен";
            public static string ActionAccessDeny => Lang == ShareEnums.Lang.English ? @"Access to actions denied" : @"Доступ к действиям запрещён";
            public static string ActionAccessAllow => Lang == ShareEnums.Lang.English ? @"Access to actions allowed" : @"Доступ к действиям разрешён";
            public static string Method => Lang == ShareEnums.Lang.English ? @"Method" : @"Метод";
            public static string DataControl => Lang == ShareEnums.Lang.English ? @"Data control" : @"Контроль данных";
            public static string DataControlField => Lang == ShareEnums.Lang.English ? @"Need to fill in the field" : @"Необходимо заполнить поле";
            #endregion
            #region Section
            public static string SectionAccessNone => Lang == ShareEnums.Lang.English ? @"No access to the section" : @"Доступ к разделу не предусмотрен";
            public static string SectionAccessDeny => Lang == ShareEnums.Lang.English ? @"Section access denied" : @"Доступ к разделу запрещён";
            public static string SectionAccessAllow => Lang == ShareEnums.Lang.English ? @"Section access allowed" : @"Доступ к разделу разрешён";
            #endregion
            #region Item
            public static string ItemAccessNone => Lang == ShareEnums.Lang.English ? @"No access to the item" : @"Доступ к разделу не предусмотрен";
            public static string ItemAccessDeny => Lang == ShareEnums.Lang.English ? @"Item access denied" : @"Доступ к элементу запрещён";
            public static string ItemAccessAllow => Lang == ShareEnums.Lang.English ? @"Item access allowed" : @"Доступ к элементу разрешён";
            #region Items
            public static string ItemAccess => Lang == ShareEnums.Lang.English ? @"Access" : @"Доступ";
            public static string ItemLog => Lang == ShareEnums.Lang.English ? @"Log" : @"Лог";
            #endregion
            #region Sections
            public static string SectionAccess => Lang == ShareEnums.Lang.English ? @"Access" : @"Доступ";
            public static string SectionLog => Lang == ShareEnums.Lang.English ? @"Log" : @"Лог";
            #endregion
            #endregion
            #region Chart
            public static string Chart => Lang == ShareEnums.Lang.English ? @"Chart" : @"Диаграмма";
            public static string ChartSmooth => Lang == ShareEnums.Lang.English ? @"Chart smooth" : @"Скруглить";
            #endregion
            #region System
            public static string SysAccess => Lang == ShareEnums.Lang.English ? @"Access" : @"Доступ";
            public static string SysAccount => Lang == ShareEnums.Lang.English ? @"Account" : @"Аккаунт";
            public static string SysLogin => Lang == ShareEnums.Lang.English ? @"Log in" : @"Вход";
            public static string SysInfo => Lang == ShareEnums.Lang.English ? @"Info" : @"Информация";
            public static string SysLogs => Lang == ShareEnums.Lang.English ? @"Logs" : @"Логи";
            #endregion
            #region Table
            public static string TableTab => Lang == ShareEnums.Lang.English ? @"Switch between panels" : @"Переключиться между панелями";
            public static string TableRead => Lang == ShareEnums.Lang.English ? @"Read data" : @"Прочитать данные";
            public static string TableReadCancel => Lang == ShareEnums.Lang.English ? @"Cancel data reading" : @"Отмена чтения данных";
            public static string TableEdit => Lang == ShareEnums.Lang.English ? @"Edit record" : @"Редактировать запись";
            public static string TableClear => Lang == ShareEnums.Lang.English ? @"Deactivate active record" : @"Деактивировать активную запись";
            public static string TableCreate => Lang == ShareEnums.Lang.English ? @"Create record" : @"Создать запись";
            public static string TableDelete => Lang == ShareEnums.Lang.English ? @"Delete record" : @"Удалить запись";
            public static string TableSelect => Lang == ShareEnums.Lang.English ? @"Highlight record" : @"Выделить запись";
            public static string TableIncludes => Lang == ShareEnums.Lang.English ? @"Included records" : @"Вложенные записи";
            public static string TableRecordSave => Lang == ShareEnums.Lang.English ? @"Save record" : @"Сохранить запись";
            public static string TableRecordCancel => Lang == ShareEnums.Lang.English ? @"Close record" : @"Закрыть запись";
            public static string TablePluHavingPlu => Lang == ShareEnums.Lang.English ? @"The PLU table already has this number" : @"Таблица PLU уже имеет такой номер";
            #endregion
            #region Table fields
            public static string FieldCategory => Lang == ShareEnums.Lang.English ? @"Category" : @"Категория";
            public static string FieldCount => Lang == ShareEnums.Lang.English ? @"Count" : @"Количество";
            public static string FieldCreated => Lang == ShareEnums.Lang.English ? @"Created" : @"Создано";
            public static string FieldIdRRef => Lang == ShareEnums.Lang.English ? @"ID 1C" : @"ID 1С";
            public static string FieldIpAddress => Lang == ShareEnums.Lang.English ? @"Ip-address" : @"IP-адрес";
            public static string FieldIsEmpty => Lang == ShareEnums.Lang.English ? @"Empty field" : @"Пустое поле";
            public static string FieldModified => Lang == ShareEnums.Lang.English ? @"Modified" : @"Изменено";
            public static string FieldName => Lang == ShareEnums.Lang.English ? @"Name" : @"Наименование";
            #endregion
            #region Dialog
            public static string DialogQuestion => Lang == ShareEnums.Lang.English ? @"Perform the operation?" : @"Выполнить операцию?";
            public static string DialogButtonYes => Lang == ShareEnums.Lang.English ? @"Yes" : @"Да";
            public static string DialogButtonCancel => Lang == ShareEnums.Lang.English ? @"Cancel" : @"Отмена";
            public static string DialogButtonNo => Lang == ShareEnums.Lang.English ? @"No" : @"Нет";
            public static string DialogResultSuccess => Lang == ShareEnums.Lang.English ? @"The operation was performed successfully." : @"Операция выполнена успешно.";
            public static string DialogResultCancel => Lang == ShareEnums.Lang.English ?
                @"Cancel operation. The necessary conditions may not have been met." :
                @"Отмена операции. Возможно, не выполнены необходимые условия.";
            public static string DialogResultFail => Lang == ShareEnums.Lang.English ? @"Operation error!" : @"Ошибка выполнения операции!";
            #endregion
            #region SQL
            public static string SqlServer => Lang == ShareEnums.Lang.English ? @"SQL-server" : @"SQL-сервер";
            public static string SqlDb => Lang == ShareEnums.Lang.English ? @"SQL-DB" : @"SQL-БД";
            public static string SqlDbCurSize => Lang == ShareEnums.Lang.English ? @"DB size" : @"Размер БД";
            public static string SqlDbMaxSize => Lang == ShareEnums.Lang.English ? @"DB size" : @"Максимальный размер БД";
            public static string SqlDbFillSize => Lang == ShareEnums.Lang.English ? @"DB fill percentage" : @"Процент заполнения БД";
            public static string SqlUser => Lang == ShareEnums.Lang.English ? @"SQL-user" : @"SQL-пользователь";
            #endregion
            #region Windows
            public static string WindowsUser => Lang == ShareEnums.Lang.English ? @"Windows-user" : @"Windows-пользователь";
            #endregion
            #region URI system
            public const string UriRouteRoot = "/";
            public const string UriRouteSystemAccount = "/account";
            public const string UriRouteSystemLogs = "/system/logs";
            public const string UriRouteSystemAccess = "/system/access";
            public const string UriRouteSystemDocs = "/system/docs";
            public const string UriRouteSystemInfo = "/system/info";
            public const string UriRouteSystemSecurity = "/system/security";
            #endregion
            #region Input control
            public const string InputControlMuchZero = @"Значение должно быть больше 0";
            #endregion
            #region Memory
            public static string MemoryTitle => Lang == ShareEnums.Lang.English ? @"Application memory manager" : @"Менеджер памяти приложения";
            public static string MemoryLimit => Lang == ShareEnums.Lang.English ? @"Memory limit" : @"Лимит памяти";
            public static string MemoryLimitNotSet => Lang == ShareEnums.Lang.English ? @"Memory limit not set!" : @"Лимит памяти не задан!";
            public static string MemoryUsed => Lang == ShareEnums.Lang.English ? @"Memory" : @"Память";
            public static string MemoryPhysical => Lang == ShareEnums.Lang.English ? @"Physical memory" : @"Физическая память";
            public static string MemoryVirtual => Lang == ShareEnums.Lang.English ? @"Virtual memory" : @"Виртуальная память";
            public static string MemoryIsExecute => Lang == ShareEnums.Lang.English ? @"Application memory manager at work." : @"Менеджер памяти приложения в работе.";
            public static string MemoryIsNotExecute => Lang == ShareEnums.Lang.English ? @"The application memory manager is not running!" : @"Менеджер памяти приложения не выполняется!";
            public static string MemoryResult => Lang == ShareEnums.Lang.English ? @"Result" : @"Результат";
            public static string MemoryException => Lang == ShareEnums.Lang.English ? @"Memory manager error" : @"Ошибка менеджера памяти";
            public static string MemoryActionStart => Lang == ShareEnums.Lang.English ? @"Run the memory manager" : @"Запустить менеджер памяти";
            public static string MemoryActionStop => Lang == ShareEnums.Lang.English ? @"Stop the memory manager" : @"Остановить менеджер памяти";
            #endregion
        }

        public static class Methods
        {
            #region Public and private methods

            public static string GetAppVersion(System.Reflection.Assembly executingAssembly)
            {
                FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
                string result = fieVersionInfo.FileVersion;
                if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
                    result = result.Substring(0, result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase));
                return result;
            }

            public static string GetCoreVersion()
            {
                FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string result = fieVersionInfo.FileVersion;
                if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
                    result = result.Substring(0, result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase));
                return result;
            }

            #endregion
        }
    }
}
