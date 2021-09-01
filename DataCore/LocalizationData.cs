// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using DataCore.Models;
using System;
using static BlazorCore.LocalizationCore;

namespace DataCore
{
    public static class LocalizationData
    {
        public static EnumLang Lang { get; set; } = EnumLang.Russian;

        public static class DeviceControl
        {
            #region Main

            public static string Index => Lang == EnumLang.English ? @"DeviceControl" : @"Управление устройствами";
            public static string IndexContinue => Lang == EnumLang.English
                ? @"Click on a menu section to continue."
                : @"Нажмите на раздел меню, чтобы продолжить.";
            public static string SqlServerDebug => "CREATIO";
            public static string SqlServerRelease => "PALYCH";
            public static string CallbackEmail => Lang == EnumLang.English ?
                    @"mailto:morozov_dv@kolbasa-vs.ru&subject=device-control.kolbasa-vs.local" :
                    @"mailto:morozov_dv@kolbasa-vs.ru&subject=device-control.kolbasa-vs.local";
            public const string DataRecords = @"записей";
            #endregion
            #region Комплексы промышленных устройств
            public static string DevicesTitle => Lang == EnumLang.English ? @"Complexes of industrial devices" : @"Комплексы промышленных устройств";
            #endregion
            #region URI route item
            public const string UriRouteItemContragent = "/item/contragent";
            public const string UriRouteItemDevice = "/item/device";
            public const string UriRouteItemHost = "/item/host";
            public const string UriRouteItemNomenclature = "/item/nomenclature";
            public const string UriRouteItemPlu = "/item/plu";
            public const string UriRouteItemPrinter = "/item/printer";
            public const string UriRouteItemPrinterResource = "/item/printerresource";
            public const string UriRouteItemPrinterType = "/item/printertype";
            public const string UriRouteItemProductionFacility = "/item/productionfacily";
            public const string UriRouteItemScale = "/item/scale";
            public const string UriRouteItemTemplate = "/item/template";
            public const string UriRouteItemTemplateResource = "/item/templateresource";
            public const string UriRouteItemWeithingFact = "/item/weithingfact";
            public const string UriRouteItemWorkshop = "/item/workshop";
            #endregion
            #region URI route section
            public const string UriRouteSectionContragents = "/section/contragents";
            public const string UriRouteSectionDevices = "/section/devices";
            public const string UriRouteSectionHosts = "/section/hosts";
            public const string UriRouteSectionBarCodeTypes = "/section/barcodetypes";
            public const string UriRouteSectionNomenclatures = "/section/nomenclatures";
            public const string UriRouteSectionPrinterTypes = "/section/printertypes";
            public const string UriRouteSectionPrinterResources = "/section/printerresources";
            public const string UriRouteSectionPrinters = "/section/printers";
            public const string UriRouteSectionProductionFacilities = "/section/productionfacilities";
            public const string UriRouteSectionScales = "/section/scales";
            public const string UriRouteSectionTemplateResources = "/section/templateresources";
            public const string UriRouteSectionTemplates = "/section/templates";
            public const string UriRouteSectionWeithingFacts = "/section/weithingfacts";
            public const string UriRouteSectionWorkshops = "/section/workshops";
            #endregion
            #region Items scales
            public static string ItemBarCodeType => Lang == EnumLang.English ? @"Barcodes type" : @"Тип штрихкода";
            public static string ItemBarcode => Lang == EnumLang.English ? @"Barcode" : @"Штрихкод";
            public static string ItemContragent => Lang == EnumLang.English ? @"Counterparty" : @"Контрагент";
            public static string ItemFont => Lang == EnumLang.English ? @"Font" : @"Шрифт";
            public static string ItemHost => Lang == EnumLang.English ? @"Host" : @"Хост";
            public static string ItemLabel => Lang == EnumLang.English ? @"Label" : @"Этикетка";
            public static string ItemLogo => Lang == EnumLang.English ? @"Logo" : @"Логотип";
            public static string ItemNomenclatureUnit => Lang == EnumLang.English ? @"Package" : @"Упаковка";
            public static string ItemNomenclature => Lang == EnumLang.English ? @"Nomenclature" : @"Номенклатура";
            public static string ItemOrderStatus => Lang == EnumLang.English ? @"Order status" : @"Статус заказа";
            public static string ItemOrderType => Lang == EnumLang.English ? @"Order type" : @"Типы заказа";
            public static string ItemOrder => Lang == EnumLang.English ? @"Order" : @"Заказ";
            public static string ItemPlu => Lang == EnumLang.English ? @"PLU" : @"ПЛУ";
            public static string ItemPrinterResource => Lang == EnumLang.English ? @"Printer resource" : @"Ресурс принтера";
            public static string ItemPrinterType => Lang == EnumLang.English ? @"Printer type" : @"Тип принтера";
            public static string ItemPrinter => Lang == EnumLang.English ? @"Printer" : @"Принтер";
            public static string ItemProductSeries => Lang == EnumLang.English ? @"Product series" : @"Серия продукта";
            public static string ItemProductionFacility => Lang == EnumLang.English ? @"Prod. facility" : @"Произв. площадка";
            public static string ItemResource => Lang == EnumLang.English ? @"Resource" : @"Ресурс";
            public static string ItemScale => Lang == EnumLang.English ? @"Device" : @"Устройство";
            public static string ItemTemplateResource => Lang == EnumLang.English ? @"Template resource" : @"Ресурс шаблона";
            public static string ItemTemplate => Lang == EnumLang.English ? @"Template" : @"Шаблон";
            public static string ItemWeithingFact => Lang == EnumLang.English ? @"Weithing fact" : @"Взвешивание";
            public static string ItemWorkshop => Lang == EnumLang.English ? @"Workshop" : @"Цех";
            #endregion
            #region Sections
            public static string SectionBarCodeTypes => Lang == EnumLang.English ? @"Barcodes types" : @"Типы штрихкодов";
            public static string SectionBarcodes => Lang == EnumLang.English ? @"Barcodes" : @"Штрихкоды";
            public static string SectionContragents => Lang == EnumLang.English ? @"Counterparties" : @"Контрагенты";
            public static string SectionFonts => Lang == EnumLang.English ? @"Fonts" : @"Шрифты";
            public static string SectionHosts => Lang == EnumLang.English ? @"Hosts" : @"Хосты";
            public static string SectionLabels => Lang == EnumLang.English ? @"Labels" : @"Этикетки";
            public static string SectionLogos => Lang == EnumLang.English ? @"Logos" : @"Логотипы";
            public static string SectionLogs => Lang == EnumLang.English ? @"Logs" : @"Логи";
            public static string SectionNomenclatureUnits => Lang == EnumLang.English ? @"Packages" : @"Упаковки";
            public static string SectionNomenclatures => Lang == EnumLang.English ? @"Nomenclatures" : @"Номенклатура";
            public static string SectionOrderStatuses => Lang == EnumLang.English ? @"Order statuses" : @"Статусы заказов";
            public static string SectionOrderTypes => Lang == EnumLang.English ? @"Order types" : @"Типы заказов";
            public static string SectionOrders => Lang == EnumLang.English ? @"Orders" : @"Заказы";
            public static string SectionPlus => Lang == EnumLang.English ? @"PLU" : @"ПЛУ";
            public static string SectionPrinterResources => Lang == EnumLang.English ? @"Printer resources" : @"Ресурсы принтера";
            public static string SectionPrinterTypes => Lang == EnumLang.English ? @"Printer types" : @"Типы принтеров";
            public static string SectionPrinters => Lang == EnumLang.English ? @"Printers" : @"Принтеры";
            public static string SectionProductSeries => Lang == EnumLang.English ? @"Product series" : @"Серии продуктов";
            public static string SectionProductionFacilities => Lang == EnumLang.English ? @"Prod. facilities" : @"Произв. площадки";
            public static string SectionResources => Lang == EnumLang.English ? @"Resources" : @"Ресурсы";
            public static string SectionScales => Lang == EnumLang.English ? @"Devices" : @"Устройства";
            public static string SectionTemplateResources => Lang == EnumLang.English ? @"Template resources" : @"Ресурсы шаблонов";
            public static string SectionTemplates => Lang == EnumLang.English ? @"Templates" : @"Шаблоны";
            public static string SectionWeithingFacts => Lang == EnumLang.English ? @"Weithing facts" : @"Взвешивания";
            public static string SectionWorkshops => Lang == EnumLang.English ? @"Workshops" : @"Цеха";
            #endregion
            #region Tables
            public static string TableActionAdd => Lang == EnumLang.English ? @"Add" : @"Добавить";
            public static string TableActionCancel => Lang == EnumLang.English ? @"Cancel" : @"Отмена";
            public static string TableActionClear => Lang == EnumLang.English ? @"Clear" : @"Очистить";
            public static string TableActionCopy => Lang == EnumLang.English ? @"Copy" : @"Копировать";
            public static string TableActionDelete => Lang == EnumLang.English ? @"Delete" : @"Удалить";
            public static string TableActionEdit => Lang == EnumLang.English ? @"Edit" : @"Редактировать";
            public static string TableActionMark => Lang == EnumLang.English ? @"Mark" : @"Пометить";
            public static string TableActionNew => Lang == EnumLang.English ? @"New" : @"Новый";
            public static string TableActionFill => Lang == EnumLang.English ? @"Fill" : @"Заполнить";
            public static string TableActionSave => Lang == EnumLang.English ? @"Save" : @"Сохранить";

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
            public static string TableFieldPrinter => Lang == EnumLang.English ? @"Printer" : @"Принтер";
            public static string TableFieldPrinterControlPanel => Lang == EnumLang.English ? @"Printer control panel" : @"Панель управления принтером";
            public static string TableFieldPrinterDarknessLevel => Lang == EnumLang.English ? @"Level of darkness" : @"Уровень темноты";
            public static string TableFieldPrinterIp => Lang == EnumLang.English ? @"IP-address" : @"IP-адрес";
            public static string TableFieldPrinterMac => Lang == EnumLang.English ? @"MAC-address" : @"MAC-адрес";
            public static string TableFieldPrinterPassword => Lang == EnumLang.English ? @"Printer password" : @"Пароль принтера";
            public static string TableFieldPrinterPeelOffSet => Lang == EnumLang.English ? @"Offset" : @"Смещение";
            public static string TableFieldPrinterPort => Lang == EnumLang.English ? @"Printer port" : @"Порт принтера";
            public static string TableFieldPrinterType => Lang == EnumLang.English ? @"Printer type" : @"Тип принтера";
            public static string TableReadData => Lang == EnumLang.English ? @"Read data" : @"Прочитать данные";
            public static string Table => Lang == EnumLang.English ? @"Table" : @"Таблица";
            #endregion
        }

