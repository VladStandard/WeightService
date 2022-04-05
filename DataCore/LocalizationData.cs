// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using System;
using System.Collections.Generic;
using static DataCore.LocalizationCore;

namespace DataCore
{
    public static class LocalizationData
    {
        public static ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        public static class DeviceControl
        {
            public static string DataRecords => Lang == ShareEnums.Lang.English ? "records" : "записей";
            public static string DevicesTitle => Lang == ShareEnums.Lang.English ? "Complexes of industrial devices" : "Комплексы промышленных устройств";
            public static string Index => Lang == ShareEnums.Lang.English ? "DeviceControl" : "Управление устройствами";
            public static string IndexAccessQuery => Lang == ShareEnums.Lang.English ? "Contact your system administrator to access." : "Свяжитесь с администратором системы, чтобы получить доступ.";
            public static string IndexContinue => Lang == ShareEnums.Lang.English ? "Click on a menu section to continue." : "Нажмите на раздел меню, чтобы продолжить.";
            public static string LinkEmail => "morozov_dv@kolbasa-vs.ru";
            public static string LinkEmailWithSubject => "mailto:morozov_dv@kolbasa-vs.ru&subject=device-control";
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
                public const string Label = "/item/label";
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
                public const string WeithingFact = "/item/weithingfact";
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
                public const string Labels = "/section/labels";
                public const string Logs = "/section/logs";
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
                public const string WeithingFacts = "/section/weithingfacts";
                public const string WorkShops = "/section/workshops";
            }
            #endregion
            #region Items
            public static string ItemBarcode => Lang == ShareEnums.Lang.English ? "Barcode" : "Штрихкод";
            public static string ItemBarCodeType => Lang == ShareEnums.Lang.English ? "Barcodes type" : "Тип штрихкода";
            public static string ItemContragent => Lang == ShareEnums.Lang.English ? "Counterparty" : "Контрагент";
            public static string ItemDevice => Lang == ShareEnums.Lang.English ? "Device" : "Устройство";
            public static string ItemError => Lang == ShareEnums.Lang.English ? "Error" : "Ошибка";
            public static string ItemFont => Lang == ShareEnums.Lang.English ? "Font" : "Шрифт";
            public static string ItemHost => Lang == ShareEnums.Lang.English ? "Host" : "Хост";
            public static string ItemLabel => Lang == ShareEnums.Lang.English ? "Label" : "Этикетка";
            public static string ItemLog => Lang == ShareEnums.Lang.English ? "Log" : "Лог";
            public static string ItemLogo => Lang == ShareEnums.Lang.English ? "Logo" : "Логотип";
            public static string ItemModule => Lang == ShareEnums.Lang.English ? "Module" : "Модуль";
            public static string ItemNomenclature => Lang == ShareEnums.Lang.English ? "Nomenclature" : "Номенклатура";
            public static string ItemNomenclatureUnit => Lang == ShareEnums.Lang.English ? "Package" : "Упаковка";
            public static string ItemOrder => Lang == ShareEnums.Lang.English ? "Order" : "Заказ";
            public static string ItemOrderStatus => Lang == ShareEnums.Lang.English ? "Order status" : "Статус заказа";
            public static string ItemOrderType => Lang == ShareEnums.Lang.English ? "Order type" : "Типы заказа";
            public static string ItemOrganization => Lang == ShareEnums.Lang.English ? "Organization" : "Организация";
            public static string ItemPlu => Lang == ShareEnums.Lang.English ? "PLU" : "ПЛУ";
            public static string ItemPrinter => Lang == ShareEnums.Lang.English ? "Printer" : "Принтер";
            public static string ItemPrinterResource => Lang == ShareEnums.Lang.English ? "Printer resource" : "Ресурс принтера";
            public static string ItemPrinterType => Lang == ShareEnums.Lang.English ? "Printer type" : "Тип принтера";
            public static string ItemProductionFacilities => Lang == ShareEnums.Lang.English ? "Prod. facilities" : "Производственные площадки";
            public static string ItemProductionFacility => Lang == ShareEnums.Lang.English ? "Prod. facility" : "Произв. площадка";
            public static string ItemProductSeries => Lang == ShareEnums.Lang.English ? "Product series" : "Серия продукта";
            public static string ItemResource => Lang == ShareEnums.Lang.English ? "Resource" : "Ресурс";
            public static string ItemScale => Lang == ShareEnums.Lang.English ? "Device" : "Устройство";
            public static string ItemTask => Lang == ShareEnums.Lang.English ? "Task" : "Задача";
            public static string ItemTaskModule => Lang == ShareEnums.Lang.English ? "Task module" : "Модуль задачи";
            public static string ItemTemplate => Lang == ShareEnums.Lang.English ? "Template" : "Шаблон";
            public static string ItemTemplateResource => Lang == ShareEnums.Lang.English ? "Template resource" : "Ресурс шаблона";
            public static string ItemWeithingFact => Lang == ShareEnums.Lang.English ? "Weithing fact" : "Взвешивание";
            public static string ItemWorkshop => Lang == ShareEnums.Lang.English ? "Workshop" : "Цех";
            #endregion
            #region Sections
            public static string SectionBarCodes => Lang == ShareEnums.Lang.English ? "Barcodes" : "Штрихкоды";
            public static string SectionBarCodesShort => Lang == ShareEnums.Lang.English ? "BC" : "ШК";
            public static string SectionBarCodeTypes => Lang == ShareEnums.Lang.English ? "Barcodes types" : "Типы штрихкодов";
            public static string SectionBarCodeTypesShort => Lang == ShareEnums.Lang.English ? "BC types" : "Типы ШК";
            public static string SectionContragents => Lang == ShareEnums.Lang.English ? "Counterparties" : "Контрагенты";
            public static string SectionDevices => Lang == ShareEnums.Lang.English ? "Devices" : "Устройства";
            public static string SectionFonts => Lang == ShareEnums.Lang.English ? "Fonts" : "Шрифты";
            public static string SectionHosts => Lang == ShareEnums.Lang.English ? "Hosts" : "Хосты";
            public static string SectionLabels => Lang == ShareEnums.Lang.English ? "Labels" : "Этикетки";
            public static string SectionLogos => Lang == ShareEnums.Lang.English ? "Logos" : "Логотипы";
            public static string SectionLogs => Lang == ShareEnums.Lang.English ? "Logs" : "Логи";
            public static string SectionModules => Lang == ShareEnums.Lang.English ? "Modules" : "Модули";
            public static string SectionNomenclatures => Lang == ShareEnums.Lang.English ? "Nomenclatures" : "Номенклатура";
            public static string SectionNomenclatureUnits => Lang == ShareEnums.Lang.English ? "Packages" : "Упаковки";
            public static string SectionOrders => Lang == ShareEnums.Lang.English ? "Orders" : "Заказы";
            public static string SectionOrderStatuses => Lang == ShareEnums.Lang.English ? "Order statuses" : "Статусы заказов";
            public static string SectionOrderTypes => Lang == ShareEnums.Lang.English ? "Order types" : "Типы заказов";
            public static string SectionOrganizations => Lang == ShareEnums.Lang.English ? "Organizations" : "Организации";
            public static string SectionPlus => Lang == ShareEnums.Lang.English ? "PLU" : "ПЛУ";
            public static string SectionPrinterResources => Lang == ShareEnums.Lang.English ? "Printer resources" : "Ресурсы принтера";
            public static string SectionPrinters => Lang == ShareEnums.Lang.English ? "Printers" : "Принтеры";
            public static string SectionPrinterTypes => Lang == ShareEnums.Lang.English ? "Printer types" : "Типы принтеров";
            public static string SectionProductionFacilities => Lang == ShareEnums.Lang.English ? "Prod. facilities" : "Произв. площадки";
            public static string SectionProductSeries => Lang == ShareEnums.Lang.English ? "Product series" : "Серии продуктов";
            public static string SectionResources => Lang == ShareEnums.Lang.English ? "Resources" : "Ресурсы";
            public static string SectionScales => Lang == ShareEnums.Lang.English ? "Devices" : "Устройства";
            public static string SectionTaskModules => Lang == ShareEnums.Lang.English ? "Task Modules" : "Модули задач";
            public static string SectionTasks => Lang == ShareEnums.Lang.English ? "Tasks" : "Задачи";
            public static string SectionTemplateResources => Lang == ShareEnums.Lang.English ? "Template resources" : "Ресурсы шаблонов";
            public static string SectionTemplates => Lang == ShareEnums.Lang.English ? "Templates" : "Шаблоны";
            public static string SectionWeithingFacts => Lang == ShareEnums.Lang.English ? "Weithing facts" : "Взвешивания";
            public static string SectionWorkshops => Lang == ShareEnums.Lang.English ? "Workshops" : "Цеха";
            #endregion
            #region Tables
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
            public static string TableFieldAccessLevel => Lang == ShareEnums.Lang.English ? "___" : "Уровень доступа";
            public static string TableFieldActive => Lang == ShareEnums.Lang.English ? "___" : "Активно";
            public static string TableFieldApp => Lang == ShareEnums.Lang.English ? "___" : "Программа";
            public static string TableFieldBarCodeTypeId => Lang == ShareEnums.Lang.English ? "___" : "ID типа ШК";
            public static string TableFieldBrand => Lang == ShareEnums.Lang.English ? "___" : "Brand";
            public static string TableFieldCategoryId => Lang == ShareEnums.Lang.English ? "___" : "ID категории";
            public static string TableFieldCategoryName => Lang == ShareEnums.Lang.English ? "Category" : "Категория";
            public static string TableFieldChangeDt => Lang == ShareEnums.Lang.English ? "Edit date" : "Дата редактирования";
            public static string TableFieldCheckGtin => Lang == ShareEnums.Lang.English ? "GTIN" : "ГТИН";
            public static string TableFieldCheckWeight => Lang == ShareEnums.Lang.English ? "___" : "Весовая продукция";
            public static string TableFieldCode => Lang == ShareEnums.Lang.English ? "___" : "Код";
            public static string TableFieldComment => Lang == ShareEnums.Lang.English ? "___" : "Комментарий";
            public static string TableFieldConsumerName => Lang == ShareEnums.Lang.English ? "___" : "ConsumerName";
            public static string TableFieldContragentId => Lang == ShareEnums.Lang.English ? "___" : "ID контрагента";
            public static string TableFieldCount => Lang == ShareEnums.Lang.English ? "___" : "Количество";
            public static string TableFieldCreateDt => Lang == ShareEnums.Lang.English ? "___" : "Дата создания";
            public static string TableFieldDayOfWeek => Lang == ShareEnums.Lang.English ? "___" : "День недели";
            public static string TableFieldDescription => Lang == ShareEnums.Lang.English ? "___" : "Описание";
            public static string TableFieldDeviceComPort => Lang == ShareEnums.Lang.English ? "___" : "COM-порт";
            public static string TableFieldDeviceIp => Lang == ShareEnums.Lang.English ? "IP-address" : "IP-адрес";
            public static string TableFieldDeviceMac => Lang == ShareEnums.Lang.English ? "MAC-address" : "MAC-адрес";
            public static string TableFieldDeviceNumber => Lang == ShareEnums.Lang.English ? "Device number" : "Номер устройства";
            public static string TableFieldDevicePort => Lang == ShareEnums.Lang.English ? "Device port" : "Порт устройства";
            public static string TableFieldDeviceReceiveTimeout => Lang == ShareEnums.Lang.English ? "Receive timeout" : "Таймаут приёма";
            public static string TableFieldDeviceSendTimeout => Lang == ShareEnums.Lang.English ? "Send timeout" : "Таймаут отправки";
            public static string TableFieldEan13 => "EAN13";
            public static string TableFieldEnabled = Lang == ShareEnums.Lang.English ? "Enabled" : "Активно";
            public static string TableFieldException => Lang == ShareEnums.Lang.English ? "Exception" : "Исключение";
            public static string TableFieldFile => Lang == ShareEnums.Lang.English ? "File" : "Файл";
            public static string TableFieldFilePath => Lang == ShareEnums.Lang.English ? "File path" : "Путь к файлу";
            public static string TableFieldFullName => Lang == ShareEnums.Lang.English ? "Full name" : "Полное наименование";
            public static string TableFieldGln => "GLN";
            public static string TableFieldGoodsBoxQuantly => Lang == ShareEnums.Lang.English ? "Investments in the box" : "Вложений в короб";
            public static string TableFieldGoodsBruttoWeight => Lang == ShareEnums.Lang.English ? "Gross weight" : "Вес брутто";
            public static string TableFieldGoodsDescription => Lang == ShareEnums.Lang.English ? "Good description" : "Описание товара";
            public static string TableFieldGoodsFullName => Lang == ShareEnums.Lang.English ? "Full name" : "Полное наименование";
            public static string TableFieldGoodsName => Lang == ShareEnums.Lang.English ? "Product" : "Товар";
            public static string TableFieldGoodsTareWeight => Lang == ShareEnums.Lang.English ? "Tare weight" : "Вес тары";
            public static string TableFieldGoodsTareWeightDescription => Lang == ShareEnums.Lang.English ? "Calculation formula: Tare weight = weight of box + (weight of package * number of inserts)" : "Расчётная формула: Вес тары = вес коробки + (вес пакета * кол. вложений)";
            public static string TableFieldGtin => Lang == ShareEnums.Lang.English ? "GTIN" : "ГТИН";
            public static string TableFieldGuidMercury => Lang == ShareEnums.Lang.English ? "GUID Mercury" : "ГУИД Меркурий";
            public static string TableFieldHost => Lang == ShareEnums.Lang.English ? "Host" : "Хост";
            public static string TableFieldHttpStatusCode => Lang == ShareEnums.Lang.English ? "Status" : "Статус";
            public static string TableFieldHttpStatusException => Lang == ShareEnums.Lang.English ? "Exception" : "Ошибка";
            public static string TableFieldHttpStatusNoException => Lang == ShareEnums.Lang.English ? "No exceptions" : "Ошибок нет";
            public static string TableFieldIcon => Lang == ShareEnums.Lang.English ? "Icon" : "Иконка";
            public static string TableFieldId => Lang == ShareEnums.Lang.English ? "ID" : "ИД";
            public static string TableFieldIdDwh => Lang == ShareEnums.Lang.English ? "ID DWH" : "ИД ДВХ";
            public static string TableFieldIdRRef => Lang == ShareEnums.Lang.English ? "ID 1C" : "ИД 1С";
            public static string TableFieldImageData => Lang == ShareEnums.Lang.English ? "Image data" : "Данные";
            public static string TableFieldImageDataInfo => Lang == ShareEnums.Lang.English ? "Info" : "Информация";
            public static string TableFieldInnerException => Lang == ShareEnums.Lang.English ? "Inner exception" : "Вложенное исключение";
            public static string TableFieldIsClose => Lang == ShareEnums.Lang.English ? "Is close" : "Закрыто";
            public static string TableFieldItf14 => "ITF14";
            public static string TableFieldKneding => Lang == ShareEnums.Lang.English ? "Kneading" : "Замес";
            public static string TableFieldLevel => Lang == ShareEnums.Lang.English ? "Level" : "Уровень";
            public static string TableFieldLine => Lang == ShareEnums.Lang.English ? "Line" : "Линия";
            public static string TableFieldLogType => Lang == ShareEnums.Lang.English ? "Log type" : "Тип лога";
            public static string TableFieldLowerWeightThreshold => Lang == ShareEnums.Lang.English ? "Lower value of the box weight" : "Нижнее значение веса короба";
            public static string TableFieldMarked => Lang == ShareEnums.Lang.English ? "In the archive" : "В архиве";
            public static string TableFieldMarkedShort => Lang == ShareEnums.Lang.English ? "x" : "х";
            public static string TableFieldMember => Lang == ShareEnums.Lang.English ? "Method" : "Метод";
            public static string TableFieldMessage => Lang == ShareEnums.Lang.English ? "Message" : "Сообщение";
            public static string TableFieldName => Lang == ShareEnums.Lang.English ? "Name" : "Наименование";
            public static string TableFieldNameFull => Lang == ShareEnums.Lang.English ? "Full name" : "Полное наименование";
            public static string TableFieldNetWeight => Lang == ShareEnums.Lang.English ? "Net weight" : "Вес нетто";
            public static string TableFieldNomenclatureId => Lang == ShareEnums.Lang.English ? "Nomenclature ID" : "ID номенклатуры";
            public static string TableFieldNomenclatureName => Lang == ShareEnums.Lang.English ? "Nomenclature" : "Номенклатура";
            public static string TableFieldNomenclatureType => Lang == ShareEnums.Lang.English ? "Type of nomenclature" : "Тип номенклатуры";
            public static string TableFieldNomenclatureUnitId => Lang == ShareEnums.Lang.English ? "Nomenclature unit ID" : "ID юнита номенклатуры";
            public static string TableFieldNominalWeight => Lang == ShareEnums.Lang.English ? "Nominal box weight" : "Номинальный вес короба";
            public static string TableFieldOrder => Lang == ShareEnums.Lang.English ? "Order" : "Заказ";
            public static string TableFieldPackQuantly => Lang == ShareEnums.Lang.English ? "Pack quantly" : "Быстрота упаковки";
            public static string TableFieldPackTypeId => Lang == ShareEnums.Lang.English ? "Package type ID" : "ID типа упаковки";
            public static string TableFieldPackWeight => Lang == ShareEnums.Lang.English ? "Package weight" : "Вес упаковки";
            public static string TableFieldPlu => Lang == ShareEnums.Lang.English ? "PLU" : "ПЛУ";
            public static string TableFieldPluDescription => Lang == ShareEnums.Lang.English ? "Use the `|` symbol to move the line." : "Для переноса строки используйте символ `|`";
            public static string TableFieldPluNumber => Lang == ShareEnums.Lang.English ? "# PLU" : "№ ПЛУ";
            public static string TableFieldPrinter => Lang == ShareEnums.Lang.English ? "Printer" : "Принтер";
            public static string TableFieldPrinterControlPanel => Lang == ShareEnums.Lang.English ? "Printer control panel" : "Панель управления принтером";
            public static string TableFieldPrinterDarknessLevel => Lang == ShareEnums.Lang.English ? "Level of darkness" : "Уровень темноты";
            public static string TableFieldPrinterIp => Lang == ShareEnums.Lang.English ? "IP-address" : "IP-адрес";
            public static string TableFieldPrinterMac => Lang == ShareEnums.Lang.English ? "MAC-address" : "MAC-адрес";
            public static string TableFieldPrinterPassword => Lang == ShareEnums.Lang.English ? "Printer password" : "Пароль принтера";
            public static string TableFieldPrinterPeelOffSet => Lang == ShareEnums.Lang.English ? "Offset" : "Смещение";
            public static string TableFieldPrinterPort => Lang == ShareEnums.Lang.English ? "Printer port" : "Порт принтера";
            public static string TableFieldPrinterPortShort => Lang == ShareEnums.Lang.English ? "Port" : "Порт";
            public static string TableFieldPrinterType => Lang == ShareEnums.Lang.English ? "Printer type" : "Тип принтера";
            public static string TableFieldProductDate => Lang == ShareEnums.Lang.English ? "Product date" : "Дата продукции";
            public static string TableFieldProductionFacilityName => Lang == ShareEnums.Lang.English ? "Production facility" : "Производственная площадка";
            public static string TableFieldRegNum => Lang == ShareEnums.Lang.English ? "#" : "№";
            public static string TableFieldResource => Lang == ShareEnums.Lang.English ? "Resource" : "Ресурс";
            public static string TableFieldRow => Lang == ShareEnums.Lang.English ? "Row" : "Строка";
            public static string TableFieldScale => Lang == ShareEnums.Lang.English ? "Device" : "Устройство";
            public static string TableFieldScaleFactor => Lang == ShareEnums.Lang.English ? "Scale factor" : "Коэф. масштабирования";
            public static string TableFieldScaleId => Lang == ShareEnums.Lang.English ? "Scale ID" : "ID весов";
            public static string TableFieldSettingsFile => Lang == ShareEnums.Lang.English ? "Settings file" : "Файл настроек";
            public static string TableFieldShelfLifeDays => Lang == ShareEnums.Lang.English ? "Shelf life (days)" : "Срок годности (суток)";
            public static string TableFieldSscc => Lang == ShareEnums.Lang.English ? "Transport packing code (SSCC)" : "Код транспортной упаковки (SSCC)";
            public static string TableFieldState => Lang == ShareEnums.Lang.English ? "Status" : "Статус";
            public static string TableFieldStorage => Lang == ShareEnums.Lang.English ? "Storage" : "Склад";
            public static string TableFieldTareWeight => Lang == ShareEnums.Lang.English ? "Tare weight" : "Вес тары";
            public static string TableFieldTaskType = Lang == ShareEnums.Lang.English ? "Task type" : "Тип задачи";
            public static string TableFieldTemplate => Lang == ShareEnums.Lang.English ? "Template" : "Шаблон";
            public static string TableFieldTemplateDefault => Lang == ShareEnums.Lang.English ? "Default template" : "Шаблон по-умолчанию";
            public static string TableFieldTemplateId => Lang == ShareEnums.Lang.English ? "Template ID" : "ID шаблона";
            public static string TableFieldTemplateIdDefault => Lang == ShareEnums.Lang.English ? "Default template ID" : "ID шаблона по-умолчанию";
            public static string TableFieldTemplateIdSeries => Lang == ShareEnums.Lang.English ? "Series ID" : "ID серии";
            public static string TableFieldTemplateSeries => Lang == ShareEnums.Lang.English ? "Summary label template" : "Шаблон суммарной этикетки";
            public static string TableFieldTitle => Lang == ShareEnums.Lang.English ? "Title" : "Заголовок";
            public static string TableFieldType = Lang == ShareEnums.Lang.English ? "Type" : "Тип";
            public static string TableFieldUid => Lang == ShareEnums.Lang.English ? "UID" : "УИД";
            public static string TableFieldUpperWeightThreshold => Lang == ShareEnums.Lang.English ? "Upper value of the box weight" : "Верхнее значение веса короба";
            public static string TableFieldUseOrder => Lang == ShareEnums.Lang.English ? "Use order" : "Использовать заказ";
            public static string TableFieldUser => Lang == ShareEnums.Lang.English ? "User" : "Пользователь";
            public static string TableFieldValue => Lang == ShareEnums.Lang.English ? "Value" : "Значение";
            public static string TableFieldVatRate => Lang == ShareEnums.Lang.English ? "VAT rate" : "Ставка НДС";
            public static string TableFieldVersion => Lang == ShareEnums.Lang.English ? "Version" : "Версия";
            public static string TableFieldWeighted => Lang == ShareEnums.Lang.English ? "Weighted" : "Весовая";
            public static string TableFieldWeithingDate => Lang == ShareEnums.Lang.English ? "Weighing date" : "Дата взвешивания";
            public static string TableFieldWorkShopId => Lang == ShareEnums.Lang.English ? "Workshop ID" : "ID цеха";
            public static string TableFieldWorkShopName => Lang == ShareEnums.Lang.English ? "Workshop" : "Цех";
            public static string TableFieldXml => "XML";
            public static string TableFieldZpl => "ZPL";
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

