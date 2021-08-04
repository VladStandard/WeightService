using System.Diagnostics;

namespace BlazorCore.Utils
{
    public static class LocalizationStrings
    {
        public static EnumLang Lang = EnumLang.Rus;

        public static class Share
        {
            #region Main
            public static string Company => Lang == EnumLang.Eng ? @"Vladimir Standard" : @"Владимирский стандарт";
            public static string Index => Lang == EnumLang.Eng ? @"Inside resources" : @"Внутренние ресурсы";
            public static string IndexDescription => Lang == EnumLang.Eng ?
                @"The site was created to help you navigate through the company's internal resources" :
                @"Сайт создан для помощи в навигации по внутренним ресурсам компании";
            public static string CallbackTitle => Lang == EnumLang.Eng ? @"Feedback" : @"Обратная связь";
            public static string DataLoading => Lang == EnumLang.Eng ? @"Loading data..." : @"Загрузка данных...";
            public static string IdentityError => Lang == EnumLang.Eng ? @"User error!" : @"Ошибка пользователя";
            public static string NotLoad => Lang == EnumLang.Eng ? @"Not load!" : @"Не загружено";
            public static string DebugMode => Lang == EnumLang.Eng ? @"Debug mode" : @"Режим отладки";
            public static string ServerDevelop => Lang == EnumLang.Eng ? @"Debug server" : @"Сервер разработки";
            public static string ServerRelease => Lang == EnumLang.Eng ? @"Debug release" : @"Промышленный сервер";
            public static string Authorization => Lang == EnumLang.Eng ? @"Authorization" : @"Авторизация";
            public static string Doc => Lang == EnumLang.Eng ? @"Documentation" : @"Документация";
            public static string ProgramVer => Lang == EnumLang.Eng ? @"Program version" : @"Версия программы";
            public static string HostName => Lang == EnumLang.Eng ? @"Host name" : @"Имя хоста";
            public static string Language => Lang == EnumLang.Eng ? @"Language" : @"Язык";
            public static string LanguageDetect => Lang == EnumLang.Eng ? @"English" : @"Русский";
            public static string IsYes (bool isTrue) => Lang == EnumLang.Eng ? isTrue ? @"Yes" : @"No" : isTrue ? @"Да" : @"Нет";
            public static string IsEnable (bool isTrue) => Lang == EnumLang.Eng ? isTrue ? @"Enable" : @"Disable" : isTrue ? @"Включено" : @"Отключено";
            #endregion
            #region Menu
            public static string MenuReferences => Lang == EnumLang.Eng ? @"References" : @"Справочники";
            public static string FileChoose => Lang == EnumLang.Eng ? @"Select a file" : @"Выбрать файл";
            public static string FileUpload => Lang == EnumLang.Eng ? @"Upload a file" : @"Загрузить файл";
            public static string FileDownload => Lang == EnumLang.Eng ? @"Download a file" : @"Скачать файл";
            public static string FileDialog => Lang == EnumLang.Eng ? @"File dialog" : @"Файловый диалог";
            public static string FileSaveDialog => Lang == EnumLang.Eng ? @"Specify the file name to save" : @"Указать имя файла для сохранения";
            public static string ServerResponse => Lang == EnumLang.Eng ? @"Server response" : @"Ответ сервера";
            public static string MenuSecurity => Lang == EnumLang.Eng ? @"Security" : @"Безопасность";
            public static string MenuLogin => Lang == EnumLang.Eng ? @"Login" : @"Логин";
            public static string MenuAccess => Lang == EnumLang.Eng ? @"Access" : @"Доступ";
            public static string MenuAccessDeny => Lang == EnumLang.Eng ? @"Access deny" : @"Доступ запрещён";
            public static string MenuAccessAllow => Lang == EnumLang.Eng ? @"Access allow" : @"Доступ разрешён";
            #endregion
            #region Chart
            public static string Chart => Lang == EnumLang.Eng ? @"Chart" : @"Диаграмма";
            public static string ChartSmooth => Lang == EnumLang.Eng ? @"Chart smooth" : @"Скруглить";
            #endregion
            #region Table
            public static string TableTab => Lang == EnumLang.Eng ? @"Switch between panels" : @"Переключиться между панелями";
            public static string TableRead => Lang == EnumLang.Eng ? @"Read data" : @"Прочитать данные";
            public static string TableReadCancel => Lang == EnumLang.Eng ? @"Cancel data reading" : @"Отмена чтения данных";
            public static string TableEdit => Lang == EnumLang.Eng ? @"Edit record" : @"Редактировать запись";
            public static string TableClear => Lang == EnumLang.Eng ? @"Deactivate active record" : @"Деактивировать активную запись";
            public static string TableCreate => Lang == EnumLang.Eng ? @"Create record" : @"Создать запись";
            public static string TableDelete => Lang == EnumLang.Eng ? @"Delete record" : @"Удалить запись";
            public static string TableSelect => Lang == EnumLang.Eng ? @"Highlight record" : @"Выделить запись";
            public static string TableIncludes => Lang == EnumLang.Eng ? @"Included records" : @"Вложенные записи";
            public static string TableRecordSave => Lang == EnumLang.Eng ? @"Save record" : @"Сохранить запись";
            public static string TableRecordCancel => Lang == EnumLang.Eng ? @"Close record" : @"Закрыть запись";
            #endregion
            #region Table fields
            public static string FieldCount => Lang == EnumLang.Eng ? @"Count" : @"Количество";
            public static string FieldCreated => Lang == EnumLang.Eng ? @"Created" : @"Создано";
            public static string FieldModified => Lang == EnumLang.Eng ? @"Modified" : @"Изменено";
            #endregion
            #region Dialog
            public static string DialogQuestion => Lang == EnumLang.Eng ? @"Perform the operation?" : @"Выполнить операцию?";
            public static string DialogButtonYes => Lang == EnumLang.Eng ? @"Yes" : @"Да";
            public static string DialogButtonCancel => Lang == EnumLang.Eng ? @"Cancel" : @"Отмена";
            public static string DialogButtonNo => Lang == EnumLang.Eng ? @"No" : @"Нет";
            public static string DialogResultSuccess => Lang == EnumLang.Eng ? @"The operation was performed successfully." : @"Операция выполнена успешно.";
            public static string DialogResultCancel => Lang == EnumLang.Eng ? 
                @"Cancel operation. The necessary conditions may not have been met." : 
                @"Отмена операции. Возможно, не выполнены необходимые условия.";
            public static string DialogResultFail => Lang == EnumLang.Eng ? @"Operation error!" : @"Ошибка выполнения операции!";
            #endregion
            #region SQL
            public static string SqlServer => Lang == EnumLang.Eng ? @"SQL-server" : @"SQL-сервер";
            public static string SqlDb => Lang == EnumLang.Eng ? @"DB" : @"БД";
            #endregion
        }

