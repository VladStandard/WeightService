// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using System;
using static DataCore.Localizations.LocaleCore;

namespace DataCore.Localizations
{
    public static class LocaleData
    {
        private static ShareEnums.Lang _lang;
        public static ShareEnums.Lang Lang { get => _lang; set => _lang = value; }

        static LocaleData()
        {
            Lang = ShareEnums.Lang.Russian;
        }

        public static class Program
        {
            public static string IsClosed => Lang == ShareEnums.Lang.English ? "The program is closed." : "Программа закрыта.";
            public static string IsLoaded => Lang == ShareEnums.Lang.English ? "The program is loaded." : "Программа загруженна.";
            public static string IsNotLoaded => Lang == ShareEnums.Lang.English ? "The program is not yet loaded!" + Environment.NewLine + "Wait for it..." : "Программа ещё не загружена!" + Environment.NewLine + "Подождите...";
            public static string IsRunning => Lang == ShareEnums.Lang.English ? "The program is running." : "Программа запущена.";
        }

        public static class DeviceControl
        {
            public static string DataRecords => Lang == ShareEnums.Lang.English ? "records" : "записей";
            public static string DevicesTitle => Lang == ShareEnums.Lang.English ? "Complexes of industrial devices" : "Комплексы промышленных устройств";
            public static string Index => Lang == ShareEnums.Lang.English ? "DeviceControl" : "Управление устройствами";
            public static string IndexAccessQuery => Lang == ShareEnums.Lang.English ? "Contact your system administrator to access." : "Свяжитесь с администратором системы, чтобы получить доступ.";
            public static string IndexContinue => Lang == ShareEnums.Lang.English ? "Click on a menu section to continue." : "Нажмите на раздел меню, чтобы продолжить.";
            public static string LinkEmail => "morozov_dv@kolbasa-vs.ru";
            public static string LinkEmailWithSubject => "mailto:morozov_dv@kolbasa-vs.ru&subject=device-control";
            public static string LinkLabelary => "http://labelary.com/viewer.html";
            public static string SqlServerDebug => "CREATIO";
            public static string SqlServerRelease => "PALYCH";
            #region URI route item
            public static class UriRouteItem
            {
                public const string Access = "/item/access";
                public const string Account = "/item/account";
                public const string BarCodeType = "/item/barcodetype";
                public const string Contragent = "/item/contragent";
                public const string Error = "/item/error";
                public const string Host = "/item/host";
                public const string Info = "/item/info";
                public const string Label = "/item/measurements/label";
                public const string Log = "/item/log";
                public const string LogType = "/item/logtype";
                public const string Nomenclature = "/item/nomenclature";
                public const string Plu = "/item/plu";
                public const string Printer = "/item/printer";
                public const string PrinterResource = "/item/printerresource";
                public const string PrinterType = "/item/printertype";
                public const string ProductionFacility = "/item/productionfacility";
                public const string Scale = "/item/scale";
                public const string TaskModule = "/item/taskmodule";
                public const string TaskTypeModule = "/item/tasktypemodule";
                public const string Template = "/item/template";
                public const string TemplateResource = "/item/templateresource";
                public const string WeithingFact = "/item/measurements/wf";
                public const string WorkShop = "/item/workshop";
            }
            #endregion
            #region URI route section
            public static class UriRouteSection
            {
                public const string Access = "/section/access";
                public const string BarCodes = "/section/barcodes";
                public const string BarCodeTypes = "/section/barcodetypes";
                public const string Contragents = "/section/contragents";
                public const string Docs = "/section/docs";
                public const string Errors = "/section/errors";
                public const string Hosts = "/section/hosts";
                public const string Info = "/section/info";
                public const string Labels = "/section/measurements/labels";
                public const string Logs = "/section/logs";
                public const string LogsNones = "/section/logs_nones";
                public const string LogsInformations = "/section/logs_informations";
                public const string LogsErrors = "/section/logs_errors";
                public const string LogsQuestions = "/section/logs_questions";
                public const string LogsStops = "/section/logs_stops";
                public const string LogsWarnings = "/section/logs_warnings";
                public const string LogTypes = "/section/logtypes";
                public const string Nomenclatures = "/section/nomenclatures";
                public const string Plus = "/section/plus";
                public const string PrinterResources = "/section/printerresources";
                public const string Printers = "/section/printers";
                public const string PrinterTypes = "/section/printertypes";
                public const string ProductionFacilities = "/section/productionfacilities";
                public const string Root = "/";
                public const string Scales = "/section/scales";
                public const string TaskModules = "/section/taskmodules";
                public const string TaskTypeModules = "/section/tasktypemodules";
                public const string TemplateResources = "/section/templateresources";
                public const string Templates = "/section/templates";
                public const string WeithingFacts = "/section/measurements/weithingfacts";
                public const string WeithingFactsAggregation = "/section/measurements/weithingfacts_aggregation";
                public const string WorkShops = "/section/workshops";
            }
            #endregion
            public static class Items
            {
                public static string Barcode => Lang == ShareEnums.Lang.English ? "Barcode" : "Штрихкод";
                public static string BarCodeType => Lang == ShareEnums.Lang.English ? "Barcodes type" : "Тип штрихкода";
                public static string Contragent => Lang == ShareEnums.Lang.English ? "Counterparty" : "Контрагент";
                public static string Device => Lang == ShareEnums.Lang.English ? "Device" : "Устройство";
                public static string Error => Lang == ShareEnums.Lang.English ? "Error" : "Ошибка";
                public static string Font => Lang == ShareEnums.Lang.English ? "Font" : "Шрифт";
                public static string Host => Lang == ShareEnums.Lang.English ? "Host" : "Хост";
                public static string Label => Lang == ShareEnums.Lang.English ? "Label" : "Этикетка";
                public static string Log => Lang == ShareEnums.Lang.English ? "Log" : "Лог";
                public static string Logo => Lang == ShareEnums.Lang.English ? "Logo" : "Логотип";
                public static string Module => Lang == ShareEnums.Lang.English ? "Module" : "Модуль";
                public static string Nomenclature => Lang == ShareEnums.Lang.English ? "Nomenclature" : "Номенклатура";
                public static string NomenclatureUnit => Lang == ShareEnums.Lang.English ? "Package" : "Упаковка";
                public static string Order => Lang == ShareEnums.Lang.English ? "Order" : "Заказ";
                public static string OrderStatus => Lang == ShareEnums.Lang.English ? "Order status" : "Статус заказа";
                public static string OrderType => Lang == ShareEnums.Lang.English ? "Order type" : "Типы заказа";
                public static string Organization => Lang == ShareEnums.Lang.English ? "Organization" : "Организация";
                public static string Plu => Lang == ShareEnums.Lang.English ? "PLU" : "ПЛУ";
                public static string ProductionFacilities => Lang == ShareEnums.Lang.English ? "Prod. facilities" : "Производственные площадки";
                public static string ProductionFacility => Lang == ShareEnums.Lang.English ? "Prod. facility" : "Произв. площадка";
                public static string ProductSeries => Lang == ShareEnums.Lang.English ? "Product series" : "Серия продукта";
                public static string Resource => Lang == ShareEnums.Lang.English ? "Resource" : "Ресурс";
                public static string Scale => Lang == ShareEnums.Lang.English ? "Device" : "Устройство";
                public static string Task => Lang == ShareEnums.Lang.English ? "Task" : "Задача";
                public static string TaskModule => Lang == ShareEnums.Lang.English ? "Task module" : "Модуль задачи";
                public static string Template => Lang == ShareEnums.Lang.English ? "Template" : "Шаблон";
                public static string TemplateResource => Lang == ShareEnums.Lang.English ? "Template resource" : "Ресурс шаблона";
                public static string WeithingFact => Lang == ShareEnums.Lang.English ? "Weithing fact" : "Взвешивание";
                public static string Workshop => Lang == ShareEnums.Lang.English ? "Workshop" : "Цех";
            }