        public static class ScalesUI
        {
            public static List<string> ListLanguages => Lang == ShareEnums.Lang.English ? new List<string> { "Russian", "English" } : new List<string> { "Russian", "English" };
            public static List<string> ListResolutions => Lang == ShareEnums.Lang.English ? new List<string> { "800x600", "1024x768", "1366x768", "1600x1024", "1920x1080", "Maximum" } : new List<string> { "800x600", "1024x768", "1366x768", "1600x1024", "1920x1080", "Максимальное" };
            public static readonly decimal MassaThreshold = 0.05M;
            public static string ButtonAddKneading => Lang == ShareEnums.Lang.English ? "Kneading" : "Замес";
            public static string ButtonNewPallet => Lang == ShareEnums.Lang.English ? "New pallet" : "Новая палета";
            public static string ButtonPrint => Lang == ShareEnums.Lang.English ? "Print" : "Печать";
            public static string ButtonRunScalesTerminal => Lang == ShareEnums.Lang.English ? "Scales Terminal" : "Весовой терминал";
            public static string ButtonScalesInit => Lang == ShareEnums.Lang.English ? $"Initialize the scales" : $"Инициализировать весы";
            public static string ButtonSelectOrder => Lang == ShareEnums.Lang.English ? $"Order" : $"Заказ";
            public static string ButtonSelectPlu => Lang == ShareEnums.Lang.English ? $"Select{Environment.NewLine}PLU" : $"Выбрать{Environment.NewLine}PLU";
            public static string ButtonSetKneading => Lang == ShareEnums.Lang.English ? "More" : "Ещё";
            public static string ButtonSettings => Lang == ShareEnums.Lang.English ? "Settings" : "Настройки";
            public static string CheckPluWeightCount => Lang == ShareEnums.Lang.English ? "Weighted products can be specified in quantities of 1 piece." : "Весовая продукция может быть указана в количестве 1 штуки.";
            public static string CheckWeightBefore(decimal currentWeight) => Lang == ShareEnums.Lang.English ? "Unload the weight platform!" + Environment.NewLine + Environment.NewLine + $"Threshold value: {MassaThreshold:0.000} {UnitKg}." + Environment.NewLine + $"Current gross value: {currentWeight:0.000} {UnitKg}." : "Разгрузите весовую платформу!" + Environment.NewLine + Environment.NewLine + $"Пороговое значение: {MassaThreshold:0.000} {UnitKg}." + Environment.NewLine + $"Текущее значение брутто: {currentWeight:0.000} {UnitKg}.";
            public static string CheckWeightIsEmpty() => Lang == ShareEnums.Lang.English ? "For products by weight, put the product on the scale!" + Environment.NewLine + $"Label printing is not possible!" : "Для весовой продукции следует положить продукт на весы!" + Environment.NewLine + $"Печать этикетки невозможна!";
            public static string CheckWeightThreshold(decimal weightNet) => Lang == ShareEnums.Lang.English ? WeightingControl + Environment.NewLine + $"Product weight: {weightNet:0.000} {UnitKg}" + Environment.NewLine + $"Label printing is not possible!" : WeightingControl + Environment.NewLine + $"Вес продукта: {weightNet:0.000} {UnitKg}" + Environment.NewLine + $"Печать этикетки невозможна!";
            public static string CheckWeightThresholds(decimal currentNet, decimal upperWeightThreshold, decimal lowerWeightThreshold) => Lang == ShareEnums.Lang.English ? WeightingControl + Environment.NewLine + $"Net weight: {currentNet:0.000} {UnitKg}" + Environment.NewLine + $"Upper weight value: {upperWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Lower weight value: {lowerWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Label printing is not possible!" : WeightingControl + Environment.NewLine + $"Вес нетто: {currentNet:0.000} {UnitKg}" + Environment.NewLine + $"Верхнее значение веса: {upperWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Нижнее значение веса: {lowerWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Печать этикетки невозможна!";
            public static string ChoosePlu => Lang == ShareEnums.Lang.English ? "First, you have to choose a PLU!" : "Сперва, необходимо выбрать PLU!";
            public static string ComPortState => Lang == ShareEnums.Lang.English ? "COM-port status" : "Состояние COM-порта";
            public static string Crc => "CRC";
            public static string DeviceControlIsPreview => Lang == ShareEnums.Lang.English ? "Open a preview-version of device management?" : "Открыть превью-версию управления устройствами?";
            public static string Exception => Lang == ShareEnums.Lang.English ? "Exception" : "Ошибка";
            public static string ExceptionSqlDb => Lang == ShareEnums.Lang.English ? "The database is unavailable!" : "База данных недоступна!";
            public static string FieldCurrentTime => Lang == ShareEnums.Lang.English ? "Now" : "Сейчас";
            public static string FieldKneading => Lang == ShareEnums.Lang.English ? "Kneading" : "Замес";
            public static string FieldProductDate => Lang == ShareEnums.Lang.English ? "Date of production" : "Дата производства";
            public static string FieldSscc => Lang == ShareEnums.Lang.English ? "Serialized Shipping Container Code" : "Код транспортной упаковки";
            public static string FieldSsccControlNumber => Lang == ShareEnums.Lang.English ? "Control number" : "Контрольное число";
            public static string FieldSsccGln => Lang == ShareEnums.Lang.English ? "GLN" : "Код GLN";
            public static string FieldSsccShort => Lang == ShareEnums.Lang.English ? "SSCC" : "Код ТУ";
            public static string FieldSsccSynonym=> Lang == ShareEnums.Lang.English ? "Synonym" : "Синоним";
            public static string FieldSsccUnitId => Lang == ShareEnums.Lang.English ? "Unit ID" : "ИД юнита";
            public static string FieldSsccUnitType => Lang == ShareEnums.Lang.English ? "Unit type" : "Тип юнита";
            public static string FieldWeightNetto => Lang == ShareEnums.Lang.English ? "Net weight" : "Вес нетто";
            public static string FieldWeightTare => Lang == ShareEnums.Lang.English ? "Tare weight" : "Вес тары";
            public static string IsNotLoaded => Lang == ShareEnums.Lang.English ? "The program is not yet loaded!" + Environment.NewLine + "Wait for it..." : "Программа ещё не загружена!" + Environment.NewLine + "Подождите...";
            public static string Labels => Lang == ShareEnums.Lang.English ? "Labels" : "Этикетки";
            public static string Line => Lang == ShareEnums.Lang.English ? "Ling" : "Строка";
            public static string MassaNotFound => Lang == ShareEnums.Lang.English ? "The device of the scales has not been found!" : "Устройство весов не обнаружено!";
            public static string MassaNotQuering => Lang == ShareEnums.Lang.English ? "Massa-K scales are not respond!" : "Весы Масса-К не отвечают!";
            public static string Memory => Lang == ShareEnums.Lang.English ? "Memory" : "Память";
            public static string MemoryFree => Lang == ShareEnums.Lang.English ? "free" : "свободно";
            public static string MemoryPhysical => Lang == ShareEnums.Lang.English ? "Physical memory" : "Физическая память";
            public static string MemoryVirtual => Lang == ShareEnums.Lang.English ? "Virtual memory" : "Виртуальная память";
            public static string Method => Lang == ShareEnums.Lang.English ? "Method" : "Метод";
            public static string OperationControl => Lang == ShareEnums.Lang.English ? "Control of operations" : "Контроль операций";
            public static string Plu => Lang == ShareEnums.Lang.English ? "PLU" : "ПЛУ";
            public static string PluCount => Lang == ShareEnums.Lang.English ? "PLU (count)" : "ПЛУ (шт)";
            public static string PluIsEmpty => Lang == ShareEnums.Lang.English ? "PLU not selected!" : "Не выбрана PLU!";
            public static string PluWeight => Lang == ShareEnums.Lang.English ? "PLU (weight)" : "ПЛУ (вес)";
            public static string Printer => Lang == ShareEnums.Lang.English ? "Printer" : "Принтер";
            public static string PrinterAvailable => Lang == ShareEnums.Lang.English ? "available" : "доступен";
            public static string PrinterDriver => Lang == ShareEnums.Lang.English ? "Driver" : "Драйвер";
            public static string PrinterInfoCaption => Lang == ShareEnums.Lang.English ? "Printer info" : "Информация о принтере";
            public static string PrinterPort => Lang == ShareEnums.Lang.English ? "Port" : "Порт";
            public static string PrinterState => Lang == ShareEnums.Lang.English ? "State" : "Состояние";
            public static string PrinterStateCode => Lang == ShareEnums.Lang.English ? "State code" : "Код состояния";
            public static string PrinterStatus => Lang == ShareEnums.Lang.English ? "Status" : "Статус";
            public static string PrinterStatusCode => Lang == ShareEnums.Lang.English ? "Status code" : "Код статуса";
            public static string PrinterTsc => Lang == ShareEnums.Lang.English ? "Printer TSC" : "Принтер TSC";
            public static string PrinterUnavailable => Lang == ShareEnums.Lang.English ? "unavailable" : "не доступен";
            public static string PrinterWarningOpenCover => Lang == ShareEnums.Lang.English ? "Open the cover of the separator before proceeding with the calibration!" : "Прежде чем продолжить калибровку, откройте крышку отделителя!";
            public static string PrinterZebra => Lang == ShareEnums.Lang.English ? "Printer Zebra" : "Принтер Zebra";
            public static string ProgramExit => Lang == ShareEnums.Lang.English ? "Ending the program ..." : "Завершение программы ...";
            public static string ProgramLoad => Lang == ShareEnums.Lang.English ? "Loading the program ..." : "Загрузка программы ...";
            public static string ProgramNotFound(string fileName) => Lang == ShareEnums.Lang.English ? "Program not found!" + Environment.NewLine + fileName + Environment.NewLine + "Contact your system administrator." : "Программа не найдена!" + Environment.NewLine + fileName + Environment.NewLine + "Обратитесь к системному администратору.";
            public static string QuestionRunApp => Lang == ShareEnums.Lang.English ? "Run the app" : "Запустить приложение";
            public static string Registration => Lang == ShareEnums.Lang.English ? "Device registration" : "Регистрация устройства";
            public static string RegistrationWarning1(Guid uid) => Lang == ShareEnums.Lang.English ? "The monoblock is registered in the information system with the identifier" + Environment.NewLine + $"{uid}" + Environment.NewLine + "Before restarting, map it to the current line in DeviceControl." : "Моноблок зарегистрирован в информационной системе с идентификатором" + Environment.NewLine + $"{uid}" + Environment.NewLine + "Перед повторным запуском сопоставьте его с текущей линией в приложении DeviceControl.";
            public static string RegistrationWarning2(Guid uid) => Lang == ShareEnums.Lang.English ? $"The monoblock is registered in the information system with the identifier {uid}" + Environment.NewLine + "Before restarting it, map it to the current line in DeviceControl" : $"Моноблок зарегистрирован в информационной системе с идентификатором {uid}" + Environment.NewLine + "Перед повторным запуском сопоставьте его с текущей линией в приложении DeviceControl.";
            public static string RequestParameters => Lang == ShareEnums.Lang.English ? "Request parameters" : "Запрос параметров";
            public static string ScaleQueue => Lang == ShareEnums.Lang.English ? "Scales message queue" : "Очередь сообщений весов";
            public static string StateCorrect => Lang == ShareEnums.Lang.English ? "correct" : "верна";
            public static string StateError => Lang == ShareEnums.Lang.English ? "error" : "ошибка";
            public static string StateNotResponsed => Lang == ShareEnums.Lang.English ? "not responsed" : "не отвечает";
            public static string StateResponsed => Lang == ShareEnums.Lang.English ? "responsed" : "отвечает";
            public static string ThreadId => "ID";
            public static string ThreadIsBackground => Lang == ShareEnums.Lang.English ? "Is background" : "Фоновый";
            public static string ThreadName => Lang == ShareEnums.Lang.English ? "Name" : "Имя";
            public static string ThreadPriorityLevel => Lang == ShareEnums.Lang.English ? "Priority level" : "Приоритет";
            public static string Threads => Lang == ShareEnums.Lang.English ? "Threads" : "Потоки";
            public static string ThreadsCount => Lang == ShareEnums.Lang.English ? "Threads count" : "Количество потоков";
            public static string ThreadStartTime => Lang == ShareEnums.Lang.English ? "Start time" : "Время запуска";
            public static string ThreadState => Lang == ShareEnums.Lang.English ? "State" : "Состояние";
            public static string UnitKg => Lang == ShareEnums.Lang.English ? "kg" : "кг";
            public static string UnitPcs => Lang == ShareEnums.Lang.English ? "pcs." : "шт.";
            public static string UnitWeight => Lang == ShareEnums.Lang.English ? "weight" : "вес";
            public static string WeightingControl => Lang == ShareEnums.Lang.English ? "The weight is out of bounds!" : "Вес выходит за границы!";
            public static string WeightingMessage => Lang == ShareEnums.Lang.English ? "Weighting message" : "Сообщение взвешивания";
            public static string WeightingProcess => Lang == ShareEnums.Lang.English ? "Weighing | Gross weight" : "Взвешивание | Вес брутто";
            public static string WeightingScaleCmd => Lang == ShareEnums.Lang.English ? "Command for scales" : "Команда для весов";
            public static string WeightingStable => Lang == ShareEnums.Lang.English ? "Scales are stable | Gross weight" : "Весы стабильны | Вес брутто";
        }

