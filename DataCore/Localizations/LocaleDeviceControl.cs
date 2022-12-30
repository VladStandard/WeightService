// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;

namespace DataCore.Localizations;

public partial class LocaleDeviceControl
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleDeviceControl _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleDeviceControl Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

    #region Public and private fields, properties, constructor

    public string Actions => Lang == Lang.English ? "Aaction" : "Действия";
    public string DataRecords => Lang == Lang.English ? "records" : "записей";
    public string DevicesTitle => Lang == Lang.English ? "Complexes of industrial devices" : "Комплексы промышленных устройств";
    public string Index => Lang == Lang.English ? "DeviceControl" : "Управление устройствами";
    public string IndexAccessQuery => Lang == Lang.English ? "Contact your system administrator to access." : "Свяжитесь с администратором системы, чтобы получить доступ.";
    public string IndexContinue => Lang == Lang.English ? "Click on a menu section to continue." : "Нажмите на раздел меню, чтобы продолжить.";
    public string ItemBarcode => Lang == Lang.English ? "Barcode" : "Штрихкод";
    public string ItemBarCode => Lang == Lang.English ? "Barcode" : "Штрихкод";
    public string ItemBarCodeType => Lang == Lang.English ? "Barcodes type" : "Тип штрихкода";
    public string ItemBox => Lang == Lang.English ? "Box" : "Коробка";
    public string ItemBundle => Lang == Lang.English ? "Bundle" : "Пакет";
    public string ItemBundleFk => Lang == Lang.English ? "Bundle" : "Упаковка";
    public string ItemContragent => Lang == Lang.English ? "Counterparty" : "Контрагент";
    public string ItemDevice => Lang == Lang.English ? "Device" : "Устройство";
    public string ItemDeviceScaleFk => Lang == Lang.English ? "Device with scale" : "Устройство с постом";
    public string ItemDeviceType => Lang == Lang.English ? "Device type" : "Тип устройства";
    public string ItemDeviceTypeFk => Lang == Lang.English ? "Device with type" : "Устройство с типом";
    public string ItemError => Lang == Lang.English ? "Error" : "Ошибка";
    public string ItemFont => Lang == Lang.English ? "Font" : "Шрифт";
    public string ItemHost => Lang == Lang.English ? "Host" : "Хост";
    public string ItemLabel => Lang == Lang.English ? "Label" : "Этикетка";
    public string ItemLog => Lang == Lang.English ? "Log" : "Лог";
    public string ItemLogo => Lang == Lang.English ? "Logo" : "Логотип";
    public string ItemModule => Lang == Lang.English ? "Module" : "Модуль";
    public string ItemNomenclature => Lang == Lang.English ? "Nomenclature" : "Номенклатура";
    public string ItemNomenclatureGroup => Lang == Lang.English ? "Nomenclature group" : "Номенклатурная группа";
    public string ItemNomenclatureUnit => Lang == Lang.English ? "Package" : "Упаковка";
    public string ItemOrder => Lang == Lang.English ? "Order" : "Заказ";
    public string ItemOrderStatus => Lang == Lang.English ? "Order status" : "Статус заказа";
    public string ItemOrderType => Lang == Lang.English ? "Order type" : "Типы заказа";
    public string ItemOrderWeighing => Lang == Lang.English ? "Order weighing" : "Взвешивание заказа";
    public string ItemOrganization => Lang == Lang.English ? "Organization" : "Организация";
    public string ItemPlu => Lang == Lang.English ? "PLU" : "ПЛУ";
    public string ItemPluBundleFk => Lang == Lang.English ? "PLU's bundle" : "Вложенность ПЛУ";
    public string ItemPluScale => Lang == Lang.English ? "Device PLU" : "ПЛУ устройства";
    public string ItemPluWeighing => Lang == Lang.English ? "Plu weighings" : "Взвешивание ПЛУ";
    public string ItemProductionFacilities => Lang == Lang.English ? "Prod. facilities" : "Производственные площадки";
    public string ItemProductionFacility => Lang == Lang.English ? "Prod. facility" : "Произв. площадка";
    public string ItemProductSeries => Lang == Lang.English ? "Product series" : "Серия продукта";
    public string ItemResource => Lang == Lang.English ? "Resource" : "Ресурс";
    public string ItemScale => Lang == Lang.English ? "Device" : "Устройство";
    public string ItemScreenShot => Lang == Lang.English ? "ScreenShot" : "Скриншот";
    public string ItemTask => Lang == Lang.English ? "Task" : "Задача";
    public string ItemTaskModule => Lang == Lang.English ? "Task module" : "Модуль задачи";
    public string ItemTemplate => Lang == Lang.English ? "Template" : "Шаблон";
    public string ItemTemplateResource => Lang == Lang.English ? "Template resource" : "Ресурс шаблона";
    public string ItemWeithingFact => Lang == Lang.English ? "Weithing fact" : "Взвешивание";
    public string ItemWorkshop => Lang == Lang.English ? "Workshop" : "Цех";
    public string ItemWorkShop => Lang == Lang.English ? "Workshop" : "Цех";
    public string LinkEmail => "morozov_dv@kolbasa-vs.ru";
    public string LinkEmailWithSubject => "mailto:morozov_dv@kolbasa-vs.ru&subject=device-control";
    public string LinkLabelary => "http://labelary.com/viewer.html";
    public string SectionAdministering => Lang == Lang.English ? "Administering" : "Администрирование";
    public string SectionAutomatedWorkplace => Lang == Lang.English ? "Automated Workplaces" : "Автоматизированные Рабочие Места";
    public string SectionAutomatedWorkplaceShort => Lang == Lang.English ? "AWs" : "АРМы";
    public string SectionBarCodes => Lang == Lang.English ? "Barcodes" : "Штрихкоды";
    public string SectionBarCodesShort => Lang == Lang.English ? "BC" : "ШК";
    public string SectionBarCodeTypes => Lang == Lang.English ? "Barcodes types" : "Типы штрихкодов";
    public string SectionBarCodeTypesShort => Lang == Lang.English ? "BC types" : "Типы ШК";
    public string SectionBoxes => Lang == Lang.English ? "Boxes" : "Коробки";
    public string SectionBrands => Lang == Lang.English ? "Brands" : "Бренды";
    public string SectionBundles => Lang == Lang.English ? "Bundles" : "Пакеты";
    public string SectionContragents => Lang == Lang.English ? "Counterparties" : "Контрагенты";
    public string SectionDevices => Lang == Lang.English ? "Devices" : "Устройства";
    public string SectionDevicesScalesFk => Lang == Lang.English ? "Devices with scales" : "Устройства с постами";
    public string SectionDevicesTypes => Lang == Lang.English ? "Device types " : "Типы устройств";
    public string SectionDevicesTypesFk => Lang == Lang.English ? "Devices with types" : "Устройства с типами";
    public string SectionDwh => Lang == Lang.English ? "DWH" : "МДМ";
    public string SectionDwhReferences => Lang == Lang.English ? "DWH References" : "DWH справочники";
    public string SectionFonts => Lang == Lang.English ? "Fonts" : "Шрифты";
    public string SectionHosts => Lang == Lang.English ? "Hosts" : "Хосты";
    public string SectionImport1C => Lang == Lang.English ? "Import from 1C" : "Импорт из 1С";
    public string SectionLabels => Lang == Lang.English ? "Labels" : "Этикетки";
    public string SectionLogos => Lang == Lang.English ? "Logos" : "Логотипы";
    public string SectionLogs => Lang == Lang.English ? "Logs" : "Логи";
    public string SectionMeasurements => Lang == Lang.English ? "Measurements" : "Измерения";
    public string SectionModules => Lang == Lang.English ? "Modules" : "Модули";
    public string SectionNestingFk => Lang == Lang.English ? "Nesting" : "Вложенность";
    public string SectionPluNestingFk => Lang == Lang.English ? "PLU's nesting" : "Вложенность ПЛУ";
    public string SectionNomenclatures => Lang == Lang.English ? "Nomenclatures" : "Номенклатуры";
    public string SectionNomenclaturesGroups => Lang == Lang.English ? "Nomenclatures groups" : "Номенклатурные группы";
    public string SectionNomenclatureUnits => Lang == Lang.English ? "Packages" : "Упаковки";
    public string SectionObsoletes => Lang == Lang.English ? "Obsoletes" : "Устаревшие";
    public string SectionOperations => Lang == Lang.English ? "Operations" : "Операции";
    public string SectionOrders => Lang == Lang.English ? "Orders" : "Заказы";
    public string SectionOrderStatuses => Lang == Lang.English ? "Order statuses" : "Статусы заказов";
    public string SectionOrdersWeighings => Lang == Lang.English ? "Orders weighings" : "Взвешивание заказов";
    public string SectionOrderTypes => Lang == Lang.English ? "Order types" : "Типы заказов";
    public string SectionOrganizations => Lang == Lang.English ? "Organizations" : "Организации";
    public string SectionPlus => Lang == Lang.English ? "PLUs" : "ПЛУ";
    public string SectionPlusBundlesFk => Lang == Lang.English ? "PLU & bundles" : "Пакеты ПЛУ";
    public string SectionPlusScales => Lang == Lang.English ? "PLU & devices" : "ПЛУ и устройства";
    public string SectionPlusWeighings => Lang == Lang.English ? "Plus weighings" : "Взвешивания ПЛУ";
    public string SectionProductionFacilities => Lang == Lang.English ? "Production facilities" : "Производственные площадки";
    public string SectionProductionFacilitiesShort => Lang == Lang.English ? "Facilities" : "Площадки";
    public string SectionProductSeries => Lang == Lang.English ? "Product series" : "Серии продуктов";
    public string SectionReferences => Lang == Lang.English ? "References" : "Справочники";
    public string SectionReferences1C => Lang == Lang.English ? "References 1C" : "Справочники 1C";
    public string SectionReferencesAdditional => Lang == Lang.English ? "Add. references" : "Доп. справочники";
    public string SectionReferencesDev => Lang == Lang.English ? "Development" : "Разработка";
    public string SectionResources => Lang == Lang.English ? "Resources" : "Ресурсы";
    public string SectionScales => Lang == Lang.English ? "Devices" : "Устройства";
    public string SectionScreenShots => Lang == Lang.English ? "Screenshots" : "Скриншоты";
    public string SectionTaskModules => Lang == Lang.English ? "Task Modules" : "Модули задач";
    public string SectionTasks => Lang == Lang.English ? "Tasks" : "Задачи";
    public string SectionTemplateResources => Lang == Lang.English ? "Template resources" : "Ресурсы шаблонов";
    public string SectionTemplates => Lang == Lang.English ? "Templates" : "Шаблоны";
    public string SectionWeighings => Lang == Lang.English ? "Weighings" : "Взвешивания";
    public string SectionWeithingFactsAggregation => Lang == Lang.English ? "Aggregation weithings" : "Взвешивания";
    public string SectionWeithingFactsAggregationShort => Lang == Lang.English ? "Aggr. weithings" : "Агр. взвешивания";
    public string SectionWorkShops => Lang == Lang.English ? "Workshops" : "Цеха";
    public string SqlServerDebug => "CREATIO";
    public string SqlServerRelease => "PALYCH";
    public string Table => Lang == Lang.English ? "Table" : "Таблица";
    public string TableActionAdd => Lang == Lang.English ? "Add" : "Добавить";
    public string TableActionCancel => Lang == Lang.English ? "Cancel" : "Отмена";
    public string TableActionClear => Lang == Lang.English ? "Clear" : "Очистить";
    public string TableActionCopy => Lang == Lang.English ? "Copy" : "Копировать";
    public string TableActionDelete => Lang == Lang.English ? "Delete" : "Удалить";
    public string TableActionEdit => Lang == Lang.English ? "Edit" : "Редактировать";
    public string TableActionFill => Lang == Lang.English ? "Fill" : "Заполнить";
    public string TableActionMark => Lang == Lang.English ? "Mark" : "Пометить";
    public string TableActions => Lang == Lang.English ? "Actions" : "Действия";
    public string TableActionSave => Lang == Lang.English ? "Save" : "Сохранить";
    public string TableActionsIsDeny => Lang == Lang.English ? "Actions is deny" : "Действия недоступны";
    public string WebAppIsStarted => Lang == Lang.English ? "Web-app is started" : "Веб-приложение запущено";

    #endregion
}
