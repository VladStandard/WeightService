// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;

namespace DataCore.Localizations
{
    public class LocaleTable
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LocaleTable _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LocaleTable Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        #region Public and private fields and properties

        public string AccessLevel => Lang == ShareEnums.Lang.English ? "Access level" : "Уровень доступа";
        public string Active => Lang == ShareEnums.Lang.English ? "Active" : "Активно";
        public string App => Lang == ShareEnums.Lang.English ? "Program" : "Программа";
        public string BarcodeType => Lang == ShareEnums.Lang.English ? "Barcode type" : "Тип штрихкода";
        public string BarCodeTypeId => Lang == ShareEnums.Lang.English ? "___" : "ID типа ШК";
        public string Brand => Lang == ShareEnums.Lang.English ? "Brand" : "Бренд";
        public string CategoryId => Lang == ShareEnums.Lang.English ? "___" : "ID категории";
        public string CategoryName => Lang == ShareEnums.Lang.English ? "Category" : "Категория";
        public string ChangeDt => Lang == ShareEnums.Lang.English ? "Edit date" : "Дата редактирования";
        public string CheckGtin => Lang == ShareEnums.Lang.English ? "GTIN" : "ГТИН";
        public string CheckWeight => Lang == ShareEnums.Lang.English ? "___" : "Весовая продукция";
        public string Code => Lang == ShareEnums.Lang.English ? "___" : "Код";
        public string Comment => Lang == ShareEnums.Lang.English ? "___" : "Комментарий";
        public string ConsumerName => Lang == ShareEnums.Lang.English ? "___" : "ConsumerName";
        public string Contragent => Lang == ShareEnums.Lang.English ? "Contragent" : "Контрагент";
        public string ContragentId => Lang == ShareEnums.Lang.English ? "___" : "ID контрагента";
        public string Count => Lang == ShareEnums.Lang.English ? "___" : "Количество";
        public string CreateDt => Lang == ShareEnums.Lang.English ? "___" : "Дата создания";
        public string Date => Lang == ShareEnums.Lang.English ? "Date" : "Дата";
        public string DayOfWeek => Lang == ShareEnums.Lang.English ? "___" : "День недели";
        public string Description => Lang == ShareEnums.Lang.English ? "___" : "Описание";
        public string Device => Lang == ShareEnums.Lang.English ? "Device" : "Устройство";
        public string DeviceComPort => Lang == ShareEnums.Lang.English ? "COM-port" : "COM-порт";
        public string DeviceIp => Lang == ShareEnums.Lang.English ? "IP-address" : "IP-адрес";
        public string DeviceMac => Lang == ShareEnums.Lang.English ? "MAC-address" : "MAC-адрес";
        public string DeviceNumber => Lang == ShareEnums.Lang.English ? "Device number" : "Номер устройства";
        public string DevicePort => Lang == ShareEnums.Lang.English ? "Device port" : "Порт устройства";
        public string DeviceReceiveTimeout => Lang == ShareEnums.Lang.English ? "Receive timeout" : "Таймаут приёма";
        public string DeviceSendTimeout => Lang == ShareEnums.Lang.English ? "Send timeout" : "Таймаут отправки";
        public string Ean13 => "EAN13";
        public string Enabled => Lang == ShareEnums.Lang.English ? "Enabled" : "Активно";
        public string Exception => Lang == ShareEnums.Lang.English ? "Exception" : "Исключение";
        public string FieldCategory => Lang == ShareEnums.Lang.English ? "Category" : "Категория";
        public string FieldCount => Lang == ShareEnums.Lang.English ? "Count" : "Количество";
        public string FieldCreated => Lang == ShareEnums.Lang.English ? "Created" : "Создано";
        public string FieldDescription => Lang == ShareEnums.Lang.English ? "Description" : "Описание";
        public string FieldIdRRef => Lang == ShareEnums.Lang.English ? "ID 1C" : "ID 1С";
        public string FieldIpAddress => Lang == ShareEnums.Lang.English ? "Ip-address" : "IP-адрес";
        public string FieldIsEmpty => Lang == ShareEnums.Lang.English ? "Empty field" : "Пустое поле";
        public string FieldIsNotInRange => Lang == ShareEnums.Lang.English ? "Field is not in the range" : "Поле не находится в диапазоне";
        public string FieldLabel => Lang == ShareEnums.Lang.English ? "Label" : "Этикетка";
        public string FieldModified => Lang == ShareEnums.Lang.English ? "Modified" : "Изменено";
        public string FieldNull => Lang == ShareEnums.Lang.English ? "Null" : "Пусто";
        public string FieldName => Lang == ShareEnums.Lang.English ? "Name" : "Наименование";
        public string FieldTitle => Lang == ShareEnums.Lang.English ? "Title" : "Заголовок";
        public string FieldUser => Lang == ShareEnums.Lang.English ? "User" : "Пользователь";
        public string File => Lang == ShareEnums.Lang.English ? "File" : "Файл";
        public string FilePath => Lang == ShareEnums.Lang.English ? "File path" : "Путь к файлу";
        public string FullName => Lang == ShareEnums.Lang.English ? "Full name" : "Полное наименование";
        public string Gln => "GLN";
        public string GoodsBoxQuantly => Lang == ShareEnums.Lang.English ? "Investments in the box" : "Вложений в короб";
        public string GoodsBruttoWeight => Lang == ShareEnums.Lang.English ? "Gross weight" : "Вес брутто";
        public string GoodsDescription => Lang == ShareEnums.Lang.English ? "Good description" : "Описание товара";
        public string GoodsFullName => Lang == ShareEnums.Lang.English ? "Full name" : "Полное наименование";
        public string GoodsName => Lang == ShareEnums.Lang.English ? "Product" : "Товар";
        public string GoodsTareWeight => Lang == ShareEnums.Lang.English ? "Tare weight" : "Вес тары";
        public string GoodsTareWeightDescription => Lang == ShareEnums.Lang.English ? "Calculation formula: Tare weight = weight of box + (weight of package * number of inserts)" : "Расчётная формула: Вес тары = вес коробки + (вес пакета * кол. вложений)";
        public string Gtin => Lang == ShareEnums.Lang.English ? "GTIN" : "ГТИН";
        public string GuidMercury => Lang == ShareEnums.Lang.English ? "GUID Mercury" : "ГУИД Меркурий";
        public string Host => Lang == ShareEnums.Lang.English ? "Host" : "Хост";
        public string HttpStatusCode => Lang == ShareEnums.Lang.English ? "Status" : "Статус";
        public string HttpStatusException => Lang == ShareEnums.Lang.English ? "Exception" : "Ошибка";
        public string HttpStatusNoException => Lang == ShareEnums.Lang.English ? "No exceptions" : "Ошибок нет";
        public string Icon => Lang == ShareEnums.Lang.English ? "Icon" : "Иконка";
        public string Id => Lang == ShareEnums.Lang.English ? "ID" : "ИД";
        public string IdDwh => Lang == ShareEnums.Lang.English ? "ID DWH" : "ИД ДВХ";
        public string IdRRef => Lang == ShareEnums.Lang.English ? "ID 1C" : "ИД 1С";
        public string ImageData => Lang == ShareEnums.Lang.English ? "Image data" : "Данные";
        public string ImageDataInfo => Lang == ShareEnums.Lang.English ? "Info" : "Информация";
        public string InnerException => Lang == ShareEnums.Lang.English ? "Inner exception" : "Вложенное исключение";
        public string IsClose => Lang == ShareEnums.Lang.English ? "Is close" : "Закрыто";
        public string IsShipping => Lang == ShareEnums.Lang.English ? "Shipping labels" : "Транспортные этикетки";
        public string IsShippingLength => Lang == ShareEnums.Lang.English ? "Count of labels in a box" : "Количество этикеток в коробе";
        public string IsShippingShort => Lang == ShareEnums.Lang.English ? "Shipping" : "Трансп.";
        public string Itf14 => "ITF14";
        public string Kneding => Lang == ShareEnums.Lang.English ? "Kneading" : "Замес";
        public string LabelTemplate => Lang == ShareEnums.Lang.English ? "Label template" : "Шаблон этикетки";
        public string Level => Lang == ShareEnums.Lang.English ? "Level" : "Уровень";
        public string Line => Lang == ShareEnums.Lang.English ? "Line" : "Линия";
        public string Link => Lang == ShareEnums.Lang.English ? "Link" : "Ссылка";
        public string LogType => Lang == ShareEnums.Lang.English ? "Log type" : "Тип лога";
        public string LowerWeightThreshold => Lang == ShareEnums.Lang.English ? "Lower value of the box weight" : "Нижнее значение веса короба";
        public string Marked => Lang == ShareEnums.Lang.English ? "In the archive" : "В архиве";
        public string MarkedShort => Lang == ShareEnums.Lang.English ? "x" : "х";
        public string Member => Lang == ShareEnums.Lang.English ? "Method" : "Метод";
        public string Message => Lang == ShareEnums.Lang.English ? "Message" : "Сообщение";
        public string Name => Lang == ShareEnums.Lang.English ? "Name" : "Наименование";
        public string NamePretty => Lang == ShareEnums.Lang.English ? "Pretty name" : "Красивое наименование";
        public string NameFull => Lang == ShareEnums.Lang.English ? "Full name" : "Полное наименование";
        public string NetWeight => Lang == ShareEnums.Lang.English ? "Net weight" : "Вес нетто";
        public string Nomenclature => Lang == ShareEnums.Lang.English ? "Nomenclature" : "Номенклатура";
        public string NomenclatureId => Lang == ShareEnums.Lang.English ? "Nomenclature ID" : "ID номенклатуры";
        public string NomenclatureName => Lang == ShareEnums.Lang.English ? "Nomenclature" : "Номенклатура";
        public string NomenclatureType => Lang == ShareEnums.Lang.English ? "Type of nomenclature" : "Тип номенклатуры";
        public string NomenclatureUnitId => Lang == ShareEnums.Lang.English ? "Nomenclature unit ID" : "ID юнита номенклатуры";
        public string NominalWeight => Lang == ShareEnums.Lang.English ? "Nominal box weight" : "Номинальный вес короба";
        public string Order => Lang == ShareEnums.Lang.English ? "Order" : "Заказ";
        public string PackQuantly => Lang == ShareEnums.Lang.English ? "Pack quantly" : "Быстрота упаковки";
        public string PackTypeId => Lang == ShareEnums.Lang.English ? "Package type ID" : "ID типа упаковки";
        public string PackWeight => Lang == ShareEnums.Lang.English ? "Package weight" : "Вес упаковки";
        public string Plu => Lang == ShareEnums.Lang.English ? "PLU" : "ПЛУ";
        public string PluId => Lang == ShareEnums.Lang.English ? "ID PLU" : "ИД ПЛУ";
        public string PluDescription => Lang == ShareEnums.Lang.English ? "Use the `|` symbol to move the line." : "Для переноса строки используйте символ `|`";
        public string PluNumber => Lang == ShareEnums.Lang.English ? "# PLU" : "№ ПЛУ";
        public string Printer => Lang == ShareEnums.Lang.English ? "Printer" : "Принтер";
        public string PrinterResource => Lang == ShareEnums.Lang.English ? "Printer resource" : "Ресурс принтера";
        public string PrinterType => Lang == ShareEnums.Lang.English ? "Printer type" : "Тип принтера";
        public string Product => Lang == ShareEnums.Lang.English ? "Product" : "Продукт";
        public string ProductDate => Lang == ShareEnums.Lang.English ? "Product date" : "Дата продукции";
        public string ProductionFacility => Lang == ShareEnums.Lang.English ? "Production facility" : "Производственная площадка";
        public string ProductionFacilityName => Lang == ShareEnums.Lang.English ? "Production facility" : "Производственная площадка";
        public string RegNum => Lang == ShareEnums.Lang.English ? "#" : "№";
        public string Resource => Lang == ShareEnums.Lang.English ? "Resource" : "Ресурс";
        public string Row => Lang == ShareEnums.Lang.English ? "Row" : "Строка";
        public string Scale => Lang == ShareEnums.Lang.English ? "Device" : "Устройство";
        public string ScaleFactor => Lang == ShareEnums.Lang.English ? "Scale factor" : "Коэф. масштабирования";
        public string ScaleId => Lang == ShareEnums.Lang.English ? "Scale ID" : "ID весов";
        public string ShelfLifeDays => Lang == ShareEnums.Lang.English ? "Shelf life (days)" : "Срок годности (суток)";
        public string Sscc => Lang == ShareEnums.Lang.English ? "Transport packing code (SSCC)" : "Код транспортной упаковки (SSCC)";
        public string State => Lang == ShareEnums.Lang.English ? "Status" : "Статус";
        public string Storage => Lang == ShareEnums.Lang.English ? "Storage" : "Склад";
        public string TableCancel => Lang == ShareEnums.Lang.English ? "Close record" : "Закрыть запись";
        public string TableClear => Lang == ShareEnums.Lang.English ? "Deactivate active record" : "Деактивировать активную запись";
        public string TableCreate => Lang == ShareEnums.Lang.English ? "Create record" : "Создать запись";
        public string TableDelete => Lang == ShareEnums.Lang.English ? "Delete record" : "Удалить запись";
        public string TableEdit => Lang == ShareEnums.Lang.English ? "Edit record" : "Редактировать запись";
        public string TableIncludes => Lang == ShareEnums.Lang.English ? "Included records" : "Вложенные записи";
        public string TablePluHavingPlu => Lang == ShareEnums.Lang.English ? "The PLU table already has this number" : "Таблица PLU уже имеет такой номер";
        public string TableRead => Lang == ShareEnums.Lang.English ? "Read data" : "Прочитать данные";
        public string TableReadCancel => Lang == ShareEnums.Lang.English ? "Cancel data reading" : "Отмена чтения данных";
        public string TableSave => Lang == ShareEnums.Lang.English ? "Save record" : "Сохранить запись";
        public string TableSelect => Lang == ShareEnums.Lang.English ? "Highlight record" : "Выделить запись";
        public string TableTab => Lang == ShareEnums.Lang.English ? "Switch between panels" : "Переключиться между панелями";
        public string TareWeight => Lang == ShareEnums.Lang.English ? "Tare weight" : "Вес тары";
        public string TaskModule => Lang == ShareEnums.Lang.English ? "Task module" : "Модуль задачи";
        public string TaskModuleType => Lang == ShareEnums.Lang.English ? "Task module type" : "Тип модуля задачи";
        public string TaskType => Lang == ShareEnums.Lang.English ? "Task type" : "Тип задачи";
        public string Template => Lang == ShareEnums.Lang.English ? "Template" : "Шаблон";
        public string TemplateLabelary => Lang == ShareEnums.Lang.English ? "Web-site Labelary" : "Веб-сайт Labelary";
        public string TemplateResource => Lang == ShareEnums.Lang.English ? "Template resource" : "Ресурс шаблона";
        public string TemplateDefault => Lang == ShareEnums.Lang.English ? "Default template" : "Шаблон по-умолчанию";
        public string TemplateId => Lang == ShareEnums.Lang.English ? "Template ID" : "ID шаблона";
        public string TemplateIdDefault => Lang == ShareEnums.Lang.English ? "Default template ID" : "ID шаблона по-умолчанию";
        public string TemplateIdSeries => Lang == ShareEnums.Lang.English ? "Series ID" : "ID серии";
        public string TemplateSeries => Lang == ShareEnums.Lang.English ? "Summary label template" : "Шаблон суммарной этикетки";
        public string Title => Lang == ShareEnums.Lang.English ? "Title" : "Заголовок";
        public string Type => Lang == ShareEnums.Lang.English ? "Type" : "Тип";
        public string Uid => Lang == ShareEnums.Lang.English ? "UID" : "УИД";
        public string UpperWeightThreshold => Lang == ShareEnums.Lang.English ? "Upper value of the box weight" : "Верхнее значение веса короба";
        public string UseOrder => Lang == ShareEnums.Lang.English ? "Use order" : "Использовать заказ";
        public string User => Lang == ShareEnums.Lang.English ? "User" : "Пользователь";
        public string Value => Lang == ShareEnums.Lang.English ? "Value" : "Значение";
        public string VatRate => Lang == ShareEnums.Lang.English ? "VAT rate" : "Ставка НДС";
        public string Version => Lang == ShareEnums.Lang.English ? "Version" : "Версия";
        public string Weighted => Lang == ShareEnums.Lang.English ? "Weighted" : "Весовая";
        public string WeithingDate => Lang == ShareEnums.Lang.English ? "Weighing date" : "Дата взвешивания";
        public string Workshop => Lang == ShareEnums.Lang.English ? "Workshop" : "Цех";
        public string WorkShopId => Lang == ShareEnums.Lang.English ? "Workshop ID" : "ID цеха";
        public string WorkShopName => Lang == ShareEnums.Lang.English ? "Workshop" : "Цех";
        public string Xml => "XML";
        public string Zpl => "ZPL";

        #endregion
    }
}
