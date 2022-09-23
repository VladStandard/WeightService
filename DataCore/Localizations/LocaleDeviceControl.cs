// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public partial class LocaleDeviceControl
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static LocaleDeviceControl _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static LocaleDeviceControl Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string WebAppIsStarted => Lang == LangEnum.English ? "Web-app is started" : "Веб-приложение запущено";
	public string Actions => Lang == LangEnum.English ? "Aaction" : "Действия";
	public string DataRecords => Lang == LangEnum.English ? "records" : "записей";
	public string DevicesTitle => Lang == LangEnum.English ? "Complexes of industrial devices" : "Комплексы промышленных устройств";
	public string Index => Lang == LangEnum.English ? "DeviceControl" : "Управление устройствами";
	public string IndexAccessQuery => Lang == LangEnum.English ? "Contact your system administrator to access." : "Свяжитесь с администратором системы, чтобы получить доступ.";
	public string IndexContinue => Lang == LangEnum.English ? "Click on a menu section to continue." : "Нажмите на раздел меню, чтобы продолжить.";
	public string ItemBarcode => Lang == LangEnum.English ? "Barcode" : "Штрихкод";
	public string ItemBarCode => Lang == LangEnum.English ? "Barcode" : "Штрихкод";
	public string ItemBarCodeType => Lang == LangEnum.English ? "Barcodes type" : "Тип штрихкода";
	public string ItemContragent => Lang == LangEnum.English ? "Counterparty" : "Контрагент";
	public string ItemDevice => Lang == LangEnum.English ? "Device" : "Устройство";
	public string ItemError => Lang == LangEnum.English ? "Error" : "Ошибка";
	public string ItemFont => Lang == LangEnum.English ? "Font" : "Шрифт";
	public string ItemHost => Lang == LangEnum.English ? "Host" : "Хост";
	public string ItemLabel => Lang == LangEnum.English ? "Label" : "Этикетка";
	public string ItemLog => Lang == LangEnum.English ? "Log" : "Лог";
	public string ItemLogo => Lang == LangEnum.English ? "Logo" : "Логотип";
	public string ItemModule => Lang == LangEnum.English ? "Module" : "Модуль";
	public string ItemNomenclature => Lang == LangEnum.English ? "Nomenclature" : "Номенклатура";
	public string ItemNomenclatureUnit => Lang == LangEnum.English ? "Package" : "Упаковка";
	public string ItemOrder => Lang == LangEnum.English ? "Order" : "Заказ";
	public string ItemOrderStatus => Lang == LangEnum.English ? "Order status" : "Статус заказа";
	public string ItemOrderType => Lang == LangEnum.English ? "Order type" : "Типы заказа";
	public string ItemOrderWeighing => Lang == LangEnum.English ? "Order weighing" : "Взвешивание заказа";
	public string ItemOrganization => Lang == LangEnum.English ? "Organization" : "Организация";
	public string ItemPlu => Lang == LangEnum.English ? "PLU" : "ПЛУ";
	public string ItemPluScale => Lang == LangEnum.English ? "Device PLU" : "ПЛУ устройства";
	public string ItemPluWeighing => Lang == LangEnum.English ? "Plu weighings" : "Взвешивание ПЛУ";
	public string ItemProductionFacilities => Lang == LangEnum.English ? "Prod. facilities" : "Производственные площадки";
	public string ItemProductionFacility => Lang == LangEnum.English ? "Prod. facility" : "Произв. площадка";
	public string ItemProductSeries => Lang == LangEnum.English ? "Product series" : "Серия продукта";
	public string ItemResource => Lang == LangEnum.English ? "Resource" : "Ресурс";
	public string ItemScale => Lang == LangEnum.English ? "Device" : "Устройство";
	public string ItemTask => Lang == LangEnum.English ? "Task" : "Задача";
	public string ItemTaskModule => Lang == LangEnum.English ? "Task module" : "Модуль задачи";
	public string ItemTemplate => Lang == LangEnum.English ? "Template" : "Шаблон";
	public string ItemTemplateResource => Lang == LangEnum.English ? "Template resource" : "Ресурс шаблона";
	public string ItemWeithingFact => Lang == LangEnum.English ? "Weithing fact" : "Взвешивание";
	public string ItemWorkshop => Lang == LangEnum.English ? "Workshop" : "Цех";
	public string ItemWorkShop => Lang == LangEnum.English ? "Workshop" : "Цех";
	public string LinkEmail => "morozov_dv@kolbasa-vs.ru";
	public string LinkEmailWithSubject => "mailto:morozov_dv@kolbasa-vs.ru&subject=device-control";
	public string LinkLabelary => "http://labelary.com/viewer.html";
	public string SectionAutomatedWorkplace => Lang == LangEnum.English ? "Automated Workplaces" : "Автоматизированные Рабочие Места";
	public string SectionAutomatedWorkplaceShort => Lang == LangEnum.English ? "AWs" : "АРМы";
	public string SectionBarCodes => Lang == LangEnum.English ? "Barcodes" : "Штрихкоды";
	public string SectionBarCodesShort => Lang == LangEnum.English ? "BC" : "ШК";
	public string SectionBarCodeTypes => Lang == LangEnum.English ? "Barcodes types" : "Типы штрихкодов";
	public string SectionBarCodeTypesShort => Lang == LangEnum.English ? "BC types" : "Типы ШК";
	public string SectionContragents => Lang == LangEnum.English ? "Counterparties" : "Контрагенты";
	public string SectionDevices => Lang == LangEnum.English ? "Devices" : "Устройства";
	public string SectionDwh => Lang == LangEnum.English ? "DWH" : "МДМ";
	public string SectionDwhReferences => Lang == LangEnum.English ? "DWH References" : "DWH справочники";
	public string SectionFonts => Lang == LangEnum.English ? "Fonts" : "Шрифты";
	public string SectionHosts => Lang == LangEnum.English ? "Hosts" : "Хосты";
	public string SectionLabels => Lang == LangEnum.English ? "Labels" : "Этикетки";
	public string SectionLogos => Lang == LangEnum.English ? "Logos" : "Логотипы";
	public string SectionLogs => Lang == LangEnum.English ? "Logs" : "Логи";
	public string SectionMeasurements => Lang == LangEnum.English ? "Measurements" : "Измерения";
	public string SectionModules => Lang == LangEnum.English ? "Modules" : "Модули";
	public string SectionNomenclatures => Lang == LangEnum.English ? "Nomenclatures" : "Номенклатура";
	public string SectionNomenclatureUnits => Lang == LangEnum.English ? "Packages" : "Упаковки";
	public string SectionObsoletes => Lang == LangEnum.English ? "Obsoletes" : "Устаревшие";
	public string SectionOrders => Lang == LangEnum.English ? "Orders" : "Заказы";
	public string SectionOrderStatuses => Lang == LangEnum.English ? "Order statuses" : "Статусы заказов";
	public string SectionOrdersWeighings => Lang == LangEnum.English ? "Orders weighings" : "Взвешивание заказов";
	public string SectionOrderTypes => Lang == LangEnum.English ? "Order types" : "Типы заказов";
	public string SectionOrganizations => Lang == LangEnum.English ? "Organizations" : "Организации";
	public string SectionPackages => Lang == LangEnum.English ? "Packages" : "Тара";
	public string SectionPlus => Lang == LangEnum.English ? "PLUs" : "ПЛУ";
	public string SectionPlusPackages => Lang == LangEnum.English ? "PLU & packages" : "ПЛУ и тара";
	public string SectionPlusScales => Lang == LangEnum.English ? "PLU & devices" : "ПЛУ и устройства";
	public string SectionPlusWeighings => Lang == LangEnum.English ? "Plus weighings" : "Взвешивания ПЛУ";
	public string SectionProductionFacilities => Lang == LangEnum.English ? "Production facilities" : "Производственные площадки";
	public string SectionProductionFacilitiesShort => Lang == LangEnum.English ? "Facilities" : "Площадки";
	public string SectionProductSeries => Lang == LangEnum.English ? "Product series" : "Серии продуктов";
	public string SectionReferences => Lang == LangEnum.English ? "References" : "Справочники";
	public string SectionReferencesAdditional => Lang == LangEnum.English ? "Add. references" : "Доп. справочники";
	public string SectionReferencesDev => Lang == LangEnum.English ? "Development" : "Разработка";
	public string SectionResources => Lang == LangEnum.English ? "Resources" : "Ресурсы";
	public string SectionScales => Lang == LangEnum.English ? "Devices" : "Устройства";
	public string SectionTaskModules => Lang == LangEnum.English ? "Task Modules" : "Модули задач";
	public string SectionTasks => Lang == LangEnum.English ? "Tasks" : "Задачи";
	public string SectionTemplateResources => Lang == LangEnum.English ? "Template resources" : "Ресурсы шаблонов";
	public string SectionTemplates => Lang == LangEnum.English ? "Templates" : "Шаблоны";
	public string SectionWeithingFactsAggregation => Lang == LangEnum.English ? "Aggregation weithings" : "Взвешивания";
	public string SectionWeithingFactsAggregationShort => Lang == LangEnum.English ? "Aggr. weithings" : "Агр. взвешивания";
	public string SectionWorkShops => Lang == LangEnum.English ? "Workshops" : "Цеха";
	public string SqlServerDebug => "CREATIO";
	public string SqlServerRelease => "PALYCH";
	public string Table => Lang == LangEnum.English ? "Table" : "Таблица";
	public string TableActionAdd => Lang == LangEnum.English ? "Add" : "Добавить";
	public string TableActionCancel => Lang == LangEnum.English ? "Cancel" : "Отмена";
	public string TableActionClear => Lang == LangEnum.English ? "Clear" : "Очистить";
	public string TableActionCopy => Lang == LangEnum.English ? "Copy" : "Копировать";
	public string TableActionDelete => Lang == LangEnum.English ? "Delete" : "Удалить";
	public string TableActionEdit => Lang == LangEnum.English ? "Edit" : "Редактировать";
	public string TableActionFill => Lang == LangEnum.English ? "Fill" : "Заполнить";
	public string TableActionMark => Lang == LangEnum.English ? "Mark" : "Пометить";
	public string TableActions => Lang == LangEnum.English ? "___" : "Действия";
	public string TableActionSave => Lang == LangEnum.English ? "Save" : "Сохранить";
	public string TableActionsIsDeny => Lang == LangEnum.English ? "___" : "Действия недоступны";

	#endregion
}
