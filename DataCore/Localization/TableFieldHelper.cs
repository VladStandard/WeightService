// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
namespace DataCore.Localization
{
    public class TableFieldHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static TableFieldHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static TableFieldHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;
        public string AccessLevel => Lang == ShareEnums.Lang.English ? "___" : "Уровень доступа";
        public string Active => Lang == ShareEnums.Lang.English ? "___" : "Активно";
        public string App => Lang == ShareEnums.Lang.English ? "___" : "Программа";
        public string BarCodeTypeId => Lang == ShareEnums.Lang.English ? "___" : "ID типа ШК";
        public string Brand => Lang == ShareEnums.Lang.English ? "___" : "Brand";
        public string CategoryId => Lang == ShareEnums.Lang.English ? "___" : "ID категории";
        public string CategoryName => Lang == ShareEnums.Lang.English ? "Category" : "Категория";
        public string ChangeDt => Lang == ShareEnums.Lang.English ? "Edit date" : "Дата редактирования";
        public string CheckGtin => Lang == ShareEnums.Lang.English ? "GTIN" : "ГТИН";
        public string CheckWeight => Lang == ShareEnums.Lang.English ? "___" : "Весовая продукция";
        public string Code => Lang == ShareEnums.Lang.English ? "___" : "Код";
        public string Comment => Lang == ShareEnums.Lang.English ? "___" : "Комментарий";
        public string ConsumerName => Lang == ShareEnums.Lang.English ? "___" : "ConsumerName";
        public string ContragentId => Lang == ShareEnums.Lang.English ? "___" : "ID контрагента";
        public string Count => Lang == ShareEnums.Lang.English ? "___" : "Количество";
        public string CreateDt => Lang == ShareEnums.Lang.English ? "___" : "Дата создания";
        public string Date => Lang == ShareEnums.Lang.English ? "Date" : "Дата";
        public string DayOfWeek => Lang == ShareEnums.Lang.English ? "___" : "День недели";
        public string Description => Lang == ShareEnums.Lang.English ? "___" : "Описание";
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
        public string Itf14 => "ITF14";
        public string Kneding => Lang == ShareEnums.Lang.English ? "Kneading" : "Замес";
        public string Level => Lang == ShareEnums.Lang.English ? "Level" : "Уровень";
        public string Line => Lang == ShareEnums.Lang.English ? "Line" : "Линия";
        public string LogType => Lang == ShareEnums.Lang.English ? "Log type" : "Тип лога";
        public string LowerWeightThreshold => Lang == ShareEnums.Lang.English ? "Lower value of the box weight" : "Нижнее значение веса короба";
        public string Marked => Lang == ShareEnums.Lang.English ? "In the archive" : "В архиве";
        public string MarkedShort => Lang == ShareEnums.Lang.English ? "x" : "х";
        public string Member => Lang == ShareEnums.Lang.English ? "Method" : "Метод";
        public string Message => Lang == ShareEnums.Lang.English ? "Message" : "Сообщение";
        public string Name => Lang == ShareEnums.Lang.English ? "Name" : "Наименование";
        public string NameFull => Lang == ShareEnums.Lang.English ? "Full name" : "Полное наименование";
        public string NetWeight => Lang == ShareEnums.Lang.English ? "Net weight" : "Вес нетто";
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
        public string PluDescription => Lang == ShareEnums.Lang.English ? "Use the `|` symbol to move the line." : "Для переноса строки используйте символ `|`";
        public string PluNumber => Lang == ShareEnums.Lang.English ? "# PLU" : "№ ПЛУ";
        public string ProductDate => Lang == ShareEnums.Lang.English ? "Product date" : "Дата продукции";
        public string ProductionFacilityName => Lang == ShareEnums.Lang.English ? "Production facility" : "Производственная площадка";
        public string RegNum => Lang == ShareEnums.Lang.English ? "#" : "№";
        public string Resource => Lang == ShareEnums.Lang.English ? "Resource" : "Ресурс";
        public string Row => Lang == ShareEnums.Lang.English ? "Row" : "Строка";
        public string Scale => Lang == ShareEnums.Lang.English ? "Device" : "Устройство";
        public string ScaleFactor => Lang == ShareEnums.Lang.English ? "Scale factor" : "Коэф. масштабирования";
        public string ScaleId => Lang == ShareEnums.Lang.English ? "Scale ID" : "ID весов";
        public string SettingsFile => Lang == ShareEnums.Lang.English ? "Settings file" : "Файл настроек";
        public string ShelfLifeDays => Lang == ShareEnums.Lang.English ? "Shelf life (days)" : "Срок годности (суток)";
        public string Sscc => Lang == ShareEnums.Lang.English ? "Transport packing code (SSCC)" : "Код транспортной упаковки (SSCC)";
        public string State => Lang == ShareEnums.Lang.English ? "Status" : "Статус";
        public string Storage => Lang == ShareEnums.Lang.English ? "Storage" : "Склад";
        public string TareWeight => Lang == ShareEnums.Lang.English ? "Tare weight" : "Вес тары";
        public string TaskType => Lang == ShareEnums.Lang.English ? "Task type" : "Тип задачи";
        public string Template => Lang == ShareEnums.Lang.English ? "Template" : "Шаблон";
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
        public string WorkShopId => Lang == ShareEnums.Lang.English ? "Workshop ID" : "ID цеха";
        public string WorkShopName => Lang == ShareEnums.Lang.English ? "Workshop" : "Цех";
        public string Xml => "XML";
        public string Zpl => "ZPL";
        
        #endregion
    }
}