        public static class Buttons
        {
            public static string Abort => Lang == ShareEnums.Lang.English ? "Abort" : "Прервать";
            public static string Cancel => Lang == ShareEnums.Lang.English ? "Cancel" : "Отмена";
            public static string Ignore => Lang == ShareEnums.Lang.English ? "Ignore" : "Игнорировать";
            public static string No => Lang == ShareEnums.Lang.English ? "No" : "Нет";
            public static string Ok => Lang == ShareEnums.Lang.English ? "Ok" : "Ок";
            public static string Retry => Lang == ShareEnums.Lang.English ? "Retry" : "Повторить";
            public static string Yes => Lang == ShareEnums.Lang.English ? "Yes" : "Да";
        }

        public static class Methods
        {
            #region Public and private methods

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
                        case ProjectsEnums.TableScale.OrdersStatuses:
                            result = DeviceControl.ItemOrderStatus;
                            break;
                        case ProjectsEnums.TableScale.OrdersTypes:
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
                        case ProjectsEnums.TableScale.PrintersResources:
                            result = DeviceControl.ItemPrinterResource;
                            break;
                        case ProjectsEnums.TableScale.PrintersTypes:
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
                        case ProjectsEnums.TableScale.TemplatesResources:
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
                        case ProjectsEnums.TableScale.Default:
                            break;
                        case ProjectsEnums.TableScale.Organizations:
                            result = DeviceControl.ItemOrganization;
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
                            result = DeviceControl.SectionBarCodes;
                            break;
                        case ProjectsEnums.TableScale.BarCodeTypes:
                            result = DeviceControl.SectionBarCodeTypes;
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
                        case ProjectsEnums.TableScale.OrdersStatuses:
                            result = DeviceControl.SectionOrderStatuses;
                            break;
                        case ProjectsEnums.TableScale.OrdersTypes:
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
                        case ProjectsEnums.TableScale.PrintersResources:
                            result = DeviceControl.SectionPrinterResources;
                            break;
                        case ProjectsEnums.TableScale.PrintersTypes:
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
                        case ProjectsEnums.TableScale.TemplatesResources:
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
                        case ProjectsEnums.TableScale.Organizations:
                            result = DeviceControl.SectionOrganizations;
                            break;
                    }
                }

                return result;
            }

            #endregion
        }
    }
}
