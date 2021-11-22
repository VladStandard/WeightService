// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.Interfaces;
using System;
using static DataShareCore.LocalizationCore;

namespace DataCore
{
    public static class LocalizationData
    {
        public static ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        public static class DeviceControl
        {
            #region Main

            public static string Index => Lang == ShareEnums.Lang.English ? @"DeviceControl" : @"Управление устройствами";
            public static string IndexContinue => Lang == ShareEnums.Lang.English
                ? @"Click on a menu section to continue." : @"Нажмите на раздел меню, чтобы продолжить.";
            public static string SqlServerDebug => "CREATIO";
            public static string SqlServerRelease => "PALYCH";
            public static string CallbackEmail => @"mailto:morozov_dv@kolbasa-vs.ru&subject=device-control.kolbasa-vs.local";
            public static string DataRecords => Lang == ShareEnums.Lang.English ? "records" : "записей";
            #endregion
            #region Комплексы промышленных устройств
            public static string DevicesTitle => Lang == ShareEnums.Lang.English ? @"Complexes of industrial devices" : @"Комплексы промышленных устройств";
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
            public static string ItemBarCodeType => Lang == ShareEnums.Lang.English ? @"Barcodes type" : @"Тип штрихкода";
            public static string ItemBarcode => Lang == ShareEnums.Lang.English ? @"Barcode" : @"Штрихкод";
            public static string ItemContragent => Lang == ShareEnums.Lang.English ? @"Counterparty" : @"Контрагент";
            public static string ItemFont => Lang == ShareEnums.Lang.English ? @"Font" : @"Шрифт";
            public static string ItemHost => Lang == ShareEnums.Lang.English ? @"Host" : @"Хост";
            public static string ItemLabel => Lang == ShareEnums.Lang.English ? @"Label" : @"Этикетка";
            public static string ItemLogo => Lang == ShareEnums.Lang.English ? @"Logo" : @"Логотип";
            public static string ItemNomenclatureUnit => Lang == ShareEnums.Lang.English ? @"Package" : @"Упаковка";
            public static string ItemNomenclature => Lang == ShareEnums.Lang.English ? @"Nomenclature" : @"Номенклатура";
            public static string ItemOrderStatus => Lang == ShareEnums.Lang.English ? @"Order status" : @"Статус заказа";
            public static string ItemOrderType => Lang == ShareEnums.Lang.English ? @"Order type" : @"Типы заказа";
            public static string ItemOrder => Lang == ShareEnums.Lang.English ? @"Order" : @"Заказ";
            public static string ItemPlu => Lang == ShareEnums.Lang.English ? @"PLU" : @"ПЛУ";
            public static string ItemPrinterResource => Lang == ShareEnums.Lang.English ? @"Printer resource" : @"Ресурс принтера";
            public static string ItemPrinterType => Lang == ShareEnums.Lang.English ? @"Printer type" : @"Тип принтера";
            public static string ItemPrinter => Lang == ShareEnums.Lang.English ? @"Printer" : @"Принтер";
            public static string ItemProductSeries => Lang == ShareEnums.Lang.English ? @"Product series" : @"Серия продукта";
            public static string ItemProductionFacility => Lang == ShareEnums.Lang.English ? @"Prod. facility" : @"Произв. площадка";
            public static string ItemResource => Lang == ShareEnums.Lang.English ? @"Resource" : @"Ресурс";
            public static string ItemScale => Lang == ShareEnums.Lang.English ? @"Device" : @"Устройство";
            public static string ItemTemplateResource => Lang == ShareEnums.Lang.English ? @"Template resource" : @"Ресурс шаблона";
            public static string ItemTemplate => Lang == ShareEnums.Lang.English ? @"Template" : @"Шаблон";
            public static string ItemWeithingFact => Lang == ShareEnums.Lang.English ? @"Weithing fact" : @"Взвешивание";
            public static string ItemWorkshop => Lang == ShareEnums.Lang.English ? @"Workshop" : @"Цех";
            #endregion
            #region Sections
            public static string SectionBarCodeTypes => Lang == ShareEnums.Lang.English ? @"Barcodes types" : @"Типы штрихкодов";
            public static string SectionBarcodes => Lang == ShareEnums.Lang.English ? @"Barcodes" : @"Штрихкоды";
            public static string SectionContragents => Lang == ShareEnums.Lang.English ? @"Counterparties" : @"Контрагенты";
            public static string SectionFonts => Lang == ShareEnums.Lang.English ? @"Fonts" : @"Шрифты";
            public static string SectionHosts => Lang == ShareEnums.Lang.English ? @"Hosts" : @"Хосты";
            public static string SectionLabels => Lang == ShareEnums.Lang.English ? @"Labels" : @"Этикетки";
            public static string SectionLogos => Lang == ShareEnums.Lang.English ? @"Logos" : @"Логотипы";
            public static string SectionLogs => Lang == ShareEnums.Lang.English ? @"Logs" : @"Логи";
            public static string SectionNomenclatureUnits => Lang == ShareEnums.Lang.English ? @"Packages" : @"Упаковки";
            public static string SectionNomenclatures => Lang == ShareEnums.Lang.English ? @"Nomenclatures" : @"Номенклатура";
            public static string SectionOrderStatuses => Lang == ShareEnums.Lang.English ? @"Order statuses" : @"Статусы заказов";
            public static string SectionOrderTypes => Lang == ShareEnums.Lang.English ? @"Order types" : @"Типы заказов";
            public static string SectionOrders => Lang == ShareEnums.Lang.English ? @"Orders" : @"Заказы";
            public static string SectionPlus => Lang == ShareEnums.Lang.English ? @"PLU" : @"ПЛУ";
            public static string SectionPrinterResources => Lang == ShareEnums.Lang.English ? @"Printer resources" : @"Ресурсы принтера";
            public static string SectionPrinterTypes => Lang == ShareEnums.Lang.English ? @"Printer types" : @"Типы принтеров";
            public static string SectionPrinters => Lang == ShareEnums.Lang.English ? @"Printers" : @"Принтеры";
            public static string SectionProductSeries => Lang == ShareEnums.Lang.English ? @"Product series" : @"Серии продуктов";
            public static string SectionProductionFacilities => Lang == ShareEnums.Lang.English ? @"Prod. facilities" : @"Произв. площадки";
            public static string SectionResources => Lang == ShareEnums.Lang.English ? @"Resources" : @"Ресурсы";
            public static string SectionScales => Lang == ShareEnums.Lang.English ? @"Devices" : @"Устройства";
            public static string SectionTemplateResources => Lang == ShareEnums.Lang.English ? @"Template resources" : @"Ресурсы шаблонов";
            public static string SectionTemplates => Lang == ShareEnums.Lang.English ? @"Templates" : @"Шаблоны";
            public static string SectionWeithingFacts => Lang == ShareEnums.Lang.English ? @"Weithing facts" : @"Взвешивания";
            public static string SectionWorkshops => Lang == ShareEnums.Lang.English ? @"Workshops" : @"Цеха";
            #endregion
            #region Tables
            public static string TableActionAdd => Lang == ShareEnums.Lang.English ? @"Add" : @"Добавить";
            public static string TableActionCancel => Lang == ShareEnums.Lang.English ? @"Cancel" : @"Отмена";
            public static string TableActionClear => Lang == ShareEnums.Lang.English ? @"Clear" : @"Очистить";
            public static string TableActionCopy => Lang == ShareEnums.Lang.English ? @"Copy" : @"Копировать";
            public static string TableActionDelete => Lang == ShareEnums.Lang.English ? @"Delete" : @"Удалить";
            public static string TableActionEdit => Lang == ShareEnums.Lang.English ? @"Edit" : @"Редактировать";
            public static string TableActionMark => Lang == ShareEnums.Lang.English ? @"Mark" : @"Пометить";
            public static string TableActionNew => Lang == ShareEnums.Lang.English ? @"New" : @"Новый";
            public static string TableActionFill => Lang == ShareEnums.Lang.English ? @"Fill" : @"Заполнить";
            public static string TableActionSave => Lang == ShareEnums.Lang.English ? @"Save" : @"Сохранить";

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
            public static string TableFieldPrinter => Lang == ShareEnums.Lang.English ? @"Printer" : @"Принтер";
            public static string TableFieldPrinterControlPanel => Lang == ShareEnums.Lang.English ? @"Printer control panel" : @"Панель управления принтером";
            public static string TableFieldPrinterDarknessLevel => Lang == ShareEnums.Lang.English ? @"Level of darkness" : @"Уровень темноты";
            public static string TableFieldPrinterIp => Lang == ShareEnums.Lang.English ? @"IP-address" : @"IP-адрес";
            public static string TableFieldPrinterMac => Lang == ShareEnums.Lang.English ? @"MAC-address" : @"MAC-адрес";
            public static string TableFieldPrinterPassword => Lang == ShareEnums.Lang.English ? @"Printer password" : @"Пароль принтера";
            public static string TableFieldPrinterPeelOffSet => Lang == ShareEnums.Lang.English ? @"Offset" : @"Смещение";
            public static string TableFieldPrinterPort => Lang == ShareEnums.Lang.English ? @"Printer port" : @"Порт принтера";
            public static string TableFieldPrinterType => Lang == ShareEnums.Lang.English ? @"Printer type" : @"Тип принтера";
            public static string TableReadData => Lang == ShareEnums.Lang.English ? @"Read data" : @"Прочитать данные";
            public static string Table => Lang == ShareEnums.Lang.English ? @"Table" : @"Таблица";
            #endregion
        }

