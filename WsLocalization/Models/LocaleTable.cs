﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalization.Models;

public class LocaleTable
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleTable _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleTable Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

    #region Public and private fields, properties, constructor

    public string AccessLevel => Lang == Lang.English ? "Access level" : "Уровень доступа";
    public string Active => Lang == Lang.English ? "Active" : "Активно";
    public string ActiveShort => Lang == Lang.English ? "Act" : "Акт";
    public string Address => Lang == Lang.English ? "Address" : "Адрес";
    public string App => Lang == Lang.English ? "Program" : "Программа";
    public string Area => Lang == Lang.English ? "Area" : "Площадка";
    public string Arm => Lang == Lang.English ? "ARM" : "АРМ";
    public string BarcodeType => Lang == Lang.English ? "Barcode type" : "Тип штрихкода";
    public string BarCodeTypeId => Lang == Lang.English ? "Barcode type ID" : "ИД типа ШК";
    public string Box => Lang == Lang.English ? "Box" : "Коробка";
    public string BoxWeight => Lang == Lang.English ? "Box weight" : "Вес коробки";
    public string Brand => Lang == Lang.English ? "Brand" : "Бренд";
    public string Bundle => Lang == Lang.English ? "Bundle" : "Пакет";
    public string BundleCount => Lang == Lang.English ? "Bundle count" : "Кол-во пакетов";
    public string BundleFkWeightTare => Lang == Lang.English ? "Bundle tare weight" : "Вес тары упаковки";
    public string BundleFkWeightTareDescription => Lang == Lang.English ? "Tare weight = weight of package * count of inserts + weight of box" : "Вес тары = вес пакета * кол. вложений + вес коробки";
    public string BundleFkWeightTareKg => Lang == Lang.English ? "Tare weight, kg" : "Вес тары, кг";
    public string BundleFkWeightTareShort => Lang == Lang.English ? "Weight" : "Вес";
    public string BundleWeight => Lang == Lang.English ? "Bundle weight" : "Вес пакета";
    public string CategoryId => Lang == Lang.English ? "Category ID" : "ИД категории";
    public string CategoryName => Lang == Lang.English ? "Category" : "Категория";
    public string ChangeDt => Lang == Lang.English ? "Edited" : "Изменено";
    public string CheckGtin => Lang == Lang.English ? "GTIN" : "ГТИН";
    public string CheckWeight => Lang == Lang.English ? "Weighing products" : "Весовая продукция";
    public string Code => Lang == Lang.English ? "Code" : "Код";
    public string Comment => Lang == Lang.English ? "Comment" : "Комментарий";
    public string Consumer => Lang == Lang.English ? "Consumer" : "Потребитель";
    public string Contragent => Lang == Lang.English ? "Contragent" : "Контрагент";
    public string ContragentId => Lang == Lang.English ? "Contragent ID" : "ИД контрагента";
    public string Count => Lang == Lang.English ? "Count" : "Кол-во";
    public string Counter => Lang == Lang.English ? "Counter" : "Счётчик";
    public string CreateDt => Lang == Lang.English ? "Created" : "Создано";
    public string Date => Lang == Lang.English ? "Date" : "Дата";
    public string DayOfWeek => Lang == Lang.English ? "Weekday" : "День недели";
    public string Deactive => Lang == Lang.English ? "Deactivate" : "Деактивировано";
    public string DeactiveShort => Lang == Lang.English ? "Deact" : "Деакт";
    public string Description => Lang == Lang.English ? "Description" : "Описание";
    public string Device => Lang == Lang.English ? "Device" : "Устройство";
    public string DeviceComPort => Lang == Lang.English ? "COM-port" : "COM-порт";
    public string DeviceIp => Lang == Lang.English ? "IP-address" : "IP-адрес";
    public string DeviceMac => Lang == Lang.English ? "MAC-address" : "MAC-адрес";
    public string DeviceNumber => Lang == Lang.English ? "Device number" : "Номер устройства";
    public string DevicePort => Lang == Lang.English ? "Device port" : "Порт устройства";
    public string DeviceReceiveTimeout => Lang == Lang.English ? "Receive timeout" : "Таймаут приёма";
    public string DeviceSendTimeout => Lang == Lang.English ? "Send timeout" : "Таймаут отправки";
    public string DeviceType => Lang == Lang.English ? "Device type" : "Тип устройства";
    public string Ean13 => Lang == Lang.English ? "EAN13" : "ШК ЕАН13";
    public string Enabled => Lang == Lang.English ? "Enabled" : "Активно";
    public string Exception => Lang == Lang.English ? "Exception" : "Исключение";
    public string ExpirationDt => Lang == Lang.English ? "Expiration date" : "Срок годности";
    public string FieldCategory => Lang == Lang.English ? "Category" : "Категория";
    public string FieldCount => Lang == Lang.English ? "Count" : "Количество";
    public string FieldCreated => Lang == Lang.English ? "Created" : "Создано";
    public string FieldDescription => Lang == Lang.English ? "Description" : "Описание";
    public string FieldEmpty => Lang == Lang.English ? "<Empty>" : "<Пусто>";
    public string FieldIdRRef => Lang == Lang.English ? "ID 1C" : "ИД 1С";
    public string FieldIpAddress => Lang == Lang.English ? "Ip-address" : "ИП-адрес";
    public string FieldIsEmpty => Lang == Lang.English ? "Empty field" : "Пустое поле";
    public string FieldIsNotInRange => Lang == Lang.English ? "Field is not in the range" : "Поле не находится в диапазоне";
    public string FieldLabel => Lang == Lang.English ? "Label" : "Этикетка";
    public string FieldModified => Lang == Lang.English ? "Modified" : "Изменено";
    public string FieldName => Lang == Lang.English ? "Name" : "Наименование";
    public string FieldNotFound => Lang == Lang.English ? "<Not found>" : "<Не найдено>";
    public string FieldPackageIsNotSelected => Lang == Lang.English ? "Nesting of the PLU is not selected" : "Вложенность ПЛУ не выбрана";
    public string FieldPackageMustBeSelected => Lang == Lang.English ? "Select the Nesting of the PLU" : "Выберите вложенность ПЛУ";
    public string FieldPluIsNotSelected => Lang == Lang.English ? "PLU is not selected" : "ПЛУ не выбрана";
    public string FieldPluMustBeSelected => Lang == Lang.English ? "Select the PLU" : "Выберите ПЛУ";
    public string FieldTitle => Lang == Lang.English ? "Title" : "Заголовок";
    public string FieldUser => Lang == Lang.English ? "User" : "Пользователь";
    public string File => Lang == Lang.English ? "File" : "Файл";
    public string FilePath => Lang == Lang.English ? "File path" : "Путь к файлу";
    public string FullName => Lang == Lang.English ? "Full name" : "Полное наименование";
    public string Gln => Lang == Lang.English ? "GLN" : "ГЛН";
    public string GoodsBruttoWeight => Lang == Lang.English ? "Gross weight" : "Вес брутто";
    public string GoodsBundleCount => Lang == Lang.English ? "Attaches" : "Вложений";
    public string GoodsBundleCountShort => Lang == Lang.English ? "Attach" : "Влож";
    public string GoodsDescription => Lang == Lang.English ? "Good description" : "Описание товара";
    public string GoodsName => Lang == Lang.English ? "Product" : "Товар";
    public string Group => Lang == Lang.English ? "Group" : "Группа";
    public string Gtin => Lang == Lang.English ? "BC GTIN" : "ШК ГТИН";
    public string GuidMercury => Lang == Lang.English ? "GUID Mercury" : "ГУИД Меркурий";
    public string Host => Lang == Lang.English ? "Host" : "Хост";
    public string HttpStatusCode => Lang == Lang.English ? "Status" : "Статус";
    public string HttpStatusException => Lang == Lang.English ? "Exception" : "Ошибка";
    public string HttpStatusNoException => Lang == Lang.English ? "No exceptions" : "Ошибок нет";
    public string Icon => Lang == Lang.English ? "Icon" : "Иконка";
    public string Id => Lang == Lang.English ? "ID" : "ИД";
    public string IdDwh => Lang == Lang.English ? "ID DWH" : "ИД ДВХ";
    public string Identity => Lang == Lang.English ? "Identity" : "Идентификатор";
    public string IdentityId => Lang == Lang.English ? "ID" : "ИД";
    public string IdentityUid => Lang == Lang.English ? "UID" : "УИД";
    public string IdRRef => Lang == Lang.English ? "ID 1C" : "ИД 1С";
    public string ImageData => Lang == Lang.English ? "Image data" : "Данные";
    public string ImageDataInfo => Lang == Lang.English ? "Info" : "Информация";
    public string InnerException => Lang == Lang.English ? "Inner exception" : "Вложенное исключение";
    public string IsActive => Lang == Lang.English ? "Active" : "Активно";
    public string IsClose => Lang == Lang.English ? "Is close" : "Закрыто";
    public string IsDefault => Lang == Lang.English ? "Default" : "По-умолчанию";
    public string IsKneading => Lang == Lang.English ? "Kneading" : "Замес";
    public string IsMarked => Lang == Lang.English ? "In the archive" : "В архиве";
    public string IsMarkedShort => Lang == Lang.English ? "x" : "х";
    public string IsOrder => Lang == Lang.English ? "Use order" : "Использовать заказ";
    public string IsShipping => Lang == Lang.English ? "Shipping labels" : "Транспортные этикетки";
    public string IsShippingLength => Lang == Lang.English ? "Count of labels in a box" : "Количество этикеток в коробе";
    public string IsShippingShort => Lang == Lang.English ? "Shipping" : "Трансп.";
    public string Itf14 => "ITF14";
    public string Label => Lang == Lang.English ? "Label" : "Этикетка";
    public string LabelTemplate => Lang == Lang.English ? "Label template" : "Шаблон этикетки";
    public string Level => Lang == Lang.English ? "Level" : "Уровень";
    public string Line => Lang == Lang.English ? "Line" : "Линия";
    public string Link => Lang == Lang.English ? "Link" : "Ссылка";
    public string LoginDt => Lang == Lang.English ? "Login" : "Залогирован";
    public string LogoutDt => Lang == Lang.English ? "Logout" : "Разлогирован";
    public string LogType => Lang == Lang.English ? "Log type" : "Тип лога";
    public string LowerWeightThreshold => Lang == Lang.English ? "Lower value of the box weight, kg" : "Нижнее значение веса короба, кг";
    public string Member => Lang == Lang.English ? "Method" : "Метод";
    public string Message => Lang == Lang.English ? "Message" : "Сообщение";
    public string Name => Lang == Lang.English ? "Name" : "Наименование";
    public string NameFull => Lang == Lang.English ? "Full name" : "Полное наименование";
    public string Nesting => Lang == Lang.English ? "Nesting" : "Вложенность";
    public string NestingCount => Lang == Lang.English ? "Nesting count" : "Кол-во вложений";
    public string Nomenclature => Lang == Lang.English ? "Nomenclature" : "Номенклатура";
    public string NomenclatureCode => Lang == Lang.English ? "Nomenclature code" : "Код номенклатуры";
    public string NomenclatureId => Lang == Lang.English ? "Nomenclature ID" : "ID номенклатуры";
    public string NomenclatureType => Lang == Lang.English ? "Type of nomenclature" : "Тип номенклатуры";
    public string NomenclatureUnitId => Lang == Lang.English ? "Nomenclature unit ID" : "ID юнита номенклатуры";
    public string Number => Lang == Lang.English ? "Number" : "Номер";
    public string NumberShort => Lang == Lang.English ? "#" : "№";
    public string Order => Lang == Lang.English ? "Order" : "Заказ";
    public string PackQuantly => Lang == Lang.English ? "Pack quantly" : "Быстрота упаковки";
    public string PackTypeId => Lang == Lang.English ? "Package type ID" : "ID типа упаковки";
    public string Parent => Lang == Lang.English ? "Parent" : "Родитель";
    public string ParentGroup => Lang == Lang.English ? "Parent group" : "Родительская группа";
    public string Plu => Lang == Lang.English ? "PLU" : "ПЛУ";
    public string PluBundleFk => Lang == Lang.English ? "PLU's bundle" : "Пакет ПЛУ";
    public string PluDescription => Lang == Lang.English ? "Use the `|` symbol to move the line." : "Для переноса строки используйте символ `|`";
    public string PluId => Lang == Lang.English ? "ID PLU" : "ИД ПЛУ";
    public string PluNesting => Lang == Lang.English ? "PLU's nesting" : "Вложенность ПЛУ";
    public string PluNumber => Lang == Lang.English ? "# PLU" : "№ ПЛУ";
    public string PluPackage => Lang == Lang.English ? "PLU's package" : "Тара ПЛУ";
    public string PluScale => Lang == Lang.English ? "Device PLU" : "ПЛУ устройства";
    public string PrettyName => Lang == Lang.English ? "Pretty name" : "Красивое наименование";
    public string Printer => Lang == Lang.English ? "Printer" : "Принтер";
    public string PrinterResource => Lang == Lang.English ? "Printer resource" : "Ресурс принтера";
    public string PrinterType => Lang == Lang.English ? "Printer type" : "Тип принтера";
    public string Product => Lang == Lang.English ? "Product" : "Продукт";
    public string ProductDt => Lang == Lang.English ? "Product date" : "Дата продукции";
    public string ProductionFacility => Lang == Lang.English ? "Production facility" : "Производственная площадка";
    public string ProductSeries => Lang == Lang.English ? "Product series" : "Серия продукта";
    public string RegNum => Lang == Lang.English ? "# reg" : "№ регистрации";
    public string ReleaseDt => Lang == Lang.English ? "Release date" : "Дата релиза";
    public string Resource => Lang == Lang.English ? "Resource" : "Ресурс";
    public string Row => Lang == Lang.English ? "Row" : "Строка";
    public string Scale => Lang == Lang.English ? "Device" : "Устройство";
    public string ScaleFactor => Lang == Lang.English ? "Scale factor" : "Коэф. масштабирования";
    public string ScaleId => Lang == Lang.English ? "Scale ID" : "ID весов";
    public string ScreenShot => Lang == Lang.English ? "Screenshot" : "Скриншот";
    public string ShelfLifeDays => Lang == Lang.English ? "Shelf life, days" : "Срок годности, суток";
    public string ShelfLifeDaysShort => Lang == Lang.English ? "Life" : "Срок";
    public string Sscc => Lang == Lang.English ? "Transport packing code (SSCC)" : "Код транспортной упаковки (SSCC)";
    public string State => Lang == Lang.English ? "Status" : "Статус";
    public string Storage => Lang == Lang.English ? "Storage" : "Склад";
    public string TableCalc => Lang == Lang.English ? "Calc" : "Расчитать";
    public string TableCancel => Lang == Lang.English ? "Close record" : "Закрыть запись";
    public string TableClear => Lang == Lang.English ? "Deactivate active record" : "Деактивировать активную запись";
    public string TableCopy => Lang == Lang.English ? "Cope record" : "Копировать запись";
    public string TableCreate => Lang == Lang.English ? "Create record" : "Создать запись";
    public string TableDelete => Lang == Lang.English ? "Delete record" : "Удалить запись";
    public string TableEdit => Lang == Lang.English ? "Edit record" : "Редактировать запись";
    public string TableIncludes => Lang == Lang.English ? "Included records" : "Вложенные записи";
    public string TableMark => Lang == Lang.English ? "Save record" : "Пометить запись на удаление";
    public string TableNew => Lang == Lang.English ? "New record" : "Новая запись";
    public string TablePluHavingPlu => Lang == Lang.English ? "The PLU table already has this number" : "Таблица PLU уже имеет такой номер";
    public string TableRead => Lang == Lang.English ? "Read data" : "Прочитать данные";
    public string TableReadCancel => Lang == Lang.English ? "Cancel data reading" : "Отмена чтения данных";
    public string TableRereadFromDb => Lang == Lang.English ? "Reread from the database" : "Перечитать из БД";
    public string TableSave => Lang == Lang.English ? "Save record" : "Сохранить запись";
    public string TableSelect => Lang == Lang.English ? "Highlight record" : "Выделить запись";
    public string TableTab => Lang == Lang.English ? "Switch between panels" : "Переключиться между панелями";
    public string TaskModule => Lang == Lang.English ? "Task module" : "Модуль задачи";
    public string TaskModuleType => Lang == Lang.English ? "Task module type" : "Тип модуля задачи";
    public string TaskType => Lang == Lang.English ? "Task type" : "Тип задачи";
    public string Template => Lang == Lang.English ? "Template" : "Шаблон";
    public string TemplateDefault => Lang == Lang.English ? "Default template" : "Шаблон по-умолчанию";
    public string TemplateId => Lang == Lang.English ? "Template ID" : "ID шаблона";
    public string TemplateIdDefault => Lang == Lang.English ? "Default template ID" : "ID шаблона по-умолчанию";
    public string TemplateIdSeries => Lang == Lang.English ? "Series ID" : "ID серии";
    public string TemplateLabelary => Lang == Lang.English ? "Web-site Labelary" : "Веб-сайт Labelary";
    public string TemplateResource => Lang == Lang.English ? "Template resource" : "Ресурс шаблона";
    public string TemplateSeries => Lang == Lang.English ? "Summary label template" : "Шаблон суммарной этикетки";
    public string Title => Lang == Lang.English ? "Title" : "Заголовок";
    public string Type => Lang == Lang.English ? "Type" : "Тип";
    public string TypeBottom => Lang == Lang.English ? "Bottom's type" : "Нижний тип";
    public string TypeRight => Lang == Lang.English ? "Right's type" : "Правый тип";
    public string TypeTop => Lang == Lang.English ? "Top's type" : "Верхний тип";
    public string Uid => Lang == Lang.English ? "UID" : "УИД";
    public string UpperWeightThreshold => Lang == Lang.English ? "Upper value of the box weight, kg" : "Верхнее значение веса короба, кг";
    public string User => Lang == Lang.English ? "User" : "Пользователь";
    public string Value => Lang == Lang.English ? "Value" : "Значение";
    public string ValueBottom => Lang == Lang.English ? "Bottom's value" : "Нижнее значение";
    public string ValueRight => Lang == Lang.English ? "Right's value" : "Правое значение";
    public string ValueTop => Lang == Lang.English ? "Top's value" : "Верхнее значение";
    public string VatRate => Lang == Lang.English ? "VAT rate" : "Ставка НДС";
    public string Version => Lang == Lang.English ? "Version" : "Версия";
    public string Weighing => Lang == Lang.English ? "Weighing" : "Взвешивание";
    public string Weight => Lang == Lang.English ? "Weight" : "Вес";
    public string Weighted => Lang == Lang.English ? "Weighted" : "Весовая";
    public string WeightTare => Lang == Lang.English ? "Tare weight" : "Вес тары";
    public string WeightMaximal => Lang == Lang.English ? "Maximal weight" : "Максимальный вес";
    public string WeightMinimal => Lang == Lang.English ? "Minimal weight" : "Минимальный вес";
    public string WeightNetto => Lang == Lang.English ? "Net weight" : "Вес нетто";
    public string WeightNominal => Lang == Lang.English ? "Nominal weight" : "Номинальный вес";
    public string WeightShort => Lang == Lang.English ? "Weight" : "Вес";
    public string WeithingDt => Lang == Lang.English ? "Weighing date" : "Дата взвешивания";
    public string WorkShop => Lang == Lang.English ? "Workshop" : "Цех";
    public string WorkShopId => Lang == Lang.English ? "Workshop ID" : "ИД цеха";
    public string WorkShopName => Lang == Lang.English ? "Workshop" : "Цех";
    public string Xml => Lang == Lang.English ? "XML" : "Поле XML";
    public string XmlPretty => Lang == Lang.English ? "Pretty XML" : "Красивый XML";
    public string Zpl => Lang == Lang.English ? "ZPL" : "ЗПЛ";

    #endregion
}