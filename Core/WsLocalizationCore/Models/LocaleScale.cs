// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class LocaleScale : WsLocalizationBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleScale _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleScale Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public List<string> ListLanguages => Lang == Lang.English ? new() { "Russian", "English" } : new List<string> { "Russian", "English" };
    public List<string> ListResolutions => Lang == Lang.English ? new() { "800x600", "1024x768", "1366x768", "1600x1024", "1920x1080", "FullScreen" } : new List<string> { "800x600", "1024x768", "1366x768", "1600x1024", "1920x1080", "Максимальное" };
    public readonly decimal MassaThresholdNegative = -1.000M;
    public readonly decimal MassaThresholdPositive = 0.050M;
    public readonly decimal MassaThresholdValue = 0.010M;
    
    public string AlreadyRunning => Lang == Lang.English ? "already running" : "уже запущено";
    public string AppTitle => Lang == Lang.English ? "Label print" : "Печать этикеток";
    public string AppWaitExit => Lang == Lang.English ? "Wait for the program to complete" : "Подождите завершения программы";
    public string AppWaitLoad => Lang == Lang.English ? "Wait for the program to start" : "Подождите запуска программы";
    public string Bundle => Lang == Lang.English ? "Bundle" : "Пакет";
    public string ButtonAddKneading => Lang == Lang.English ? "Kneading" : "Замес";
    public string ButtonNewPallet => Lang == Lang.English ? $"New{Environment.NewLine}pallet" : $"Новая{Environment.NewLine}палета";
    public string ButtonPlu => Lang == Lang.English ? "PLU" : "ПЛУ";
    public string ButtonRunScalesTerminal => Lang == Lang.English ? $"Scales{Environment.NewLine}Terminal" : $"Весовой{Environment.NewLine}терминал";
    public string ButtonScaleChange(int number) => Lang == Lang.English ? $"Change ARM [{number}]" : $"Сменить АРМ [{number}]";
    public string ButtonScalesInit => Lang == Lang.English ? "Initialize scales" : "Инициализировать весы";
    public string ButtonScalesInitShort => ">0<";
    public string ButtonSelectOrder => Lang == Lang.English ? "Order" : "Заказ";
    public string ButtonSelectPlu(int count) => Lang == Lang.English ? $"PLU{Environment.NewLine}({count} pieces)" : $"ПЛУ{Environment.NewLine}({count} шт.)";
    public string ButtonSetKneading => Lang == Lang.English ? "More" : "Ещё";
    public string ButtonSettings => Lang == Lang.English ? "Settings" : "Настройки";
    public string ButtonSwitchPlu => Lang == Lang.English ? "Switch PLU" : "Смена ПЛУ";
    public string CheckPluWeightCount => Lang == Lang.English ? "Weighted products can be specified in quantities of 1 piece." : "Весовая продукция может быть указана в количестве 1 штуки.";
    public string CheckWeightBefore(decimal currentWeight) => Lang == Lang.English ? "Unload the weight platform!" + Environment.NewLine + Environment.NewLine + $"Threshold value: {MassaThresholdValue:0.000} {WeightUnitKg}." + Environment.NewLine + $"Current gross value: {currentWeight:0.000} {WeightUnitKg}." : "Разгрузите весовую платформу!" + Environment.NewLine + Environment.NewLine + $"Пороговое значение: {MassaThresholdValue:0.000} {WeightUnitKg}." + Environment.NewLine + $"Текущее значение брутто: {currentWeight:0.000} {WeightUnitKg}.";
    public string CheckWeightIsEmpty() => Lang == Lang.English ? "For products by weight, put the product on the scale!" : "Для весовой продукции следует положить продукт на весы!";
    public string CheckWeightIsZero => Lang == Lang.English ? "The platform is empty!" : "Платформа пустая!";
    public string CheckWeightThreshold(decimal weightNet) => Lang == Lang.English ? $"{WeightingControl} Product weight: {weightNet:0.000} {WeightUnitKg}" : $"{WeightingControl} Вес продукта: {weightNet:0.000} {WeightUnitKg}";
    public string CheckWeightThresholds(decimal currentNet, decimal upperWeightThreshold, decimal nominalWeight, decimal lowerWeightThreshold) => Lang == Lang.English ? $"{WeightingControl} Net weight: {currentNet:0.000} {WeightUnitKg} Upper value: {upperWeightThreshold:0.000} {WeightUnitKg} Nominal value: {nominalWeight:0.000} {WeightUnitKg} Lower value: {lowerWeightThreshold:0.000} {WeightUnitKg}" : $"{WeightingControl} Вес нетто: {currentNet:0.000} {WeightUnitKg} Верхнее значение: {upperWeightThreshold:0.000} {WeightUnitKg} Номинальное значение: {nominalWeight:0.000} {WeightUnitKg} Нижнее значение: {lowerWeightThreshold:0.000} {WeightUnitKg}";
    public string CommunicateWithAdmin => Lang == Lang.English ? "Contact your system administrator." : "Свяжитесь с администратором системы.";
    public string ComPort => Lang == Lang.English ? "COM-port" : "COM-порт";
    public string ComPortState => Lang == Lang.English ? "COM-port status" : "Состояние COM-порта";
    public string Crc => "CRC";
    public string Default => Lang == Lang.English ? "Default" : "По-умолчанию";
    public string DeviceControlIsPreview => Lang == Lang.English ? "Open a preview-version of device management?" : "Открыть превью-версию управления устройствами?";
    public string Exception => Lang == Lang.English ? "Exception" : "Ошибка";
    public string ExceptionSqlDb => Lang == Lang.English ? "The database is unavailable!" : "База данных недоступна!";
    public string FieldCurrentTime => Lang == Lang.English ? "Now" : "Сейчас";
    public string FieldDate => Lang == Lang.English ? "Date" : "Дата";
    public string FieldKneading => Lang == Lang.English ? "Kneading" : "Замес";
    public string FieldPalletSize => Lang == Lang.English ? "Pallet size" : "Размер паллеты";
    public string FieldProductDate => Lang == Lang.English ? "Date of production" : "Дата производства";
    public string FieldSscc => Lang == Lang.English ? "Serialized Shipping Container Code" : "Код транспортной упаковки";
    public string FieldSsccControlNumber => Lang == Lang.English ? "Control number" : "Контрольное число";
    public string FieldSsccGln => Lang == Lang.English ? "GLN" : "Код GLN";
    public string FieldSsccShort => Lang == Lang.English ? "SSCC" : "Код ТУ";
    public string FieldSsccSynonym => Lang == Lang.English ? "Synonym" : "Синоним";
    public string FieldSsccUnitId => Lang == Lang.English ? "Unit ID" : "ИД юнита";
    public string FieldSsccUnitType => Lang == Lang.English ? "Unit type" : "Тип юнита";
    public string FieldThresholdLower => Lang == Lang.English ? "lower" : "нижнее";
    public string FieldThresholdNominal => Lang == Lang.English ? "nominal" : "номинальное";
    public string FieldThresholds => Lang == Lang.English ? "Weight thresholds" : "Пороговые значения веса";
    public string FieldThresholdUpper => Lang == Lang.English ? "upper" : "верхнее";
    public string FieldTime => Lang == Lang.English ? "Time" : "Время";
    public string FieldWeightNetto => Lang == Lang.English ? "Net weight" : "Вес нетто";
    public string FieldWeightTare => Lang == Lang.English ? "Tare weight" : "Вес тары";
    public string HostNotFound(string deviceName) => Lang == Lang.English ? $"Host '{deviceName}' not detected." : $"Хост '{deviceName}' не обнаружен.";
    public string HostQuestionWriteToDb(string deviceName) => Lang == Lang.English ? $"Add new host '{deviceName}' into the DB?" : $"Добавить новый хост '{deviceName}' в БД?";
    public string HostUidNotFound => Lang == Lang.English ? "Host UID not detected." : "УИД хоста не обнаружен.";
    public string HostUidQuestionWriteToDb => Lang == Lang.English ? "Write a new UID into the database table?" : "Записать новый УИД в таблицу БД?";
    public string HostUidQuestionWriteToFile => Lang == Lang.English ? "Write a new UID to the file?" : "Записать новый УИД в файл?";
    public string IsConnectWithMassa => Lang == Lang.English ? "The scales are connected." : "Весы подключены.";
    public string IsDataNotExists => Lang == Lang.English ? "Check the connection of the scales!" : "Проверьте подключение весов!";
    public string IsException(string? message) => Lang == Lang.English ? $"Error! {message}" : $"Ошибка! {message}";
    public string IsNotConnectWithMassa => Lang == Lang.English ? "Check the connection of the USB-adapter!" : "Проверьте подключение ЮСБ-адапетра!";
    public string LabelContextExpirationDt => Lang == Lang.English ? "Good to" : "Годен до";
    public string LabelContextKneading => Lang == Lang.English ? "Kneading" : "Замес";
    public string LabelContextNesting => Lang == Lang.English ? "Nesting" : "Вложенность";
    public string LabelContextPlu => Lang == Lang.English ? "PLU" : "ПЛУ";
    public string LabelContextProductDt => Lang == Lang.English ? "Date of production" : "Дата изготовления";
    public string LabelContextWeight => Lang == Lang.English ? "Weight" : "Вес";
    public string LabelContextWorkShop => Lang == Lang.English ? "WorkShop/Line" : "Цех/Линия";
    public string Labels => Lang == Lang.English ? "Labels" : "Этикетки";
    public string Line => Lang == Lang.English ? "Ling" : "Строка";
    public string MassaDevice => Lang == Lang.English ? "Massa-K" : "Масса-К";
    public string MassaExchange => Lang == Lang.English ? "Massa exchange" : "Масса обмен";
    public string MassaIsNotCalc => Lang == Lang.English ? "The weight has not been calculated!" : "Вес не расчитан!";
    public string MassaIsNotFound => Lang == Lang.English ? "The scales has not been found!" : "Весы не обнаружены!";
    public string MassaIsNotQuering => Lang == Lang.English ? "The scales are not respond!" : "Весы не отвечают!";
    public string MassaIsNotRespond => Lang == Lang.English ? "The scales Massa-K are not respond!" : "Весы Масса-К не отвечают!";
    public string MassaK => Lang == Lang.English ? "Scales Massa-K" : "Весы Масса-К";
    public string MassaManager => Lang == Lang.English ? "Massa manager" : "Менеджер массы";
    public string MassaWaitStable => Lang == Lang.English ? "Wait for the weight platform to stabilize." : "Дождитесь стабилизации весовой платформы.";
    public string Memory => Lang == Lang.English ? "Memory" : "Память";
    public string MemoryAll => Lang == Lang.English ? "all" : "всего";
    public string MemoryBusy => Lang == Lang.English ? "busy" : "занято";
    public string MemoryFree => Lang == Lang.English ? "free" : "свободно";
    public string MemoryPhysical => Lang == Lang.English ? "Physical memory" : "Физическая память";
    public string MemoryVirtual => Lang == Lang.English ? "Virtual memory" : "Виртуальная память";
    public string Message => Lang == Lang.English ? "Message" : "Сообщение";
    public string Method => Lang == Lang.English ? "Method" : "Метод";
    public string OperationControl => Lang == Lang.English ? "Control of operations" : "Контроль операций";
    public string PackagedInModifiedAtmosphere => Lang == Lang.English ? "Packaged in modified atmosphere" : "Упаковано в модифицированной атмосфере";
    public string Plu => Lang == Lang.English ? "PLU" : "ПЛУ";
    public string PluCode => Lang == Lang.English ? "Code" : "Код";
    public string PluCodeNotSet => Lang == Lang.English ? "Code is not set" : "Код не задан";
    public string PluCount => Lang == Lang.English ? "PLU (count)" : "ПЛУ (шт)";
    public string PluDescriptionNotSet => Lang == Lang.English ? "Descr is not set" : "Описание не задано";
    public string PluDescriptionSet => Lang == Lang.English ? "Descr is not set" : "Описание задано";
    public string PluGtin => Lang == Lang.English ? "GTIN" : "ГТИН";
    public string PluGtinIsNotSet => Lang == Lang.English ? "GTIN is not set" : "ГТИН не задан";
    public string PluIsPiece => Lang == Lang.English ? "pcs." : "шт";
    public string PluIsWeight => Lang == Lang.English ? "weight" : "вес";
    public string PluNotSelect => Lang == Lang.English ? "PLU is not selected!" : "ПЛУ не выбрана!";
    public string PluNotSelectWeight => Lang == Lang.English ? "Weight PLU is not selected!" : "Весовая ПЛУ не выбрана!";
    public string PluPackageNotSelect => Lang == Lang.English ? "PLU nesting is not selected!" : "Тара ПЛУ не выбрана!";
    public string PluPage => Lang == Lang.English ? "Page" : "Страница";
    public string PluTemplateNotSet => Lang == Lang.English ? "Template is not set!" : "Шаблон не задан!";
    public string PluTemplateSet => Lang == Lang.English ? "Template is not set" : "Шаблон задан";
    public string PluWeight => Lang == Lang.English ? "PLU (weight)" : "ПЛУ (вес)";
    public string ProgramExit => Lang == Lang.English ? "Ending the program ..." : "Завершение программы ...";
    public string ProgramLoad => Lang == Lang.English ? "Loading the program ..." : "Загрузка программы ...";
    public string ProgramNotFound(string fileName) => Lang == Lang.English ? "Program not found!" + Environment.NewLine + fileName + Environment.NewLine + "Contact your system administrator." : "Программа не найдена!" + Environment.NewLine + fileName + Environment.NewLine + "Обратитесь к системному администратору.";
    public string QuestionCloseApp => Lang == Lang.English ? "Close the app" : "Завершить приложение";
    public string QuestionPerformOperation => Lang == Lang.English ? "Perform the operation?" : "Выполнить операцию?";
    public string QuestionRunApp => Lang == Lang.English ? "Run the app" : "Запустить приложение";
    public string QuestionWriteToDb => Lang == Lang.English ? "Add new record into the DB?" : "Добавить новую запись в БД?";
    public string Registration => Lang == Lang.English ? "Device registration" : "Регистрация устройства";
    public string RegistrationSuccess(string deviceName, string scaleName) => Lang == Lang.English ? $"Host '{deviceName}' and ARM '{scaleName}' are found." : $"Хост '{deviceName}' и АРМ '{scaleName}' найдены.";
    public string RegistrationWarning(Guid uid) => Lang == Lang.English ? $"Host UID: {uid}. Before restarting, map the host to an ARM in the DeviceControl application." : $"УИД хоста: {uid}. Перед повторным запуском, сопоставьте хост с АРМом в приложении DeviceControl.";
    public string RegistrationWarningHostNotFound(string deviceName) => Lang == Lang.English ? $"Host '{deviceName}' not found!" : $"Хост '{deviceName}' не найден!";
    public string RegistrationWarningScaleNotFound(string deviceName) => Lang == Lang.English ? $"ARM for host '{deviceName}' not found!" : $"АРМ для хоста '{deviceName}' не найден!";
    public string RequestParameters => Lang == Lang.English ? "Request parameters" : "Запрос параметров";
    public string Restore => Lang == Lang.English ? "Restore" : "Восстановить";
    public string RestoreDevice => Lang == Lang.English ? "Restore device" : "Восстановить устроство";
    public string ScaleQueue => Lang == Lang.English ? "Scales message queue" : "Очередь сообщений весов";
    public string ScheduleForNextDay => Lang == Lang.English ? "Schedule for the next day" : "Расписание на следующий день";
    public string ScheduleForNextHour => Lang == Lang.English ? "Schedule for the next hour" : "Расписание на следующий час";
    public string ScreenResolution => Lang == Lang.English ? "Screen resolution" : "Разрешение экрана";
    public string SetPlu(int number, string name) => Lang == Lang.English ? $"Switch PLU {number} | {name}" : $"Смена ПЛУ {number} | {name}";
    public string SetPluNesting(int number, string name, short bundleCount) => Lang == Lang.English ? $"Switch PLU nesting {number} | {name} | {bundleCount}" : $"Смена вложенности ПЛУ {number} | {name} | {bundleCount}";
    public string SetLine(long id, string name) => Lang == Lang.English ? $"Switch Line {id} | {name}" : $"Смена линии {id} | {name}";
    public string ShippingLabels => Lang == Lang.English ? "Shipping labels" : "Транспортные этикетки";
    public string StateCorrect => Lang == Lang.English ? "correct" : "верна";
    public string StateDisable => Lang == Lang.English ? "disable" : "отключено";
    public string StateError => Lang == Lang.English ? "error" : "ошибка";
    public string StateIsNotResponsed => Lang == Lang.English ? "is not responsed" : "нет ответа";
    public string StateIsResponsed => Lang == Lang.English ? "is responsed" : "есть ответ";
    public string ThreadId => "ID";
    public string ThreadIsBackground => Lang == Lang.English ? "Is background" : "Фоновый";
    public string ThreadName => Lang == Lang.English ? "Name" : "Имя";
    public string ThreadPriorityLevel => Lang == Lang.English ? "Priority level" : "Приоритет";
    public string Threads => Lang == Lang.English ? "Threads" : "Потоки";
    public string ThreadsCount => Lang == Lang.English ? "Threads count" : "Количество потоков";
    public string ThreadStartTime => Lang == Lang.English ? "Start time" : "Время запуска";
    public string ThreadState => Lang == Lang.English ? "State" : "Состояние";
    public string UnitWeight => Lang == Lang.English ? "weight" : "вес";
    public string WaitAppComplete => Lang == Lang.English ? "Wait for application to complete" : "Ожидайте завершения приложения";
    public string WeightingControl => Lang == Lang.English ? "The weight is out of bounds!" : "Вес выходит за границы!";
    public string WeightingIsCalc => Lang == Lang.English ? "Stable is calculated" : "Рассчитывается вес";
    public string WeightingIsStableDescription => Lang == Lang.English ? "Scales are stable | Gross weight" : "Весы стабильны | Вес брутто";
    public string WeightingIsStableFlag => Lang == Lang.English ? "Stable" : "Стабилизация";
    public string WeightingIsStableNo => Lang == Lang.English ? "Scale are not stable" : "Весы не стабильны";
    public string WeightingIsStableYes => Lang == Lang.English ? "Scale are stable" : "Весы стабильны";
    public string WeightingMessage => Lang == Lang.English ? "Weighting message" : "Сообщение взвешивания";
    public string WeightingProcess => Lang == Lang.English ? "Weighing | Gross weight" : "Взвешивание | Вес брутто";
    public string WeightingScaleCmd => Lang == Lang.English ? "Command for scales" : "Команда для весов";
    public string WeightUnitGr => Lang == Lang.English ? "gr" : "гр";
    public string WeightUnitKg => Lang == Lang.English ? "kg" : "кг";
    public string WeightUnitPcs => Lang == Lang.English ? "pcs." : "шт.";

    #endregion
}