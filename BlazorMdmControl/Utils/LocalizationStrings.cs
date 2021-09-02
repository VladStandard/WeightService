// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;
using System.Reflection;

namespace MdmControlBlazor.Utils
{
    public static class LocalizationStrings
    {
        public const int Timeout = 5000;
        #region Главная
        public const string SqlServerRelease = "SQLSRSP01";
        public const string MainCompany = @"Владимирский стандарт";
        public const string MainTitle = @"Управление НСИ";
        public const string MainCallbackTitle = @"Обратная связь";
        public const string MainCallbackEmail = @"mailto:morozov_dv@kolbasa-vs.ru?cc=dvivakin@kolbasa-vs.ru&subject=mdm-dwh.kolbasa-vs.local";
        public const string DataLoading = @"Загрузка данных...";
        public const string MethodError = @"Ошибка метода";
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
        #region Главная
        public const string HomeTitle = @"Главная";
        public static string HomeText = $@"Версия программы: {GetAppVersion()}";
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
        #region Комплексы промышленных устройств
        public const string DevicesTitle = @"Комплексы промышленных устройств";
        #endregion
        #region URI
        public const string UriRouteRoot = "/";
        public const string UriRouteDocs = "/Docs";
        public const string UriRouteMemory = "/Memory";
        public const string UriRouteNomenclature = "/Nomenclature";
        public const string UriRouteNomenclatures = "/Nomenclatures";
        public const string UriRouteNomenclatureMaster = "/NomenclatureMaster";
        public const string UriRouteNomenclatureNonNormilise = "/NomenclatureNonNormilise";
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
        public const string MenuLogin = @"Логин";
        public const string MenuAccess = @"Доступ";
        public const string MenuAccessDeny = @"Доступ запрещён";
        public const string MenuAccessAllow = @"Доступ разрешён";
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
        #endregion
        #region Таблица мастер записей
        public const string TableMasterRead = @"Прочитать данные мастер записей";
        public const string TableMasterEdit = @"Редактировать мастер запись";
        public const string TableMasterClear = @"Деактивировать активную мастер запись";
        public const string TableMasterCreate = @"Создать мастер запись";
        public const string TableMasterDelete = @"Удалить мастер запись";
        public const string TableMasterInclude = @"Добавить в мастер запись";
        public const string TableMasterExclude = @"Исключить из мастер записи";
        public const string TableMasterSave = @"Сохранить мастер запись";
        public const string TableMasterCancel = @"Закрыть мастер запись";
        public const string TableMasterSetRelevanceTrue = @"Сделать мастер запись актуальной";
        public const string TableMasterSetRelevanceFalse = @"Сделать мастер запись не актуальной";
        #endregion
        #region Таблица ненормализованных записей
        public const string TableNonNormalizeRead = @"Прочитать данные ненормализованных записей";
        public const string TableNonNormalizeEdit = @"Редактировать ненормализованную запись";
        public const string TableNonNormalizeClear = @"Деактивировать активную ненормализованную запись";
        public const string TableNonNormalizeCreate = @"Создать ненормализованную запись";
        public const string TableNonNormalizeDelete = @"Удалить ненормализованную запись";
        public const string TableNonNormalizeSave = @"Сохранить ненормализованную запись";
        public const string TableNonNormalizeCancel = @"Закрыть ненормализованную запись";
        public const string TableNonNormalizeSetRelevanceTrue = @"Сделать ненормализованную запись актуальной";
        public const string TableNonNormalizeSetRelevanceFalse = @"Сделать ненормализованную запись не актуальной";
        public const string TableNonNormalizeSetIsProductTrue = @"Отобразить продукты для ненормализованных записей";
        public const string TableNonNormalizeSetIsProductFalse = @"Отобразить все ненормализованные записи";
        #endregion
        #region Таблица
        public const string Table = @"Таблица";
        public const string TableEntityRead = @"Прочитать данные";
        public const string TableEntityClear = @"Деактивировать активную запись";
        public const string TableActions = @"Действия";
        public const string TableActionsIsDeny = @"Действия недоступны";
        public const string TableActionEdit = @"Редактировать";
        public const string TableActionFill = @"Заполнить";
        public const string TableActionAdd = @"Добавить";
        public const string TableActionClear = @"Очистить";
        public const string TableActionDelete = @"Удалить";
        public const string TableActionCopy = @"Копировать";
        public const string TableActionSave = @"Сохранить";
        public const string TableActionCancel = @"Закрыть";
        public const string TableRowsCount = @"Записей";
        public const string TableTitleInformationSystemsShort = @"Информационные системы";
        public const string TableTitleNomenclaturesShort = @"Номенклатура";
        public const string TableTitleNomenclatureNonNormalize = @"Ненормализованные записи";
        public const string TableTitleNomenclatureMaster = @"Мастер записи";
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
        public const string TableFieldParent = @"Родитель";
        public const string TableFieldParents = @"Родители";
        public const string TableFieldArticle = @"Артикуль";
        public const string TableFieldWeighted = @"Взвешено";
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
        public const string TableFieldInformationSystem = @"ИС";
        public const string TableFieldDescription = @"Описание";
        public const string TableFieldComment = @"Комментарий";
        public const string TableFieldXml = @"XML";
        public const string TableFieldBrand = @"Бренд";
        public const string TableFieldGuidMercury = @"GUID Mercury";
        public const string TableFieldKeepTrackOfCharacteristics = @"Хранить изменения характеристик";
        public const string TableFieldIsService = @"Сервис";
        public const string TableFieldIsProduct = @"Продукт";
        public const string TableFieldAdditionalDescriptionOfNomenclature = @"Дополнительное описание";
        public const string TableFieldNomenclatureGroupCost = @"NomenclatureGroupCost";
        public const string TableFieldNomenclatureGroup = @"NomenclatureGroup";
        public const string TableFieldArticleCost = @"ArticleCost";
        public const string TableFieldRelevanceStatus = @"Актуальность";
        public const string TableFieldNormalizationStatus = @"Нормализация";
        public const string TableFieldMasterId = @"Мастер";
        public const string TableFieldNomenclatureType = @"Тип номенклатуры";
        public const string TableFieldVatRate = @"Ставка НДС";
        public const string TableFieldUnit = @"Единица измерения";
        public const string TableFieldWeight = @"Вес";
        public const string TableFieldBoxTypeId = @"BoxTypeId";
        public const string TableFieldBoxType = @"BoxType";
        public const string TableFieldPackTypeId = @"PackTypeId";
        public const string TableFieldPackType = @"PackType";
        public const string TableFieldStatus = @"Статус";
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
        public const string TableFieldGoodsTareWeight = @"Вес тары";
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
        public const string TableFieldCheckWeight = @"Контроль веса";
        public const string TableFieldCheckGtin = @"v";
        public const string TableFieldHost = @"Хост";
        public const string TableFieldUseIsProduct = @"Продукт";
        public const string TableRecordSave = @"Сохранить запись";
        #endregion
        #region Контроль ввода
        public const string InputControlMuchZero = @"Значение должно быть больше 0";
        #endregion

        private static string GetAppVersion()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            return fieVersionInfo.FileVersion;
        }
    }
}