        public static class MdmControl
        {

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
            public static string CreatioTitle => Lang == EnumLang.English ? @"Creatio-server" : @"Creatio-сервер";
            public static string CreatioWebinarsText => Lang == EnumLang.English ? @"Webinars" : @"Вебинары";
            public const string CreatioWebinarsPath = @"\\isexcd02\Webinars\";
            public static string CreatioOfficialTitle => Lang == EnumLang.English ? @"The official sites of Terrasoft Creatio" : @"Официальные сайты Terrasoft Creatio";
            public static string CreatioAcademyText => Lang == EnumLang.English ? @"Academy" : @"Академия";
            public const string CreatioAcademyPath = @"https://academy.terrasoft.ru/";
            public static string CreatioMarketplaceText => Lang == EnumLang.English ? @"Marketplace" : @"Маркетплейс";
            public const string CreatioMarketplacePath = @"https://marketplace.terrasoft.ru/";
            public static string CreatioCommunityText => Lang == EnumLang.English ? @"Community" : @"Сообщество";
            public const string CreatioCommunityPath = @"https://community.terrasoft.ru/";
            public static string CreatioTableTitle => Lang == EnumLang.English ? @"Локальные сайты" : @"Локальные сайты";
            public static string CreatioTableFieldName => Lang == EnumLang.English ? @"Site" : @"Сайт";
            public static string CreatioTableFieldLink => Lang == EnumLang.English ? @"Link" : @"Ссылка";
            public static string CreatioTableFieldDev => Lang == EnumLang.English ? @"Configuration" : @"Конфигурация";
            public static string CreatioCreDevDmName => Lang == EnumLang.English ? @"Website development Morozov D.V." : @"Сайт разработки Морозов Д.В.";
            public const string CreatioCreDevDmLink = @"http://cre-dev-dm.kolbasa-vs.local/";
            public const string CreatioCreDevDmDev = @"http://cre-dev-dm.kolbasa-vs.local/0/dev";
            public static string CreatioCreDevIaName => Lang == EnumLang.English ? @"Website development Andreev I.A." : @"Сайт разработки Андреев И.А.";
            public const string CreatioCreDevIaLink = @"http://cre-dev-ia.kolbasa-vs.local/";
            public const string CreatioCreDevIaDev = @"http://cre-dev-ia.kolbasa-vs.local/0/dev";
            public static string CreatioCreTestName => Lang == EnumLang.English ? @"Website testing" : @"Сайт тестирования";
            public const string CreatioCreTestLink = @"http://cre-test.kolbasa-vs.local/";
            public const string CreatioCreTestDev = @"http://cre-test.kolbasa-vs.local/0/dev";
            public static string CreatioCreStudyText => Lang == EnumLang.English ? @"cre-study | Training site" : @"cre-study | Сайт обучения";
            public const string CreatioCreStudyPath = @"http://cre-study.kolbasa-vs.local/";
            public static string CreatioCreStudyDevText => Lang == EnumLang.English ? @"cre-study | Training Configuration" : @"cre-study | Конфигурация обучения";
            public const string CreatioCreStudyDevPath = @"http://cre-study.kolbasa-vs.local/0/dev";
            public static string CreatioCreUpgradeText => Lang == EnumLang.English ? @"Cre-upgrade | update site" : @"cre-upgrade | Сайт обновления";
            public const string CreatioCreUpgradePath = @"http://cre-upgrade.kolbasa-vs.local/";
            public static string CreatioCreUpgradeDevText => Lang == EnumLang.English ? @"Cre-upgrade | Upgrade Configuration" : @"cre-upgrade | Конфигурация обновления";
            public const string CreatioCreUpgradeDevPath = @"http://cre-upgrade.kolbasa-vs.local/0/dev";
            public static string CreatioRemoteTitle => Lang == EnumLang.English ? @"Public sites" : @"Публичные сайты";
            public static string CreatioTerrasoftPreText => Lang == EnumLang.English
                ? @"dev-kolbasa-vs.terrasoft.ru | Site pre-prod 1" : @"dev-kolbasa-vs.terrasoft.ru | Сайт пре-прод 1";
            public const string CreatioTerrasoftPrePath = @"https://dev-kolbasa-vs.terrasoft.ru/";
            public static string CreatioTerrasoftPreDevText => Lang == EnumLang.English
                ? @"dev-kolbasa-vs.terrasoft.ru | Configuration pre-prod 1" : @"dev-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public const string CreatioTerrasoftPreDevPath = @"https://dev-kolbasa-vs.terrasoft.ru/0/dev";
            public static string CreatioTerrasoftPre2Text => Lang == EnumLang.English
                ? @"dev2-kolbasa-vs.terrasoft.ru | Pre-prod 2 website" : @"dev2-kolbasa-vs.terrasoft.ru | Сайт пре-прод 2";
            public const string CreatioTerrasoftPre2Path = @"https://dev2-kolbasa-vs.terrasoft.ru/";
            public static string CreatioTerrasoftPre2DevText => Lang == EnumLang.English
                ? @"dev2-kolbasa-vs.terrasoft.ru | Configuration pre-prod 1" : @"dev2-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public const string CreatioTerrasoftPre2DevPath = @"https://dev2-kolbasa-vs.terrasoft.ru/0/dev";
            public static string CreatioTerrasoftText => Lang == EnumLang.English
                ? @"kolbasa-vs.terrasoft.ru | Product environment" : @"dev2-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public const string CreatioTerrasoftPath = @"https://kolbasa-vs.terrasoft.ru/";
            public static string CreatioTerrasoftDevText => Lang == EnumLang.English
                ? @"kolbasa-vs.terrasoft.ru | Configuration of the product environment" : @"kolbasa-vs.terrasoft.ru | Конфигурация продуктовой среды";
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