        public static class Resources
        {
            #region Main
            public static string AppTitle => Lang == EnumLang.Eng ? @"Resources VS" : @"Ресурсы ВС";
            public static string CallbackEmail => Lang == EnumLang.Eng ?
                @"mailto:morozov_dv@kolbasa-vs.ru?cc=ivakin_dv@kolbasa-vs.ru&subject=resources-vs.kolbasa-vs.local" :
                @"mailto:morozov_dv@kolbasa-vs.ru?cc=ivakin_dv@kolbasa-vs.ru&subject=resources-vs.kolbasa-vs.local";
            public static string SysAdmin => Lang == EnumLang.Eng ? @"Administration" : @"Администрирование";
            public static string SupportCreatio =>
                Lang == EnumLang.Eng
                    ? @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/CaseSection/"
                    : @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/CaseSection/"; 
            #endregion
            #region Support
            public static string SupportTitle => Lang == EnumLang.Eng ? @"Support service" : @"Служба поддержки";
            public static string SupportLinkCreatioText => Lang == EnumLang.Eng ? @"Creatio appeals" : @"Creatio обращения";
            public static string SupportLinkHelpText => Lang == EnumLang.Eng ? @"Write a letter" : @"Написать письмо";

            public static string SupportLinkHelpPath =>
                Lang == EnumLang.Eng
                    ? @"mailto:helpdesk@kolbasa-vs.ru?subject=Appeal"
                    : @"mailto:helpdesk@kolbasa-vs.ru?subject=Обращение";

            #endregion
            #region Contacts
            public static string ContactsTitle => Lang == EnumLang.Eng ? @"Contacts" : @"Контакты";
            public static string ContactsPhoneText => Lang == EnumLang.Eng ? @"Phone directory" : @"Телефонный справочник";

