// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Utils;

public static partial class WsLocaleData
{
    public static class Resources
    {
        #region Main
        public static string AppTitle => Lang == WsEnumLanguage.English ? "Resources VS" : "Ресурсы ВС";
        public static string CallbackEmail => "mailto:morozov_dv@kolbasa-vs.ru&subject=resources-vs.kolbasa-vs.local";
        public static string SupportCreatio => "https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/CaseSection/";
        public static string SysAdmin => Lang == WsEnumLanguage.English ? "Administration" : "Администрирование";
        #endregion
        #region Support
        public static string SupportLinkCreatioText => Lang == WsEnumLanguage.English ? "Creatio appeals" : "Creatio обращения";
        public static string SupportLinkHelpPath => Lang == WsEnumLanguage.English ? "mailto:helpdesk@kolbasa-vs.ru?subject=Appeal" : "mailto:helpdesk@kolbasa-vs.ru?subject=Обращение";
        public static string SupportLinkHelpText => Lang == WsEnumLanguage.English ? "Write a letter" : "Написать письмо";
        public static string SupportTitle => Lang == WsEnumLanguage.English ? "Support service" : "Служба поддержки";
        #endregion
        #region Contacts
        public static string ContactsCreatioPath => "https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/ContactSectionV2/";
        public static string ContactsCreatioText => Lang == WsEnumLanguage.English ? "Creatio contacts" : "Creatio контакты";
        public static string ContactsPhonePath => "http://sqlsrsp01.kolbasa-vs.local/Reports/browse/PhoneRP";
        public static string ContactsPhoneText => Lang == WsEnumLanguage.English ? "Phone directory" : "Телефонный справочник";
        public static string ContactsTitle => Lang == WsEnumLanguage.English ? "Contacts" : "Контакты";
        #endregion
        #region IT department
        public static string DepartmentItTitle => Lang == WsEnumLanguage.English ? "IT department" : "ИТ отдел";
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
        public static string ZabbixGlobalText => Lang == WsEnumLanguage.English ? "Global view" : "Глобальное представление";
        public static string ZabbixKolbasaPath => "http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7";
        public static string ZabbixKolbasaText => "kolbasa-vs-terrasoft";
        public static string ZabbixTitle => "Zabbix";
        public static string ZabbixWebPath => "http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7";
        public static string ZabbixWebText => Lang == WsEnumLanguage.English ? "Web monitoring" : "Веб-мониторинг";
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
        public static string CreatioAcademyText => Lang == WsEnumLanguage.English ? "Academy" : "Академия";
        public static string CreatioCommunityText => Lang == WsEnumLanguage.English ? "Community" : "Сообщество";
        public static string CreatioCreDevDmName => Lang == WsEnumLanguage.English ? "Website development Morozov D.V." : "Сайт разработки Морозов Д.В.";
        public static string CreatioCreDevIaName => Lang == WsEnumLanguage.English ? "Website development Andreev I.A." : "Сайт разработки Андреев И.А.";
        public static string CreatioCreStudyDevText => Lang == WsEnumLanguage.English ? "cre-study | Training Configuration" : "cre-study | Конфигурация обучения";
        public static string CreatioCreStudyText => Lang == WsEnumLanguage.English ? "cre-study | Training site" : "cre-study | Сайт обучения";
        public static string CreatioCreTestName => Lang == WsEnumLanguage.English ? "Website testing" : "Сайт тестирования";
        public static string CreatioCreUpgradeDevText => Lang == WsEnumLanguage.English ? "Cre-upgrade | Upgrade Configuration" : "cre-upgrade | Конфигурация обновления";
        public static string CreatioCreUpgradeText => Lang == WsEnumLanguage.English ? "Cre-upgrade | update site" : "cre-upgrade | Сайт обновления";
        public static string CreatioMarketplaceText => Lang == WsEnumLanguage.English ? "Marketplace" : "Маркетплейс";
        public static string CreatioOfficialTitle => Lang == WsEnumLanguage.English ? "The official sites of Terrasoft Creatio" : "Официальные сайты Terrasoft Creatio";
        public static string CreatioRemoteTitle => Lang == WsEnumLanguage.English ? "Public sites" : "Публичные сайты";
        public static string CreatioTableFieldDev => Lang == WsEnumLanguage.English ? "Configuration" : "Конфигурация";
        public static string CreatioTableFieldLink => Lang == WsEnumLanguage.English ? "Link" : "Ссылка";
        public static string CreatioTableFieldName => Lang == WsEnumLanguage.English ? "Site" : "Сайт";
        public static string CreatioTableTitle => Lang == WsEnumLanguage.English ? "Local sites" : "Локальные сайты";
        public static string CreatioTerrasoftDevText => Lang == WsEnumLanguage.English ? "kolbasa-vs.terrasoft.ru | Configuration of the product environment" : "kolbasa-vs.terrasoft.ru | Конфигурация продуктовой среды";
        public static string CreatioTerrasoftPre2DevText => Lang == WsEnumLanguage.English ? "dev2-kolbasa-vs.terrasoft.ru | Configuration pre-prod 1" : "dev2-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
        public static string CreatioTerrasoftPre2Text => Lang == WsEnumLanguage.English ? "dev2-kolbasa-vs.terrasoft.ru | Pre-prod 2 website" : "dev2-kolbasa-vs.terrasoft.ru | Сайт пре-прод 2";
        public static string CreatioTerrasoftPreDevText => Lang == WsEnumLanguage.English ? "dev-kolbasa-vs.terrasoft.ru | Configuration pre-prod 1" : "dev-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
        public static string CreatioTerrasoftPreText => Lang == WsEnumLanguage.English ? "dev-kolbasa-vs.terrasoft.ru | Site pre-prod 1" : "dev-kolbasa-vs.terrasoft.ru | Сайт пре-прод 1";
        public static string CreatioTerrasoftText => Lang == WsEnumLanguage.English ? "kolbasa-vs.terrasoft.ru | Product environment" : "dev2-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
        public static string CreatioTitle => Lang == WsEnumLanguage.English ? "Creatio-server" : "Creatio-сервер";
        public static string CreatioWebinarsText => Lang == WsEnumLanguage.English ? "Webinars" : "Вебинары";
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
}