        public static class MdmControl
        {

        }

        public static class Resources
        {
            #region Main
            public static string AppTitle => Lang == ShareEnums.Lang.English ? @"Resources VS" : @"Ресурсы ВС";
            public static string CallbackEmail => Lang == ShareEnums.Lang.English ?
                @"mailto:morozov_dv@kolbasa-vs.ru&subject=resources-vs.kolbasa-vs.local" :
                @"mailto:morozov_dv@kolbasa-vs.ru&subject=resources-vs.kolbasa-vs.local";
            public static string SysAdmin => Lang == ShareEnums.Lang.English ? @"Administration" : @"Администрирование";
            public static string SupportCreatio =>
                Lang == ShareEnums.Lang.English
                    ? @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/CaseSection/"
                    : @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/CaseSection/";
            #endregion
            #region Support
            public static string SupportTitle => Lang == ShareEnums.Lang.English ? @"Support service" : @"Служба поддержки";
            public static string SupportLinkCreatioText => Lang == ShareEnums.Lang.English ? @"Creatio appeals" : @"Creatio обращения";
            public static string SupportLinkHelpText => Lang == ShareEnums.Lang.English ? @"Write a letter" : @"Написать письмо";

            public static string SupportLinkHelpPath =>
                Lang == ShareEnums.Lang.English
                    ? @"mailto:helpdesk@kolbasa-vs.ru?subject=Appeal"
                    : @"mailto:helpdesk@kolbasa-vs.ru?subject=Обращение";

