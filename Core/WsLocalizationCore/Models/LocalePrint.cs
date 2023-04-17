// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public class LocalePrint
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocalePrint _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocalePrint Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

    #region Public and private fields, properties, constructor

    public string ActionPrint => Lang == Lang.English ? "Print" : "Печать";
    public string Available => Lang == Lang.English ? "available" : "доступен";
    public string ClearQueue => Lang == Lang.English ? "Clear queue" : "Очистить очередь";
    public string Communication => Lang == Lang.English ? "Communication" : "Связь";
    public string ControlPanel => Lang == Lang.English ? "Printer control panel" : "Панель управления принтером";
    public string DarknessLevel => Lang == Lang.English ? "Level of darkness" : "Уровень темноты";
    public string DeviceCheckConnect => Lang == Lang.English ? "Check the device connection." : "Проверьте подключение устройства.";
    public string DeviceCommunication => Lang == Lang.English ? "Communication with the printer" : "Связь с принтером";
    public string DeviceIsUnavailable => Lang == Lang.English ? "Printer is unavailable!" : "Принтер не доступен!";
    public string DeviceMainCheckStatus => Lang == Lang.English ? "Check the main printer status!" : "Проверьте состояние основного принтера!";
    public string DeviceMainIsUnavailable => Lang == Lang.English ? "Main printer is unavailable!" : "Основной принтер не доступен!";
    public string DeviceName => Lang == Lang.English ? "Printer name" : "Имя принтера";
    public string DeviceNameIsUnavailable => Lang == Lang.English ? "Device is unavailable" : "Устройство не доступно";
    public string DeviceNameShort => Lang == Lang.English ? "Printer" : "Принтер";
    public string DeviceShippingCheckStatus => Lang == Lang.English ? "Check the shipping printer status!" : "Проверьте состояние транспортного принтера!";
    public string DeviceShippingIsUnavailable => Lang == Lang.English ? "Shipping printer is unavailable!" : "Транспортный принтер не доступен!";
    public string Driver => Lang == Lang.English ? "Driver" : "Драйвер";
    public string ErrorPlu(int pluNumber, string goodName) => Lang == Lang.English ? $"Print error for PLU: {pluNumber} ({goodName})!" : $"Ошибка печати для ПЛУ: {pluNumber} ({goodName})";
    public string HttpStatusCode => Lang == Lang.English ? "Http status code" : "Код состояния Http";
    public string InfoCaption => Lang == Lang.English ? "Printer info" : "Информация о принтере";
    public string Ip => Lang == Lang.English ? "IP-address" : "IP-адрес";
    public string Mac => Lang == Lang.English ? "MAC-address" : "MAC-адрес";
    public string Mode => Lang == Lang.English ? "Mode" : "Режим";
    public string ModeApplicator => Lang == Lang.English ? "Applicator" : "Аппликатор";
    public string ModeCutter => Lang == Lang.English ? "Cutter" : "Отрезание";
    public string ModeDelayedCut => Lang == Lang.English ? "Delayed Cut" : "Отложенный срез";
    public string ModeKiosk => Lang == Lang.English ? "Kiosk" : "Киоск";
    public string Model => Lang == Lang.English ? "Model" : "Модель";
    public string ModeLinerlessPeel => Lang == Lang.English ? "Linerless Peel" : "Бесслойный пилинг";
    public string ModeLinerlessRewind => Lang == Lang.English ? "Linerless Rewind" : "Бесслойная перемотка";
    public string ModePartialCutter => Lang == Lang.English ? "Partial Cutter" : "Частичная резка";
    public string ModePeelOff => Lang == Lang.English ? "Peel-Off" : "Отклеивание";
    public string ModeRewind => Lang == Lang.English ? "Rewind" : "Перемотка";
    public string ModeRfid => Lang == Lang.English ? "RFID" : "РФИД";
    public string ModeTearOff => Lang == Lang.English ? "Tear-Off" : "Отрывание";
    public string ModeUnknown => Lang == Lang.English ? "Unknown" : "Неизвестный";
    public string Name => Lang == Lang.English ? "Printer" : "Принтер";
    public string NameMain => Lang == Lang.English ? "Printer" : "Принтер";
    public string NameMainTsc => Lang == Lang.English ? "Printer TSC" : "Принтер ТСК";
    public string NameMainTscShort => Lang == Lang.English ? "TSC" : "ТСК";
    public string NameMainZebra => Lang == Lang.English ? "Main printer Zebra" : "Основной принтер Зебра";
    public string NameMainZebraShort => Lang == Lang.English ? "Zebra" : "Зебра";
    public string Names => Lang == Lang.English ? "Printers" : "Принтеры";
    public string NameShipping => Lang == Lang.English ? "Shipping printer" : "Транспортный принтер";
    public string NameShippingTsc => Lang == Lang.English ? "Shipping printer TSC" : "Транспортный принтер ТСК";
    public string NameShippingTscShort => Lang == Lang.English ? "Shipping TSC" : "Транспортный ТСК";
    public string NameShippingZebra => Lang == Lang.English ? "Shipping printer Zebra" : "Транспортный принтер Зебра";
    public string NameShippingZebraShort => Lang == Lang.English ? "Shipping Zebra" : "Транспортный Зебра";
    public string NamesMain => Lang == Lang.English ? "Main printers" : "Основные принтеры";
    public string NamesShipping => Lang == Lang.English ? "Shipping printers" : "Транспортные принтеры";
    public string Password => Lang == Lang.English ? "Printer password" : "Пароль принтера";
    public string PeelOffSet => Lang == Lang.English ? "Offset" : "Смещение";
    public string Port => Lang == Lang.English ? "Printer port" : "Порт принтера";
    public string PortShort => Lang == Lang.English ? "Port" : "Порт";
    public string PrinterStatus => Lang == Lang.English ? "Printer status" : "Состояние принтера";
    public string PrintPlugin => Lang == Lang.English ? "Printer plugin" : "Плагин принтера";
    public string PrintPluginExt => Lang == Lang.English ? "Printer extension" : "Расширение принтера";
    public string QuestionUseFakeData => Lang == Lang.English ? "Use fake data?" : "Использовать фейк данные?";
    public string QuestionPrint => Lang == Lang.English ? "Continue printing?" : "Продолжить печать?";
    public string QuestionPrintCheckAccess => Lang == Lang.English ? "Check printer access?" : "Проверить доступ к принтеру?";
    public string QuestionPrintSendCmd => Lang == Lang.English ? "Send cmd to the print?" : "Отправить команду на печать?";
    public string Resource => Lang == Lang.English ? "Printer resource" : "Ресурс принтера";
    public string Resources => Lang == Lang.English ? "Printer resources" : "Ресурсы принтера";
    public string ResourcesClear => Lang == Lang.English ? "Clear resources" : "Удалить ресурсы";
    public string ResourcesLoadGrf => Lang == Lang.English ? "Load GRF (pics)" : "Загрузить GRF (картинки)";
    public string ResourcesLoadTtf => Lang == Lang.English ? "Load TTF (fonts)" : "Загрузить TTF (шрифты)";
    public string SensorPeeler => Lang == Lang.English ? "Sensor Peeler" : "Датчик Peeler";
    public string State => Lang == Lang.English ? "State" : "Состояние";
    public string StateCode => Lang == Lang.English ? "State code" : "Код состояния";
    public string Status => Lang == Lang.English ? "Status" : "Состояние";
    public string StatusCode => Lang == Lang.English ? "Status code" : "Код статуса";
    public string StatusIsHeadCold => Lang == Lang.English ? "Is cold" : "Холодный";
    public string StatusIsHeadOpen => Lang == Lang.English ? "Is open" : "Открыт";
    public string StatusIsHeadTooHot => Lang == Lang.English ? "Is hot" : "Горячий";
    public string StatusIsPaperOut => Lang == Lang.English ? "Paper out" : "Закончилась лента";
    public string StatusIsPartialFormatInProgress => Lang == Lang.English ? "Partial format in progress" : "Выполняется частичный формат";
    public string StatusIsPaused => Lang == Lang.English ? "Paused" : "Приостановлено";
    public string StatusIsReadyToPrint => Lang == Lang.English ? "Is ready" : "Готов";
    public string StatusIsReceiveBufferFull => Lang == Lang.English ? "Receive buffer full" : "Буфер приема заполнен";
    public string StatusIsRibbonOut => Lang == Lang.English ? "Ribbon out" : "Лента на выходе";
    public string StatusIsUnavailable => Lang == Lang.English ? "Status is unavailable" : "Статус не доступен";
    public string Template => Lang == Lang.English ? "Template" : "Шаблон";
    public string TemplateResource => Lang == Lang.English ? "Template resource" : "Ресурс шаблона";
    public string Type => Lang == Lang.English ? "Printer type" : "Тип принтера";
    public string Types => Lang == Lang.English ? "Printer types" : "Типы принтеров";
    public string WarningOpenCover => Lang == Lang.English ? "Open the cover of the separator before proceeding with the calibration!" : "Прежде чем продолжить калибровку, откройте крышку отделителя!";

    #endregion
}