            public static class Sections
            {
                public static string AutomatedWorkplace => Lang == ShareEnums.Lang.English ? "Automated Workplaces" : "Автоматизированные Рабочие Места";
                public static string AutomatedWorkplaceShort => Lang == ShareEnums.Lang.English ? "AWs" : "АРМы";
                public static string BarCodes => Lang == ShareEnums.Lang.English ? "Barcodes" : "Штрихкоды";
                public static string BarCodesShort => Lang == ShareEnums.Lang.English ? "BC" : "ШК";
                public static string BarCodeTypes => Lang == ShareEnums.Lang.English ? "Barcodes types" : "Типы штрихкодов";
                public static string BarCodeTypesShort => Lang == ShareEnums.Lang.English ? "BC types" : "Типы ШК";
                public static string Contragents => Lang == ShareEnums.Lang.English ? "Counterparties" : "Контрагенты";
                public static string Devices => Lang == ShareEnums.Lang.English ? "Devices" : "Устройства";
                public static string Fonts => Lang == ShareEnums.Lang.English ? "Fonts" : "Шрифты";
                public static string Hosts => Lang == ShareEnums.Lang.English ? "Hosts" : "Хосты";
                public static string Labels => Lang == ShareEnums.Lang.English ? "Labels" : "Этикетки";
                public static string Logos => Lang == ShareEnums.Lang.English ? "Logos" : "Логотипы";
                public static string Logs => Lang == ShareEnums.Lang.English ? "Logs" : "Логи";
                public static string Measurements => Lang == ShareEnums.Lang.English ? "Measurements" : "Измерения";
                public static string Modules => Lang == ShareEnums.Lang.English ? "Modules" : "Модули";
                public static string Nomenclatures => Lang == ShareEnums.Lang.English ? "Nomenclatures" : "Номенклатура";
                public static string NomenclatureUnits => Lang == ShareEnums.Lang.English ? "Packages" : "Упаковки";
                public static string Orders => Lang == ShareEnums.Lang.English ? "Orders" : "Заказы";
                public static string OrderStatuses => Lang == ShareEnums.Lang.English ? "Order statuses" : "Статусы заказов";
                public static string OrderTypes => Lang == ShareEnums.Lang.English ? "Order types" : "Типы заказов";
                public static string Organizations => Lang == ShareEnums.Lang.English ? "Organizations" : "Организации";
                public static string Plus => Lang == ShareEnums.Lang.English ? "PLU" : "ПЛУ";
                public static string ProductionFacilities => Lang == ShareEnums.Lang.English ? "Production facilities" : "Производственные площадки";
                public static string ProductionFacilitiesShort => Lang == ShareEnums.Lang.English ? "Facilities" : "Площадки";
                public static string ProductSeries => Lang == ShareEnums.Lang.English ? "Product series" : "Серии продуктов";
                public static string References => Lang == ShareEnums.Lang.English ? "References" : "Справочники";
                public static string ReferencesAdditional => Lang == ShareEnums.Lang.English ? "Add. references" : "Доп. справочники";
                public static string ReferencesDev => Lang == ShareEnums.Lang.English ? "Development" : "Разработка";
                public static string ReferencesDwh => Lang == ShareEnums.Lang.English ? "DWH References" : "DWH справочники";
                public static string Resources => Lang == ShareEnums.Lang.English ? "Resources" : "Ресурсы";
                public static string Scales => Lang == ShareEnums.Lang.English ? "Devices" : "Устройства";
                public static string TaskModules => Lang == ShareEnums.Lang.English ? "Task Modules" : "Модули задач";
                public static string Tasks => Lang == ShareEnums.Lang.English ? "Tasks" : "Задачи";
                public static string TemplateResources => Lang == ShareEnums.Lang.English ? "Template resources" : "Ресурсы шаблонов";
                public static string Templates => Lang == ShareEnums.Lang.English ? "Templates" : "Шаблоны";
                public static string WeithingFacts => Lang == ShareEnums.Lang.English ? "Weithing facts" : "Взвешивания";
                public static string WeithingFactsAggregation => Lang == ShareEnums.Lang.English ? "Aggregation weithings" : "Взвешивания";
                public static string WeithingFactsAggregationShort => Lang == ShareEnums.Lang.English ? "Aggr. weithings" : "Агр. взвешивания";
                public static string WorkShops => Lang == ShareEnums.Lang.English ? "Workshops" : "Цеха";
            }
            #region Tables
            public static string Actions => Lang == ShareEnums.Lang.English ? "Aaction" : "Действия";
            public static string Table => Lang == ShareEnums.Lang.English ? "Table" : "Таблица";
            public static string TableActionAdd => Lang == ShareEnums.Lang.English ? "Add" : "Добавить";
            public static string TableActionCancel => Lang == ShareEnums.Lang.English ? "Cancel" : "Отмена";
            public static string TableActionClear => Lang == ShareEnums.Lang.English ? "Clear" : "Очистить";
            public static string TableActionCopy => Lang == ShareEnums.Lang.English ? "Copy" : "Копировать";
            public static string TableActionDelete => Lang == ShareEnums.Lang.English ? "Delete" : "Удалить";
            public static string TableActionEdit => Lang == ShareEnums.Lang.English ? "Edit" : "Редактировать";
            public static string TableActionFill => Lang == ShareEnums.Lang.English ? "Fill" : "Заполнить";
            public static string TableActionMark => Lang == ShareEnums.Lang.English ? "Mark" : "Пометить";
            public static string TableActionNew => Lang == ShareEnums.Lang.English ? "New" : "Новый";
            public static string TableActions => Lang == ShareEnums.Lang.English ? "___" : "Действия";
            public static string TableActionSave => Lang == ShareEnums.Lang.English ? "Save" : "Сохранить";
            public static string TableActionsIsDeny => Lang == ShareEnums.Lang.English ? "___" : "Действия недоступны";
            public static string TableReadData => Lang == ShareEnums.Lang.English ? "Read data" : "Прочитать данные";
            #endregion
        }

