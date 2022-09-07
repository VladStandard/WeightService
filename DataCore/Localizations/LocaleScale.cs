// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleScale
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleScale _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleScale Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public List<string> ListLanguages => Lang == LangEnum.English ? new List<string> { "Russian", "English" } : new List<string> { "Russian", "English" };
    public List<string> ListResolutions => Lang == LangEnum.English ? new List<string> { "800x600", "1024x768", "1366x768", "1600x1024", "1920x1080", "Maximum" } : new List<string> { "800x600", "1024x768", "1366x768", "1600x1024", "1920x1080", "Максимальное" };
    public readonly decimal MassaThresholdNegative = -1.000M;
    public readonly decimal MassaThresholdPositive = 0.050M;
    public readonly decimal MassaThresholdValue = 0.010M;
    public string AlreadyRunning => Lang == LangEnum.English ? "already running" : "уже запущено";
    public string ButtonAddKneading => Lang == LangEnum.English ? "Kneading" : "Замес";
    public string ButtonNewPallet => Lang == LangEnum.English ? $"New{Environment.NewLine}pallet" : $"Новая{Environment.NewLine}палета";
    public string ButtonRunScalesTerminal => Lang == LangEnum.English ? $"Scales{Environment.NewLine}Terminal" : $"Весовой{Environment.NewLine}терминал";
    public string ButtonScaleChange(int number) => Lang == LangEnum.English ? $"Change ARM [{number}]" : $"Сменить АРМ [{number}]";
    public string ButtonScalesInit => Lang == LangEnum.English ? $"Initialize scales" : $"Инициализировать весы";
    public string ButtonScalesInitShort => $">0<";
    public string ButtonSelectOrder => Lang == LangEnum.English ? $"Order" : $"Заказ";
    public string ButtonSelectPlu(ushort count) => Lang == LangEnum.English ? $"PLU{Environment.NewLine}({count} pieces)" : $"ПЛУ{Environment.NewLine}({count} шт.)";
    public string ButtonSetKneading => Lang == LangEnum.English ? "More" : "Ещё";
    public string ButtonSettings => Lang == LangEnum.English ? "Settings" : "Настройки";
    public string CheckPluWeightCount => Lang == LangEnum.English ? "Weighted products can be specified in quantities of 1 piece." : "Весовая продукция может быть указана в количестве 1 штуки.";
    public string CheckWeightBefore(decimal currentWeight) => Lang == LangEnum.English ? "Unload the weight platform!" + Environment.NewLine + Environment.NewLine + $"Threshold value: {MassaThresholdValue:0.000} {UnitKg}." + Environment.NewLine + $"Current gross value: {currentWeight:0.000} {UnitKg}." : "Разгрузите весовую платформу!" + Environment.NewLine + Environment.NewLine + $"Пороговое значение: {MassaThresholdValue:0.000} {UnitKg}." + Environment.NewLine + $"Текущее значение брутто: {currentWeight:0.000} {UnitKg}.";
    public string CheckWeightIsEmpty() => Lang == LangEnum.English ? "For products by weight, put the product on the scale!" + Environment.NewLine + $"Label printing is not possible!" : "Для весовой продукции следует положить продукт на весы!" + Environment.NewLine + $"Печать этикетки невозможна!";
    public string CheckWeightThreshold(decimal weightNet) => Lang == LangEnum.English ? WeightingControl + Environment.NewLine + $"Product weight: {weightNet:0.000} {UnitKg}" + Environment.NewLine + $"Label printing is not possible!" : WeightingControl + Environment.NewLine + $"Вес продукта: {weightNet:0.000} {UnitKg}" + Environment.NewLine + $"Печать этикетки невозможна!";
    public string CheckWeightThresholds(decimal currentNet, decimal upperWeightThreshold, decimal nominalWeight, decimal lowerWeightThreshold) => Lang == LangEnum.English ? WeightingControl + Environment.NewLine + $"Net weight: {currentNet:0.000} {UnitKg}" + Environment.NewLine + $"Upper weight value: {upperWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Nominal weight value: {nominalWeight:0.000} {UnitKg}" + Environment.NewLine + $"Lower weight value: {lowerWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Label printing is not possible!" : WeightingControl + Environment.NewLine + $"Вес нетто: {currentNet:0.000} {UnitKg}" + Environment.NewLine + $"Верхнее значение веса: {upperWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Номинальное значение веса: {nominalWeight:0.000} {UnitKg}" + Environment.NewLine + $"Нижнее значение веса: {lowerWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Печать этикетки невозможна!";
    public string CommunicateWithAdmin => Lang == LangEnum.English ? "Contact your system administrator." : "Свяжитесь с администратором системы.";
    public string ComPort => Lang == LangEnum.English ? "COM-port" : "COM-порт";
    public string ComPortState => Lang == LangEnum.English ? "COM-port status" : "Состояние COM-порта";
    public string Crc => "CRC";
    public string Default => Lang == LangEnum.English ? $"Default" : $"По-умолчанию";
    public string DeviceControlIsPreview => Lang == LangEnum.English ? "Open a preview-version of device management?" : "Открыть превью-версию управления устройствами?";
    public string Exception => Lang == LangEnum.English ? "Exception" : "Ошибка";
    public string ExceptionSqlDb => Lang == LangEnum.English ? "The database is unavailable!" : "База данных недоступна!";
    public string FieldCurrentTime => Lang == LangEnum.English ? "Now" : "Сейчас";
    public string FieldDate => Lang == LangEnum.English ? "Date" : "Дата";
    public string FieldKneading => Lang == LangEnum.English ? "Kneading" : "Замес";
    public string FieldPalletSize => Lang == LangEnum.English ? "Pallet size" : "Размер паллеты";
    public string FieldProductDate => Lang == LangEnum.English ? "Date of production" : "Дата производства";
    public string FieldSscc => Lang == LangEnum.English ? "Serialized Shipping Container Code" : "Код транспортной упаковки";
    public string FieldSsccControlNumber => Lang == LangEnum.English ? "Control number" : "Контрольное число";
    public string FieldSsccGln => Lang == LangEnum.English ? "GLN" : "Код GLN";
    public string FieldSsccShort => Lang == LangEnum.English ? "SSCC" : "Код ТУ";
    public string FieldSsccSynonym => Lang == LangEnum.English ? "Synonym" : "Синоним";
    public string FieldSsccUnitId => Lang == LangEnum.English ? "Unit ID" : "ИД юнита";
    public string FieldSsccUnitType => Lang == LangEnum.English ? "Unit type" : "Тип юнита";
    public string FieldThresholdLower => Lang == LangEnum.English ? "lower" : "нижнее";
    public string FieldThresholdNominal => Lang == LangEnum.English ? "nominal" : "номинальное";
    public string FieldThresholds => Lang == LangEnum.English ? "Weight thresholds" : "Пороговые значения веса";
    public string FieldThresholdUpper => Lang == LangEnum.English ? "upper" : "верхнее";
    public string FieldTime => Lang == LangEnum.English ? "Time" : "Время";
    public string FieldWeightNetto => Lang == LangEnum.English ? "Net weight" : "Вес нетто";
    public string FieldWeightTare => Lang == LangEnum.English ? "Tare weight" : "Вес тары";
    public string HostNotFound(string hostName) => Lang == LangEnum.English ? $"Host '{hostName}' not detected." : $"Хост '{hostName}' не обнаружен.";
    public string HostQuestionWriteToDb(string hostName) => Lang == LangEnum.English ? $"Add new host '{hostName}' into the DB?" : $"Добавить новый хост '{hostName}' в БД?";
    public string HostUidNotFound => Lang == LangEnum.English ? "Host UID not detected." : "УИД хоста не обнаружен.";
    public string HostUidQuestionWriteToDb => Lang == LangEnum.English ? "Write a new UID into the database table?" : "Записать новый УИД в таблицу БД?";
    public string HostUidQuestionWriteToFile => Lang == LangEnum.English ? "Write a new UID to the file?" : "Записать новый УИД в файл?";
    public string IsConnectWithMassa => Lang == LangEnum.English ? "The scales are connected." : "Весы подключены.";
    public string IsDataNotExists => Lang == LangEnum.English ? "Check the connection of the scales!" : "Проверьте подключение весов!";
    public string IsException(string message) => Lang == LangEnum.English ? $"Error! {message}" : $"Ошибка! {message}";
    public string IsNotConnectWithMassa => Lang == LangEnum.English ? "Check the connection of the USB-adapter!" : "Проверьте подключение ЮСБ-адапетра!";
    public string Labels => Lang == LangEnum.English ? "Labels" : "Этикетки";
    public string Line => Lang == LangEnum.English ? "Ling" : "Строка";
    public string MassaDevice => Lang == LangEnum.English ? "Massa-K" : "Масса-К";
    public string MassaExchange => Lang == LangEnum.English ? "Massa exchange" : "Масса обмен";
    public string MassaIsNotCalc => Lang == LangEnum.English ? "The weight has not been calculated!" : "Вес не расчитан!";
    public string MassaIsNotFound => Lang == LangEnum.English ? "The scales has not been found!" : "Весы не обнаружены!";
    public string MassaIsNotQuering => Lang == LangEnum.English ? "The scales are not respond!" : "Весы не отвечают!";
    public string MassaIsNotRespond => Lang == LangEnum.English ? $"The scales Massa-K are not respond!" : $"Весы Масса-К не отвечают!";
    public string MassaManager => Lang == LangEnum.English ? "Massa manager" : "Менеджер массы";
    public string MassaWaitStable => Lang == LangEnum.English ? "Wait for the weight platform to stabilize." : "Дождитесь стабилизации весовой платформы.";
    public string Memory => Lang == LangEnum.English ? "Memory" : "Память";
    public string MemoryAll => Lang == LangEnum.English ? "all" : "всего";
    public string MemoryBusy => Lang == LangEnum.English ? "busy" : "занято";
    public string MemoryFree => Lang == LangEnum.English ? "free" : "свободно";
    public string MemoryPhysical => Lang == LangEnum.English ? "Physical memory" : "Физическая память";
    public string MemoryVirtual => Lang == LangEnum.English ? "Virtual memory" : "Виртуальная память";
    public string Message => Lang == LangEnum.English ? "Message" : "Сообщение";
    public string Method => Lang == LangEnum.English ? "Method" : "Метод";
    public string OperationControl => Lang == LangEnum.English ? "Control of operations" : "Контроль операций";
    public string Plu => Lang == LangEnum.English ? "PLU" : "ПЛУ";
    public string PluCode => Lang == LangEnum.English ? "Code" : "Код";
    public string PluCodeNotSet => Lang == LangEnum.English ? "Code is not set" : "Код не задан";
    public string PluCount => Lang == LangEnum.English ? "PLU (count)" : "ПЛУ (шт)";
    public string PluDescriptionNotSet => Lang == LangEnum.English ? "Descr is not set" : "Описание не задано";
    public string PluDescriptionSet => Lang == LangEnum.English ? "Descr is not set" : "Описание задано";
    public string PluTemplateNotSet => Lang == LangEnum.English ? "Template is not set" : "Шаблон не задан";
    public string PluTemplateSet => Lang == LangEnum.English ? "Template is not set" : "Шаблон задан";
    public string PluGtin => Lang == LangEnum.English ? "GTIN" : "ГТИН";
    public string PluGtinNotSet => Lang == LangEnum.English ? "GTIN is not set" : "ГТИН не задан";
    public string PluIsPiece => Lang == LangEnum.English ? "pcs." : "шт";
    public string PluIsWeight => Lang == LangEnum.English ? "weight" : "вес";
    public string PluNotSelect => Lang == LangEnum.English ? "PLU is not selected!" : "ПЛУ не выбрана!";
    public string PluNotSelectWeight => Lang == LangEnum.English ? "Weight PLU is not selected!" : "Весовая ПЛУ не выбрана!";
    public string PluPage => Lang == LangEnum.English ? "Page" : "Страница";
    public string PluSet(long id, int number, string name) => Lang == LangEnum.English ? $"Selected PLU: {id} | {number} | {name}" : $"Выбрана ПЛУ: {id} | {number} | {name}";
    public string PluWeight => Lang == LangEnum.English ? "PLU (weight)" : "ПЛУ (вес)";
    public string ProgramExit => Lang == LangEnum.English ? "Ending the program ..." : "Завершение программы ...";
    public string ProgramLoad => Lang == LangEnum.English ? "Loading the program ..." : "Загрузка программы ...";
    public string ProgramNotFound(string fileName) => Lang == LangEnum.English ? "Program not found!" + Environment.NewLine + fileName + Environment.NewLine + "Contact your system administrator." : "Программа не найдена!" + Environment.NewLine + fileName + Environment.NewLine + "Обратитесь к системному администратору.";
    public string QuestionPerformOperation => Lang == LangEnum.English ? "Perform the operation?" : "Выполнить операцию?";
    public string QuestionRunApp => Lang == LangEnum.English ? "Run the app" : "Запустить приложение";
    public string QuestionWriteToDb => Lang == LangEnum.English ? "Add new record into the DB?" : "Добавить новую запись в БД?";
    public string Registration => Lang == LangEnum.English ? "Device registration" : "Регистрация устройства";
    public string RegistrationSuccess(string hostName, string scaleName) => Lang == LangEnum.English ? $"Host '{hostName}' and ARM '{scaleName}' are found." : $"Хост '{hostName}' и АРМ '{scaleName}' найдены.";
    public string RegistrationWarning(Guid uid) => Lang == LangEnum.English ? $"Host UID: {uid}. Before restarting, map the host to an ARM in the DeviceControl application." : $"УИД хоста: {uid}. Перед повторным запуском, сопоставьте хост с АРМом в приложении DeviceControl.";
    public string RegistrationWarningHostNotFound(string hostName) => Lang == LangEnum.English ? $"Host '{hostName}' not found!" : $"Хост '{hostName}' не найден!";
    public string RegistrationWarningScaleNotFound(string hostName) => Lang == LangEnum.English ? $"ARM for host '{hostName}' not found!" : $"АРМ для хоста '{hostName}' не найден!";
    public string RequestParameters => Lang == LangEnum.English ? "Request parameters" : "Запрос параметров";
    public string Restore => Lang == LangEnum.English ? $"Restore" : $"Восстановить";
    public string RestoreDevice => Lang == LangEnum.English ? $"Restore device" : $"Восстановить устроство";
    public string ScaleQueue => Lang == LangEnum.English ? "Scales message queue" : "Очередь сообщений весов";
    public string ScheduleForNextDay => Lang == LangEnum.English ? "Schedule for next day" : "Расписание на следующий день";
    public string ScreenResolution => Lang == LangEnum.English ? "Screen resolution" : "Разрешение экрана";
    public string ShippingLabels => Lang == LangEnum.English ? "Shipping labels" : "Транспортные этикетки";
    public string StateCorrect => Lang == LangEnum.English ? "correct" : "верна";
    public string StateDisable => Lang == LangEnum.English ? "disable" : "отключено";
    public string StateError => Lang == LangEnum.English ? "error" : "ошибка";
    public string StateNotResponsed => Lang == LangEnum.English ? "not responsed" : "не отвечает";
    public string StateResponsed => Lang == LangEnum.English ? "responsed" : "отвечает";
    public string ThreadId => "ID";
    public string ThreadIsBackground => Lang == LangEnum.English ? "Is background" : "Фоновый";
    public string ThreadName => Lang == LangEnum.English ? "Name" : "Имя";
    public string ThreadPriorityLevel => Lang == LangEnum.English ? "Priority level" : "Приоритет";
    public string Threads => Lang == LangEnum.English ? "Threads" : "Потоки";
    public string ThreadsCount => Lang == LangEnum.English ? "Threads count" : "Количество потоков";
    public string ThreadStartTime => Lang == LangEnum.English ? "Start time" : "Время запуска";
    public string ThreadState => Lang == LangEnum.English ? "State" : "Состояние";
    public string UnitKg => Lang == LangEnum.English ? "kg" : "кг";
    public string UnitPcs => Lang == LangEnum.English ? "pcs." : "шт.";
    public string UnitWeight => Lang == LangEnum.English ? "weight" : "вес";
    public string WeightingControl => Lang == LangEnum.English ? "The weight is out of bounds!" : "Вес выходит за границы!";
    public string WeightingIsCalc => Lang == LangEnum.English ? "Stable is calculated" : "Рассчитывается вес";
    public string WeightingIsStableDescription => Lang == LangEnum.English ? "Scales are stable | Gross weight" : "Весы стабильны | Вес брутто";
    public string WeightingIsStableFlag => Lang == LangEnum.English ? "Stable" : "Стабилизация";
    public string WeightingIsStableNo => Lang == LangEnum.English ? "Scale are not stable" : "Весы не стабильны";
    public string WeightingIsStableYes => Lang == LangEnum.English ? "Scale are stable" : "Весы стабильны";
    public string WeightingMessage => Lang == LangEnum.English ? "Weighting message" : "Сообщение взвешивания";
    public string WeightingProcess => Lang == LangEnum.English ? "Weighing | Gross weight" : "Взвешивание | Вес брутто";
    public string WeightingScaleCmd => Lang == LangEnum.English ? "Command for scales" : "Команда для весов";

    #endregion
}