        public static class Methods
        {
            #region Public and private methods

            public static string GetItemTitle(ITableEntity table)
            {
                string result = string.Empty;
                if (table is TableSystemEntity tableSystem)
                {
                    switch (tableSystem.Value)
                    {
                        case EnumTableSystem.Accesses:
                            result = Strings.ItemAccess;
                            break;
                        case EnumTableSystem.Logs:
                            result = Strings.ItemLog;
                            break;
                    }
                }
                if (table is TableScaleEntity tableScales)
                {
                    switch (tableScales.Value)
                    {
                        case EnumTableScale.BarcodeTypes:
                            result = DeviceControl.ItemBarCodeType;
                            break;
                        case EnumTableScale.Contragents:
                            result = DeviceControl.ItemContragent;
                            break;
                        case EnumTableScale.Hosts:
                            result = DeviceControl.ItemHost;
                            break;
                        case EnumTableScale.Labels:
                            result = DeviceControl.ItemLabel;
                            break;
                        case EnumTableScale.Nomenclatures:
                            result = DeviceControl.ItemNomenclature;
                            break;
                        case EnumTableScale.OrderStatuses:
                            result = DeviceControl.ItemOrderStatus;
                            break;
                        case EnumTableScale.OrderTypes:
                            result = DeviceControl.ItemOrderType;
                            break;
                        case EnumTableScale.Orders:
                            result = DeviceControl.ItemOrder;
                            break;
                        case EnumTableScale.Plus:
                            result = DeviceControl.ItemPlu;
                            break;
                        case EnumTableScale.Printers:
                            result = DeviceControl.ItemPrinter;
                            break;
                        case EnumTableScale.PrinterResources:
                            result = DeviceControl.ItemPrinterResource;
                            break;
                        case EnumTableScale.PrinterTypes:
                            result = DeviceControl.ItemPrinterType;
                            break;
                        case EnumTableScale.ProductSeries:
                            result = DeviceControl.ItemProductSeries;
                            break;
                        case EnumTableScale.ProductionFacilities:
                            result = DeviceControl.ItemProductionFacility;
                            break;
                        case EnumTableScale.Scales:
                            result = DeviceControl.ItemScale;
                            break;
                        case EnumTableScale.TemplateResources:
                            result = DeviceControl.ItemTemplateResource;
                            break;
                        case EnumTableScale.Templates:
                            result = DeviceControl.ItemTemplate;
                            break;
                        case EnumTableScale.WeithingFacts:
                            result = DeviceControl.ItemWeithingFact;
                            break;
                        case EnumTableScale.Workshops:
                            result = DeviceControl.ItemWorkshop;
                            break;
                    }
                }
                return result;
            }