            #endregion
            #region Contacts
            public static string ContactsTitle => Lang == ShareEnums.Lang.English ? @"Contacts" : @"Контакты";
            public static string ContactsPhoneText => Lang == ShareEnums.Lang.English ? @"Phone directory" : @"Телефонный справочник";

            public static string ContactsPhonePath =>
                Lang == ShareEnums.Lang.English
                    ? @"http://sqlsrsp01.kolbasa-vs.local/Reports/browse/PhoneRP"
                    : @"http://sqlsrsp01.kolbasa-vs.local/Reports/browse/PhoneRP";

            public static string ContactsCreatioText => Lang == ShareEnums.Lang.English ? @"Creatio contacts" : @"Creatio контакты";

            public static string ContactsCreatioPath =>
                Lang == ShareEnums.Lang.English
                    ? @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/ContactSectionV2/"
                    : @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/ContactSectionV2/";

            #endregion
            #region IT department
            public static string DepartmentItTitle => Lang == ShareEnums.Lang.English ? @"IT department" : @"ИТ отдел";

            #endregion
            #region Redmine
            public static string RedmineTitle => Lang == ShareEnums.Lang.English ? @"Redmine" : @"Redmine";
            public static string RedmineWikiText => Lang == ShareEnums.Lang.English ? @"Wiki" : @"Wiki";
            public const string RedmineWikiPath = @"http://192.168.0.50/projects/resources_it/wiki/Wiki";
            public const string RedmineProjectsText = @"проекты";
            public const string RedmineProjectsPath = @"http://192.168.0.50/projects";
            public const string RedmineGoogleText = @"Ссылка на гугл таблицы";
            public const string RedmineGooglePath = @"http://192.168.0.50/projects/resources_it/wiki/%D0%A1%D1%81%D1%8B%D0%BB%D0%BA%D0%B0_%D0%BD%D0%B0_%D0%B3%D1%83%D0%B3%D0%BB_%D1%82%D0%B0%D0%B1%D0%BB%D0%B8%D1%86%D1%8B";
            #endregion
            #region Zabbix
            public static string ZabbixTitle => Lang == ShareEnums.Lang.English ? @"Zabbix" : @"Zabbix";
            public static string ZabbixKolbasaText => Lang == ShareEnums.Lang.English ? @"kolbasa-vs-terrasoft" : @"kolbasa-vs-terrasoft";