            public static string ContactsPhonePath =>
                Lang == EnumLang.Eng
                    ? @"http://sqlsrsp01.kolbasa-vs.local/Reports/browse/PhoneRP"
                    : @"http://sqlsrsp01.kolbasa-vs.local/Reports/browse/PhoneRP";

            public static string ContactsCreatioText => Lang == EnumLang.Eng ? @"Creatio contacts" : @"Creatio контакты";

            public static string ContactsCreatioPath =>
                Lang == EnumLang.Eng
                    ? @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/ContactSectionV2/"
                    : @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/ContactSectionV2/";

            #endregion
            #region IT department
            public static string DepartmentItTitle => Lang == EnumLang.Eng ? @"IT department" : @"ИТ отдел";

            #endregion
            #region Redmine
            public static string RedmineTitle => Lang == EnumLang.Eng ? @"Redmine" : @"Redmine";
            public static string RedmineWikiText => Lang == EnumLang.Eng ? @"Wiki" : @"Wiki";
            public const string RedmineWikiPath = @"http://192.168.0.50/projects/resources_it/wiki/Wiki";
            public const string RedmineProjectsText = @"проекты";
            public const string RedmineProjectsPath = @"http://192.168.0.50/projects";
            public const string RedmineGoogleText = @"Ссылка на гугл таблицы";
            public const string RedmineGooglePath = @"http://192.168.0.50/projects/resources_it/wiki/%D0%A1%D1%81%D1%8B%D0%BB%D0%BA%D0%B0_%D0%BD%D0%B0_%D0%B3%D1%83%D0%B3%D0%BB_%D1%82%D0%B0%D0%B1%D0%BB%D0%B8%D1%86%D1%8B";
            #endregion
            #region Zabbix
            public static string ZabbixTitle => Lang == EnumLang.Eng ? @"Zabbix" : @"Zabbix";
            public static string ZabbixKolbasaText => Lang == EnumLang.Eng ? @"kolbasa-vs-terrasoft" : @"kolbasa-vs-terrasoft";

            public static string ZabbixKolbasaPath =>
                Lang == EnumLang.Eng
                    ? @"http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7"
                    : @"http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7";

            public static string ZabbixGlobalText => Lang == EnumLang.Eng ? @"Global view" : @"Глобальное представление";

            public static string ZabbixGlobalPath =>
                Lang == EnumLang.Eng
                    ? @"http://zabbix.kolbasa-vs.local/zabbix/zabbix.php?action=dashboard.view"
                    : @"http://zabbix.kolbasa-vs.local/zabbix/zabbix.php?action=dashboard.view";

            public static string ZabbixWebText => Lang == EnumLang.Eng ? @"Web monitoring" : @"Веб-мониторинг";

            public static string ZabbixWebPath =>
                Lang == EnumLang.Eng
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

            public static string SqlServerDebug => "CREATIO";
            public static string SqlServerRelease => "PALYCH";
            public static string CallbackEmail => Lang == EnumLang.Eng ?
                    @"mailto:morozov_dv@kolbasa-vs.ru?cc=ivakin_dv@kolbasa-vs.ru&subject=device-control.kolbasa-vs.local" :
                    @"mailto:morozov_dv@kolbasa-vs.ru?cc=ivakin_dv@kolbasa-vs.ru&subject=device-control.kolbasa-vs.local";
            public const string DataRecords = @"записей";
            #endregion
            #region Methods
            public static string MethodOnInitializedAsync => Lang == EnumLang.Eng ? @"Method OnInitializedAsync" : @"Метод OnInitializedAsync";