            public static string GetItemTitle(ITableEntity table, int itemId)
            {
                return $"{GetItemTitle(table)}. ID: {itemId}";
            }

            public static string GetItemTitle(ITableEntity table, Guid itemUid)
            {
                return $"{GetItemTitle(table)}. UID: {itemUid}";
            }

            public static string GetSectionTitle(ITableEntity table)
            {
                string result = string.Empty;
                if (table is TableSystemEntity tableSystem)
                {
                    switch (tableSystem.Value)
                    {
                        case EnumTableSystem.Accesses:
                            result = Strings.SectionAccess;
                            break;
                        case EnumTableSystem.Logs:
                            result = Strings.SectionLog;
                            break;
                    }
                }
                if (table is TableScaleEntity tableScales)
                {
                    switch (tableScales.Value)
                    {
                        case EnumTableScale.BarcodeTypes:
                            result = DeviceControl.SectionBarcodes;
                            break;
                        case EnumTableScale.Contragents:
                            result = DeviceControl.SectionContragents;
                            break;
                        case EnumTableScale.Hosts:
                            result = DeviceControl.SectionHosts;
                            break;
                        case EnumTableScale.Labels:
                            result = DeviceControl.SectionLabels;
                            break;
                        case EnumTableScale.Nomenclatures:
                            result = DeviceControl.SectionNomenclatures;
                            break;
                        case EnumTableScale.OrderStatuses:
                            result = DeviceControl.SectionOrderStatuses;
                            break;
                        case EnumTableScale.OrderTypes:
                            result = DeviceControl.SectionOrderTypes;
                            break;
                        case EnumTableScale.Orders:
                            result = DeviceControl.SectionOrders;
                            break;
                        case EnumTableScale.Plus:
                            result = DeviceControl.SectionPlus;
                            break;
                        case EnumTableScale.Printers:
                            result = DeviceControl.SectionPrinters;
                            break;
                        case EnumTableScale.PrinterResources:
                            result = DeviceControl.SectionPrinterResources;
                            break;
                        case EnumTableScale.PrinterTypes:
                            result = DeviceControl.SectionPrinterTypes;
                            break;
                        case EnumTableScale.ProductSeries:
                            result = DeviceControl.SectionProductSeries;
                            break;
                        case EnumTableScale.ProductionFacilities:
                            result = DeviceControl.SectionProductionFacilities;
                            break;
                        case EnumTableScale.Scales:
                            result = DeviceControl.SectionScales;
                            break;
                        case EnumTableScale.TemplateResources:
                            result = DeviceControl.SectionTemplateResources;
                            break;
                        case EnumTableScale.Templates:
                            result = DeviceControl.SectionTemplates;
                            break;
                        case EnumTableScale.WeithingFacts:
                            result = DeviceControl.SectionWeithingFacts;
                            break;
                        case EnumTableScale.Workshops:
                            result = DeviceControl.SectionWorkshops;
                            break;
                    }
                }
                return result;
            }

            #endregion
        }
    }
}
