// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
namespace DataCore.Localizations;

public partial class LocaleDeviceControl
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static LocaleDeviceControl _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static LocaleDeviceControl Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

    #region Public and private fields, properties, constructor

    public string WebAppIsStarted => Lang == ShareEnums.Lang.English ? "Web-app is started" : "Веб-приложение запущено";
	public string Actions => Lang == ShareEnums.Lang.English ? "Aaction" : "Действия";
	public string DataRecords => Lang == ShareEnums.Lang.English ? "records" : "записей";
	public string DevicesTitle => Lang == ShareEnums.Lang.English ? "Complexes of industrial devices" : "Комплексы промышленных устройств";
	public string Index => Lang == ShareEnums.Lang.English ? "DeviceControl" : "Управление устройствами";
	public string IndexAccessQuery => Lang == ShareEnums.Lang.English ? "Contact your system administrator to access." : "Свяжитесь с администратором системы, чтобы получить доступ.";
	public string IndexContinue => Lang == ShareEnums.Lang.English ? "Click on a menu section to continue." : "Нажмите на раздел меню, чтобы продолжить.";
	public string ItemBarcode => Lang == ShareEnums.Lang.English ? "Barcode" : "Штрихкод";
	public string ItemBarCodeType => Lang == ShareEnums.Lang.English ? "Barcodes type" : "Тип штрихкода";
	public string ItemContragent => Lang == ShareEnums.Lang.English ? "Counterparty" : "Контрагент";
	public string ItemDevice => Lang == ShareEnums.Lang.English ? "Device" : "Устройство";
	public string ItemError => Lang == ShareEnums.Lang.English ? "Error" : "Ошибка";
	public string ItemFont => Lang == ShareEnums.Lang.English ? "Font" : "Шрифт";
	public string ItemHost => Lang == ShareEnums.Lang.English ? "Host" : "Хост";
	public string ItemLabel => Lang == ShareEnums.Lang.English ? "Label" : "Этикетка";
	public string ItemLog => Lang == ShareEnums.Lang.English ? "Log" : "Лог";
	public string ItemLogo => Lang == ShareEnums.Lang.English ? "Logo" : "Логотип";
	public string ItemModule => Lang == ShareEnums.Lang.English ? "Module" : "Модуль";
	public string ItemNomenclature => Lang == ShareEnums.Lang.English ? "Nomenclature" : "Номенклатура";
	public string ItemNomenclatureUnit => Lang == ShareEnums.Lang.English ? "Package" : "Упаковка";
	public string ItemOrder => Lang == ShareEnums.Lang.English ? "Order" : "Заказ";
	public string ItemOrderStatus => Lang == ShareEnums.Lang.English ? "Order status" : "Статус заказа";
	public string ItemOrderType => Lang == ShareEnums.Lang.English ? "Order type" : "Типы заказа";
	public string ItemOrderWeighing => Lang == ShareEnums.Lang.English ? "Order weighing" : "Взвешивание заказа";
	public string ItemOrganization => Lang == ShareEnums.Lang.English ? "Organization" : "Организация";
	public string ItemPlu => Lang == ShareEnums.Lang.English ? "PLU" : "ПЛУ";
	public string ItemPluScale => Lang == ShareEnums.Lang.English ? "Device PLU" : "ПЛУ устройства";
	public string ItemProductionFacilities => Lang == ShareEnums.Lang.English ? "Prod. facilities" : "Производственные площадки";
	public string ItemProductionFacility => Lang == ShareEnums.Lang.English ? "Prod. facility" : "Произв. площадка";
	public string ItemProductSeries => Lang == ShareEnums.Lang.English ? "Product series" : "Серия продукта";
	public string ItemResource => Lang == ShareEnums.Lang.English ? "Resource" : "Ресурс";
	public string ItemScale => Lang == ShareEnums.Lang.English ? "Device" : "Устройство";
	public string ItemTask => Lang == ShareEnums.Lang.English ? "Task" : "Задача";
	public string ItemTaskModule => Lang == ShareEnums.Lang.English ? "Task module" : "Модуль задачи";
	public string ItemTemplate => Lang == ShareEnums.Lang.English ? "Template" : "Шаблон";
	public string ItemTemplateResource => Lang == ShareEnums.Lang.English ? "Template resource" : "Ресурс шаблона";
	public string ItemWeithingFact => Lang == ShareEnums.Lang.English ? "Weithing fact" : "Взвешивание";
	public string ItemWorkshop => Lang == ShareEnums.Lang.English ? "Workshop" : "Цех";
	public string LinkEmail => "morozov_dv@kolbasa-vs.ru";
	public string LinkEmailWithSubject => "mailto:morozov_dv@kolbasa-vs.ru&subject=device-control";
	public string LinkLabelary => "http://labelary.com/viewer.html";
	public string SectionAutomatedWorkplace => Lang == ShareEnums.Lang.English ? "Automated Workplaces" : "Автоматизированные Рабочие Места";
	public string SectionAutomatedWorkplaceShort => Lang == ShareEnums.Lang.English ? "AWs" : "АРМы";
	public string SectionBarCodes => Lang == ShareEnums.Lang.English ? "Barcodes" : "Штрихкоды";
	public string SectionBarCodesShort => Lang == ShareEnums.Lang.English ? "BC" : "ШК";
	public string SectionBarCodeTypes => Lang == ShareEnums.Lang.English ? "Barcodes types" : "Типы штрихкодов";
	public string SectionBarCodeTypesShort => Lang == ShareEnums.Lang.English ? "BC types" : "Типы ШК";
	public string SectionContragents => Lang == ShareEnums.Lang.English ? "Counterparties" : "Контрагенты";
	public string SectionDevices => Lang == ShareEnums.Lang.English ? "Devices" : "Устройства";
	public string SectionDwh => Lang == ShareEnums.Lang.English ? "DWH" : "МДМ";
	public string SectionDwhReferences => Lang == ShareEnums.Lang.English ? "DWH References" : "DWH справочники";
	public string SectionFonts => Lang == ShareEnums.Lang.English ? "Fonts" : "Шрифты";
	public string SectionHosts => Lang == ShareEnums.Lang.English ? "Hosts" : "Хосты";
	public string SectionLabels => Lang == ShareEnums.Lang.English ? "Labels" : "Этикетки";
	public string SectionLogos => Lang == ShareEnums.Lang.English ? "Logos" : "Логотипы";
	public string SectionLogs => Lang == ShareEnums.Lang.English ? "Logs" : "Логи";
	public string SectionMeasurements => Lang == ShareEnums.Lang.English ? "Measurements" : "Измерения";
	public string SectionModules => Lang == ShareEnums.Lang.English ? "Modules" : "Модули";
	public string SectionNomenclatures => Lang == ShareEnums.Lang.English ? "Nomenclatures" : "Номенклатура";
	public string SectionNomenclatureUnits => Lang == ShareEnums.Lang.English ? "Packages" : "Упаковки";
	public string SectionObsoletes => Lang == ShareEnums.Lang.English ? "Obsoletes" : "Устаревшие";
	public string SectionOrders => Lang == ShareEnums.Lang.English ? "Orders" : "Заказы";
	public string SectionOrderStatuses => Lang == ShareEnums.Lang.English ? "Order statuses" : "Статусы заказов";
	public string SectionOrdersWeighings => Lang == ShareEnums.Lang.English ? "Orders weighings" : "Взвешивание заказов";
	public string SectionOrderTypes => Lang == ShareEnums.Lang.English ? "Order types" : "Типы заказов";
	public string SectionOrganizations => Lang == ShareEnums.Lang.English ? "Organizations" : "Организации";
	public string SectionPlus => Lang == ShareEnums.Lang.English ? "PLUs" : "ПЛУ";
	public string SectionPlusScales => Lang == ShareEnums.Lang.English ? "Devices PLUs" : "ПЛУ устройств";
	public string SectionPlusWeighings => Lang == ShareEnums.Lang.English ? "Plus weighings" : "Взвешивания ПЛУ";
	public string SectionProductionFacilities => Lang == ShareEnums.Lang.English ? "Production facilities" : "Производственные площадки";
	public string SectionProductionFacilitiesShort => Lang == ShareEnums.Lang.English ? "Facilities" : "Площадки";
	public string SectionProductSeries => Lang == ShareEnums.Lang.English ? "Product series" : "Серии продуктов";
	public string SectionReferences => Lang == ShareEnums.Lang.English ? "References" : "Справочники";
	public string SectionReferencesAdditional => Lang == ShareEnums.Lang.English ? "Add. references" : "Доп. справочники";
	public string SectionReferencesDev => Lang == ShareEnums.Lang.English ? "Development" : "Разработка";
	public string SectionResources => Lang == ShareEnums.Lang.English ? "Resources" : "Ресурсы";
	public string SectionScales => Lang == ShareEnums.Lang.English ? "Devices" : "Устройства";
	public string SectionTaskModules => Lang == ShareEnums.Lang.English ? "Task Modules" : "Модули задач";
	public string SectionTasks => Lang == ShareEnums.Lang.English ? "Tasks" : "Задачи";
	public string SectionTemplateResources => Lang == ShareEnums.Lang.English ? "Template resources" : "Ресурсы шаблонов";
	public string SectionTemplates => Lang == ShareEnums.Lang.English ? "Templates" : "Шаблоны";
	public string SectionWeithingFactsAggregation => Lang == ShareEnums.Lang.English ? "Aggregation weithings" : "Взвешивания";
	public string SectionWeithingFactsAggregationShort => Lang == ShareEnums.Lang.English ? "Aggr. weithings" : "Агр. взвешивания";
	public string SectionWorkShops => Lang == ShareEnums.Lang.English ? "Workshops" : "Цеха";
	public string SqlServerDebug => "CREATIO";
	public string SqlServerRelease => "PALYCH";
	public string Table => Lang == ShareEnums.Lang.English ? "Table" : "Таблица";
	public string TableActionAdd => Lang == ShareEnums.Lang.English ? "Add" : "Добавить";
	public string TableActionCancel => Lang == ShareEnums.Lang.English ? "Cancel" : "Отмена";
	public string TableActionClear => Lang == ShareEnums.Lang.English ? "Clear" : "Очистить";
	public string TableActionCopy => Lang == ShareEnums.Lang.English ? "Copy" : "Копировать";
	public string TableActionDelete => Lang == ShareEnums.Lang.English ? "Delete" : "Удалить";
	public string TableActionEdit => Lang == ShareEnums.Lang.English ? "Edit" : "Редактировать";
	public string TableActionFill => Lang == ShareEnums.Lang.English ? "Fill" : "Заполнить";
	public string TableActionMark => Lang == ShareEnums.Lang.English ? "Mark" : "Пометить";
	public string TableActionNew => Lang == ShareEnums.Lang.English ? "New" : "Новый";
	public string TableActions => Lang == ShareEnums.Lang.English ? "___" : "Действия";
	public string TableActionSave => Lang == ShareEnums.Lang.English ? "Save" : "Сохранить";
	public string TableActionsIsDeny => Lang == ShareEnums.Lang.English ? "___" : "Действия недоступны";

	#endregion
}