            public static string ZabbixKolbasaPath =>
                Lang == ShareEnums.Lang.English
                    ? @"http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7"
                    : @"http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7";

            public static string ZabbixGlobalText => Lang == ShareEnums.Lang.English ? @"Global view" : @"Глобальное представление";

            public static string ZabbixGlobalPath =>
                Lang == ShareEnums.Lang.English
                    ? @"http://zabbix.kolbasa-vs.local/zabbix/zabbix.php?action=dashboard.view"
                    : @"http://zabbix.kolbasa-vs.local/zabbix/zabbix.php?action=dashboard.view";

            public static string ZabbixWebText => Lang == ShareEnums.Lang.English ? @"Web monitoring" : @"Веб-мониторинг";

            public static string ZabbixWebPath =>
                Lang == ShareEnums.Lang.English
                    ? @"http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7"
                    : @"http://zabbix.kolbasa-vs.local/zabbix/httpdetails.php?httptestid=7";

            #endregion
            #region Creatio
            public static string CreatioTitle => Lang == ShareEnums.Lang.English ? @"Creatio-server" : @"Creatio-сервер";
            public static string CreatioWebinarsText => Lang == ShareEnums.Lang.English ? @"Webinars" : @"Вебинары";
            public const string CreatioWebinarsPath = @"\\isexcd02\Webinars\";
            public static string CreatioOfficialTitle => Lang == ShareEnums.Lang.English ? @"The official sites of Terrasoft Creatio" : @"Официальные сайты Terrasoft Creatio";
            public static string CreatioAcademyText => Lang == ShareEnums.Lang.English ? @"Academy" : @"Академия";
            public const string CreatioAcademyPath = @"https://academy.terrasoft.ru/";
            public static string CreatioMarketplaceText => Lang == ShareEnums.Lang.English ? @"Marketplace" : @"Маркетплейс";
            public const string CreatioMarketplacePath = @"https://marketplace.terrasoft.ru/";
            public static string CreatioCommunityText => Lang == ShareEnums.Lang.English ? @"Community" : @"Сообщество";
            public const string CreatioCommunityPath = @"https://community.terrasoft.ru/";
            public static string CreatioTableTitle => Lang == ShareEnums.Lang.English ? @"Локальные сайты" : @"Локальные сайты";
            public static string CreatioTableFieldName => Lang == ShareEnums.Lang.English ? @"Site" : @"Сайт";
            public static string CreatioTableFieldLink => Lang == ShareEnums.Lang.English ? @"Link" : @"Ссылка";
            public static string CreatioTableFieldDev => Lang == ShareEnums.Lang.English ? @"Configuration" : @"Конфигурация";
            public static string CreatioCreDevDmName => Lang == ShareEnums.Lang.English ? @"Website development Morozov D.V." : @"Сайт разработки Морозов Д.В.";
            public const string CreatioCreDevDmLink = @"http://cre-dev-dm.kolbasa-vs.local/";
            public const string CreatioCreDevDmDev = @"http://cre-dev-dm.kolbasa-vs.local/0/dev";
            public static string CreatioCreDevIaName => Lang == ShareEnums.Lang.English ? @"Website development Andreev I.A." : @"Сайт разработки Андреев И.А.";
            public const string CreatioCreDevIaLink = @"http://cre-dev-ia.kolbasa-vs.local/";
            public const string CreatioCreDevIaDev = @"http://cre-dev-ia.kolbasa-vs.local/0/dev";
            public static string CreatioCreTestName => Lang == ShareEnums.Lang.English ? @"Website testing" : @"Сайт тестирования";
            public const string CreatioCreTestLink = @"http://cre-test.kolbasa-vs.local/";
            public const string CreatioCreTestDev = @"http://cre-test.kolbasa-vs.local/0/dev";
            public static string CreatioCreStudyText => Lang == ShareEnums.Lang.English ? @"cre-study | Training site" : @"cre-study | Сайт обучения";
            public const string CreatioCreStudyPath = @"http://cre-study.kolbasa-vs.local/";
            public static string CreatioCreStudyDevText => Lang == ShareEnums.Lang.English ? @"cre-study | Training Configuration" : @"cre-study | Конфигурация обучения";
            public const string CreatioCreStudyDevPath = @"http://cre-study.kolbasa-vs.local/0/dev";
            public static string CreatioCreUpgradeText => Lang == ShareEnums.Lang.English ? @"Cre-upgrade | update site" : @"cre-upgrade | Сайт обновления";
            public const string CreatioCreUpgradePath = @"http://cre-upgrade.kolbasa-vs.local/";
            public static string CreatioCreUpgradeDevText => Lang == ShareEnums.Lang.English ? @"Cre-upgrade | Upgrade Configuration" : @"cre-upgrade | Конфигурация обновления";
            public const string CreatioCreUpgradeDevPath = @"http://cre-upgrade.kolbasa-vs.local/0/dev";
            public static string CreatioRemoteTitle => Lang == ShareEnums.Lang.English ? @"Public sites" : @"Публичные сайты";
            public static string CreatioTerrasoftPreText => Lang == ShareEnums.Lang.English
                ? @"dev-kolbasa-vs.terrasoft.ru | Site pre-prod 1" : @"dev-kolbasa-vs.terrasoft.ru | Сайт пре-прод 1";
            public const string CreatioTerrasoftPrePath = @"https://dev-kolbasa-vs.terrasoft.ru/";
            public static string CreatioTerrasoftPreDevText => Lang == ShareEnums.Lang.English
                ? @"dev-kolbasa-vs.terrasoft.ru | Configuration pre-prod 1" : @"dev-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public const string CreatioTerrasoftPreDevPath = @"https://dev-kolbasa-vs.terrasoft.ru/0/dev";
            public static string CreatioTerrasoftPre2Text => Lang == ShareEnums.Lang.English
                ? @"dev2-kolbasa-vs.terrasoft.ru | Pre-prod 2 website" : @"dev2-kolbasa-vs.terrasoft.ru | Сайт пре-прод 2";
            public const string CreatioTerrasoftPre2Path = @"https://dev2-kolbasa-vs.terrasoft.ru/";
            public static string CreatioTerrasoftPre2DevText => Lang == ShareEnums.Lang.English
                ? @"dev2-kolbasa-vs.terrasoft.ru | Configuration pre-prod 1" : @"dev2-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public const string CreatioTerrasoftPre2DevPath = @"https://dev2-kolbasa-vs.terrasoft.ru/0/dev";
            public static string CreatioTerrasoftText => Lang == ShareEnums.Lang.English
                ? @"kolbasa-vs.terrasoft.ru | Product environment" : @"dev2-kolbasa-vs.terrasoft.ru | Конфигурация пре-прод 1";
            public const string CreatioTerrasoftPath = @"https://kolbasa-vs.terrasoft.ru/";
            public static string CreatioTerrasoftDevText => Lang == ShareEnums.Lang.English
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

