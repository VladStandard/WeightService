﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleTable
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleTable _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleTable Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string AccessLevel => Lang == LangEnum.English ? "Access level" : "Уровень доступа";
    public string Active => Lang == LangEnum.English ? "Active" : "Активно";
    public string ActiveShort => Lang == LangEnum.English ? "Act" : "Акт";
    public string Address => Lang == LangEnum.English ? "Address" : "Адрес";
    public string App => Lang == LangEnum.English ? "Program" : "Программа";
    public string Area => Lang == LangEnum.English ? "Area" : "Площадка";
    public string Arm => Lang == LangEnum.English ? "ARM" : "АРМ";
    public string BarcodeType => Lang == LangEnum.English ? "Barcode type" : "Тип штрихкода";
    public string BarCodeTypeId => Lang == LangEnum.English ? "Barcode type ID" : "ИД типа ШК";
    public string Brand => Lang == LangEnum.English ? "Brand" : "Бренд";
    public string CategoryId => Lang == LangEnum.English ? "Category ID" : "ИД категории";
    public string CategoryName => Lang == LangEnum.English ? "Category" : "Категория";
    public string ChangeDt => Lang == LangEnum.English ? "Edit date" : "Дата редактирования";
    public string CheckGtin => Lang == LangEnum.English ? "GTIN" : "ГТИН";
    public string CheckWeight => Lang == LangEnum.English ? "Weighing products" : "Весовая продукция";
    public string Code => Lang == LangEnum.English ? "Code" : "Код";
    public string Comment => Lang == LangEnum.English ? "Comment" : "Комментарий";
    public string Consumer => Lang == LangEnum.English ? "Consumer" : "Потребитель";
    public string Contragent => Lang == LangEnum.English ? "Contragent" : "Контрагент";
    public string ContragentId => Lang == LangEnum.English ? "Contragent ID" : "ИД контрагента";
    public string Count => Lang == LangEnum.English ? "Count" : "Количество";
    public string Counter => Lang == LangEnum.English ? "Counter" : "Счётчик";
    public string CreateDt => Lang == LangEnum.English ? "Creation date" : "Дата создания";
    public string Date => Lang == LangEnum.English ? "Date" : "Дата";
    public string DayOfWeek => Lang == LangEnum.English ? "Weekday" : "День недели";
    public string Deactive => Lang == LangEnum.English ? "Deactivate" : "Деактивировано";
    public string DeactiveShort => Lang == LangEnum.English ? "Deact" : "Деакт";
    public string Description => Lang == LangEnum.English ? "Description" : "Описание";
    public string Device => Lang == LangEnum.English ? "Device" : "Устройство";
    public string DeviceComPort => Lang == LangEnum.English ? "COM-port" : "COM-порт";
    public string DeviceIp => Lang == LangEnum.English ? "IP-address" : "IP-адрес";
    public string DeviceMac => Lang == LangEnum.English ? "MAC-address" : "MAC-адрес";
    public string DeviceNumber => Lang == LangEnum.English ? "Device number" : "Номер устройства";
    public string DevicePort => Lang == LangEnum.English ? "Device port" : "Порт устройства";
    public string DeviceReceiveTimeout => Lang == LangEnum.English ? "Receive timeout" : "Таймаут приёма";
    public string DeviceSendTimeout => Lang == LangEnum.English ? "Send timeout" : "Таймаут отправки";
    public string Ean13 => Lang == LangEnum.English ? "EAN13" : "ШК ЕАН13";
    public string Enabled => Lang == LangEnum.English ? "Enabled" : "Активно";
    public string Exception => Lang == LangEnum.English ? "Exception" : "Исключение";
    public string FieldCategory => Lang == LangEnum.English ? "Category" : "Категория";
    public string FieldCount => Lang == LangEnum.English ? "Count" : "Количество";
    public string FieldCreated => Lang == LangEnum.English ? "Created" : "Создано";
    public string FieldDescription => Lang == LangEnum.English ? "Description" : "Описание";
    public string FieldIdRRef => Lang == LangEnum.English ? "ID 1C" : "ИД 1С";
    public string FieldIpAddress => Lang == LangEnum.English ? "Ip-address" : "ИП-адрес";
    public string FieldIsEmpty => Lang == LangEnum.English ? "Empty field" : "Пустое поле";
    public string FieldIsNotInRange => Lang == LangEnum.English ? "Field is not in the range" : "Поле не находится в диапазоне";
    public string FieldLabel => Lang == LangEnum.English ? "Label" : "Этикетка";
    public string FieldModified => Lang == LangEnum.English ? "Modified" : "Изменено";
    public string FieldName => Lang == LangEnum.English ? "Name" : "Наименование";
    public string FieldNull => Lang == LangEnum.English ? "Null" : "Пусто";
    public string FieldTitle => Lang == LangEnum.English ? "Title" : "Заголовок";
    public string FieldUser => Lang == LangEnum.English ? "User" : "Пользователь";
    public string File => Lang == LangEnum.English ? "File" : "Файл";
    public string FilePath => Lang == LangEnum.English ? "File path" : "Путь к файлу";
    public string FullName => Lang == LangEnum.English ? "Full name" : "Полное наименование";
    public string Gln => "GLN";
    public string GoodsBoxQuantly => Lang == LangEnum.English ? "Investments in the box" : "Вложений в короб";
    public string GoodsBoxQuantlyShort => Lang == LangEnum.English ? "Attach" : "Влож";
    public string GoodsBruttoWeight => Lang == LangEnum.English ? "Gross weight" : "Вес брутто";
    public string GoodsDescription => Lang == LangEnum.English ? "Good description" : "Описание товара";
    public string GoodsName => Lang == LangEnum.English ? "Product" : "Товар";
    public string TareWeight => Lang == LangEnum.English ? "Tare weight, kg" : "Вес тары, кг";
    public string TareWeightDescription => Lang == LangEnum.English ? "Calculation formula: Tare weight = weight of box + (weight of package * number of inserts)" : "Расчётная формула: Вес тары = вес коробки + (вес пакета * кол. вложений)";
    public string TareWeightShort => Lang == LangEnum.English ? "Weight" : "Вес";
    public string Gtin => Lang == LangEnum.English ? "BC GTIN" : "ШК ГТИН";
    public string GuidMercury => Lang == LangEnum.English ? "GUID Mercury" : "ГУИД Меркурий";
    public string Host => Lang == LangEnum.English ? "Host" : "Хост";
    public string HttpStatusCode => Lang == LangEnum.English ? "Status" : "Статус";
    public string HttpStatusException => Lang == LangEnum.English ? "Exception" : "Ошибка";
    public string HttpStatusNoException => Lang == LangEnum.English ? "No exceptions" : "Ошибок нет";
    public string Icon => Lang == LangEnum.English ? "Icon" : "Иконка";
    public string Id => Lang == LangEnum.English ? "ID" : "ИД";
    public string IdDwh => Lang == LangEnum.English ? "ID DWH" : "ИД ДВХ";
    public string Identity => Lang == LangEnum.English ? "Identity" : "Идентификатор";
    public string IdentityId => Lang == LangEnum.English ? "ID" : "ИД";
    public string IdentityUid => Lang == LangEnum.English ? "UID" : "УИД";
    public string IdRRef => Lang == LangEnum.English ? "ID 1C" : "ИД 1С";
    public string ImageData => Lang == LangEnum.English ? "Image data" : "Данные";
    public string ImageDataInfo => Lang == LangEnum.English ? "Info" : "Информация";
    public string InnerException => Lang == LangEnum.English ? "Inner exception" : "Вложенное исключение";
    public string IsActive => Lang == LangEnum.English ? "Active" : "Активно";
    public string IsClose => Lang == LangEnum.English ? "Is close" : "Закрыто";
    public string IsKneading => Lang == LangEnum.English ? "Kneading" : "Замес";
    public string IsMarked => Lang == LangEnum.English ? "In the archive" : "В архиве";
    public string IsMarkedShort => Lang == LangEnum.English ? "x" : "х";
    public string IsOrder => Lang == LangEnum.English ? "Use order" : "Использовать заказ";
    public string IsShipping => Lang == LangEnum.English ? "Shipping labels" : "Транспортные этикетки";
    public string IsShippingLength => Lang == LangEnum.English ? "Count of labels in a box" : "Количество этикеток в коробе";
    public string IsShippingShort => Lang == LangEnum.English ? "Shipping" : "Трансп.";
    public string Itf14 => "ITF14";
    public string LabelTemplate => Lang == LangEnum.English ? "Label template" : "Шаблон этикетки";
    public string Level => Lang == LangEnum.English ? "Level" : "Уровень";
    public string Line => Lang == LangEnum.English ? "Line" : "Линия";
    public string Link => Lang == LangEnum.English ? "Link" : "Ссылка";
    public string LogType => Lang == LangEnum.English ? "Log type" : "Тип лога";
    public string LowerWeightThreshold => Lang == LangEnum.English ? "Lower value of the box weight, kg" : "Нижнее значение веса короба, кг";
    public string Member => Lang == LangEnum.English ? "Method" : "Метод";
    public string Message => Lang == LangEnum.English ? "Message" : "Сообщение";
    public string Name => Lang == LangEnum.English ? "Name" : "Наименование";
    public string NameFull => Lang == LangEnum.English ? "Full name" : "Полное наименование";
    public string NamePretty => Lang == LangEnum.English ? "Pretty name" : "Красивое наименование";
    public string NetWeight => Lang == LangEnum.English ? "Net weight" : "Вес нетто";
    public string Nomenclature => Lang == LangEnum.English ? "Nomenclature" : "Номенклатура";
    public string NomenclatureId => Lang == LangEnum.English ? "Nomenclature ID" : "ID номенклатуры";
    public string NomenclatureType => Lang == LangEnum.English ? "Type of nomenclature" : "Тип номенклатуры";
    public string NomenclatureUnitId => Lang == LangEnum.English ? "Nomenclature unit ID" : "ID юнита номенклатуры";
    public string NominalWeight => Lang == LangEnum.English ? "Nominal box weight, kg" : "Номинальный вес короба, кг";
    public string Number => Lang == LangEnum.English ? "Number" : "Номер";
    public string NumberShort => Lang == LangEnum.English ? "#" : "№";
    public string Order => Lang == LangEnum.English ? "Order" : "Заказ";
    public string PackQuantly => Lang == LangEnum.English ? "Pack quantly" : "Быстрота упаковки";
    public string PackTypeId => Lang == LangEnum.English ? "Package type ID" : "ID типа упаковки";
    public string PackWeight => Lang == LangEnum.English ? "Package weight" : "Вес упаковки";
    public string Plu => Lang == LangEnum.English ? "PLU" : "ПЛУ";
    public string PluDescription => Lang == LangEnum.English ? "Use the `|` symbol to move the line." : "Для переноса строки используйте символ `|`";
    public string PluId => Lang == LangEnum.English ? "ID PLU" : "ИД ПЛУ";
    public string PluNumber => Lang == LangEnum.English ? "# PLU" : "№ ПЛУ";
    public string PluScale => Lang == LangEnum.English ? "Device PLU" : "ПЛУ устройства";
    public string Printer => Lang == LangEnum.English ? "Printer" : "Принтер";
    public string PrinterResource => Lang == LangEnum.English ? "Printer resource" : "Ресурс принтера";
    public string PrinterType => Lang == LangEnum.English ? "Printer type" : "Тип принтера";
    public string Product => Lang == LangEnum.English ? "Product" : "Продукт";
    public string ProductDate => Lang == LangEnum.English ? "Product date" : "Дата продукции";
    public string ProductionFacility => Lang == LangEnum.English ? "Production facility" : "Производственная площадка";
    public string ProductionFacilityName => Lang == LangEnum.English ? "Production facility" : "Производственная площадка";
    public string RegNum => Lang == LangEnum.English ? "#" : "№";
    public string ReleaseDt => Lang == LangEnum.English ? "Release date" : "Дата релиза";
    public string Resource => Lang == LangEnum.English ? "Resource" : "Ресурс";
    public string Row => Lang == LangEnum.English ? "Row" : "Строка";
    public string Scale => Lang == LangEnum.English ? "Device" : "Устройство";
    public string ScaleFactor => Lang == LangEnum.English ? "Scale factor" : "Коэф. масштабирования";
    public string ScaleId => Lang == LangEnum.English ? "Scale ID" : "ID весов";
    public string ShelfLifeDays => Lang == LangEnum.English ? "Shelf life, days" : "Срок годности, суток";
    public string ShelfLifeDaysShort => Lang == LangEnum.English ? "Life" : "Срок";
    public string Sscc => Lang == LangEnum.English ? "Transport packing code (SSCC)" : "Код транспортной упаковки (SSCC)";
    public string State => Lang == LangEnum.English ? "Status" : "Статус";
    public string Storage => Lang == LangEnum.English ? "Storage" : "Склад";
    public string TableCalc => Lang == LangEnum.English ? "Calc" : "Расчитать";
    public string TableCancel => Lang == LangEnum.English ? "Close record" : "Закрыть запись";
    public string TableClear => Lang == LangEnum.English ? "Deactivate active record" : "Деактивировать активную запись";
    public string TableCopy => Lang == LangEnum.English ? "Cope record" : "Копировать запись";
    public string TableCreate => Lang == LangEnum.English ? "Create record" : "Создать запись";
    public string TableDelete => Lang == LangEnum.English ? "Delete record" : "Удалить запись";
    public string TableEdit => Lang == LangEnum.English ? "Edit record" : "Редактировать запись";
    public string TableIncludes => Lang == LangEnum.English ? "Included records" : "Вложенные записи";
    public string TableMark => Lang == LangEnum.English ? "Save record" : "Пометить запись на удаление";
    public string TableNew => Lang == LangEnum.English ? "New record" : "Новая запись";
    public string TablePluHavingPlu => Lang == LangEnum.English ? "The PLU table already has this number" : "Таблица PLU уже имеет такой номер";
    public string TableRead => Lang == LangEnum.English ? "Read data" : "Прочитать данные";
    public string TableReadCancel => Lang == LangEnum.English ? "Cancel data reading" : "Отмена чтения данных";
    public string TableRereadFromDb => Lang == LangEnum.English ? "Reread from the database" : "Перечитать из БД";
    public string TableSave => Lang == LangEnum.English ? "Save record" : "Сохранить запись";
    public string TableSelect => Lang == LangEnum.English ? "Highlight record" : "Выделить запись";
    public string TableTab => Lang == LangEnum.English ? "Switch between panels" : "Переключиться между панелями";
    public string TaskModule => Lang == LangEnum.English ? "Task module" : "Модуль задачи";
    public string TaskModuleType => Lang == LangEnum.English ? "Task module type" : "Тип модуля задачи";
    public string TaskType => Lang == LangEnum.English ? "Task type" : "Тип задачи";
    public string Template => Lang == LangEnum.English ? "Template" : "Шаблон";
    public string TemplateDefault => Lang == LangEnum.English ? "Default template" : "Шаблон по-умолчанию";
    public string TemplateId => Lang == LangEnum.English ? "Template ID" : "ID шаблона";
    public string TemplateIdDefault => Lang == LangEnum.English ? "Default template ID" : "ID шаблона по-умолчанию";
    public string TemplateIdSeries => Lang == LangEnum.English ? "Series ID" : "ID серии";
    public string TemplateLabelary => Lang == LangEnum.English ? "Web-site Labelary" : "Веб-сайт Labelary";
    public string TemplateResource => Lang == LangEnum.English ? "Template resource" : "Ресурс шаблона";
    public string TemplateSeries => Lang == LangEnum.English ? "Summary label template" : "Шаблон суммарной этикетки";
    public string Title => Lang == LangEnum.English ? "Title" : "Заголовок";
    public string Type => Lang == LangEnum.English ? "Type" : "Тип";
    public string Uid => Lang == LangEnum.English ? "UID" : "УИД";
    public string UpperWeightThreshold => Lang == LangEnum.English ? "Upper value of the box weight, kg" : "Верхнее значение веса короба, кг";
    public string User => Lang == LangEnum.English ? "User" : "Пользователь";
    public string Value => Lang == LangEnum.English ? "Value" : "Значение";
    public string VatRate => Lang == LangEnum.English ? "VAT rate" : "Ставка НДС";
    public string Version => Lang == LangEnum.English ? "Version" : "Версия";
    public string Weighted => Lang == LangEnum.English ? "Weighted" : "Весовая";
    public string WeightShort => Lang == LangEnum.English ? "Weight" : "Вес";
    public string WeithingDate => Lang == LangEnum.English ? "Weighing date" : "Дата взвешивания";
    public string WorkShop => Lang == LangEnum.English ? "Workshop" : "Цех";
    public string WorkShopId => Lang == LangEnum.English ? "Workshop ID" : "ИД цеха";
    public string WorkShopName => Lang == LangEnum.English ? "Workshop" : "Цех";
    public string Xml => Lang == LangEnum.English ? "XML" : "Поле XML";
    public string XmlPretty => Lang == LangEnum.English ? "Pretty XML" : "Красивый XML";
    public string Zpl => Lang == LangEnum.English ? "ZPL" : "ЗПЛ";

    #endregion
}
