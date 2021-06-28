using System.Diagnostics;
// ReSharper disable UnusedMember.Global

namespace BlazorDeviceControl.Utils
{
    public static class LocalizationStrings
    {
        public const int Timeout = 5000;
        #region Главная
        public const string SqlServerProduct = "SQLDBEP04";
        public const string Company = @"Владимирский стандарт";
        public const string IndexTitle = @"Внутренние ресурсы";
        public const string IndexDescription = @"Управление устройствами";
        public const string CallbackTitle = @"Обратная связь";
        public const string CallbackEmail = @"mailto:morozov_dv@kolbasa-vs.ru?cc=dvivakin@kolbasa-vs.ru&subject=blazor-device-control.kolbasa-vs.local";
        public const string DataLoading = @"Загрузка данных...";
        public const string DataRecords = @"записей";
        public const string IsDebug = @"Режим отладки";
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
        public const string UriRouteTableZebraPrinters = "/zebraprinters";
        public const string UriRouteTableZebraPrinterTypes = "/printertypes";
        public const string UriRouteTableContragents = "/contragents";
        public const string UriRouteTableNomenclature = "/nomenclature";
        public const string UriRouteTableProductionFacilities = "/productionfacilities";
        public const string UriRouteTableTemplates = "/templates";
        public const string UriRouteTableTemplateResources = "/templateresources";
        public const string UriRouteTableWeithingFacts = "/weithingfacts";
        public const string UriRouteTableWorkshops = "/workshops";
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
        public const string TableTitleAttributeDefinationListShort = @"AttributeDefinationList";
        public const string TableTitleAttributeDefinationListFull = @"AttributeDefinationList [AttributeDefinationList]";
        public const string TableTitleAttributeValuesShort = @"AttributeValues";
        public const string TableTitleAttributeValuesFull = @"AttributeValues [AttributeValues]";
        public const string TableTitleBarCodesShort = @"Штрихкоды";
        public const string TableTitleBarCodesFull = @"Штрихкоды [BarCodes]";
        public const string TableTitleBarCodeTypesShort = @"Типы ШК";
        public const string TableTitleBarCodeTypesFull = @"Типы ШК [BarCodeTypes]";
        public const string TableTitleContragentsShort = @"Контрагенты";
        public const string TableTitleContragentsFull = @"Контрагенты [Contragents]";
        public const string TableTitleNomenclatureShort = @"Номенклатура";
        public const string TableTitleNomenclatureFull = @"Номенклатура [Nomenclature]";
        public const string TableTitleNomenclatureUnitsShort = @"Упаковки";
        public const string TableTitleNomenclatureUnitsFull = @"Упаковки [NomenclatureUnits]";
        public const string TableTitleOrdersShort = @"Заказы";
        public const string TableTitleOrdersFull = @"Заказы [Orders]";
        public const string TableTitleOrderStatusShort = @"Статусы заказов";
        public const string TableTitleOrderStatusFull = @"Статусы заказов [OrderStatus]";
        public const string TableTitleOrderTypesShort = @"Типы заказов";
        public const string TableTitleOrderTypesFull = @"Типы заказов [OrderTypes]";
        public const string TableTitlePluShort = @"PLU";
        public const string TableTitlePluFull = @"PLU [PLU]";
        public const string TableTitleProductionFacilityShort = @"Произв. площадки";
        public const string TableTitleProductionFacilityFulll = @"Производственные площадки [ProductionFacility]";
        public const string TableTitleProductSeriesShort = @"Серии продуктов";
        public const string TableTitleProductSeriesFull = @"Серии продуктов [ProductSeries]";
        public const string TableTitleZebraPrintersShort = @"Принтеры";
        public const string TableTitleZebraPrinterTypesShort = @"Типы принтеров";
        public const string TableTitleZebraPrintersFull = @"Принтеры [ZebraPrinter]";
        public const string TableTitleHostsShort = @"Хосты";
        public const string TableTitleHostsFull = @"Хосты [Hosts]";
        public const string TableTitleScalesShort = @"Устройства";
        public const string TableTitleScalesFull = @"Устройства [Scales]";
        public const string TableTitleSsccStorageShort = @"SSCCStorage";
        public const string TableTitleSsccStorageFull = @"SSCCStorage [SSCCStorage]";
        public const string TableTitleResources = @"Ресурсы шаблонов";
        public const string TableTitleTemplateResourcesShort = @"Ресурсы шаблонов";
        public const string TableTitleTemplateResourcesFull = @"Ресурсы шаблонов [TemplateResources]";
        public const string TableTitleTemplatesShort = @"Шаблоны";
        public const string TableTitleTemplatesFull = @"Шаблоны [Templates]";
        public const string TableTitleWeithingFactShort = @"Взвешивания";
        public const string TableTitleWeithingFactFull = @"Взвешивания [WeithingFact]";
        public const string TableTitleWorkShopShort = @"Цеха";
        public const string TableTitleWorkShopFull = @"Цеха [WorkShop]";
        public const string TableFieldId = @"ID";
        public const string TableFieldName = @"Наименование";
        public const string TableFieldLine = @"Линия";
        public const string TableFieldPluDescription = @"Для переноса строки используйте символ `|`";
        public const string TableFieldType = @"Тип";
        public const string TableFieldTitle = @"Заголовок";
        public const string TableFieldCategoryId = @"ID категории";
        public const string TableFieldCategoryName = @"Категория";
        public const string TableFieldImageData = @"ImageData";
        public const string TableFieldImageDataInfo = @"Информация";
        public const string TableFieldProductionFacilityName = @"Площадка";
        public const string TableFieldDeviceIp = @"IP устройства";
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
        public const string TableFieldCreateDate = @"Дата создания";
        public const string TableFieldModifiedDate = @"Дата редактирования";
        public const string TableFieldIdRRef = @"ID 1C";
        public const string TableFieldStorage = @"Склад";
        public const string TableFieldState = @"Статус";
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

        #endregion
        #region Контроль ввода
        public const string InputControlMuchZero = @"Значение должно быть больше 0";
        #endregion

        private static string GetAppVersion()
        {
            System.Reflection.Assembly executingAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            return fieVersionInfo.FileVersion;
        }
    }
}