        public static class ScalesUI
        {
            public static string Exception => Lang == ShareEnums.Lang.English ? @"Exception" : @"Ошибка";
            public static string ExceptionSqlDb => Lang == ShareEnums.Lang.English ? @"The database is unavailable!" : @"База данных недоступна!";
            public static string Registration => Lang == ShareEnums.Lang.English ? @"Device registration" : @"Регистрация устройства";
            public static string RegistrationWarning1(Guid uid) => Lang == ShareEnums.Lang.English 
                ? @"The monoblock is registered in the information system with the identifier" + Environment.NewLine +
                  $"{uid}" + Environment.NewLine +
                  "Before restarting, map it to the current line in DeviceControl."
                : "Моноблок зарегистрирован в информационной системе с идентификатором" + Environment.NewLine +
                 $"{uid}" + Environment.NewLine +
                  "Перед повторным запуском сопоставьте его с текущей линией в приложении DeviceControl.";
            public static string RegistrationWarning2(Guid uid) => Lang == ShareEnums.Lang.English 
                ? $@"The monoblock is registered in the information system with the identifier {uid}" + Environment.NewLine +
                    "Before restarting it, map it to the current line in DeviceControl"
                : $@"Моноблок зарегистрирован в информационной системе с идентификатором {uid}" + Environment.NewLine +
                    "Перед повторным запуском сопоставьте его с текущей линией в приложении DeviceControl.";
            public static string OperationControl => Lang == ShareEnums.Lang.English ? @"Control of operations" : @"Контроль операций";
            public static readonly decimal MassaThreshold = 0.05M;
            public static string MassaCheck(decimal currentWeight) => Lang == ShareEnums.Lang.English 
                ? @"Unload the weight platform!" + Environment.NewLine + Environment.NewLine +
                 $@"Threshold value: {MassaThreshold:0.000} {UnitKg}." + Environment.NewLine +
                 $@"Current gross value: {currentWeight:0.000} {UnitKg}." + Environment.NewLine + Environment.NewLine +
                 $@"  {Buttons.Yes} - ignore and continue." + Environment.NewLine +
                 $@"  {Buttons.No} - suspend and unload."
                : @"Разгрузите весовую платформу!" + Environment.NewLine + Environment.NewLine +
                 $@"Пороговое значение: {MassaThreshold:0.000} {UnitKg}." + Environment.NewLine + 
                 $@"Текущее значение брутто: {currentWeight:0.000} {UnitKg}." + Environment.NewLine + Environment.NewLine +
                 $@"  {Buttons.Yes} - игнорировать и продолжить." + Environment.NewLine +
                 $@"  {Buttons.No} - приостановить и разгрузить.";
            public static string PrinterWarningOpenCover => Lang == ShareEnums.Lang.English 
                ? @"Open the cover of the separator before proceeding with the calibration!"
                : @"Прежде чем продолжить калибровку, откройте крышку отделителя!";
            public static string ProgramNotFound(string fileName) => Lang == ShareEnums.Lang.English 
                ? @"Program not found!" + Environment.NewLine + fileName + Environment.NewLine + "Contact your system administrator."
                : @"Программа не найдена!" + Environment.NewLine + fileName + Environment.NewLine + "Обратитесь к системному администратору.";
            public static string MassaNotQuering => Lang == ShareEnums.Lang.English
                ? @"Massa-K scales are not respond!" : @"Весы Масса-К не отвечают!";
            public static string IsNotLoaded => Lang == ShareEnums.Lang.English
                ? @"The program is not yet loaded!" + Environment.NewLine + @"Wait for it..."
                : @"Программа ещё не загружена!" + Environment.NewLine + @"Подождите...";
            public static string PrinterInfoCaption => Lang == ShareEnums.Lang.English
                ? @"Printer info"
                : @"Информация о принтере";
            public static string CheckPluWeightCount => Lang == ShareEnums.Lang.English
                ? @"Weighted products can be specified in quantities of 1 piece."
                : @"Весовая продукция может быть указана в количестве 1 штуки.";
            public static string Labels => Lang == ShareEnums.Lang.English ? @"Labels" : @"Этикетки";
            public static string Printer => Lang == ShareEnums.Lang.English ? @"Printer" : @"Принтер";
            public static string Memory => Lang == ShareEnums.Lang.English ? @"Memory" : @"Память";
            public static string MemoryPhysical => Lang == ShareEnums.Lang.English ? @"Physical memory" : @"Физическая память";
            public static string MemoryVirtual => Lang == ShareEnums.Lang.English ? @"Virtual memory" : @"Виртуальная память";
            public static string MemoryFree => Lang == ShareEnums.Lang.English ? @"free" : @"свободно";
            public static string Plu => Lang == ShareEnums.Lang.English ? @"PLU" : @"PLU";
            public static string PluCount => Lang == ShareEnums.Lang.English ? @"PLU (count)" : @"PLU (шт)";
            public static string PluWeight => Lang == ShareEnums.Lang.English ? @"PLU (weight)" : @"PLU (вес)";
            public static string UnitKg => Lang == ShareEnums.Lang.English ? @"kg" : @"кг";
            public static string ComPortState => Lang == ShareEnums.Lang.English ? @"COM-port status" : @"Состояние COM-порта";
            public static string ScaleQueue => Lang == ShareEnums.Lang.English ? @"Scales message queue" : @"Очередь сообщений весов";
            public static string RequestParameters => Lang == ShareEnums.Lang.English ? @"Request parameters" : @"Запрос параметров";
            public static string WeightingControl => Lang == ShareEnums.Lang.English ? @"The weight is out of bounds!" : @"Вес выходит за границы!";
            public static string WeightingProcess => Lang == ShareEnums.Lang.English ? @"Weighing | Gross weight" : @"Взвешивание | Вес брутто";
            public static string WeightingStable => Lang == ShareEnums.Lang.English ? @"Scales are stable | Gross weight" : @"Весы стабильны | Вес брутто";
            public static string WeightingMessage => Lang == ShareEnums.Lang.English ? @"Weighting message" : @"Сообщение взвешивания";
            public static string WeightingScaleCmd => Lang == ShareEnums.Lang.English ? @"Command for scales" : @"Команда для весов";
            public static string Crc => Lang == ShareEnums.Lang.English ? @"CRC" : @"CRC";
            public static string StateResponsed => Lang == ShareEnums.Lang.English ? @"responsed" : @"отвечает";
            public static string StateNotResponsed => Lang == ShareEnums.Lang.English ? @"not responsed" : @"не отвечает";
            public static string StateCorrect => Lang == ShareEnums.Lang.English ? @"correct" : @"верна";
            public static string StateError => Lang == ShareEnums.Lang.English ? @"error" : @"ошибка";
            public static string QuestionRunApp => Lang == ShareEnums.Lang.English ? @"Run the app" : @"Запустить приложение";
            public static string ChoosePlu => Lang == ShareEnums.Lang.English
                ? @"First, you have to choose a PLU!" : @"Сперва, необходимо выбрать PLU!";

