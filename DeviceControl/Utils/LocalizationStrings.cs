using System.Diagnostics;
using DeviceControl.Core;

// ReSharper disable UnusedMember.Global

namespace BlazorDeviceControl.Utils
{
    public static class LocalizationStrings
    {
        public const int Timeout = 5000;
        #region Главная
        public const string SqlServerProduct = "PALYCH";
        public const string Company = @"Владимирский стандарт";
        public const string IndexTitle = @"Внутренние ресурсы";
        public const string IndexDescription = @"Управление устройствами";
        public const string CallbackTitle = @"Обратная связь";
        public const string CallbackEmail = @"mailto:morozov_dv@kolbasa-vs.ru?cc=dvivakin@kolbasa-vs.ru&subject=blazor-device-control.kolbasa-vs.local";
        public const string DataLoading = @"Загрузка данных...";
        public const string DataRecords = @"записей";
        public const string IsDebug = @"Режим отладки";
        public const string ServerDevelop = @"Сервер разработки";
        public const string ServerProduct = @"Промышленный сервер";
        #endregion
        #region Служба поддержки
        public const string SupportTitle = @"Служба поддержки";
        public const string SupportLinkCreatioText = @"Creatio обращения";
        public const string SupportLinkCreatioPath = @"https://kolbasa-vs.terrasoft.ru/0/Nui/ViewModule.aspx#SectionModuleV2/CaseSection/";
        public const string SupportLinkHelpText = @"Написать письмо";
        public const string SupportLinkHelpPath = @"mailto:helpdesk@kolbasa-vs.ru?subject=Обращение";
        #endregion
        #region Авторизация
        public const string AuthorizationTitle = @"Авторизация";
        #endregion
        #region Memory
        public const string MemoryTitle = @"Менеджер памяти приложения";
        public const string MemoryLimit = @"Лимит памяти";
        public const string MemoryLimitNotSet = @"Лимит памяти не задан!";
        public const string MemoryUsed = @"Занимаемая память";
        public const string MemoryPhysical = @"Физическая память";
        public const string MemoryVirtual = @"Виртуальная память";
        public const string MemoryIsExecute = @"Менеджер памяти приложения в работе.";
        public const string MemoryIsNotExecute = @"Менеджер памяти приложения не выполняется!";
        public const string MemoryResult = @"Результат";
        public const string MemoryException = @"Ошибка менеджера памяти";
        public const string MemoryActionStart = @"Запустить менеджер памяти";
        public const string MemoryActionStop = @"Остановить менеджер памяти";
        #endregion
        #region Документация
        public const string DocTitle = @"Документация";
        public static string DocText = $@"Версия программы: {GetAppVersion()}";
        #endregion
        #region Комплексы промышленных устройств
        public const string DevicesTitle = @"Комплексы промышленных устройств";
        #endregion
        #region URI
        public const string UriRouteRoot = "/";
        public const string UriRouteTableScales = "/scales";
        public const string UriRouteTableHosts = "/hosts";
        public const string UriRouteTablePrinters = "/printers";
        public const string UriRouteTablePrinterTypes = "/printertypes";
        public const string UriRouteTableContragents = "/contragents";
        public const string UriRouteTableNomenclature = "/nomenclature";
        public const string UriRouteTableProductionFacilities = "/productionfacilities";
        public const string UriRouteTableTemplates = "/templates";
        public const string UriRouteTableTemplateResources = "/templateresources";
        public const string UriRouteTableWeithingFacts = "/weithingfacts";
        public const string UriRouteTableWorkshops = "/workshops";
        public const string UriRouteItemPrinter = "/printer";
        public const string UriRouteTableLogs = "/logs";
        #endregion
        #region Меню
        public const string MenuReferences = "Справочники";
        public const string FileChoose = @"Выбрать файл";
        public const string FileUpload = @"Загрузить файл";
        public const string FileDownload = @"Скачать файл";
        public const string FileDialog = @"Файловый диалог";
        public const string FileSaveDialog = @"Указать имя файла для сохранения";
        public const string ServerResponse = @"Ответ сервера";
        public const string Chart = @"Диаграмма";
        public const string ChartSmooth = @"Скруглить";
        public const string ChartCreated = @"Создано";
        public const string ChartModified = @"Изменено";
        public const string ChartCount = @"Количество";
        #endregion
        #region Таблицы
        public const string Table = @"Таблица";
        public const string TableReadData = @"Прочитать данные";
        public const string TableActions = @"Действия";
        public const string TableActionsIsDeny = @"Действия недоступны";
        public const string TableActionEdit = @"Редактировать";
        public const string TableActionFill = @"Заполнить";
        public const string TableActionAdd = @"Добавить";
        public const string TableActionClear = @"Очистить";
        public const string TableActionDelete = @"Удалить";
        public const string TableActionDeleteForever = @"Удалить навсегда";
        public const string TableActionCopy = @"Копировать";
        public const string TableActionCancel = @"Отмена";
        public const string TableActionSave = @"Сохранить";
        public const string TableTitleFonts = @"Шрифты";
        public const string TableTitleLabels = @"Логотипы";
        public const string TableTitleBarCodesShort = @"Штрихкоды";
        public const string TableTitleBarCodeTypesShort = @"Типы ШК";
        public const string TableTitleContragentsShort = @"Контрагенты";
        public const string TableTitleNomenclatureShort = @"Номенклатура";
        public const string TableTitleNomenclatureUnitsShort = @"Упаковки";
        public const string TableTitleOrdersShort = @"Заказы";
        public const string TableTitleOrderStatusShort = @"Статусы заказов";
        public const string TableTitleOrderTypesShort = @"Типы заказов";
        public const string TableTitlePluShort = @"PLU";
        public const string TableTitleProductionFacilityShort = @"Произв. площадки";
        public const string TableTitleProductSeriesShort = @"Серии продуктов";
        public const string TableTitlePrinters = @"Принтеры";
        public const string TableTitlePrinterTypes = @"Типы принтеров";
        public const string TableTitleHostsShort = @"Хосты";
        public const string TableTitleScalesShort = @"Устройства";
        public const string TableTitleSsccStorageShort = @"SSCCStorage";
        public const string TableTitleResources = @"Ресурсы шаблонов";
        public const string TableTitleTemplateResourcesShort = @"Ресурсы шаблонов";
        public const string TableTitleTemplatesShort = @"Шаблоны";
        public const string TableTitleWeithingFactShort = @"Взвешивания";
        public const string TableTitleLogs = @"Логи";
        public const string TableTitleWorkShopShort = @"Цеха";
        public const string TableFieldId = @"ID";
        public const string TableFieldName = @"Наименование";
        public const string TableFieldLine = @"Линия";
        public const string TableFieldRow = @"Строка";
        public const string TableFieldFile = @"Файл";
        public const string TableFieldMember = @"Метод";
        public const string TableFieldIcon = @"Иконка";
        public const string TableFieldMessage = @"Сообщение";
        public const string TableFieldPluDescription = @"Для переноса строки используйте символ `|`";
        public const string TableFieldType = @"Тип";
        public const string TableFieldTitle = @"Заголовок";
        public const string TableFieldCategoryId = @"ID категории";
        public const string TableFieldCategoryName = @"Категория";
        public const string TableFieldImageData = @"ImageData";
        public const string TableFieldImageDataInfo = @"Информация";
        public const string TableFieldProductionFacilityName = @"Площадка";
        public const string TableFieldDeviceIp = @"IP устройства";
        public const string TableFieldIdRref = @"ID 1C";
        public const string TableFieldDevicePort = @"Порт устройства";
        public const string TableFieldDeviceMac = @"MAC устройства";
        public const string TableFieldDeviceSendTimeout = @"Таймаут отправки";
        public const string TableFieldDeviceReceiveTimeout = @"Таймаут приёма";
        public const string TableFieldDeviceComPort = @"COM-порт";
        public const string TableFieldDeviceNumber = @"Номер устройства";
        public const string TableFieldUseOrder = @"Использовать заказ";
        public const string TableFieldTemplateIdDefault = @"ID шаблона";
        public const string TableFieldTemplateIdSeries = @"ID серии";
        public const string TableFieldTemplateSeries = @"Шаблон суммарной этикетки";
        public const string TableFieldTemplateDefault = @"Шаблон по-умолчанию";
        public const string TableFieldWorkShopId = @"ID цеха";
        public const string TableFieldWorkShopName = @"Цех";
        public const string TableFieldScaleFactor = @"Scale factor";
        public const string TableFieldNameFull = @"Полное наименование";
        public const string TableFieldDescription = @"Описание";
        public const string TableFieldComment = @"Комментарий";
        public const string TableFieldXml = @"XML";
        public const string TableFieldBrand = @"Brand";
        public const string TableFieldGuidMercury = @"GUID Mercury";
        public const string TableFieldNomenclatureType = @"Тип номенклатуры";
        public const string TableFieldVatRate = @"Ставка НДС";
        public const string TableFieldCode = @"Код";
        public const string TableFieldModifiedDate = @"Дата редактирования";
        public const string TableFieldIdRRef = @"ID 1C";
        public const string TableFieldStorage = @"Склад";
        public const string TableFieldState = @"Статус";
        public const string TableFieldScale = @"Устройство";
        public const string TableFieldScaleId = @"ID весов";
        public const string TableFieldUid = @"UUID";
        public const string TableFieldIsClose = @"IsClose";
        public const string TableFieldSscc = @"SSCC";
        public const string TableFieldValue = @"Значение";
        public const string TableFieldMarked = @"В архиве";
        public const string TableFieldSettingsFile = @"Файл настроек";
        public const string TableFieldPackWeight = @"PackWeight";
        public const string TableFieldPackQuantly = @"PackQuantly";
        public const string TableFieldPackTypeId = @"PackTypeId";
        public const string TableFieldBarCodeTypeId = @"ID типа ШК";
        public const string TableFieldNomenclatureId = @"ID номенклатуры";
        public const string TableFieldNomenclatureName = @"Номенклатура";
        public const string TableFieldNomenclatureUnitId = @"ID юнита номенклатуры";
        public const string TableFieldContragentId = @"ID контрагента";
        public const string TableFieldGoodsName = @"Товар";
        public const string TableFieldGoodsFullName = @"Полное наименование";
        public const string TableFieldGoodsDescription = @"Описание товара";
        public const string TableFieldTemplateId = @"ID шаблона";
        public const string TableFieldTemplate = @"Шаблон";
        public const string TableFieldResource = @"Ресурс";
        public const string TableFieldGtin = @"GTIN";
        public const string TableFieldEan13 = @"EAN13";
        public const string TableFieldItf14 = @"ITF14";
        public const string TableFieldShelfLifeDays = @"Срок годности (суток)";
        public const string TableFieldGoodsTareWeight = @"Тара";
        public const string TableFieldGoodsBruttoWeight = @"Вес брутто";
        public const string TableFieldGoodsBoxQuantly = @"Вложений в короб";
        public const string TableFieldConsumerName = @"ConsumerName";
        public const string TableFieldGln = @"Gln";
        public const string TableFieldPlu = @"PLU";
        public const string TableFieldActive = @"Активно";
        public const string TableFieldZebraPrinter = @"Принтер";
        public const string TableFieldZebraPrinterIp = @"IP-адрес";
        public const string TableFieldZebraPrinterPort = @"Порт принтера";
        public const string TableFieldZebraPrinterMac = @"MAC-адрес";
        public const string TableFieldZebraPrinterPeelOffSet = @"Смещение";
        public const string TableFieldZebraPrinterDarknessLevel = @"Уровень темноты";
        public const string TableFieldZebraPrinterType = @"Тип принтера";
        public const string TableFieldZebraPrinterPassword = @"Пароль принтера";
        public const string TableFieldUpperWeightThreshold = @"Верхнее значение веса короба";
        public const string TableFieldNominalWeight = @"Номинальный вес короба";
        public const string TableFieldLowerWeightThreshold = @"Нижнее значение веса короба";
        public const string TableFieldCheckWeight = @"Весовая продукция";
        public const string TableFieldCheckGtin = @"v";
        public const string TableFieldHost = @"Хост";
        public const string TableFieldWeithingDate = @"Дата взвешивания";
        public const string TableFieldCreateDate = @"Дата создания";
        public const string TableFieldProductDate = @"Дата продукции";
        public const string TableFieldNetWeight = @"Вес нетто";
        public const string TableFieldTareWeight = @"Вес тары";
        public const string TableFieldRegNum = @"#";
        public const string TableFieldKneding = @"Взвешено";
        public const string TableFieldCount = @"Количество";
        public const string TableFieldDayOfWeek = @"День недели";
        public const string TableFieldApp = @"Программа";
        public const string TableFieldVersion = @"Версия";
        #endregion
        #region Item
        public const string ItemTitlePrinter = @"Принтер";
        public const string ItemTitleBarCodeType = @"Тип ШК";
        public const string ItemTitleContragents = @"Контрагент";
        public const string ItemTitleHosts = @"Хост";
        public const string ItemTitleLabels = @"Этикетка";
        public const string ItemTitleNomenclature = @"Номенклатура";
        public const string ItemTitleOrders = @"Заказ";
        public const string ItemTitleOrderStatus = @"Статус заказа";
        public const string ItemTitleOrderTypes = @"Тип заказа";
        public const string ItemTitlePlu = @"ПЛУ";
        public const string ItemTitleProductionFacility = @"ProductionFacility";
        public const string ItemTitleProductSeries = @"ProductSeries";
        public const string ItemTitleScales = @"Устройство";
        public const string ItemTitleSsccStorage = @"SsccStorage";
        public const string ItemTitleTemplateResources = @"Ресурс шаблона";
        public const string ItemTitleTemplates = @"Шаблон";
        public const string ItemTitleWeithingFact = @"Взвешивание";
        public const string ItemTitleWorkShop = @"WorkShop";
        public const string ItemTitlePrinterResourceRef = @"Ресурс принтера";
        public const string ItemTitlePrinterType = @"Тип принтера";
        #endregion
        #region Таблица
        public const string TableTab = @"Переключиться между панелями";
        public const string TableRead = @"Прочитать данные";
        public const string TableReadCancel = @"Отмена чтения данных";
        public const string TableEdit = @"Редактировать запись";
        public const string TableClear = @"Деактивировать активную запись";
        public const string TableCreate = @"Создать запись";
        public const string TableDelete = @"Удалить запись";
        public const string TableSelect = @"Выделить запись";
        public const string TableIncludes = @"Вложенные записи";
        public const string TableRecordSave = @"Сохранить запись";
        public const string TableRecordCancel = @"Закрыть запись";
        #endregion
        #region Контроль ввода
        public const string InputControlMuchZero = @"Значение должно быть больше 0";
        #endregion
        #region Dialog
        public const string DialogQuestion = @"Выполнить операцию?";
        public const string DialogButtonYes = @"Да";
        public const string DialogButtonCancel = @"Отмена";
        public const string DialogButtonNo = @"Нет";
        public const string DialogResultSuccess = @"Операция выполнена успешно.";
        public const string DialogResultCancel = @"Отмена операции. Возможно, не выполнены необходимые условия.";
        public const string DialogResultFail = @"Ошибка выполнения операции!";
        #endregion

