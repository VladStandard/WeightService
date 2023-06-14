// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class WsLocaleDeviceControl : WsLocaleBase
{
    #region Public and private fields, properties, constructor

    public string Actions => Lang == WsEnumLanguage.English ? "Aaction" : "Действия";
    public string DataRecords => Lang == WsEnumLanguage.English ? "records" : "записей";
    public string DevicesTitle => Lang == WsEnumLanguage.English ? "Complexes of industrial devices" : "Комплексы промышленных устройств";
    public string Index => Lang == WsEnumLanguage.English ? "DeviceControl" : "Управление устройствами";
    public string IndexAccessQuery => Lang == WsEnumLanguage.English ? "Contact your system administrator to access." : "Свяжитесь с администратором системы, чтобы получить доступ.";
    public string IndexContinue => Lang == WsEnumLanguage.English ? "Click on a menu section to continue." : "Нажмите на раздел меню, чтобы продолжить.";
    public string ItemBarcode => Lang == WsEnumLanguage.English ? "Barcode" : "Штрихкод";
    public string ItemBarCode => Lang == WsEnumLanguage.English ? "Barcode" : "Штрихкод";
    public string ItemBarCodeType => Lang == WsEnumLanguage.English ? "Barcodes type" : "Тип штрихкода";
    public string ItemBox => Lang == WsEnumLanguage.English ? "Box" : "Коробка";
    public string ItemBundle => Lang == WsEnumLanguage.English ? "Bundle" : "Пакет";
    public string ItemContragent => Lang == WsEnumLanguage.English ? "Counterparty" : "Контрагент";
    public string ItemDevice => Lang == WsEnumLanguage.English ? "Device" : "Устройство";
    public string ItemDeviceScaleFk => Lang == WsEnumLanguage.English ? "Device with scale" : "Устройство с постом";
    public string ItemDeviceType => Lang == WsEnumLanguage.English ? "Device type" : "Тип устройства";
    public string ItemDeviceTypeFk => Lang == WsEnumLanguage.English ? "Device with type" : "Устройство с типом";
    public string ItemError => Lang == WsEnumLanguage.English ? "Error" : "Ошибка";
    public string ItemFont => Lang == WsEnumLanguage.English ? "Font" : "Шрифт";
    public string ItemHost => Lang == WsEnumLanguage.English ? "Host" : "Хост";
    public string ItemLabel => Lang == WsEnumLanguage.English ? "Label" : "Этикетка";
    public string ItemLog => Lang == WsEnumLanguage.English ? "Log" : "Лог";
    public string ItemLogo => Lang == WsEnumLanguage.English ? "Logo" : "Логотип";
    public string ItemModule => Lang == WsEnumLanguage.English ? "Module" : "Модуль";
    public string ItemNomenclature => Lang == WsEnumLanguage.English ? "Nomenclature" : "Номенклатура";
    public string ItemNomenclatureGroup => Lang == WsEnumLanguage.English ? "Nomenclature group" : "Номенклатурная группа";
    public string ItemNomenclatureUnit => Lang == WsEnumLanguage.English ? "Package" : "Упаковка";
    public string ItemOrder => Lang == WsEnumLanguage.English ? "Order" : "Заказ";
    public string ItemOrderStatus => Lang == WsEnumLanguage.English ? "Order status" : "Статус заказа";
    public string ItemOrderType => Lang == WsEnumLanguage.English ? "Order type" : "Типы заказа";
    public string ItemOrderWeighing => Lang == WsEnumLanguage.English ? "Order weighing" : "Взвешивание заказа";
    public string ItemOrganization => Lang == WsEnumLanguage.English ? "Organization" : "Организация";
    public string ItemPlu => Lang == WsEnumLanguage.English ? "PLU" : "ПЛУ";
    public string ItemPluBundleFk => Lang == WsEnumLanguage.English ? "PLU's bundle" : "Пакет ПЛУ";
    public string ItemPluNestingFk => Lang == WsEnumLanguage.English ? "PLU's nesting" : "Вложенность ПЛУ";
    public string ItemPluScale => Lang == WsEnumLanguage.English ? "Device PLU" : "ПЛУ устройства";
    public string ItemPluWeighing => Lang == WsEnumLanguage.English ? "Plu weighings" : "Взвешивание ПЛУ";
    public string ItemPlusStorage => Lang == WsEnumLanguage.English ? "PLU storage" : "Cпособ хранения ПЛУ";
    public string ItemProductionFacilities => Lang == WsEnumLanguage.English ? "Prod. facilities" : "Производственные площадки";
    public string ItemProductionFacility => Lang == WsEnumLanguage.English ? "Prod. facility" : "Произв. площадка";
    public string ItemProductSeries => Lang == WsEnumLanguage.English ? "Product series" : "Серия продукта";
    public string ItemResource => Lang == WsEnumLanguage.English ? "Resource" : "Ресурс";
    public string ItemScale => Lang == WsEnumLanguage.English ? "Line" : "Линия";
    public string ItemScreenShot => Lang == WsEnumLanguage.English ? "ScreenShot" : "Скриншот";
    public string ItemTask => Lang == WsEnumLanguage.English ? "Task" : "Задача";
    public string ItemTaskModule => Lang == WsEnumLanguage.English ? "Task module" : "Модуль задачи";
    public string ItemTemplate => Lang == WsEnumLanguage.English ? "Template" : "Шаблон";
    public string ItemTemplateResource => Lang == WsEnumLanguage.English ? "Template resource" : "Ресурс шаблона";
    public string ItemWeithingFact => Lang == WsEnumLanguage.English ? "Weithing fact" : "Взвешивание";
    public string ItemWorkshop => Lang == WsEnumLanguage.English ? "Workshop" : "Цех";
    public string ItemWorkShop => Lang == WsEnumLanguage.English ? "Workshop" : "Цех";
    public string LinkEmail => "morozov_dv@kolbasa-vs.ru";
    public string LinkEmailWithSubject => "mailto:morozov_dv@kolbasa-vs.ru&subject=device-control";
    public string LinkLabelary => "http://labelary.com/viewer.html";
    public string SectionAdministering => Lang == WsEnumLanguage.English ? "Administering" : "Администрирование";
    public string SectionAutomatedWorkplace => Lang == WsEnumLanguage.English ? "Automated Workplaces" : "Автоматизированные Рабочие Места";
    public string SectionBarCodes => Lang == WsEnumLanguage.English ? "Barcodes" : "Штрихкоды";
    public string SectionBarCodesShort => Lang == WsEnumLanguage.English ? "BC" : "ШК";
    public string SectionBarCodeTypes => Lang == WsEnumLanguage.English ? "Barcodes types" : "Типы штрихкодов";
    public string SectionBarCodeTypesShort => Lang == WsEnumLanguage.English ? "BC types" : "Типы ШК";
    public string SectionBoxes => Lang == WsEnumLanguage.English ? "Boxes" : "Коробки";
    public string SectionBrands => Lang == WsEnumLanguage.English ? "Brands" : "Бренды";
    public string SectionBundles => Lang == WsEnumLanguage.English ? "Bundles" : "Пакеты";
    public string SectionContragents => Lang == WsEnumLanguage.English ? "Counterparties" : "Контрагенты";
    public string SectionDevices => Lang == WsEnumLanguage.English ? "Devices" : "Устройства";
    public string SectionDevicesScalesFk => Lang == WsEnumLanguage.English ? "Devices with scales" : "Устройства с постами";
    public string SectionDevicesTypes => Lang == WsEnumLanguage.English ? "Device types " : "Типы устройств";
    public string SectionDevicesTypesFk => Lang == WsEnumLanguage.English ? "Devices with types" : "Устройства с типами";
    public string SectionDwh => Lang == WsEnumLanguage.English ? "DWH" : "МДМ";
    public string SectionDwhReferences => Lang == WsEnumLanguage.English ? "DWH References" : "DWH справочники";
    public string SectionFonts => Lang == WsEnumLanguage.English ? "Fonts" : "Шрифты";
    public string SectionHosts => Lang == WsEnumLanguage.English ? "Hosts" : "Хосты";
    public string SectionImport1C => Lang == WsEnumLanguage.English ? "Import from 1C" : "Импорт из 1С";
    public string SectionLabels => Lang == WsEnumLanguage.English ? "Labels" : "Этикетки";
    public string SectionLogos => Lang == WsEnumLanguage.English ? "Logos" : "Логотипы";
    public string SectionLogs => Lang == WsEnumLanguage.English ? "Logs" : "Логи";
    public string SectionMeasurements => Lang == WsEnumLanguage.English ? "Measurements" : "Измерения";
    public string SectionModules => Lang == WsEnumLanguage.English ? "Modules" : "Модули";
    public string SectionNestingFk => Lang == WsEnumLanguage.English ? "Nesting" : "Вложенности";
    public string SectionNomenclatures => Lang == WsEnumLanguage.English ? "Nomenclatures" : "Номенклатуры";
    public string SectionNomenclaturesGroups => Lang == WsEnumLanguage.English ? "Nomenclatures groups" : "Номенклатурные группы";
    public string SectionNomenclatureUnits => Lang == WsEnumLanguage.English ? "Packages" : "Упаковки";
    public string SectionObsoletes => Lang == WsEnumLanguage.English ? "Obsoletes" : "Устаревшие";
    public string SectionOperations => Lang == WsEnumLanguage.English ? "Operations" : "Операции";
    public string SectionOrders => Lang == WsEnumLanguage.English ? "Orders" : "Заказы";
    public string SectionOrderStatuses => Lang == WsEnumLanguage.English ? "Order statuses" : "Статусы заказов";
    public string SectionOrdersWeighings => Lang == WsEnumLanguage.English ? "Orders weighings" : "Взвешивание заказов";
    public string SectionOrderTypes => Lang == WsEnumLanguage.English ? "Order types" : "Типы заказов";
    public string SectionOrganizations => Lang == WsEnumLanguage.English ? "Organizations" : "Организации";
    public string SectionPlus => Lang == WsEnumLanguage.English ? "PLUs" : "ПЛУ";
    public string SectionPlusStorage => Lang == WsEnumLanguage.English ? "PLUs storage" : "Cпособы хранения ПЛУ";
    public string SectionPlusBundlesFk => Lang == WsEnumLanguage.English ? "PLU & bundles" : "Пакеты ПЛУ";
    public string SectionPlusNestingFk => Lang == WsEnumLanguage.English ? "PLU's nesting" : "Вложенности ПЛУ";
    public string SectionPlusScales => Lang == WsEnumLanguage.English ? "PLU & Lines" : "ПЛУ и Линия";
    public string SectionPlusWeightings => Lang == WsEnumLanguage.English ? "Plus Weightings" : "Взвешивания ПЛУ";
    public string SectionProductionFacilities => Lang == WsEnumLanguage.English ? "Production facilities" : "Производственные площадки";
    public string SectionProductionFacilitiesShort => Lang == WsEnumLanguage.English ? "Facilities" : "Площадки";
    public string SectionProductSeries => Lang == WsEnumLanguage.English ? "Product series" : "Серии продуктов";
    public string SectionReferences => Lang == WsEnumLanguage.English ? "References" : "Справочники";
    public string SectionReferences1C => Lang == WsEnumLanguage.English ? "References 1C" : "Справочники 1C";
    public string SectionReferencesAdditional => Lang == WsEnumLanguage.English ? "Add. references" : "Доп. справочники";
    public string SectionReferencesDev => Lang == WsEnumLanguage.English ? "Development" : "Разработка";
    public string SectionResources => Lang == WsEnumLanguage.English ? "Resources" : "Ресурсы";
    public string SectionScales => Lang == WsEnumLanguage.English ? "Lines" : "Линии";
    public string SectionScreenShots => Lang == WsEnumLanguage.English ? "Screenshots" : "Скриншоты";
    public string SectionTaskModules => Lang == WsEnumLanguage.English ? "Task Modules" : "Модули задач";
    public string SectionTasks => Lang == WsEnumLanguage.English ? "Tasks" : "Задачи";
    public string SectionTemplateResources => Lang == WsEnumLanguage.English ? "Template resources" : "Ресурсы шаблонов";
    public string SectionTemplates => Lang == WsEnumLanguage.English ? "Templates" : "Шаблоны";
    public string SectionWeighings => Lang == WsEnumLanguage.English ? "Weighings" : "Взвешивания";
    public string SectionWeithingFactsAggregation => Lang == WsEnumLanguage.English ? "Aggregation weithings" : "Агрегированные взвешивания";
    public string SectionWeithingFactsAggregationWithoutPlu => Lang == WsEnumLanguage.English ? "Aggregation weithings without PLU" : "Агрегированные взвешивания без ПЛУ";
    public string SectionWeithingFactsAggregationWithPLU => Lang == WsEnumLanguage.English ? "Aggregation weithings with PLU" : "Агрегированные взвешивания с ПЛУ";
    public string SectionWeithingFactsAggregationShort => Lang == WsEnumLanguage.English ? "Aggr. weithings" : "Агр. взвешивания";
    public string SectionWorkShops => Lang == WsEnumLanguage.English ? "Workshops" : "Цеха";
    public string SqlServerDevelopAleksandrov => "LOCALHOST";
    public string SqlServerDevelopVs => "CREATIO"; // CREATIO\INS1
    public string SqlServerReleaseAleksandrov => "LOCALHOST"; // PALYCH\LUTON
    public string SqlServerReleaseVs => "PALYCH"; // PALYCH\LUTON
    public string Table => Lang == WsEnumLanguage.English ? "Table" : "Таблица";
    public string TableActionAdd => Lang == WsEnumLanguage.English ? "Add" : "Добавить";
    public string TableActionCancel => Lang == WsEnumLanguage.English ? "Cancel" : "Отмена";
    public string TableActionClear => Lang == WsEnumLanguage.English ? "Clear" : "Очистить";
    public string TableActionCopy => Lang == WsEnumLanguage.English ? "Copy" : "Копировать";
    public string TableActionDelete => Lang == WsEnumLanguage.English ? "Delete" : "Удалить";
    public string TableActionEdit => Lang == WsEnumLanguage.English ? "Edit" : "Редактировать";
    public string TableActionFill => Lang == WsEnumLanguage.English ? "Fill" : "Заполнить";
    public string TableActionMark => Lang == WsEnumLanguage.English ? "Mark" : "Пометить";
    public string TableActions => Lang == WsEnumLanguage.English ? "Actions" : "Действия";
    public string TableActionSave => Lang == WsEnumLanguage.English ? "Save" : "Сохранить";
    public string TableActionsIsDeny => Lang == WsEnumLanguage.English ? "Actions is deny" : "Действия недоступны";
    public string WebAppIsStarted => Lang == WsEnumLanguage.English ? "Web-app is started" : "Веб-приложение запущено";

    #endregion
}