// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;

namespace DataCore.Localizations
{
    public class LocaleScale
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LocaleScale _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LocaleScale Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        #region Public and private fields and properties

        public List<string> ListLanguages => Lang == ShareEnums.Lang.English ? new List<string> { "Russian", "English" } : new List<string> { "Russian", "English" };
        public List<string> ListResolutions => Lang == ShareEnums.Lang.English ? new List<string> { "800x600", "1024x768", "1366x768", "1600x1024", "1920x1080", "Maximum" } : new List<string> { "800x600", "1024x768", "1366x768", "1600x1024", "1920x1080", "Максимальное" };
        public readonly decimal MassaThresholdNegative = -1.000M;
        public readonly decimal MassaThresholdPositive = 0.050M;
        public readonly decimal MassaThresholdValue = 0.010M;
        public string AlreadyRunning => Lang == ShareEnums.Lang.English ? "already running" : "уже запущено";
        public string ButtonAddKneading => Lang == ShareEnums.Lang.English ? "Kneading" : "Замес";
        public string ButtonScaleChange => Lang == ShareEnums.Lang.English ? $"Change ARM" : $"Сменить АРМ";
        public string ButtonNewPallet => Lang == ShareEnums.Lang.English ? $"New{Environment.NewLine}pallet" : $"Новая{Environment.NewLine}палета";
        public string ButtonRunScalesTerminal => Lang == ShareEnums.Lang.English ? $"Scales{Environment.NewLine}Terminal" : $"Весовой{Environment.NewLine}терминал";
        public string ButtonScalesInit => Lang == ShareEnums.Lang.English ? $"Initialize scales" : $"Инициализировать весы";
        public string ButtonScalesInitShort => $">0<";
        public string ButtonSelectOrder => Lang == ShareEnums.Lang.English ? $"Order" : $"Заказ";
        public string ButtonSelectPlu(ushort count) => Lang == ShareEnums.Lang.English ? $"PLU{Environment.NewLine}({count} pieces)" : $"ПЛУ{Environment.NewLine}({count} шт.)";
        public string ButtonSetKneading => Lang == ShareEnums.Lang.English ? "More" : "Ещё";
        public string ButtonSettings => Lang == ShareEnums.Lang.English ? "Settings" : "Настройки";
        public string CheckPluWeightCount => Lang == ShareEnums.Lang.English ? "Weighted products can be specified in quantities of 1 piece." : "Весовая продукция может быть указана в количестве 1 штуки.";
        public string CheckWeightBefore(decimal currentWeight) => Lang == ShareEnums.Lang.English ? "Unload the weight platform!" + Environment.NewLine + Environment.NewLine + $"Threshold value: {MassaThresholdValue:0.000} {UnitKg}." + Environment.NewLine + $"Current gross value: {currentWeight:0.000} {UnitKg}." : "Разгрузите весовую платформу!" + Environment.NewLine + Environment.NewLine + $"Пороговое значение: {MassaThresholdValue:0.000} {UnitKg}." + Environment.NewLine + $"Текущее значение брутто: {currentWeight:0.000} {UnitKg}.";
        public string CheckWeightIsEmpty() => Lang == ShareEnums.Lang.English ? "For products by weight, put the product on the scale!" + Environment.NewLine + $"Label printing is not possible!" : "Для весовой продукции следует положить продукт на весы!" + Environment.NewLine + $"Печать этикетки невозможна!";
        public string CheckWeightThreshold(decimal weightNet) => Lang == ShareEnums.Lang.English ? WeightingControl + Environment.NewLine + $"Product weight: {weightNet:0.000} {UnitKg}" + Environment.NewLine + $"Label printing is not possible!" : WeightingControl + Environment.NewLine + $"Вес продукта: {weightNet:0.000} {UnitKg}" + Environment.NewLine + $"Печать этикетки невозможна!";
        public string CheckWeightThresholds(decimal currentNet, decimal upperWeightThreshold, decimal nominalWeight, decimal lowerWeightThreshold) => Lang == ShareEnums.Lang.English ? WeightingControl + Environment.NewLine + $"Net weight: {currentNet:0.000} {UnitKg}" + Environment.NewLine + $"Upper weight value: {upperWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Nominal weight value: {nominalWeight:0.000} {UnitKg}" + Environment.NewLine + $"Lower weight value: {lowerWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Label printing is not possible!" : WeightingControl + Environment.NewLine + $"Вес нетто: {currentNet:0.000} {UnitKg}" + Environment.NewLine + $"Верхнее значение веса: {upperWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Номинальное значение веса: {nominalWeight:0.000} {UnitKg}" + Environment.NewLine + $"Нижнее значение веса: {lowerWeightThreshold:0.000} {UnitKg}" + Environment.NewLine + $"Печать этикетки невозможна!";
        public string CommunicateWithAdmin => Lang == ShareEnums.Lang.English ? "Contact your system administrator." : "Свяжитесь с администратором системы.";
        public string ComPort => Lang == ShareEnums.Lang.English ? "COM-port" : "COM-порт";
        public string ComPortState => Lang == ShareEnums.Lang.English ? "COM-port status" : "Состояние COM-порта";
        public string Crc => "CRC";
        public string Apply => Lang == ShareEnums.Lang.English ? $"Apply" : $"Применить";
        public string Default => Lang == ShareEnums.Lang.English ? $"Default" : $"По-умолчанию";
        public string DeviceControlIsPreview => Lang == ShareEnums.Lang.English ? "Open a preview-version of device management?" : "Открыть превью-версию управления устройствами?";
        public string Exception => Lang == ShareEnums.Lang.English ? "Exception" : "Ошибка";
        public string ExceptionSqlDb => Lang == ShareEnums.Lang.English ? "The database is unavailable!" : "База данных недоступна!";
        public string FieldCurrentTime => Lang == ShareEnums.Lang.English ? "Now" : "Сейчас";
        public string FieldDate => Lang == ShareEnums.Lang.English ? "Date" : "Дата";
        public string FieldKneading => Lang == ShareEnums.Lang.English ? "Kneading" : "Замес";
        public string FieldPalletSize => Lang == ShareEnums.Lang.English ? "Pallet size" : "Размер паллеты";
        public string FieldProductDate => Lang == ShareEnums.Lang.English ? "Date of production" : "Дата производства";
        public string FieldSscc => Lang == ShareEnums.Lang.English ? "Serialized Shipping Container Code" : "Код транспортной упаковки";
        public string FieldSsccControlNumber => Lang == ShareEnums.Lang.English ? "Control number" : "Контрольное число";
        public string FieldSsccGln => Lang == ShareEnums.Lang.English ? "GLN" : "Код GLN";
        public string FieldSsccShort => Lang == ShareEnums.Lang.English ? "SSCC" : "Код ТУ";
        public string FieldSsccSynonym => Lang == ShareEnums.Lang.English ? "Synonym" : "Синоним";
        public string FieldSsccUnitId => Lang == ShareEnums.Lang.English ? "Unit ID" : "ИД юнита";
        public string FieldSsccUnitType => Lang == ShareEnums.Lang.English ? "Unit type" : "Тип юнита";
        public string FieldThresholdLower => Lang == ShareEnums.Lang.English ? "lower" : "нижнее";
        public string FieldThresholdNominal => Lang == ShareEnums.Lang.English ? "nominal" : "номинальное";
        public string FieldThresholds => Lang == ShareEnums.Lang.English ? "Weight thresholds" : "Пороговые значения веса";
        public string FieldThresholdUpper => Lang == ShareEnums.Lang.English ? "upper" : "верхнее";
        public string FieldTime => Lang == ShareEnums.Lang.English ? "Time" : "Время";
        public string FieldWeightNetto => Lang == ShareEnums.Lang.English ? "Net weight" : "Вес нетто";
        public string FieldWeightTare => Lang == ShareEnums.Lang.English ? "Tare weight" : "Вес тары";
        public string HostNotFound(string hostName) => Lang == ShareEnums.Lang.English ? $"Host '{hostName}' not detected." : $"Хост '{hostName}' не обнаружен.";
        public string HostQuestionWriteToDb(string hostName) => Lang == ShareEnums.Lang.English ? $"Add new host '{hostName}' into the DB?" : $"Добавить новый хост '{hostName}' в БД?";
        public string HostUidNotFound => Lang == ShareEnums.Lang.English ? "Host UID not detected." : "УИД хоста не обнаружен.";
        public string HostUidQuestionWriteToDb => Lang == ShareEnums.Lang.English ? "Write a new UID into the database table?" : "Записать новый УИД в таблицу БД?";
        public string HostUidQuestionWriteToFile => Lang == ShareEnums.Lang.English ? "Write a new UID to the file?" : "Записать новый УИД в файл?";
        public string IsConnectWithMassa => Lang == ShareEnums.Lang.English ? "The scales are connected." : "Весы подключены.";
        public string IsDataNotExists => Lang == ShareEnums.Lang.English ? "Check the connection of the scales!" : "Проверьте подключение весов!";
        public string IsException(string message) => Lang == ShareEnums.Lang.English ? $"Error! {message}" : $"Ошибка! {message}";
        public string IsNotConnectWithMassa => Lang == ShareEnums.Lang.English ? "Check the connection of the USB-adapter!" : "Проверьте подключение ЮСБ-адапетра!";
        public string Labels => Lang == ShareEnums.Lang.English ? "Labels" : "Этикетки";
        public string Line => Lang == ShareEnums.Lang.English ? "Ling" : "Строка";
        public string MassaDevice => Lang == ShareEnums.Lang.English ? "Massa-K" : "Масса-К";
        public string MassaExchange => Lang == ShareEnums.Lang.English ? "Massa exchange" : "Масса обмен";
        public string MassaIsNotCalc => Lang == ShareEnums.Lang.English ? "The weight has not been calculated!" : "Вес не расчитан!";
        public string MassaIsNotFound => Lang == ShareEnums.Lang.English ? "The scales has not been found!" : "Весы не обнаружены!";
        public string MassaIsNotQuering => Lang == ShareEnums.Lang.English ? "The scales are not respond!" : "Весы не отвечают!";
        public string MassaIsNotRespond => Lang == ShareEnums.Lang.English ? $"The scales Massa-K are not respond!" : $"Весы Масса-К не отвечают!";
        public string MassaManager => Lang == ShareEnums.Lang.English ? "Massa manager" : "Менеджер массы";
        public string MassaWaitStable => Lang == ShareEnums.Lang.English ? "Wait for the weight platform to stabilize." : "Дождитесь стабилизации весовой платформы.";
        public string Memory => Lang == ShareEnums.Lang.English ? "Memory" : "Память";
        public string MemoryAll => Lang == ShareEnums.Lang.English ? "all" : "всего";
        public string MemoryBusy => Lang == ShareEnums.Lang.English ? "busy" : "занято";
        public string MemoryFree => Lang == ShareEnums.Lang.English ? "free" : "свободно";
        public string MemoryPhysical => Lang == ShareEnums.Lang.English ? "Physical memory" : "Физическая память";
        public string MemoryVirtual => Lang == ShareEnums.Lang.English ? "Virtual memory" : "Виртуальная память";
        public string Message => Lang == ShareEnums.Lang.English ? "Message" : "Сообщение";
        public string Method => Lang == ShareEnums.Lang.English ? "Method" : "Метод";
        public string OperationControl => Lang == ShareEnums.Lang.English ? "Control of operations" : "Контроль операций";
        public string Plu => Lang == ShareEnums.Lang.English ? "PLU" : "ПЛУ";
        public string PluCount => Lang == ShareEnums.Lang.English ? "PLU (count)" : "ПЛУ (шт)";
        public string PluNotSelect => Lang == ShareEnums.Lang.English ? "PLU is not selected!" : "ПЛУ не выбрана!";
        public string PluNotSelectWeight => Lang == ShareEnums.Lang.English ? "Weight PLU is not selected!" : "Весовая ПЛУ не выбрана!";
        public string PluPage => Lang == ShareEnums.Lang.English ? "Page" : "Страница";
        public string PluSet(long id, int number, string name) => Lang == ShareEnums.Lang.English ? $"Selected PLU: {id} | {number} | {name}" : $"Выбрана ПЛУ: {id} | {number} | {name}";
        public string PluWeight => Lang == ShareEnums.Lang.English ? "PLU (weight)" : "ПЛУ (вес)";
        public string ProgramExit => Lang == ShareEnums.Lang.English ? "Ending the program ..." : "Завершение программы ...";
        public string ProgramLoad => Lang == ShareEnums.Lang.English ? "Loading the program ..." : "Загрузка программы ...";
        public string ProgramNotFound(string fileName) => Lang == ShareEnums.Lang.English ? "Program not found!" + Environment.NewLine + fileName + Environment.NewLine + "Contact your system administrator." : "Программа не найдена!" + Environment.NewLine + fileName + Environment.NewLine + "Обратитесь к системному администратору.";
        public string QuestionPerformOperation => Lang == ShareEnums.Lang.English ? "Perform the operation?" : "Выполнить операцию?";
        public string QuestionRunApp => Lang == ShareEnums.Lang.English ? "Run the app" : "Запустить приложение";
        public string QuestionWriteToDb => Lang == ShareEnums.Lang.English ? "Add new record into the DB?" : "Добавить новую запись в БД?";
        public string Registration => Lang == ShareEnums.Lang.English ? "Device registration" : "Регистрация устройства";
        public string RegistrationSuccess(string hostName, string scaleName) => Lang == ShareEnums.Lang.English ? $"Host '{hostName}' and ARM '{scaleName}' are found." : $"Хост '{hostName}' и АРМ '{scaleName}' найдены.";
        public string RegistrationWarning(Guid uid) => Lang == ShareEnums.Lang.English ? $"Host UID: {uid}. Before restarting, map the host to an ARM in the DeviceControl application." : $"УИД хоста: {uid}. Перед повторным запуском, сопоставьте хост с АРМом в приложении DeviceControl.";
        public string RegistrationWarningHostNotFound(string hostName) => Lang == ShareEnums.Lang.English ? $"Host '{hostName}' not found!" : $"Хост '{hostName}' не найден!";
        public string RegistrationWarningScaleNotFound(string hostName) => Lang == ShareEnums.Lang.English ? $"ARM for host '{hostName}' not found!" : $"АРМ для хоста '{hostName}' не найден!";
        public string RequestParameters => Lang == ShareEnums.Lang.English ? "Request parameters" : "Запрос параметров";
        public string Restore => Lang == ShareEnums.Lang.English ? $"Restore" : $"Восстановить";
        public string RestoreDevice => Lang == ShareEnums.Lang.English ? $"Restore device" : $"Восстановить устроство";
        public string ScaleQueue => Lang == ShareEnums.Lang.English ? "Scales message queue" : "Очередь сообщений весов";
        public string ScheduleForNextDay => Lang == ShareEnums.Lang.English ? "Schedule for next day" : "Расписание на следующий день";
        public string ScreenResolution => Lang == ShareEnums.Lang.English ? "Screen resolution" : "Разрешение экрана";
        public string ShippingLabels => Lang == ShareEnums.Lang.English ? "Shipping labels" : "Транспортные этикетки";
        public string StateCorrect => Lang == ShareEnums.Lang.English ? "correct" : "верна";
        public string StateDisable => Lang == ShareEnums.Lang.English ? "disable" : "отключено";
        public string StateError => Lang == ShareEnums.Lang.English ? "error" : "ошибка";
        public string StateNotResponsed => Lang == ShareEnums.Lang.English ? "not responsed" : "не отвечает";
        public string StateResponsed => Lang == ShareEnums.Lang.English ? "responsed" : "отвечает";
        public string ThreadId => "ID";
        public string ThreadIsBackground => Lang == ShareEnums.Lang.English ? "Is background" : "Фоновый";
        public string ThreadName => Lang == ShareEnums.Lang.English ? "Name" : "Имя";
        public string ThreadPriorityLevel => Lang == ShareEnums.Lang.English ? "Priority level" : "Приоритет";
        public string Threads => Lang == ShareEnums.Lang.English ? "Threads" : "Потоки";
        public string ThreadsCount => Lang == ShareEnums.Lang.English ? "Threads count" : "Количество потоков";
        public string ThreadStartTime => Lang == ShareEnums.Lang.English ? "Start time" : "Время запуска";
        public string ThreadState => Lang == ShareEnums.Lang.English ? "State" : "Состояние";
        public string UnitKg => Lang == ShareEnums.Lang.English ? "kg" : "кг";
        public string UnitPcs => Lang == ShareEnums.Lang.English ? "pcs." : "шт.";
        public string UnitWeight => Lang == ShareEnums.Lang.English ? "weight" : "вес";
        public string WeightingControl => Lang == ShareEnums.Lang.English ? "The weight is out of bounds!" : "Вес выходит за границы!";
        public string WeightingIsCalc => Lang == ShareEnums.Lang.English ? "Stable is calculated" : "Рассчитывается вес";
        public string WeightingIsStableDescription => Lang == ShareEnums.Lang.English ? "Scales are stable | Gross weight" : "Весы стабильны | Вес брутто";
        public string WeightingIsStableFlag => Lang == ShareEnums.Lang.English ? "Stable" : "Стабилизация";
        public string WeightingIsStableNo => Lang == ShareEnums.Lang.English ? "Scale are not stable" : "Весы не стабильны";
        public string WeightingIsStableYes => Lang == ShareEnums.Lang.English ? "Scale are stable" : "Весы стабильны";
        public string WeightingMessage => Lang == ShareEnums.Lang.English ? "Weighting message" : "Сообщение взвешивания";
        public string WeightingProcess => Lang == ShareEnums.Lang.English ? "Weighing | Gross weight" : "Взвешивание | Вес брутто";
        public string WeightingScaleCmd => Lang == ShareEnums.Lang.English ? "Command for scales" : "Команда для весов";

        #endregion
    }
}