            #endregion
            #region Memory
            public const string MemoryTitle = @"Менеджер памяти приложения";
            public const string MemoryLimit = @"Лимит памяти";
            public const string MemoryLimitNotSet = @"Лимит памяти не задан!";
            public const string MemoryUsed = @"Занимаемая память";
            public const string MemoryPhysical = @"Физическая память";
            public const string MemoryVirtual = @"Виртуальная память";
            public const string MemoryIsExecute = @"Менеджер памяти приложения в работе.";
            public const string MemoryIsNotExecute = @"Менеджер памяти приложения не выполняется!";
            public const string MemoryResult = @"Результат";
            public const string MemoryException = @"Ошибка менеджера памяти";
            public const string MemoryActionStart = @"Запустить менеджер памяти";
            public const string MemoryActionStop = @"Остановить менеджер памяти";
            #endregion
            #region Комплексы промышленных устройств
            public const string DevicesTitle = @"Комплексы промышленных устройств";
            #endregion
            #region URI
            public const string UriRouteRoot = "/";
            public const string UriRouteTableScales = "/scales";
            public const string UriRouteTableHosts = "/hosts";
            public const string UriRouteTablePrinters = "/printers";
            public const string UriRouteTablePrinterTypes = "/printertypes";
            public const string UriRouteTableContragents = "/contragents";
            public const string UriRouteTableNomenclature = "/nomenclature";
            public const string UriRouteTableProductionFacilities = "/productionfacilities";
            public const string UriRouteTableTemplates = "/templates";
            public const string UriRouteTableTemplateResources = "/templateresources";
            public const string UriRouteTableWeithingFacts = "/weithingfacts";
            public const string UriRouteTableWorkshops = "/workshops";
            public const string UriRouteItemPrinter = "/printer";
            public const string UriRouteTableLogs = "/logs";
            public const string UriRouteSecurity = "/security";
            public const string UriRouteLogin = "/login";
            public const string UriRouteAccess = "/access";
            #endregion
            #region Таблицы
            public const string Table = @"Таблица";
            public const string TableReadData = @"Прочитать данные";
            public const string TableActions = @"Действия";
            public const string TableActionsIsDeny = @"Действия недоступны";
            public const string TableActionEdit = @"Редактировать";
            public const string TableActionFill = @"Заполнить";
            public const string TableActionAdd = @"Добавить";
            public const string TableActionClear = @"Очистить";
            public const string TableActionDelete = @"Удалить";
            public const string TableActionDeleteForever = @"Удалить навсегда";
            public const string TableActionCopy = @"Копировать";
            public const string TableActionCancel = @"Отмена";
            public const string TableActionSave = @"Сохранить";
            public const string TableTitleFonts = @"Шрифты";
            public const string TableTitleLabels = @"Логотипы";
            public const string TableTitleBarCodesShort = @"Штрихкоды";
            public const string TableTitleBarCodeTypesShort = @"Типы ШК";
            public const string TableTitleContragentsShort = @"Контрагенты";
            public const string TableTitleNomenclatureShort = @"Номенклатура";
            public const string TableTitleNomenclatureUnitsShort = @"Упаковки";
            public const string TableTitleOrdersShort = @"Заказы";
            public const string TableTitleOrderStatusShort = @"Статусы заказов";
            public const string TableTitleOrderTypesShort = @"Типы заказов";
            public const string TableTitlePluShort = @"PLU";
            public const string TableTitleProductionFacilityShort = @"Произв. площадки";
            public const string TableTitleProductSeriesShort = @"Серии продуктов";
            public const string TableTitlePrinters = @"Принтеры";
            public const string TableTitlePrinterTypes = @"Типы принтеров";
            public const string TableTitleHostsShort = @"Хосты";
            public const string TableTitleScalesShort = @"Устройства";
            public const string TableTitleSsccStorageShort = @"SSCCStorage";
            public const string TableTitleResources = @"Ресурсы шаблонов";
            public const string TableTitleTemplateResourcesShort = @"Ресурсы шаблонов";
            public const string TableTitleTemplatesShort = @"Шаблоны";
            public const string TableTitleWeithingFactShort = @"Взвешивания";
            public const string TableTitleAccess = @"Доступ";
            public const string TableTitleLogs = @"Логи";
            public const string TableTitleWorkShopShort = @"Цеха";
            public const string TableFieldId = @"ID";
            public const string TableFieldName = @"Наименование";
            public const string TableFieldLine = @"Линия";
            public const string TableFieldRow = @"Строка";
            public const string TableFieldFile = @"Файл";
            public const string TableFieldMember = @"Метод";
            public const string TableFieldIcon = @"Иконка";
            public const string TableFieldMessage = @"Сообщение";
            public const string TableFieldPluDescription = @"Для переноса строки используйте символ `|`";
            public const string TableFieldType = @"Тип";
            public const string TableFieldTitle = @"Заголовок";
            public const string TableFieldCategoryId = @"ID категории";
            public const string TableFieldCategoryName = @"Категория";
            public const string TableFieldImageData = @"ImageData";
            public const string TableFieldImageDataInfo = @"Информация";
            public const string TableFieldProductionFacilityName = @"Площадка";
            public const string TableFieldDeviceIp = @"IP устройства";
            public const string TableFieldIdRref = @"ID 1C";
            public const string TableFieldDevicePort = @"Порт устройства";
            public const string TableFieldDeviceMac = @"MAC устройства";
            public const string TableFieldDeviceSendTimeout = @"Таймаут отправки";
            public const string TableFieldDeviceReceiveTimeout = @"Таймаут приёма";
            public const string TableFieldDeviceComPort = @"COM-порт";
            public const string TableFieldDeviceNumber = @"Номер устройства";
            public const string TableFieldUseOrder = @"Использовать заказ";
            public const string TableFieldTemplateIdDefault = @"ID шаблона";
            public const string TableFieldTemplateIdSeries = @"ID серии";
            public const string TableFieldTemplateSeries = @"Шаблон суммарной этикетки";
            public const string TableFieldTemplateDefault = @"Шаблон по-умолчанию";
            public const string TableFieldWorkShopId = @"ID цеха";
            public const string TableFieldWorkShopName = @"Цех";
            public const string TableFieldScaleFactor = @"Scale factor";
            public const string TableFieldNameFull = @"Полное наименование";
            public const string TableFieldDescription = @"Описание";
            public const string TableFieldComment = @"Комментарий";
            public const string TableFieldXml = @"XML";
            public const string TableFieldBrand = @"Brand";
            public const string TableFieldGuidMercury = @"GUID Mercury";
            public const string TableFieldNomenclatureType = @"Тип номенклатуры";
            public const string TableFieldVatRate = @"Ставка НДС";
            public const string TableFieldCode = @"Код";
            public const string TableFieldModifiedDate = @"Дата редактирования";
            public const string TableFieldIdRRef = @"ID 1C";
            public const string TableFieldStorage = @"Склад";
            public const string TableFieldState = @"Статус";
            public const string TableFieldScale = @"Устройство";
            public const string TableFieldScaleId = @"ID весов";
            public const string TableFieldUid = @"UUID";
            public const string TableFieldIsClose = @"IsClose";
            public const string TableFieldSscc = @"SSCC";
            public const string TableFieldValue = @"Значение";
            public const string TableFieldMarked = @"В архиве";
            public const string TableFieldSettingsFile = @"Файл настроек";
            public const string TableFieldPackWeight = @"PackWeight";
            public const string TableFieldPackQuantly = @"PackQuantly";
            public const string TableFieldPackTypeId = @"PackTypeId";
            public const string TableFieldBarCodeTypeId = @"ID типа ШК";
            public const string TableFieldNomenclatureId = @"ID номенклатуры";
            public const string TableFieldNomenclatureName = @"Номенклатура";
            public const string TableFieldNomenclatureUnitId = @"ID юнита номенклатуры";
            public const string TableFieldContragentId = @"ID контрагента";
            public const string TableFieldGoodsName = @"Товар";
            public const string TableFieldGoodsFullName = @"Полное наименование";
            public const string TableFieldGoodsDescription = @"Описание товара";
            public const string TableFieldTemplateId = @"ID шаблона";
            public const string TableFieldTemplate = @"Шаблон";
            public const string TableFieldResource = @"Ресурс";
            public const string TableFieldGtin = @"GTIN";
            public const string TableFieldEan13 = @"EAN13";
            public const string TableFieldItf14 = @"ITF14";
            public const string TableFieldShelfLifeDays = @"Срок годности (суток)";
            public const string TableFieldGoodsTareWeight = @"Тара";
            public const string TableFieldGoodsBruttoWeight = @"Вес брутто";
            public const string TableFieldGoodsBoxQuantly = @"Вложений в короб";
            public const string TableFieldConsumerName = @"ConsumerName";
            public const string TableFieldGln = @"Gln";
            public const string TableFieldPlu = @"PLU";
            public const string TableFieldActive = @"Активно";
            public const string TableFieldZebraPrinter = @"Принтер";
            public const string TableFieldZebraPrinterIp = @"IP-адрес";
            public const string TableFieldZebraPrinterPort = @"Порт принтера";
            public const string TableFieldZebraPrinterMac = @"MAC-адрес";
            public const string TableFieldZebraPrinterPeelOffSet = @"Смещение";
            public const string TableFieldZebraPrinterDarknessLevel = @"Уровень темноты";
            public const string TableFieldZebraPrinterType = @"Тип принтера";
            public const string TableFieldZebraPrinterPassword = @"Пароль принтера";
            public const string TableFieldUpperWeightThreshold = @"Верхнее значение веса короба";
            public const string TableFieldNominalWeight = @"Номинальный вес короба";
            public const string TableFieldLowerWeightThreshold = @"Нижнее значение веса короба";
            public const string TableFieldCheckWeight = @"Весовая продукция";
            public const string TableFieldCheckGtin = @"v";
            public const string TableFieldHost = @"Хост";
            public const string TableFieldWeithingDate = @"Дата взвешивания";
            public const string TableFieldCreateDt = @"Дата создания";
            public const string TableFieldChangeDt = @"Дата измнения";
            public const string TableFieldProductDate = @"Дата продукции";
            public const string TableFieldNetWeight = @"Вес нетто";
            public const string TableFieldTareWeight = @"Вес тары";
            public const string TableFieldRegNum = @"#";
            public const string TableFieldKneding = @"Взвешено";
            public const string TableFieldCount = @"Количество";
            public const string TableFieldDayOfWeek = @"День недели";
            public const string TableFieldApp = @"Программа";
            public const string TableFieldVersion = @"Версия";
            public const string TableFieldUser = @"Пользователь";
            public const string TableFieldLevel = @"Уровень";
            public const string TableFieldAccessLevel = @"Уровень доступа";
            #endregion
            #region Item
            public const string ItemTitlePrinter = @"Принтер";
            public const string ItemTitleBarCodeType = @"Тип ШК";
            public const string ItemTitleContragents = @"Контрагент";
            public const string ItemTitleHosts = @"Хост";
            public const string ItemTitleLabels = @"Этикетка";
            public const string ItemTitleNomenclature = @"Номенклатура";
            public const string ItemTitleOrders = @"Заказ";
            public const string ItemTitleOrderStatus = @"Статус заказа";
            public const string ItemTitleOrderTypes = @"Тип заказа";
            public const string ItemTitlePlu = @"ПЛУ";
            public const string ItemTitleProductionFacility = @"ProductionFacility";
            public const string ItemTitleProductSeries = @"ProductSeries";
            public const string ItemTitleScales = @"Устройство";
            public const string ItemTitleSsccStorage = @"SsccStorage";
            public const string ItemTitleTemplateResources = @"Ресурс шаблона";
            public const string ItemTitleTemplates = @"Шаблон";
            public const string ItemTitleWeithingFact = @"Взвешивание";
            public const string ItemTitleWorkShop = @"WorkShop";
            public const string ItemTitlePrinterResourceRef = @"Ресурс принтера";
            public const string ItemTitlePrinterType = @"Тип принтера";
            public const string ItemTitleLog = @"Лог";
            #endregion
            #region Контроль ввода
            public const string InputControlMuchZero = @"Значение должно быть больше 0";