            public static string ButtonRunScalesTerminal => Lang == ShareEnums.Lang.English ? @"Scales Terminal" : @"Весовой терминал";
            public static string ButtonScalesInit => Lang == ShareEnums.Lang.English 
                ? $@"Initialize{Environment.NewLine}the scales" : $@"Инициали-{Environment.NewLine}зировать весы";
            public static string ButtonSelectOrder => Lang == ShareEnums.Lang.English 
                ? $@"Select{Environment.NewLine}order" : $@"Выбрать{Environment.NewLine}заказ";
            public static string ButtonSettings => Lang == ShareEnums.Lang.English ? @"Settings" : @"Настройки";
            public static string ButtonNewPallet => Lang == ShareEnums.Lang.English ? @"New pallet" : @"Новая палета";
            public static string ButtonAddKneading => Lang == ShareEnums.Lang.English ? @"Kneading" : @"Замес";
            public static string ButtonSelectPlu => Lang == ShareEnums.Lang.English 
                ? $@"Select{Environment.NewLine}PLU" : $@"Выбрать{Environment.NewLine}PLU";
            public static string ButtonSetKneading => Lang == ShareEnums.Lang.English ? @"More" : @"Ещё";
            public static string ButtonPrint => Lang == ShareEnums.Lang.English ? @"Print" : @"Печать";

