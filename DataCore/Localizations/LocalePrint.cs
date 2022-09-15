// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocalePrint
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocalePrint _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocalePrint Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string ActionPrint => Lang == LangEnum.English ? "Print" : "Печать";
    public string Available => Lang == LangEnum.English ? "available" : "доступен";
    public string ClearQueue => Lang == LangEnum.English ? "Clear queue" : "Очистить очередь";
    public string Communication => Lang == LangEnum.English ? "Communication" : "Связь";
    public string ControlPanel => Lang == LangEnum.English ? "Printer control panel" : "Панель управления принтером";
    public string DarknessLevel => Lang == LangEnum.English ? "Level of darkness" : "Уровень темноты";
    public string DeviceCheckConnect => Lang == LangEnum.English ? "Check the device connection." : "Проверьте подключение устройства.";
    public string DeviceCommunication => Lang == LangEnum.English ? "Communication with the printer" : "Связь с принтером";
    public string DeviceIsUnavailable => Lang == LangEnum.English ? "Printer is unavailable!" : "Принтер не доступен!";
    public string DeviceMainCheckStatus => Lang == LangEnum.English ? "Check the main printer status!" : "Проверьте состояние основного принтера!";
    public string DeviceMainIsUnavailable => Lang == LangEnum.English ? "Main printer is unavailable!" : "Основной принтер не доступен!";
    public string DeviceName => Lang == LangEnum.English ? "Printer name" : "Имя принтера";
    public string DeviceNameIsUnavailable => Lang == LangEnum.English ? "Device is unavailable" : "Устройство не доступно";
    public string DeviceNameShort => Lang == LangEnum.English ? "Printer" : "Принтер";
    public string DeviceShippingCheckStatus => Lang == LangEnum.English ? "Check the shipping printer status!" : "Проверьте состояние транспортного принтера!";
    public string DeviceShippingIsUnavailable => Lang == LangEnum.English ? "Shipping printer is unavailable!" : "Транспортный принтер не доступен!";
    public string Driver => Lang == LangEnum.English ? "Driver" : "Драйвер";
    public string ErrorPlu(int pluNumber, string goodName) => Lang == LangEnum.English ? $"Print error for PLU: {pluNumber} ({goodName})!" : "Ошибка печати для ПЛУ: {pluNumber} ({goodName})";
    public string HttpStatusCode => Lang == LangEnum.English ? "Http status code" : "Код состояния Http";
    public string InfoCaption => Lang == LangEnum.English ? "Printer info" : "Информация о принтере";
    public string Ip => Lang == LangEnum.English ? "IP-address" : "IP-адрес";
    public string Mac => Lang == LangEnum.English ? "MAC-address" : "MAC-адрес";
    public string Mode => Lang == LangEnum.English ? "Mode" : "Режим";
    public string ModeApplicator => Lang == LangEnum.English ? "Applicator" : "Аппликатор";
    public string ModeCutter => Lang == LangEnum.English ? "Cutter" : "Отрезание";
    public string ModeDelayedCut => Lang == LangEnum.English ? "Delayed Cut" : "Отложенный срез";
    public string ModeKiosk => Lang == LangEnum.English ? "Kiosk" : "Киоск";
    public string Model => Lang == LangEnum.English ? "Model" : "Модель";
    public string ModeLinerlessPeel => Lang == LangEnum.English ? "Linerless Peel" : "Бесслойный пилинг";
    public string ModeLinerlessRewind => Lang == LangEnum.English ? "Linerless Rewind" : "Бесслойная перемотка";
    public string ModePartialCutter => Lang == LangEnum.English ? "Partial Cutter" : "Частичная резка";
    public string ModePeelOff => Lang == LangEnum.English ? "Peel-Off" : "Отклеивание";
    public string ModeRewind => Lang == LangEnum.English ? "Rewind" : "Перемотка";
    public string ModeRfid => Lang == LangEnum.English ? "RFID" : "РФИД";
    public string ModeTearOff => Lang == LangEnum.English ? "Tear-Off" : "Отрывание";
    public string ModeUnknown => Lang == LangEnum.English ? "Unknown" : "Неизвестный";
    public string Name => Lang == LangEnum.English ? "Printer" : "Принтер";
    public string NameMain => Lang == LangEnum.English ? "Main printer" : "Основной принтер";
    public string NameMainTsc => Lang == LangEnum.English ? "Main printer TSC" : "Основной принтер ТСК";
    public string NameMainTscShort => Lang == LangEnum.English ? "TSC" : "ТСК";
    public string NameMainZebra => Lang == LangEnum.English ? "Main printer Zebra" : "Основной принтер Зебра";
    public string NameMainZebraShort => Lang == LangEnum.English ? "Zebra" : "Зебра";
    public string Names => Lang == LangEnum.English ? "Printers" : "Принтеры";
    public string NameShipping => Lang == LangEnum.English ? "Shipping printer" : "Транспортный принтер";
    public string NameShippingTsc => Lang == LangEnum.English ? "Shipping printer TSC" : "Транспортный принтер ТСК";
    public string NameShippingTscShort => Lang == LangEnum.English ? "Shipping TSC" : "Транспортный ТСК";
    public string NameShippingZebra => Lang == LangEnum.English ? "Shipping printer Zebra" : "Транспортный принтер Зебра";
    public string NameShippingZebraShort => Lang == LangEnum.English ? "Shipping Zebra" : "Транспортный Зебра";
    public string NamesMain => Lang == LangEnum.English ? "Main printers" : "Основные принтеры";
    public string NamesShipping => Lang == LangEnum.English ? "Shipping printers" : "Транспортные принтеры";
    public string Password => Lang == LangEnum.English ? "Printer password" : "Пароль принтера";
    public string PeelOffSet => Lang == LangEnum.English ? "Offset" : "Смещение";
    public string Port => Lang == LangEnum.English ? "Printer port" : "Порт принтера";
    public string PortShort => Lang == LangEnum.English ? "Port" : "Порт";
    public string PrinterStatus => Lang == LangEnum.English ? "Printer status" : "Состояние принтера";
    public string PrintManager => Lang == LangEnum.English ? "Print manager" : "Менеджер принтера";
    public string QuestionPrint => Lang == LangEnum.English ? "Continue printing?" : "Продолжить печать?";
    public string Resource => Lang == LangEnum.English ? "Printer resource" : "Ресурс принтера";
    public string Resources => Lang == LangEnum.English ? "Printer resources" : "Ресурсы принтера";
    public string ResourcesClear => Lang == LangEnum.English ? "Clear resources" : "Удалить ресурсы";
    public string ResourcesLoadGrf => Lang == LangEnum.English ? "Load GRF (pics)" : "Загрузить GRF (картинки)";
    public string ResourcesLoadTtf => Lang == LangEnum.English ? "Load TTF (fonts)" : "Загрузить TTF (шрифты)";
    public string SensorPeeler => Lang == LangEnum.English ? "Sensor Peeler" : "Датчик Peeler";
    public string State => Lang == LangEnum.English ? "State" : "Состояние";
    public string StateCode => Lang == LangEnum.English ? "State code" : "Код состояния";
    public string Status => Lang == LangEnum.English ? "Status" : "Состояние";
    public string StatusCode => Lang == LangEnum.English ? "Status code" : "Код статуса";
    public string StatusIsHeadCold => Lang == LangEnum.English ? "Is cold" : "Холодный";
    public string StatusIsHeadOpen => Lang == LangEnum.English ? "Is open" : "Открыт";
    public string StatusIsHeadTooHot => Lang == LangEnum.English ? "Is hot" : "Горячий";
    public string StatusIsPaperOut => Lang == LangEnum.English ? "Paper out" : "Закончилась лента";
    public string StatusIsPartialFormatInProgress => Lang == LangEnum.English ? "Partial format in progress" : "Выполняется частичный формат";
    public string StatusIsPaused => Lang == LangEnum.English ? "Paused" : "Приостановлено";
    public string StatusIsReadyToPrint => Lang == LangEnum.English ? "Is ready" : "Готов";
    public string StatusIsReceiveBufferFull => Lang == LangEnum.English ? "Receive buffer full" : "Буфер приема заполнен";
    public string StatusIsRibbonOut => Lang == LangEnum.English ? "Ribbon out" : "Лента на выходе";
    public string StatusIsUnavailable => Lang == LangEnum.English ? "Status is unavailable" : "Статус не доступен";
    public string TemplateResource => Lang == LangEnum.English ? "Template resource" : "Ресурс шаблона";
    public string Type => Lang == LangEnum.English ? "Printer type" : "Тип принтера";
    public string Types => Lang == LangEnum.English ? "Printer types" : "Типы принтеров";
    public string WarningOpenCover => Lang == LangEnum.English ? "Open the cover of the separator before proceeding with the calibration!" : "Прежде чем продолжить калибровку, откройте крышку отделителя!";

    #endregion
}