        public static class Resources
        {
            #region Main
            public static string AppTitle => Lang == ShareEnums.Lang.English ? "Resources VS" : "Ресурсы ВС";
            public static string CallbackEmail => "mailto:morozov_dv@kolbasa-vs.ru&subject=resources-vs.kolbasa-vs.local";
            public static string SupportCreatio => "https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/CaseSection/";
            public static string SysAdmin => Lang == ShareEnums.Lang.English ? "Administration" : "Администрирование";
            #endregion
            #region Support
            public static string SupportLinkCreatioText => Lang == ShareEnums.Lang.English ? "Creatio appeals" : "Creatio обращения";
            public static string SupportLinkHelpPath => Lang == ShareEnums.Lang.English ? "mailto:helpdesk@kolbasa-vs.ru?subject=Appeal" : "mailto:helpdesk@kolbasa-vs.ru?subject=Обращение";
            public static string SupportLinkHelpText => Lang == ShareEnums.Lang.English ? "Write a letter" : "Написать письмо";
            public static string SupportTitle => Lang == ShareEnums.Lang.English ? "Support service" : "Служба поддержки";
            #endregion
            #region Contacts
            public static string ContactsCreatioPath => "https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/ContactSectionV2/";
            public static string ContactsCreatioText => Lang == ShareEnums.Lang.English ? "Creatio contacts" : "Creatio контакты";
            public static string ContactsPhonePath => "http://sqlsrsp01.kolbasa-vs.local/Reports/browse/PhoneRP";
            public static string ContactsPhoneText => Lang == ShareEnums.Lang.English ? "Phone directory" : "Телефонный справочник";
            public static string ContactsTitle => Lang == ShareEnums.Lang.English ? "Contacts" : "Контакты";
            #endregion
            #region IT department
            public static string DepartmentItTitle => Lang == ShareEnums.Lang.English ? "IT department" : "ИТ отдел";
            #endregion
            #region Redmine
            public const string RedmineGooglePath = "http://192.168.0.50/projects/resources_it/wiki/%D0%A1%D1%81%D1%8B%D0%BB%D0%BA%D0%B0_%D0%BD%D0%B0_%D0%B3%D1%83%D0%B3%D0%BB_%D1%82%D0%B0%D0%B1%D0%BB%D0%B8%D1%86%D1%8B";
            public const string RedmineGoogleText = "Ссылка на гугл таблицы";
            public const string RedmineProjectsPath = "http://192.168.0.50/projects";
            public const string RedmineProjectsText = "проекты";
            public const string RedmineWikiPath = "http://192.168.0.50/projects/resources_it/wiki/Wiki";
            public static string RedmineTitle => "Redmine";
            public static string RedmineWikiText => "Wiki";
            #endregion
            #region Zabbix
            public static string ZabbixGlobalPath => "http://zabbix.kolbasa-vs.local/zabbix/zabbix.php?action=dashboard.view";
            public static string ZabbixGlobalText => Lang == ShareEnums.Lang.English ? "Global view" : "Глобальное представление";
            public static string ZabbixKolbasaPath => "http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7";
            public static string ZabbixKolbasaText => "kolbasa-vs-terrasoft";
            public static string ZabbixTitle => "Zabbix";
            public static string ZabbixWebPath => "http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7";
            public static string ZabbixWebText => Lang == ShareEnums.Lang.English ? "Web monitoring" : "Веб-мониторинг";
            #endregion
            #region Creatio
            public const string CreatioAcademyPath = "https://academy.terrasoft.ru/";
            public const string CreatioCommunityPath = "https://community.terrasoft.ru/";
            public const string CreatioCreDevDmDev = "http://cre-dev-dm.kolbasa-vs.local/0/dev";
            public const string CreatioCreDevDmLink = "http://cre-dev-dm.kolbasa-vs.local/";
            public const string CreatioCreDevIaDev = "http://cre-dev-ia.kolbasa-vs.local/0/dev";
            public const string CreatioCreDevIaLink = "http://cre-dev-ia.kolbasa-vs.local/";
            public const string CreatioCreStudyDevPath = "http://cre-study.kolbasa-vs.local/0/dev";
            public const string CreatioCreStudyPath = "http://cre-study.kolbasa-vs.local/";
            public const string CreatioCreTestDev = "http://cre-test.kolbasa-vs.local/0/dev";
            public const string CreatioCreTestLink = "http://cre-test.kolbasa-vs.local/";
            public const string CreatioCreUpgradeDevPath = "http://cre-upgrade.kolbasa-vs.local/0/dev";
            public const string CreatioCreUpgradePath = "http://cre-upgrade.kolbasa-vs.local/";
            public const string CreatioMarketplacePath = "https://marketplace.terrasoft.ru/";
            public const string CreatioTerrasoftDevPath = "https://kolbasa-vs.terrasoft.ru/0/dev";
            public const string CreatioTerrasoftPath = "https://kolbasa-vs.terrasoft.ru/";
            public const string CreatioTerrasoftPre2DevPath = "https://dev2-kolbasa-vs.terrasoft.ru/0/dev";
            public const string CreatioTerrasoftPre2Path = "https://dev2-kolbasa-vs.terrasoft.ru/";
            public const string CreatioTerrasoftPreDevPath = "https://dev-kolbasa-vs.terrasoft.ru/0/dev";
            public const string CreatioTerrasoftPrePath = "https://dev-kolbasa-vs.terrasoft.ru/";
            public const string CreatioWebinarsPath = "\\isexcd02\\Webinars\\";
            public static string CreatioAcademyText => Lang == ShareEnums.Lang.English ? "Academy" : "Академия";
            public static string CreatioCommunityText => Lang == ShareEnums.Lang.English ? "Community" : "Сообщество";
            public static string CreatioCreDevDmName => Lang == ShareEnums.Lang.English ? "Website development Morozov D.V." : "Сайт разработки Морозов Д.В.";
            public static string CreatioCreDevIaName => Lang == ShareEnums.Lang.English ? "Website development Andreev I.A." : "Сайт разработки Андреев И.А.";
            public static string CreatioCreStudyDevText => Lang == ShareEnums.Lang.English ? "cre-study | Training Configuration" : "cre-study | Конфигурация обучения";
            public static string CreatioCreStudyText => Lang == ShareEnums.Lang.English ? "cre-study | Training site" : "cre-study | Сайт обучения";
            public static string CreatioCreTestName => Lang == ShareEnums.Lang.English ? "Website testing" : "Сайт тестирования";
            public static string CreatioCreUpgradeDevText => Lang == ShareEnums.Lang.English ? "Cre-upgrade | Upgrade Configuration" : "cre-upgrade | Конфигурация обновления";
            public static string CreatioCreUpgradeText => Lang == ShareEnums.Lang.English ? "Cre-upgrade | update site" : "cre-upgrade | Сайт обновления";
            public static string CreatioMarketplaceText => Lang == ShareEnums.Lang.English ? "Marketplace" : "Маркетплейс";
            public static string CreatioOfficialTitle => Lang == ShareEnums.Lang.English ? "The official sites of Terrasoft Creatio" : "Официальные сайты Terrasoft Creatio";
            public static string CreatioRemoteTitle => Lang == ShareEnums.Lang.English ? "Public sites" : "Публичные сайты";
            public static string CreatioTableFieldDev => Lang == ShareEnums.Lang.English ? "Configuration" : "Конфигурация";
            public static string CreatioTableFieldLink => Lang == ShareEnums.Lang.English ? "Link" : "Ссылка";
            public static string CreatioTableFieldName => Lang == ShareEnums.Lang.English ? "Site" : "Сайт";
            public static string CreatioTableTitle => Lang == ShareEnums.Lang.English ? "Local sites" : "Локальные сайты";
            public static string CreatioTerrasoftDevText => Lang == ShareEnums.Lang.English ? "kolbasa-vs.terrasoft.ru | Configuration of the product environment" : "kolbasa-vs.terrasoft.ru | Конфигурация продуктовой среды";
            public static string CreatioTerrasoftPre2DevText => Lang == ShareEnums.Lang.English ? "dev2-kolbasa-vs.terrasoft.ru | Configuration pre-prod 1" : "dev2-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public static string CreatioTerrasoftPre2Text => Lang == ShareEnums.Lang.English ? "dev2-kolbasa-vs.terrasoft.ru | Pre-prod 2 website" : "dev2-kolbasa-vs.terrasoft.ru | Сайт пре-прод 2";
            public static string CreatioTerrasoftPreDevText => Lang == ShareEnums.Lang.English ? "dev-kolbasa-vs.terrasoft.ru | Configuration pre-prod 1" : "dev-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public static string CreatioTerrasoftPreText => Lang == ShareEnums.Lang.English ? "dev-kolbasa-vs.terrasoft.ru | Site pre-prod 1" : "dev-kolbasa-vs.terrasoft.ru | Сайт пре-прод 1";
            public static string CreatioTerrasoftText => Lang == ShareEnums.Lang.English ? "kolbasa-vs.terrasoft.ru | Product environment" : "dev2-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public static string CreatioTitle => Lang == ShareEnums.Lang.English ? "Creatio-server" : "Creatio-сервер";
            public static string CreatioWebinarsText => Lang == ShareEnums.Lang.English ? "Webinars" : "Вебинары";
            #endregion
            #region Контроль версий
            public const string VersionControlSvnVisualStudioPath = "http://192.168.0.50/svn/visualstudio/";
            public const string VersionControlSvnVisualStudioText = "SVN visualstudio";
            public const string VersionControlTitle = "Контроль версий";
            #endregion
            #region Веб-сервер
            public const string IisServerBlazorBackupPath = "https://blazor-backup-control.kolbasa-vs.local/";
            public const string IisServerBlazorBackupText = "blazor-backup-control | Управление бэкапами";
            public const string IisServerBlazorDao1Path = "https://data-accessor-test-1.kolbasa-vs.local/";
            public const string IisServerBlazorDao1Text = "data-accessor-test-1 | Доступ к данным 1";
            public const string IisServerBlazorDao2Path = "https://data-accessor-test-2.kolbasa-vs.local/";
            public const string IisServerBlazorDao2Text = "data-accessor-test-2 | Доступ к данным 2";
            public const string IisServerBlazorDbDocPath = "https://database-doc.kolbasa-vs.local/";
            public const string IisServerBlazorDbDocText = "database-doc | Database Routine Maintanance Project";
            public const string IisServerBlazorDevicePath = "https://blazor-device-control.kolbasa-vs.local/";
            public const string IisServerBlazorDeviceText = "blazor-device-control | Управление устройствами";
            public const string IisServerBlazorExamplePath = "https://blazor-example.kolbasa-vs.local/";
            public const string IisServerBlazorExampleText = "blazor-example | Пример сайта на Blazor";
            public const string IisServerBlazorResourcesVsPath = "https://resources-vs.kolbasa-vs.local/";
            public const string IisServerBlazorResourcesVsText = "resources-vs | Внутренние ресурсы";
            public const string IisServerBlazorSmart1Path = "https://smart-net-data-accessor-test-1.kolbasa-vs.local/";
            public const string IisServerBlazorSmart1Text = "smart-net-data-accessor-test-1 | DAO example 1";
            public const string IisServerBlazorSmart2Path = "https://smart-net-data-accessor-test-2.kolbasa-vs.local/";
            public const string IisServerBlazorSmart2Text = "smart-net-data-accessor-test-2 | | DAO example 2";
            public const string IisServerTitle = "Веб-сервер";
            #endregion
        }