            #endregion
            public static string GetItemTitle(EnumTable table)
            {
                return table switch
                {
                    EnumTable.Printer => ItemTitlePrinter,
                    EnumTable.BarCodeTypes => ItemTitleBarCodeType,
                    EnumTable.Contragents => ItemTitleContragents,
                    EnumTable.Hosts => ItemTitleHosts,
                    EnumTable.Labels => ItemTitleLabels,
                    EnumTable.Nomenclature => ItemTitleNomenclature,
                    EnumTable.Orders => ItemTitleOrders,
                    EnumTable.OrderStatus => ItemTitleOrderStatus,
                    EnumTable.OrderTypes => ItemTitleOrderTypes,
                    EnumTable.Plu => ItemTitlePlu,
                    EnumTable.ProductionFacility => ItemTitleProductionFacility,
                    EnumTable.ProductSeries => ItemTitleProductSeries,
                    EnumTable.Scales => ItemTitleScales,
                    EnumTable.SsccStorage => ItemTitleSsccStorage,
                    EnumTable.TemplateResources => ItemTitleTemplateResources,
                    EnumTable.Templates => ItemTitleTemplates,
                    EnumTable.WeithingFact => ItemTitleWeithingFact,
                    EnumTable.WorkShop => ItemTitleWorkShop,
                    EnumTable.PrinterResourceRef => ItemTitlePrinterResourceRef,
                    EnumTable.PrinterType => ItemTitlePrinterType,
                    EnumTable.Logs => ItemTitleLog,
                    _ => string.Empty,
                };
            }
        }

        public static class MdmControl
        {
            
        }

        public static string GetAppVersion()
        {
            System.Reflection.Assembly executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            return fieVersionInfo.FileVersion;
        }
    }
}