            public static string FieldCurrentTime => Lang == ShareEnums.Lang.English ? @"Now" : @"Сейчас";
            public static string FieldWeightNetto => Lang == ShareEnums.Lang.English ? @"Net weight" : @"Вес нетто";
            public static string FieldWeightTare => Lang == ShareEnums.Lang.English ? @"Tare weight" : @"Вес тары";
            public static string FieldKneading => Lang == ShareEnums.Lang.English ? @"Kneading" : @"Замес";
            public static string FieldProductDate => Lang == ShareEnums.Lang.English ? @"Date of production" : @"Дата производства";
        }

        public static class Buttons
        {
            public static string Yes => Lang == ShareEnums.Lang.English ? @"Yes" : @"Да";
            public static string Retry => Lang == ShareEnums.Lang.English ? @"Retry" : @"Повторить";
            public static string No => Lang == ShareEnums.Lang.English ? @"No" : @"Нет";
            public static string Ignore => Lang == ShareEnums.Lang.English ? @"Ignore" : @"Игнорировать";
            public static string Cancel => Lang == ShareEnums.Lang.English ? @"Cancel" : @"Отмена";
            public static string Abort => Lang == ShareEnums.Lang.English ? @"Abort" : @"Прервать";
            public static string Ok => Lang == ShareEnums.Lang.English ? @"Ok" : @"Ок";
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
                        case ProjectsEnums.TableSystem.Accesses:
                            result = Strings.ItemAccess;
                            break;
                        case ProjectsEnums.TableSystem.Logs:
                            result = Strings.ItemLog;
                            break;
                    }
                }
                if (table is TableScaleEntity tableScales)
                {
                    switch (tableScales.Value)
                    {
                        case ProjectsEnums.TableScale.BarcodeTypes:
                            result = DeviceControl.ItemBarCodeType;
                            break;
                        case ProjectsEnums.TableScale.Contragents:
                            result = DeviceControl.ItemContragent;
                            break;
                        case ProjectsEnums.TableScale.Hosts:
                            result = DeviceControl.ItemHost;
                            break;
                        case ProjectsEnums.TableScale.Labels:
                            result = DeviceControl.ItemLabel;
                            break;
                        case ProjectsEnums.TableScale.Nomenclatures:
                            result = DeviceControl.ItemNomenclature;
                            break;
                        case ProjectsEnums.TableScale.OrderStatuses:
                            result = DeviceControl.ItemOrderStatus;
                            break;
                        case ProjectsEnums.TableScale.OrderTypes:
                            result = DeviceControl.ItemOrderType;
                            break;
                        case ProjectsEnums.TableScale.Orders:
                            result = DeviceControl.ItemOrder;
                            break;
                        case ProjectsEnums.TableScale.Plus:
                            result = DeviceControl.ItemPlu;
                            break;
                        case ProjectsEnums.TableScale.Printers:
                            result = DeviceControl.ItemPrinter;
                            break;
                        case ProjectsEnums.TableScale.PrinterResources:
                            result = DeviceControl.ItemPrinterResource;
                            break;
                        case ProjectsEnums.TableScale.PrinterTypes:
                            result = DeviceControl.ItemPrinterType;
                            break;
                        case ProjectsEnums.TableScale.ProductSeries:
                            result = DeviceControl.ItemProductSeries;
                            break;
                        case ProjectsEnums.TableScale.ProductionFacilities:
                            result = DeviceControl.ItemProductionFacility;
                            break;
                        case ProjectsEnums.TableScale.Scales:
                            result = DeviceControl.ItemScale;
                            break;
                        case ProjectsEnums.TableScale.TemplateResources:
                            result = DeviceControl.ItemTemplateResource;
                            break;
                        case ProjectsEnums.TableScale.Templates:
                            result = DeviceControl.ItemTemplate;
                            break;
                        case ProjectsEnums.TableScale.WeithingFacts:
                            result = DeviceControl.ItemWeithingFact;
                            break;
                        case ProjectsEnums.TableScale.Workshops:
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
                        case ProjectsEnums.TableSystem.Accesses:
                            result = Strings.SectionAccess;
                            break;
                        case ProjectsEnums.TableSystem.Logs:
                            result = Strings.SectionLog;
                            break;
                    }
                }
                if (table is TableScaleEntity tableScales)
                {
                    switch (tableScales.Value)
                    {
                        case ProjectsEnums.TableScale.BarcodeTypes:
                            result = DeviceControl.SectionBarcodes;
                            break;
                        case ProjectsEnums.TableScale.Contragents:
                            result = DeviceControl.SectionContragents;
                            break;
                        case ProjectsEnums.TableScale.Hosts:
                            result = DeviceControl.SectionHosts;
                            break;
                        case ProjectsEnums.TableScale.Labels:
                            result = DeviceControl.SectionLabels;
                            break;
                        case ProjectsEnums.TableScale.Nomenclatures:
                            result = DeviceControl.SectionNomenclatures;
                            break;
                        case ProjectsEnums.TableScale.OrderStatuses:
                            result = DeviceControl.SectionOrderStatuses;
                            break;
                        case ProjectsEnums.TableScale.OrderTypes:
                            result = DeviceControl.SectionOrderTypes;
                            break;
                        case ProjectsEnums.TableScale.Orders:
                            result = DeviceControl.SectionOrders;
                            break;
                        case ProjectsEnums.TableScale.Plus:
                            result = DeviceControl.SectionPlus;
                            break;
                        case ProjectsEnums.TableScale.Printers:
                            result = DeviceControl.SectionPrinters;
                            break;
                        case ProjectsEnums.TableScale.PrinterResources:
                            result = DeviceControl.SectionPrinterResources;
                            break;
                        case ProjectsEnums.TableScale.PrinterTypes:
                            result = DeviceControl.SectionPrinterTypes;
                            break;
                        case ProjectsEnums.TableScale.ProductSeries:
                            result = DeviceControl.SectionProductSeries;
                            break;
                        case ProjectsEnums.TableScale.ProductionFacilities:
                            result = DeviceControl.SectionProductionFacilities;
                            break;
                        case ProjectsEnums.TableScale.Scales:
                            result = DeviceControl.SectionScales;
                            break;
                        case ProjectsEnums.TableScale.TemplateResources:
                            result = DeviceControl.SectionTemplateResources;
                            break;
                        case ProjectsEnums.TableScale.Templates:
                            result = DeviceControl.SectionTemplates;
                            break;
                        case ProjectsEnums.TableScale.WeithingFacts:
                            result = DeviceControl.SectionWeithingFacts;
                            break;
                        case ProjectsEnums.TableScale.Workshops:
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