        public static class Paths
        {
            public const string ScalesTerminal = "C:\\Program Files (x86)\\Massa-K\\ScalesTerminal 100\\ScalesTerminal.exe";
        }

        public static class Methods
        {
            public static string GetItemTitle(TableBase table)
            {
                string result = string.Empty;

                ProjectsEnums.TableSystem tableSystem = ProjectsEnums.GetTableSystem(table.Name);
                {
                    switch (tableSystem)
                    {
                        case ProjectsEnums.TableSystem.Accesses:
                            result = Strings.ItemAccess;
                            break;
                        case ProjectsEnums.TableSystem.Logs:
                            result = Strings.ItemLog;
                            break;
                    }
                }

                ProjectsEnums.TableScale tableScale = ProjectsEnums.GetTableScale(table.Name);
                {
                    switch (tableScale)
                    {
                        case ProjectsEnums.TableScale.BarCodeTypes:
                            result = DeviceControl.Items.BarCodeType;
                            break;
                        case ProjectsEnums.TableScale.Contragents:
                            result = DeviceControl.Items.Contragent;
                            break;
                        case ProjectsEnums.TableScale.Hosts:
                            result = DeviceControl.Items.Host;
                            break;
                        case ProjectsEnums.TableScale.Labels:
                            result = DeviceControl.Items.Label;
                            break;
                        case ProjectsEnums.TableScale.Nomenclatures:
                            result = DeviceControl.Items.Nomenclature;
                            break;
                        case ProjectsEnums.TableScale.OrdersStatuses:
                            result = DeviceControl.Items.OrderStatus;
                            break;
                        case ProjectsEnums.TableScale.OrdersTypes:
                            result = DeviceControl.Items.OrderType;
                            break;
                        case ProjectsEnums.TableScale.Orders:
                            result = DeviceControl.Items.Order;
                            break;
                        case ProjectsEnums.TableScale.Plus:
                            result = DeviceControl.Items.Plu;
                            break;
                        case ProjectsEnums.TableScale.Printers:
                            result = Print.Name;
                            break;
                        case ProjectsEnums.TableScale.PrintersResources:
                            result = Print.Resource;
                            break;
                        case ProjectsEnums.TableScale.PrintersTypes:
                            result = Print.Type;
                            break;
                        case ProjectsEnums.TableScale.ProductSeries:
                            result = DeviceControl.Items.ProductSeries;
                            break;
                        case ProjectsEnums.TableScale.ProductionFacilities:
                            result = DeviceControl.Items.ProductionFacility;
                            break;
                        case ProjectsEnums.TableScale.Scales:
                            result = DeviceControl.Items.Scale;
                            break;
                        case ProjectsEnums.TableScale.TemplatesResources:
                            result = DeviceControl.Items.TemplateResource;
                            break;
                        case ProjectsEnums.TableScale.Templates:
                            result = DeviceControl.Items.Template;
                            break;
                        case ProjectsEnums.TableScale.WeithingFacts:
                            result = DeviceControl.Items.WeithingFact;
                            break;
                        case ProjectsEnums.TableScale.Workshops:
                            result = DeviceControl.Items.Workshop;
                            break;
                        case ProjectsEnums.TableScale.Default:
                            break;
                        case ProjectsEnums.TableScale.Organizations:
                            result = DeviceControl.Items.Organization;
                            break;
                        case ProjectsEnums.TableScale.BarCodes:
                            break;
                    }
                }
                return result;
            }