        private static string GetAppVersion()
        {
            System.Reflection.Assembly executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            return fieVersionInfo.FileVersion;
        }

        public static string GetItemTitle(EnumTable table)
        {
            switch (table)
            {
                case EnumTable.Printer:
                    return ItemTitlePrinter;
                case EnumTable.BarCodeTypes:
                    return ItemTitleBarCodeType;
                case EnumTable.Contragents:
                    return ItemTitleContragents;
                case EnumTable.Hosts:
                    return ItemTitleHosts;
                case EnumTable.Labels:
                    return ItemTitleLabels;
                case EnumTable.Nomenclature:
                    return ItemTitleNomenclature;
                case EnumTable.Orders:
                    return ItemTitleOrders;
                case EnumTable.OrderStatus:
                    return ItemTitleOrderStatus;
                case EnumTable.OrderTypes:
                    return ItemTitleOrderTypes;
                case EnumTable.Plu:
                    return ItemTitlePlu;
                case EnumTable.ProductionFacility:
                    return ItemTitleProductionFacility;
                case EnumTable.ProductSeries:
                    return ItemTitleProductSeries;
                case EnumTable.Scales:
                    return ItemTitleScales;
                case EnumTable.SsccStorage:
                    return ItemTitleSsccStorage;
                case EnumTable.TemplateResources:
                    return ItemTitleTemplateResources;
                case EnumTable.Templates:
                    return ItemTitleTemplates;
                case EnumTable.WeithingFact:
                    return ItemTitleWeithingFact;
                case EnumTable.WorkShop:
                    return ItemTitleWorkShop;
                case EnumTable.PrinterResourceRef:
                    return ItemTitlePrinterResourceRef;
                case EnumTable.PrinterType:
                    return ItemTitlePrinterType;
            }
            return string.Empty;
        }
    }
}
