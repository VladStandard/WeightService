// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;

namespace DataShareCore
{
    public static class LocalizationCore
    {
        public static EnumLang Lang { get; set; } = EnumLang.Russian;

        public static class Strings
        {
            public static class Main
            {
                public static string PageError => Lang == EnumLang.English ? @"Sorry, there's nothing at this address." : @"Извините, по этому адресу ничего нет.";
                public static string Company => Lang == EnumLang.English ? @"Vladimir Standard" : @"Владимирский стандарт";
                public static string Index => Lang == EnumLang.English ? @"Inside resources" : @"Внутренние ресурсы";
                public static string IndexDescription => Lang == EnumLang.English ?
                    @"The site was created to help you navigate through the company's internal resources" :
                    @"Сайт создан для помощи в навигации по внутренним ресурсам компании";
                public static string CallbackTitle => Lang == EnumLang.English ? @"Feedback" : @"Обратная связь";
                public static string DataLoading => Lang == EnumLang.English ? @"Loading data..." : @"Загрузка данных...";
                public static string DataLoadComplete => Lang == EnumLang.English ? @"Data downloaded successfully." : @"Данные загружены успешно.";
                public static string DataLoadError => Lang == EnumLang.English ? @"Error loading data!" : @"Ошибка загрузки данных!";
                public static string IdentityError => Lang == EnumLang.English ? @"User error!" : @"Ошибка пользователя";
                public static string NotLoad => Lang == EnumLang.English ? @"Not load!" : @"Не загружено";
                public static string DebugMode => Lang == EnumLang.English ? @"Debug mode" : @"Режим отладки";
                public static string ItemsCount => Lang == EnumLang.English ? @"Count of records" : @"Количество записей";
                public static string ButtonWidth => Lang == EnumLang.English ? @"Buttons width" : @"Ширина кнопок";
                public static string ButtonHeight => Lang == EnumLang.English ? @"Buttons height" : @"Высота кнопок";
                public static string ServerDevelop => Lang == EnumLang.English ? @"Debug server" : @"Сервер разработки";
                public static string ServerRelease => Lang == EnumLang.English ? @"Debug release" : @"Промышленный сервер";
                public static string Authorization => Lang == EnumLang.English ? @"Authorization" : @"Авторизация";
                public static string Doc => Lang == EnumLang.English ? @"Documentation" : @"Документация";
                public static string ProgramVer => Lang == EnumLang.English ? @"Program version" : @"Версия программы";
                public static string CoreVer => Lang == EnumLang.English ? @"Core version" : @"Версия ядра";
                public static string AppSettings => Lang == EnumLang.English ? @"App settings" : @"Настройки приложения";
                public static string UserSettings => Lang == EnumLang.English ? @"User settings" : @"Пользовательские настройки";
                public static string SettingName => Lang == EnumLang.English ? @"Setting name" : @"Название настройки";
                public static string SettingValue => Lang == EnumLang.English ? @"Setting value" : @"Значение настройки";
                public static string RecordName => Lang == EnumLang.English ? @"Setting name" : @"Название записи";
                public static string RecordValue => Lang == EnumLang.English ? @"Setting value" : @"Значение записи";
                public static string HostName => Lang == EnumLang.English ? @"Host name" : @"Имя хоста";
                public static string Language => Lang == EnumLang.English ? @"Language" : @"Язык";
                public static string LanguageDetect => Lang == EnumLang.English ? @"English" : @"Русский";
                public static string IsYes(bool isTrue) => Lang == EnumLang.English ? isTrue ? @"Yes" : @"No" : isTrue ? @"Да" : @"Нет";
                public static string IsEnableIt(bool isTrue) => Lang == EnumLang.English ? isTrue ? @"Enable" : @"Disable" : isTrue ? @"Включено" : @"Отключено";
                public static string IsEnableShe(bool isTrue) => Lang == EnumLang.English ? isTrue ? @"Enable" : @"Disable" : isTrue ? @"Включена" : @"Отключена";
                public static string IsEnableHe(bool isTrue) => Lang == EnumLang.English ? isTrue ? @"Enable" : @"Disable" : isTrue ? @"Включен" : @"Отключен";
            }
            #region Menu
            public static string MenuReferences => Lang == EnumLang.English ? @"References" : @"Справочники";
            public static string FileChoose => Lang == EnumLang.English ? @"Select a file" : @"Выбрать файл";
            public static string FileUpload => Lang == EnumLang.English ? @"Upload a file" : @"Загрузить файл";
            public static string FileDownload => Lang == EnumLang.English ? @"Download a file" : @"Скачать файл";
            public static string FileDialog => Lang == EnumLang.English ? @"File dialog" : @"Файловый диалог";
            public static string FileSaveDialog => Lang == EnumLang.English ? @"Specify the file name to save" : @"Указать имя файла для сохранения";
            public static string ServerResponse => Lang == EnumLang.English ? @"Server response" : @"Ответ сервера";
            public static string MenuSecurity => Lang == EnumLang.English ? @"Security" : @"Безопасность";
            public static string Login => Lang == EnumLang.English ? @"Login" : @"Логин";
            public static string MenuAccess => Lang == EnumLang.English ? @"Menu access" : @"Доступ к меню";
            public static string MenuAccessDeny => Lang == EnumLang.English ? @"Menu access denied" : @"Доступ к меню запрещён";
            public static string MenuAccessAllow => Lang == EnumLang.English ? @"Menu access allowed" : @"Доступ к меню разрешён";
            public static string MenuInfo => Lang == EnumLang.English ? @"Info" : @"Информация";
            #endregion
            #region Action
            public static string ActionAccessNone => Lang == EnumLang.English ? @"No access to the actions" : @"Доступ к действиям не предусмотрен";
            public static string ActionAccessDeny => Lang == EnumLang.English ? @"Access to actions denied" : @"Доступ к действиям запрещён";
            public static string ActionAccessAllow => Lang == EnumLang.English ? @"Access to actions allowed" : @"Доступ к действиям разрешён";
            public static string Method => Lang == EnumLang.English ? @"Method" : @"Метод";
            public static string DataControl => Lang == EnumLang.English ? @"Data control" : @"Контроль данных";
            public static string DataControlField => Lang == EnumLang.English ? @"Need to fill in the field" : @"Необходимо заполнить поле";
            #endregion
            #region Section
            public static string SectionAccessNone => Lang == EnumLang.English ? @"No access to the section" : @"Доступ к разделу не предусмотрен";
            public static string SectionAccessDeny => Lang == EnumLang.English ? @"Section access denied" : @"Доступ к разделу запрещён";
            public static string SectionAccessAllow => Lang == EnumLang.English ? @"Section access allowed" : @"Доступ к разделу разрешён";
            #endregion
            #region Item
            public static string ItemAccessNone => Lang == EnumLang.English ? @"No access to the item" : @"Доступ к разделу не предусмотрен";
            public static string ItemAccessDeny => Lang == EnumLang.English ? @"Item access denied" : @"Доступ к элементу запрещён";
            public static string ItemAccessAllow => Lang == EnumLang.English ? @"Item access allowed" : @"Доступ к элементу разрешён";
            #region Items
            public static string ItemAccess => Lang == EnumLang.English ? @"Access" : @"Доступ";
            public static string ItemLog => Lang == EnumLang.English ? @"Log" : @"Лог";
            #endregion
            #region Sections
            public static string SectionAccess => Lang == EnumLang.English ? @"Access" : @"Доступ";
            public static string SectionLog => Lang == EnumLang.English ? @"Log" : @"Лог";
            #endregion
            #endregion
            #region Chart
            public static string Chart => Lang == EnumLang.English ? @"Chart" : @"Диаграмма";
            public static string ChartSmooth => Lang == EnumLang.English ? @"Chart smooth" : @"Скруглить";
            #endregion
            #region System
            public static string SysAccess => Lang == EnumLang.English ? @"Access" : @"Доступ";
            public static string SysInfo => Lang == EnumLang.English ? @"Info" : @"Информация";
            public static string SysLogs => Lang == EnumLang.English ? @"Logs" : @"Логи";
            #endregion
            #region Table
            public static string TableTab => Lang == EnumLang.English ? @"Switch between panels" : @"Переключиться между панелями";
            public static string TableRead => Lang == EnumLang.English ? @"Read data" : @"Прочитать данные";
            public static string TableReadCancel => Lang == EnumLang.English ? @"Cancel data reading" : @"Отмена чтения данных";
            public static string TableEdit => Lang == EnumLang.English ? @"Edit record" : @"Редактировать запись";
            public static string TableClear => Lang == EnumLang.English ? @"Deactivate active record" : @"Деактивировать активную запись";
            public static string TableCreate => Lang == EnumLang.English ? @"Create record" : @"Создать запись";
            public static string TableDelete => Lang == EnumLang.English ? @"Delete record" : @"Удалить запись";
            public static string TableSelect => Lang == EnumLang.English ? @"Highlight record" : @"Выделить запись";
            public static string TableIncludes => Lang == EnumLang.English ? @"Included records" : @"Вложенные записи";
            public static string TableRecordSave => Lang == EnumLang.English ? @"Save record" : @"Сохранить запись";
            public static string TableRecordCancel => Lang == EnumLang.English ? @"Close record" : @"Закрыть запись";
            #endregion
            #region Table fields
            public static string FieldCount => Lang == EnumLang.English ? @"Count" : @"Количество";
            public static string FieldCreated => Lang == EnumLang.English ? @"Created" : @"Создано";
            public static string FieldModified => Lang == EnumLang.English ? @"Modified" : @"Изменено";
            #endregion
            #region Dialog
            public static string DialogQuestion => Lang == EnumLang.English ? @"Perform the operation?" : @"Выполнить операцию?";
            public static string DialogButtonYes => Lang == EnumLang.English ? @"Yes" : @"Да";
            public static string DialogButtonCancel => Lang == EnumLang.English ? @"Cancel" : @"Отмена";
            public static string DialogButtonNo => Lang == EnumLang.English ? @"No" : @"Нет";
            public static string DialogResultSuccess => Lang == EnumLang.English ? @"The operation was performed successfully." : @"Операция выполнена успешно.";
            public static string DialogResultCancel => Lang == EnumLang.English ?
                @"Cancel operation. The necessary conditions may not have been met." :
                @"Отмена операции. Возможно, не выполнены необходимые условия.";
            public static string DialogResultFail => Lang == EnumLang.English ? @"Operation error!" : @"Ошибка выполнения операции!";
            #endregion
            #region SQL
            public static string SqlServer => Lang == EnumLang.English ? @"SQL-server" : @"SQL-сервер";
            public static string SqlDb => Lang == EnumLang.English ? @"SQL-DB" : @"SQL-БД";
            public static string SqlUser => Lang == EnumLang.English ? @"SQL-user" : @"SQL-пользователь";
            #endregion
            #region Windows
            public static string WindowsUser => Lang == EnumLang.English ? @"Windows-user" : @"Windows-пользователь";
            #endregion
            #region URI system
            public const string UriRouteRoot = "/";
            public const string UriRouteSystemLogs = "/system/logs";
            public const string UriRouteSystemAccess = "/system/access";
            public const string UriRouteSystemDocs = "/system/docs";
            public const string UriRouteSystemInfo = "/system/info";
            public const string UriRouteSystemLogin = "/system/login";
            public const string UriRouteSystemSecurity = "/system/security";
            #endregion
            #region Input control
            public const string InputControlMuchZero = @"Значение должно быть больше 0";
            #endregion
            #region Memory
            public static string MemoryTitle => Lang == EnumLang.English ? @"Application memory manager" : @"Менеджер памяти приложения";
            public static string MemoryLimit => Lang == EnumLang.English ? @"Memory limit" : @"Лимит памяти";
            public static string MemoryLimitNotSet => Lang == EnumLang.English ? @"Memory limit not set!" : @"Лимит памяти не задан!";
            public static string MemoryUsed => Lang == EnumLang.English ? @"Memory" : @"Память";
            public static string MemoryPhysical => Lang == EnumLang.English ? @"Physical memory" : @"Физическая память";
            public static string MemoryVirtual => Lang == EnumLang.English ? @"Virtual memory" : @"Виртуальная память";
            public static string MemoryIsExecute => Lang == EnumLang.English ? @"Application memory manager at work." : @"Менеджер памяти приложения в работе.";
            public static string MemoryIsNotExecute => Lang == EnumLang.English ? @"The application memory manager is not running!" : @"Менеджер памяти приложения не выполняется!";
            public static string MemoryResult => Lang == EnumLang.English ? @"Result" : @"Результат";
            public static string MemoryException => Lang == EnumLang.English ? @"Memory manager error" : @"Ошибка менеджера памяти";
            public static string MemoryActionStart => Lang == EnumLang.English ? @"Run the memory manager" : @"Запустить менеджер памяти";
            public static string MemoryActionStop => Lang == EnumLang.English ? @"Stop the memory manager" : @"Остановить менеджер памяти";
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