            public static string GetItemTitle(TableBase table, int itemId)
            {
                return $"{GetItemTitle(table)}. ID: {itemId}";
            }

            public static string GetItemTitle(TableBase table, Guid itemUid)
            {
                return $"{GetItemTitle(table)}. UID: {itemUid}";
            }

            public static string GetSectionTitle(TableBase table)
            {
                string result = string.Empty;

                ProjectsEnums.TableSystem tableSystem = ProjectsEnums.GetTableSystem(table.Name);
                {
                    switch (tableSystem)
                    {
                        case ProjectsEnums.TableSystem.Accesses:
                            result = Strings.SectionAccess;
                            break;
                        case ProjectsEnums.TableSystem.Logs:
                            result = Strings.SectionLog;
                            break;
                    }
                }

                ProjectsEnums.TableScale tableScale = ProjectsEnums.GetTableScale(table.Name);
                {
                    switch (tableScale)
                    {
                        case ProjectsEnums.TableScale.Default:
                            break;
                        case ProjectsEnums.TableScale.BarCodes:
                            result = DeviceControl.Sections.BarCodes;
                            break;
                        case ProjectsEnums.TableScale.BarCodeTypes:
                            result = DeviceControl.Sections.BarCodeTypes;
                            break;
                        case ProjectsEnums.TableScale.Contragents:
                            result = DeviceControl.Sections.Contragents;
                            break;
                        case ProjectsEnums.TableScale.Hosts:
                            result = DeviceControl.Sections.Hosts;
                            break;
                        case ProjectsEnums.TableScale.Labels:
                            result = DeviceControl.Sections.Labels;
                            break;
                        case ProjectsEnums.TableScale.Nomenclatures:
                            result = DeviceControl.Sections.Nomenclatures;
                            break;
                        case ProjectsEnums.TableScale.OrdersStatuses:
                            result = DeviceControl.Sections.OrderStatuses;
                            break;
                        case ProjectsEnums.TableScale.OrdersTypes:
                            result = DeviceControl.Sections.OrderTypes;
                            break;
                        case ProjectsEnums.TableScale.Orders:
                            result = DeviceControl.Sections.Orders;
                            break;
                        case ProjectsEnums.TableScale.Plus:
                            result = DeviceControl.Sections.Plus;
                            break;
                        case ProjectsEnums.TableScale.Printers:
                            result = Print.Name;
                            break;
                        case ProjectsEnums.TableScale.PrintersResources:
                            result = Print.Resources;
                            break;
                        case ProjectsEnums.TableScale.PrintersTypes:
                            result = Print.Types;
                            break;
                        case ProjectsEnums.TableScale.ProductSeries:
                            result = DeviceControl.Sections.ProductSeries;
                            break;
                        case ProjectsEnums.TableScale.ProductionFacilities:
                            result = DeviceControl.Sections.ProductionFacilities;
                            break;
                        case ProjectsEnums.TableScale.Scales:
                            result = DeviceControl.Sections.Scales;
                            break;
                        case ProjectsEnums.TableScale.TemplatesResources:
                            result = DeviceControl.Sections.TemplateResources;
                            break;
                        case ProjectsEnums.TableScale.Templates:
                            result = DeviceControl.Sections.Templates;
                            break;
                        case ProjectsEnums.TableScale.WeithingFacts:
                            result = DeviceControl.Sections.WeithingFacts;
                            break;
                        case ProjectsEnums.TableScale.Workshops:
                            result = DeviceControl.Sections.WorkShops;
                            break;
                        case ProjectsEnums.TableScale.Organizations:
                            result = DeviceControl.Sections.Organizations;
                            break;
                    }
                }

                return result;
            }
        }
    }
}
