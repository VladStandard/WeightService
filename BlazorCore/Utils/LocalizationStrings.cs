using System;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace BlazorCore.Utils
{
    public static class LocalizationStrings
    {
        public static EnumLang Lang { get; set; } = EnumLang.Russian;

        public static class Share
        {
            #region Main
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
            public static string SectionRowCount => Lang == EnumLang.English ? @"Rows count of the section table" : @"Количество строк таблицы раздела";
            public static string ItemRowCount => Lang == EnumLang.English ? @"Rows count of the item table" : @"Количество строк таблицы элемента";
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
            public static string HostName => Lang == EnumLang.English ? @"Host name" : @"Имя хоста";
            public static string Language => Lang == EnumLang.English ? @"Language" : @"Язык";
            public static string LanguageDetect => Lang == EnumLang.English ? @"English" : @"Русский";
            public static string IsYes(bool isTrue) => Lang == EnumLang.English ? isTrue ? @"Yes" : @"No" : isTrue ? @"Да" : @"Нет";
            public static string IsEnableIt(bool isTrue) => Lang == EnumLang.English ? isTrue ? @"Enable" : @"Disable" : isTrue ? @"Включено" : @"Отключено";
            public static string IsEnableShe(bool isTrue) => Lang == EnumLang.English ? isTrue ? @"Enable" : @"Disable" : isTrue ? @"Включена" : @"Отключена";
            public static string IsEnableHe(bool isTrue) => Lang == EnumLang.English ? isTrue ? @"Enable" : @"Disable" : isTrue ? @"Включен" : @"Отключен";
            #endregion
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
            #region URI
            public const string UriRouteRoot = "/";
            public const string UriRouteSectionLogs = "/system/logs";
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
            public static string MemoryUsed => Lang == EnumLang.English ? @"Occupied memory" : @"Занимаемая память";
            public static string MemoryPhysical => Lang == EnumLang.English ? @"Physical memory" : @"Физическая память";
            public static string MemoryVirtual => Lang == EnumLang.English ? @"Virtual memory" : @"Виртуальная память";
            public static string MemoryIsExecute => Lang == EnumLang.English ? @"Application memory manager at work." : @"Менеджер памяти приложения в работе.";
            public static string MemoryIsNotExecute => Lang == EnumLang.English ? @"The application memory manager is not running!" : @"Менеджер памяти приложения не выполняется!";
            public static string MemoryResult => Lang == EnumLang.English ? @"Результат" : @"Результат";
            public static string MemoryException => Lang == EnumLang.English ? @"Memory manager error" : @"Ошибка менеджера памяти";
            public static string MemoryActionStart => Lang == EnumLang.English ? @"Run the memory manager" : @"Запустить менеджер памяти";
            public static string MemoryActionStop => Lang == EnumLang.English ? @"Stop the memory manager" : @"Остановить менеджер памяти";
            #endregion
        }

        public static class Resources
        {
            #region Main
            public static string AppTitle => Lang == EnumLang.English ? @"Resources VS" : @"Ресурсы ВС";
            public static string CallbackEmail => Lang == EnumLang.English ?
                @"mailto:morozov_dv@kolbasa-vs.ru&subject=resources-vs.kolbasa-vs.local" :
                @"mailto:morozov_dv@kolbasa-vs.ru&subject=resources-vs.kolbasa-vs.local";
            public static string SysAdmin => Lang == EnumLang.English ? @"Administration" : @"Администрирование";
            public static string SupportCreatio =>
                Lang == EnumLang.English
                    ? @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/CaseSection/"
                    : @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/CaseSection/";
            #endregion
            #region Support
            public static string SupportTitle => Lang == EnumLang.English ? @"Support service" : @"Служба поддержки";
            public static string SupportLinkCreatioText => Lang == EnumLang.English ? @"Creatio appeals" : @"Creatio обращения";
            public static string SupportLinkHelpText => Lang == EnumLang.English ? @"Write a letter" : @"Написать письмо";

            public static string SupportLinkHelpPath =>
                Lang == EnumLang.English
                    ? @"mailto:helpdesk@kolbasa-vs.ru?subject=Appeal"
                    : @"mailto:helpdesk@kolbasa-vs.ru?subject=Обращение";

            #endregion
            #region Contacts
            public static string ContactsTitle => Lang == EnumLang.English ? @"Contacts" : @"Контакты";
            public static string ContactsPhoneText => Lang == EnumLang.English ? @"Phone directory" : @"Телефонный справочник";

            public static string ContactsPhonePath =>
                Lang == EnumLang.English
                    ? @"http://sqlsrsp01.kolbasa-vs.local/Reports/browse/PhoneRP"
                    : @"http://sqlsrsp01.kolbasa-vs.local/Reports/browse/PhoneRP";

            public static string ContactsCreatioText => Lang == EnumLang.English ? @"Creatio contacts" : @"Creatio контакты";

            public static string ContactsCreatioPath =>
                Lang == EnumLang.English
                    ? @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/ContactSectionV2/"
                    : @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/ContactSectionV2/";

            #endregion
            #region IT department
            public static string DepartmentItTitle => Lang == EnumLang.English ? @"IT department" : @"ИТ отдел";

            #endregion
            #region Redmine
            public static string RedmineTitle => Lang == EnumLang.English ? @"Redmine" : @"Redmine";
            public static string RedmineWikiText => Lang == EnumLang.English ? @"Wiki" : @"Wiki";
            public const string RedmineWikiPath = @"http://192.168.0.50/projects/resources_it/wiki/Wiki";
            public const string RedmineProjectsText = @"проекты";
            public const string RedmineProjectsPath = @"http://192.168.0.50/projects";
            public const string RedmineGoogleText = @"Ссылка на гугл таблицы";
            public const string RedmineGooglePath = @"http://192.168.0.50/projects/resources_it/wiki/%D0%A1%D1%81%D1%8B%D0%BB%D0%BA%D0%B0_%D0%BD%D0%B0_%D0%B3%D1%83%D0%B3%D0%BB_%D1%82%D0%B0%D0%B1%D0%BB%D0%B8%D1%86%D1%8B";
            #endregion
            #region Zabbix
            public static string ZabbixTitle => Lang == EnumLang.English ? @"Zabbix" : @"Zabbix";
            public static string ZabbixKolbasaText => Lang == EnumLang.English ? @"kolbasa-vs-terrasoft" : @"kolbasa-vs-terrasoft";

            public static string ZabbixKolbasaPath =>
                Lang == EnumLang.English
                    ? @"http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7"
                    : @"http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7";

            public static string ZabbixGlobalText => Lang == EnumLang.English ? @"Global view" : @"Глобальное представление";

            public static string ZabbixGlobalPath =>
                Lang == EnumLang.English
                    ? @"http://zabbix.kolbasa-vs.local/zabbix/zabbix.php?action=dashboard.view"
                    : @"http://zabbix.kolbasa-vs.local/zabbix/zabbix.php?action=dashboard.view";

            public static string ZabbixWebText => Lang == EnumLang.English ? @"Web monitoring" : @"Веб-мониторинг";

            public static string ZabbixWebPath =>
                Lang == EnumLang.English
                    ? @"http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7"
                    : @"http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7";

            #endregion
            #region Creatio
            public const string CreatioTitle = @"Creatio-сервер";
            public const string CreatioWebinarsText = @"Вебинары";
            public const string CreatioWebinarsPath = @"\\isexcd02\Webinars\";
            public const string CreatioOfficialTitle = @"Официальные сайты Terrasoft Creatio";
            public const string CreatioAcademyText = @"Академия";
            public const string CreatioAcademyPath = @"https://academy.terrasoft.ru/";
            public const string CreatioMarketplaceText = @"Маркетплейс";
            public const string CreatioMarketplacePath = @"https://marketplace.terrasoft.ru/";
            public const string CreatioCommunityText = @"Сообщество";
            public const string CreatioCommunityPath = @"https://community.terrasoft.ru/";
            public const string CreatioTableTitle = @"Локальные сайты";
            public const string CreatioTableFieldName = @"Сайт";
            public const string CreatioTableFieldLink = @"Ссылка";
            public const string CreatioTableFieldDev = @"Конфигурация";
            public const string CreatioCreDevDmName = @"Сайт разработки Морозов Д.В.";
            public const string CreatioCreDevDmLink = @"http://cre-dev-dm.kolbasa-vs.local/";
            public const string CreatioCreDevDmDev = @"http://cre-dev-dm.kolbasa-vs.local/0/dev";
            public const string CreatioCreDevIaName = @"Сайт разработки Андреев И.А.";
            public const string CreatioCreDevIaLink = @"http://cre-dev-ia.kolbasa-vs.local/";
            public const string CreatioCreDevIaDev = @"http://cre-dev-ia.kolbasa-vs.local/0/dev";
            public const string CreatioCreTestName = @"Сайт тестирования";
            public const string CreatioCreTestLink = @"http://cre-test.kolbasa-vs.local/";
            public const string CreatioCreTestDev = @"http://cre-test.kolbasa-vs.local/0/dev";

            public const string CreatioCreStudyText = @"cre-study | Сайт обучения";
            public const string CreatioCreStudyPath = @"http://cre-study.kolbasa-vs.local/";
            public const string CreatioCreStudyDevText = @"cre-study | Конфигурация обучения";
            public const string CreatioCreStudyDevPath = @"http://cre-study.kolbasa-vs.local/0/dev";
            public const string CreatioCreUpgradeText = @"cre-upgrade | Сайт обновления";
            public const string CreatioCreUpgradePath = @"http://cre-upgrade.kolbasa-vs.local/";
            public const string CreatioCreUpgradeDevText = @"cre-upgrade | Конфигурация обновления";
            public const string CreatioCreUpgradeDevPath = @"http://cre-upgrade.kolbasa-vs.local/0/dev";
            public const string CreatioRemoteTitle = @"Публичные сайты";
            public const string CreatioTerrasoftPreText = @"dev-kolbasa-vs.terrasoft.ru | Сайт пре-прод 1";
            public const string CreatioTerrasoftPrePath = @"https://dev-kolbasa-vs.terrasoft.ru/";
            public const string CreatioTerrasoftPreDevText = @"dev-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public const string CreatioTerrasoftPreDevPath = @"https://dev-kolbasa-vs.terrasoft.ru/0/dev";
            public const string CreatioTerrasoftPre2Text = @"dev2-kolbasa-vs.terrasoft.ru | Сайт пре-прод 2";
            public const string CreatioTerrasoftPre2Path = @"https://dev2-kolbasa-vs.terrasoft.ru/";
            public const string CreatioTerrasoftPre2DevText = @"dev2-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public const string CreatioTerrasoftPre2DevPath = @"https://dev2-kolbasa-vs.terrasoft.ru/0/dev";
            public const string CreatioTerrasoftText = @"kolbasa-vs.terrasoft.ru | Продуктовая среда";
            public const string CreatioTerrasoftPath = @"https://kolbasa-vs.terrasoft.ru/";
            public const string CreatioTerrasoftDevText = @"kolbasa-vs.terrasoft.ru | Конфигурация продуктовой среды";
            public const string CreatioTerrasoftDevPath = @"https://kolbasa-vs.terrasoft.ru/0/dev";
            #endregion
            #region Контроль версий
            public const string VersionControlTitle = @"Контроль версий";
            public const string VersionControlSvnVisualStudioText = @"SVN visualstudio";
            public const string VersionControlSvnVisualStudioPath = @"http://192.168.0.50/svn/visualstudio/";
            #endregion
            #region Веб-сервер
            public const string IisServerTitle = @"Веб-сервер";
            public const string IisServerBlazorDeviceText = @"blazor-device-control | Управление устройствами";
            public const string IisServerBlazorDevicePath = @"https://blazor-device-control.kolbasa-vs.local/";
            public const string IisServerBlazorBackupText = @"blazor-backup-control | Управление бэкапами";
            public const string IisServerBlazorBackupPath = @"https://blazor-backup-control.kolbasa-vs.local/";
            public const string IisServerBlazorExampleText = @"blazor-example | Пример сайта на Blazor";
            public const string IisServerBlazorExamplePath = @"https://blazor-example.kolbasa-vs.local/";
            public const string IisServerBlazorDao1Text = @"data-accessor-test-1 | Доступ к данным 1";
            public const string IisServerBlazorDao1Path = @"https://data-accessor-test-1.kolbasa-vs.local/";
            public const string IisServerBlazorDao2Text = @"data-accessor-test-2 | Доступ к данным 2";
            public const string IisServerBlazorDao2Path = @"https://data-accessor-test-2.kolbasa-vs.local/";
            public const string IisServerBlazorDbDocText = @"database-doc | Database Routine Maintanance Project";
            public const string IisServerBlazorDbDocPath = @"https://database-doc.kolbasa-vs.local/";
            public const string IisServerBlazorResourcesVsText = @"resources-vs | Внутренние ресурсы";
            public const string IisServerBlazorResourcesVsPath = @"https://resources-vs.kolbasa-vs.local/";
            public const string IisServerBlazorSmart1Text = @"smart-net-data-accessor-test-1 | DAO example 1";
            public const string IisServerBlazorSmart1Path = @"https://smart-net-data-accessor-test-1.kolbasa-vs.local/";
            public const string IisServerBlazorSmart2Text = @"smart-net-data-accessor-test-2 | | DAO example 2";
            public const string IisServerBlazorSmart2Path = @"https://smart-net-data-accessor-test-2.kolbasa-vs.local/";
            #endregion
        }

        public static class DeviceControl
        {
            #region Main

            public static string Index => Lang == EnumLang.English ? @"DeviceControl" : @"Управление устройствами";
            public static string IndexContinue => Lang == EnumLang.English
                ? @"Click on a section in the panel to continue."
                : @"Для продолжения работы кликните по разделу на панели.";
            public static string SqlServerDebug => "CREATIO";
            public static string SqlServerRelease => "PALYCH";
            public static string CallbackEmail => Lang == EnumLang.English ?
                    @"mailto:morozov_dv@kolbasa-vs.ru&subject=device-control.kolbasa-vs.local" :
                    @"mailto:morozov_dv@kolbasa-vs.ru&subject=device-control.kolbasa-vs.local";
            public const string DataRecords = @"записей";
            #endregion
            #region Methods
            public static string Method => Lang == EnumLang.English ? @"Method" : @"Метод";
            #endregion
            #region Комплексы промышленных устройств
            public static string DevicesTitle => Lang == EnumLang.English ? @"Complexes of industrial devices" : @"Комплексы промышленных устройств";
            #endregion
            #region URI
            public const string UriRouteItemPrinter = "/item/printer";
            public const string UriRouteItemScale = "/item/scale";
            public const string UriRouteItemPlu = "/item/plu";
            public const string UriRouteSectionContragents = "/section/contragents";
            public const string UriRouteSectionDevices = "/section/devices";
            public const string UriRouteSectionHosts = "/section/hosts";
            public const string UriRouteSectionNomenclatures = "/section/nomenclatures";
            public const string UriRouteSectionPrinterTypes = "/section/printertypes";
            public const string UriRouteSectionPrinters = "/section/printers";
            public const string UriRouteSectionProductionFacilities = "/section/productionfacilities";
            public const string UriRouteSectionScales = "/section/scales";
            public const string UriRouteSectionTemplateResources = "/section/templateresources";
            public const string UriRouteSectionTemplates = "/section/templates";
            public const string UriRouteSectionWeithingFacts = "/section/weithingfacts";
            public const string UriRouteSectionWorkshops = "/section/workshops";
            #endregion
            #region Sections
            public static string SectionFonts => Lang == EnumLang.English ? @"Fonts" : @"Шрифты";
            public static string SectionLogos => Lang == EnumLang.English ? @"Logos" : @"Логотипы";
            public static string SectionBarcodes => Lang == EnumLang.English ? @"Barcodes" : @"Штрихкоды";
            public static string SectionBarCodeTypes => Lang == EnumLang.English ? @"Barcodes types" : @"Типы штрихкодов";
            public static string SectionContragents => Lang == EnumLang.English ? @"Counterparties" : @"Контрагенты";
            public static string SectionNomenclatureUnits => Lang == EnumLang.English ? @"Packages" : @"Упаковки";
            public static string SectionOrders => Lang == EnumLang.English ? @"Orders" : @"Заказы";
            public static string SectionOrderStatuses => Lang == EnumLang.English ? @"Order statuses" : @"Статусы заказов";
            public static string SectionOrderTypes => Lang == EnumLang.English ? @"Order types" : @"Типы заказов";
            public static string SectionPlus => Lang == EnumLang.English ? @"PLU" : @"ПЛУ";
            public static string SectionProductionFacilities => Lang == EnumLang.English ? @"Prod. facilities" : @"Произв. площадки";
            public static string SectionProductSeries => Lang == EnumLang.English ? @"Product series" : @"Серии продуктов";
            public static string SectionPrinters => Lang == EnumLang.English ? @"Printers" : @"Принтеры";
            public static string SectionPrinterTypes => Lang == EnumLang.English ? @"Printer types" : @"Типы принтеров";
            public static string SectionHosts => Lang == EnumLang.English ? @"Hosts" : @"Хосты";
            public static string SectionLabels => Lang == EnumLang.English ? @"Labels" : @"Этикетки";
            public static string SectionScales => Lang == EnumLang.English ? @"Devices" : @"Устройства";
            public static string SectionResources => Lang == EnumLang.English ? @"Resources" : @"Ресурсы";
            public static string SectionTemplateResources => Lang == EnumLang.English ? @"Template resources" : @"Ресурсы шаблонов";
            public static string SectionTemplates => Lang == EnumLang.English ? @"Templates" : @"Шаблоны";
            public static string SectionWeithingFacts => Lang == EnumLang.English ? @"Weithing facts" : @"Взвешивания";
            public static string SectionNomenclatures => Lang == EnumLang.English ? @"Nomenclatures" : @"Номенклатура";
            public static string SectionWorkShops => Lang == EnumLang.English ? @"Workshops" : @"Цеха";
            public static string SectionPrinterResourceRef => Lang == EnumLang.English ? @"Printer resources" : @"Ресурсы принтера";
            public static string SectionLogs => Lang == EnumLang.English ? @"Logs" : @"Логи";
            #endregion
            #region Tables
            public const string TableActionAdd = @"Добавить";
            public const string TableActionCancel = @"Отмена";
            public const string TableActionClear = @"Очистить";
            public const string TableActionCopy = @"Копировать";
            public const string TableActionDelete = @"Удалить навсегда";
            public const string TableActionEdit = @"Редактировать";
            public const string TableActionFill = @"Заполнить";
            public const string TableActionMarked = @"Пометить на удаление";
            public const string TableActionNew = @"Новый";
            public const string TableActionSave = @"Сохранить";
            public const string TableActions = @"Действия";
            public const string TableActionsIsDeny = @"Действия недоступны";
            public const string TableFieldAccessLevel = @"Уровень доступа";
            public const string TableFieldActive = @"Активно";
            public const string TableFieldApp = @"Программа";
            public const string TableFieldBarCodeTypeId = @"ID типа ШК";
            public const string TableFieldBrand = @"Brand";
            public const string TableFieldCategoryId = @"ID категории";
            public const string TableFieldCategoryName = @"Категория";
            public const string TableFieldChangeDt = @"Дата измнения";
            public const string TableFieldCheckGtin = @"v";
            public const string TableFieldCheckWeight = @"Весовая продукция";
            public const string TableFieldCode = @"Код";
            public const string TableFieldComment = @"Комментарий";
            public const string TableFieldConsumerName = @"ConsumerName";
            public const string TableFieldContragentId = @"ID контрагента";
            public const string TableFieldCount = @"Количество";
            public const string TableFieldCreateDt = @"Дата создания";
            public const string TableFieldDayOfWeek = @"День недели";
            public const string TableFieldDescription = @"Описание";
            public const string TableFieldDeviceComPort = @"COM-порт";
            public const string TableFieldDeviceIp = @"IP устройства";
            public const string TableFieldDeviceMac = @"MAC устройства";
            public const string TableFieldDeviceNumber = @"Номер устройства";
            public const string TableFieldDevicePort = @"Порт устройства";
            public const string TableFieldDeviceReceiveTimeout = @"Таймаут приёма";
            public const string TableFieldDeviceSendTimeout = @"Таймаут отправки";
            public const string TableFieldEan13 = @"EAN13";
            public const string TableFieldFile = @"Файл";
            public const string TableFieldGln = @"Gln";
            public const string TableFieldGoodsBoxQuantly = @"Вложений в короб";
            public const string TableFieldGoodsBruttoWeight = @"Вес брутто";
            public const string TableFieldGoodsDescription = @"Описание товара";
            public const string TableFieldGoodsFullName = @"Полное наименование";
            public const string TableFieldGoodsName = @"Товар";
            public const string TableFieldGoodsTareWeight = @"Тара";
            public const string TableFieldGtin = @"GTIN";
            public const string TableFieldGuidMercury = @"GUID Mercury";
            public const string TableFieldHost = @"Хост";
            public const string TableFieldIcon = @"Иконка";
            public const string TableFieldId = @"ID";
            public const string TableFieldIdRRef = @"ID 1C";
            public const string TableFieldIdRref = @"ID 1C";
            public const string TableFieldImageData = @"ImageData";
            public const string TableFieldImageDataInfo = @"Информация";
            public const string TableFieldIsClose = @"IsClose";
            public const string TableFieldItf14 = @"ITF14";
            public const string TableFieldKneding = @"Взвешено";
            public const string TableFieldLevel = @"Уровень";
            public const string TableFieldLine = @"Линия";
            public const string TableFieldLowerWeightThreshold = @"Нижнее значение веса короба";
            public const string TableFieldMarked = @"В архиве";
            public const string TableFieldMember = @"Метод";
            public const string TableFieldMessage = @"Сообщение";
            public const string TableFieldModifiedDate = @"Дата редактирования";
            public const string TableFieldName = @"Наименование";
            public const string TableFieldNameFull = @"Полное наименование";
            public const string TableFieldNetWeight = @"Вес нетто";
            public const string TableFieldNomenclatureId = @"ID номенклатуры";
            public const string TableFieldNomenclatureName = @"Номенклатура";
            public const string TableFieldNomenclatureType = @"Тип номенклатуры";
            public const string TableFieldNomenclatureUnitId = @"ID юнита номенклатуры";
            public const string TableFieldNominalWeight = @"Номинальный вес короба";
            public const string TableFieldPackQuantly = @"PackQuantly";
            public const string TableFieldPackTypeId = @"PackTypeId";
            public const string TableFieldPackWeight = @"PackWeight";
            public const string TableFieldPlu = @"PLU";
            public const string TableFieldPluDescription = @"Для переноса строки используйте символ `|`";
            public const string TableFieldProductDate = @"Дата продукции";
            public const string TableFieldProductionFacilityName = @"Площадка";
            public const string TableFieldRegNum = @"#";
            public const string TableFieldResource = @"Ресурс";
            public const string TableFieldRow = @"Строка";
            public const string TableFieldScale = @"Устройство";
            public const string TableFieldScaleFactor = @"Scale factor";
            public const string TableFieldScaleId = @"ID весов";
            public const string TableFieldSettingsFile = @"Файл настроек";
            public const string TableFieldShelfLifeDays = @"Срок годности (суток)";
            public const string TableFieldSscc = @"SSCC";
            public const string TableFieldState = @"Статус";
            public const string TableFieldStorage = @"Склад";
            public const string TableFieldTareWeight = @"Вес тары";
            public const string TableFieldTemplate = @"Шаблон";
            public const string TableFieldTemplateDefault = @"Шаблон по-умолчанию";
            public const string TableFieldTemplateId = @"ID шаблона";
            public const string TableFieldTemplateIdDefault = @"ID шаблона";
            public const string TableFieldTemplateIdSeries = @"ID серии";
            public const string TableFieldTemplateSeries = @"Шаблон суммарной этикетки";
            public const string TableFieldTitle = @"Заголовок";
            public const string TableFieldType = @"Тип";
            public const string TableFieldUid = @"UUID";
            public const string TableFieldUpperWeightThreshold = @"Верхнее значение веса короба";
            public const string TableFieldUseOrder = @"Использовать заказ";
            public const string TableFieldUser = @"Пользователь";
            public const string TableFieldValue = @"Значение";
            public const string TableFieldVatRate = @"Ставка НДС";
            public const string TableFieldVersion = @"Версия";
            public const string TableFieldWeithingDate = @"Дата взвешивания";
            public const string TableFieldWorkShopId = @"ID цеха";
            public const string TableFieldWorkShopName = @"Цех";
            public const string TableFieldXml = @"XML";
            public const string TableFieldZebraPrinter = @"Принтер";
            public const string TableFieldZebraPrinterDarknessLevel = @"Уровень темноты";
            public const string TableFieldZebraPrinterIp = @"IP-адрес";
            public const string TableFieldZebraPrinterMac = @"MAC-адрес";
            public const string TableFieldZebraPrinterPassword = @"Пароль принтера";
            public const string TableFieldZebraPrinterPeelOffSet = @"Смещение";
            public const string TableFieldZebraPrinterPort = @"Порт принтера";
            public const string TableFieldZebraPrinterType = @"Тип принтера";
            public const string TableReadData = @"Прочитать данные";
            public const string Table = @"Таблица";
            #endregion
            #region Item
            public static string ItemTitleBarcode => Lang == EnumLang.English ? @"Barcode" : @"Штрих-код";
            public static string ItemTitleBarcodeType => Lang == EnumLang.English ? @"Barcode type" : @"Тип штрих-кода";
            public static string ItemTitleContragent => Lang == EnumLang.English ? @"Counterparty" : @"Контрагент";
            public static string ItemTitleHost => Lang == EnumLang.English ? @"Host" : @"Хост";
            public static string ItemTitleLabel => Lang == EnumLang.English ? @"Label" : @"Этикетка";
            public static string ItemTitleLog => Lang == EnumLang.English ? @"Log" : @"Тип Лог";
            public static string ItemTitleNomenclature => Lang == EnumLang.English ? @"Nomenclature" : @"Номенклатура";
            public static string ItemTitleOrderStatus => Lang == EnumLang.English ? @"Order status" : @"Статус заказа";
            public static string ItemTitleOrderTypes => Lang == EnumLang.English ? @"Order type" : @"Тип заказа";
            public static string ItemTitleOrders => Lang == EnumLang.English ? @"Order" : @"Заказ";
            public static string ItemTitlePlu => Lang == EnumLang.English ? @"PLU" : @"Тип ПЛУ";
            public static string ItemTitlePrinter => Lang == EnumLang.English ? @"Printer" : @"Принтер";
            public static string ItemTitlePrinterResourceRef => Lang == EnumLang.English ? @"Printer resource" : @"Ресурс принтера";
            public static string ItemTitlePrinterType => Lang == EnumLang.English ? @"Printer type" : @"Тип принтера";
            public static string ItemTitleProductSeries => Lang == EnumLang.English ? @"Product Series" : @"Серия продукта";
            public static string ItemTitleProductionFacility => Lang == EnumLang.English ? @"ProductionFacility" : @"Производственный комплекс";
            public static string ItemTitleScales => Lang == EnumLang.English ? @"Weighing device" : @"Весовое устройство";
            public static string ItemTitleTemplate => Lang == EnumLang.English ? @"Template" : @"Шаблон";
            public static string ItemTitleTemplateResource => Lang == EnumLang.English ? @"Template resource" : @"Ресурс шаблона";
            public static string ItemTitleWeithingFact => Lang == EnumLang.English ? @"Weighing" : @"Взвешивание";
            public static string ItemTitleWorkShop => Lang == EnumLang.English ? @"Workshop" : @"Организация";
            #endregion
            public static string GetSectionTitle(EnumTableScales table) => table switch
            {
                EnumTableScales.BarcodeTypes => SectionBarcodes,
                EnumTableScales.Contragents => SectionContragents,
                EnumTableScales.Hosts => SectionHosts,
                EnumTableScales.Labels => SectionLabels,
                EnumTableScales.Logs => SectionLogs,
                EnumTableScales.Nomenclature => SectionNomenclatures,
                EnumTableScales.OrderStatus => SectionOrderStatuses,
                EnumTableScales.OrderTypes => SectionOrderTypes,
                EnumTableScales.Orders => SectionOrders,
                EnumTableScales.Plu => SectionPlus,
                EnumTableScales.Printer => SectionPrinters,
                EnumTableScales.PrinterResourceRef => SectionPrinterResourceRef,
                EnumTableScales.PrinterType => SectionPrinterTypes,
                EnumTableScales.ProductSeries => SectionProductSeries,
                EnumTableScales.ProductionFacility => SectionProductionFacilities,
                EnumTableScales.Scales => SectionScales,
                EnumTableScales.TemplateResources => SectionTemplateResources,
                EnumTableScales.Templates => SectionTemplates,
                EnumTableScales.WeithingFact => SectionWeithingFacts,
                EnumTableScales.WorkShop => SectionWorkShops,
                EnumTableScales.Default => throw new NotImplementedException(),
                _ => string.Empty,
            };
            public static string GetItemTitle(EnumTableScales table) => table switch
            {
                EnumTableScales.BarcodeTypes => ItemTitleBarcodeType,
                EnumTableScales.Contragents => ItemTitleContragent,
                EnumTableScales.Hosts => ItemTitleHost,
                EnumTableScales.Labels => ItemTitleLabel,
                EnumTableScales.Logs => ItemTitleLog,
                EnumTableScales.Nomenclature => ItemTitleNomenclature,
                EnumTableScales.OrderStatus => ItemTitleOrderStatus,
                EnumTableScales.OrderTypes => ItemTitleOrderTypes,
                EnumTableScales.Orders => ItemTitleOrders,
                EnumTableScales.Plu => ItemTitlePlu,
                EnumTableScales.Printer => ItemTitlePrinter,
                EnumTableScales.PrinterResourceRef => ItemTitlePrinterResourceRef,
                EnumTableScales.PrinterType => ItemTitlePrinterType,
                EnumTableScales.ProductSeries => ItemTitleProductSeries,
                EnumTableScales.ProductionFacility => ItemTitleProductionFacility,
                EnumTableScales.Scales => ItemTitleScales,
                EnumTableScales.TemplateResources => ItemTitleTemplateResource,
                EnumTableScales.Templates => ItemTitleTemplate,
                EnumTableScales.WeithingFact => ItemTitleWeithingFact,
                EnumTableScales.WorkShop => ItemTitleWorkShop,
                EnumTableScales.Default => throw new NotImplementedException(),
                _ => string.Empty,
            };
            public static string GetItemTitle(EnumTableScales table, int itemId) => table switch
            {
                EnumTableScales.BarcodeTypes => $"{ItemTitleBarcodeType}. ID {itemId}",
                EnumTableScales.Contragents => $"{ItemTitleContragent}. ID {itemId}",
                EnumTableScales.Hosts => $"{ItemTitleHost}. ID {itemId}",
                EnumTableScales.Labels => $"{ItemTitleLabel}. ID {itemId}",
                EnumTableScales.Logs => $"{ItemTitleLog}. ID {itemId}",
                EnumTableScales.Nomenclature => $"{ItemTitleNomenclature}. ID {itemId}",
                EnumTableScales.OrderStatus => $"{ItemTitleOrderStatus}. ID {itemId}",
                EnumTableScales.OrderTypes => $"{ItemTitleOrderTypes}. ID {itemId}",
                EnumTableScales.Orders => $"{ItemTitleOrders}. ID {itemId}",
                EnumTableScales.Plu => $"{ItemTitlePlu}. ID {itemId}",
                EnumTableScales.Printer => $"{ItemTitlePrinter}. ID {itemId}",
                EnumTableScales.PrinterResourceRef => $"{ItemTitlePrinterResourceRef}. ID {itemId}",
                EnumTableScales.PrinterType => $"{ItemTitlePrinterType}. ID {itemId}",
                EnumTableScales.ProductSeries => $"{ItemTitleProductSeries}. ID {itemId}",
                EnumTableScales.ProductionFacility => $"{ItemTitleProductionFacility}. ID {itemId}",
                EnumTableScales.Scales => $"{ItemTitleScales}. ID {itemId}",
                EnumTableScales.TemplateResources => $"{ItemTitleTemplateResource}. ID {itemId}",
                EnumTableScales.Templates => $"{ItemTitleTemplate}. ID {itemId}",
                EnumTableScales.WeithingFact => $"{ItemTitleWeithingFact}. ID {itemId}",
                EnumTableScales.WorkShop => $"{ItemTitleWorkShop}. ID {itemId}",
                EnumTableScales.Default => throw new NotImplementedException(),
                _ => string.Empty,
            };
            public static string GetItemTitle(EnumTableScales table, Guid itemUid) => table switch
            {
                EnumTableScales.BarcodeTypes => $"{ItemTitleBarcodeType}. UID {itemUid}",
                EnumTableScales.Contragents => $"{ItemTitleContragent}. UID {itemUid}",
                EnumTableScales.Hosts => $"{ItemTitleHost}. UID {itemUid}",
                EnumTableScales.Labels => $"{ItemTitleLabel}. UID {itemUid}",
                EnumTableScales.Logs => $"{ItemTitleLog}. UID {itemUid}",
                EnumTableScales.Nomenclature => $"{ItemTitleNomenclature}. UID {itemUid}",
                EnumTableScales.OrderStatus => $"{ItemTitleOrderStatus}. UID {itemUid}",
                EnumTableScales.OrderTypes => $"{ItemTitleOrderTypes}. UID {itemUid}",
                EnumTableScales.Orders => $"{ItemTitleOrders}. UID {itemUid}",
                EnumTableScales.Plu => $"{ItemTitlePlu}. UID {itemUid}",
                EnumTableScales.Printer => $"{ItemTitlePrinter}. UID {itemUid}",
                EnumTableScales.PrinterResourceRef => $"{ItemTitlePrinterResourceRef}. UID {itemUid}",
                EnumTableScales.PrinterType => $"{ItemTitlePrinterType}. UID {itemUid}",
                EnumTableScales.ProductSeries => $"{ItemTitleProductSeries}. UID {itemUid}",
                EnumTableScales.ProductionFacility => $"{ItemTitleProductionFacility}. UID {itemUid}",
                EnumTableScales.Scales => $"{ItemTitleScales}. UID {itemUid}",
                EnumTableScales.TemplateResources => $"{ItemTitleTemplateResource}. UID {itemUid}",
                EnumTableScales.Templates => $"{ItemTitleTemplate}. UID {itemUid}",
                EnumTableScales.WeithingFact => $"{ItemTitleWeithingFact}. UID {itemUid}",
                EnumTableScales.WorkShop => $"{ItemTitleWorkShop}. UID {itemUid}",
                EnumTableScales.Default => throw new NotImplementedException(),
                _ => string.Empty,
            };
        }

        public static class MdmControl
        {

        }

        [SupportedOSPlatform(@"windows")]
        public static string GetAppVersion(System.Reflection.Assembly executingAssembly)
        {
            FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            string result = fieVersionInfo.FileVersion;
            if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
                result = result.Substring(0, result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase));
            return result;
        }

        [SupportedOSPlatform(@"windows")]
        public static string GetCoreVersion()
        {
            FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string result = fieVersionInfo.FileVersion;
            if (!string.IsNullOrEmpty(result) && result.EndsWith(".0"))
                result = result.Substring(0, result.IndexOf(".0", StringComparison.InvariantCultureIgnoreCase));
            return result;
        }
    }
}
