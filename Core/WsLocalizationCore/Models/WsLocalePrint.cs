// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class WsLocalePrint : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public string ActionPrint => Lang == WsEnumLanguage.English ? "Print" : "Печать";
    public string Available => Lang == WsEnumLanguage.English ? "available" : "доступен";
    public string ClearQueue => Lang == WsEnumLanguage.English ? "Clear queue" : "Очистить очередь";
    public string Communication => Lang == WsEnumLanguage.English ? "Communication" : "Связь";
    public string ControlPanel => Lang == WsEnumLanguage.English ? "Printer control panel" : "Панель управления принтером";
    public string DarknessLevel => Lang == WsEnumLanguage.English ? "Level of darkness" : "Уровень темноты";
    public string DeviceCheckConnect => Lang == WsEnumLanguage.English ? "Check the device connection." : "Проверьте подключение устройства.";
    public string DeviceCommunication => Lang == WsEnumLanguage.English ? "Communication with the printer" : "Связь с принтером";
    public string DeviceIsUnavailable => Lang == WsEnumLanguage.English ? "Printer is unavailable!" : "Принтер не доступен!";
    public string DeviceMainCheckStatus => Lang == WsEnumLanguage.English ? "Check the main printer status!" : "Проверьте состояние основного принтера!";
    public string DeviceMainIsUnavailable => Lang == WsEnumLanguage.English ? "Main printer is unavailable!" : "Основной принтер не доступен!";
    public string DeviceName => Lang == WsEnumLanguage.English ? "Printer name" : "Имя принтера";
    public string DeviceNameIsUnavailable => Lang == WsEnumLanguage.English ? "Device is unavailable" : "Устройство не доступно";
    public string DeviceNameShort => Lang == WsEnumLanguage.English ? "Printer" : "Принтер";
    public string DeviceShippingCheckStatus => Lang == WsEnumLanguage.English ? "Check the shipping printer status!" : "Проверьте состояние транспортного принтера!";
    public string DeviceShippingIsUnavailable => Lang == WsEnumLanguage.English ? "Shipping printer is unavailable!" : "Транспортный принтер не доступен!";
    public string Driver => Lang == WsEnumLanguage.English ? "Driver" : "Драйвер";
    public string ErrorPlu(int pluNumber, string goodName) => Lang == WsEnumLanguage.English ? $"Print error for PLU: {pluNumber} ({goodName})!" : $"Ошибка печати для ПЛУ: {pluNumber} ({goodName})";
    public string HttpStatusCode => Lang == WsEnumLanguage.English ? "Http status code" : "Код состояния Http";
    public string InfoCaption => Lang == WsEnumLanguage.English ? "Printer info" : "Информация о принтере";
    public string Ip => Lang == WsEnumLanguage.English ? "IP-address" : "IP-адрес";
    public string Mac => Lang == WsEnumLanguage.English ? "MAC-address" : "MAC-адрес";
    public string Mode => Lang == WsEnumLanguage.English ? "Mode" : "Режим";
    public string ModeApplicator => Lang == WsEnumLanguage.English ? "Applicator" : "Аппликатор";
    public string ModeCutter => Lang == WsEnumLanguage.English ? "Cutter" : "Отрезание";
    public string ModeDelayedCut => Lang == WsEnumLanguage.English ? "Delayed Cut" : "Отложенный срез";
    public string ModeKiosk => Lang == WsEnumLanguage.English ? "Kiosk" : "Киоск";
    public string Model => Lang == WsEnumLanguage.English ? "Model" : "Модель";
    public string ModeLinerlessPeel => Lang == WsEnumLanguage.English ? "Linerless Peel" : "Бесслойный пилинг";
    public string ModeLinerlessRewind => Lang == WsEnumLanguage.English ? "Linerless Rewind" : "Бесслойная перемотка";
    public string ModePartialCutter => Lang == WsEnumLanguage.English ? "Partial Cutter" : "Частичная резка";
    public string ModePeelOff => Lang == WsEnumLanguage.English ? "Peel-Off" : "Отклеивание";
    public string ModeRewind => Lang == WsEnumLanguage.English ? "Rewind" : "Перемотка";
    public string ModeRfid => Lang == WsEnumLanguage.English ? "RFID" : "РФИД";
    public string ModeTearOff => Lang == WsEnumLanguage.English ? "Tear-Off" : "Отрывание";
    public string ModeUnknown => Lang == WsEnumLanguage.English ? "Unknown" : "Неизвестный";
    public string Name => Lang == WsEnumLanguage.English ? "Printer" : "Принтер";
    public string NameMain => Lang == WsEnumLanguage.English ? "Printer" : "Принтер";
    public string NameMainTsc => Lang == WsEnumLanguage.English ? "Printer TSC" : "Принтер ТСК";
    public string NameMainTscShort => Lang == WsEnumLanguage.English ? "TSC" : "ТСК";
    public string NameMainZebra => Lang == WsEnumLanguage.English ? "Main printer Zebra" : "Основной принтер Зебра";
    public string NameMainZebraShort => Lang == WsEnumLanguage.English ? "Zebra" : "Зебра";
    public string Names => Lang == WsEnumLanguage.English ? "Printers" : "Принтеры";
    public string NameShipping => Lang == WsEnumLanguage.English ? "Shipping printer" : "Транспортный принтер";
    public string NameShippingTsc => Lang == WsEnumLanguage.English ? "Shipping printer TSC" : "Транспортный принтер ТСК";
    public string NameShippingTscShort => Lang == WsEnumLanguage.English ? "Shipping TSC" : "Транспортный ТСК";
    public string NameShippingZebra => Lang == WsEnumLanguage.English ? "Shipping printer Zebra" : "Транспортный принтер Зебра";
    public string NameShippingZebraShort => Lang == WsEnumLanguage.English ? "Shipping Zebra" : "Транспортный Зебра";
    public string NamesMain => Lang == WsEnumLanguage.English ? "Main printers" : "Основные принтеры";
    public string NamesShipping => Lang == WsEnumLanguage.English ? "Shipping printers" : "Транспортные принтеры";
    public string Password => Lang == WsEnumLanguage.English ? "Printer password" : "Пароль принтера";
    public string PeelOffSet => Lang == WsEnumLanguage.English ? "Offset" : "Смещение";
    public string Port => Lang == WsEnumLanguage.English ? "Printer port" : "Порт принтера";
    public string PortShort => Lang == WsEnumLanguage.English ? "Port" : "Порт";
    public string PrinterStatus => Lang == WsEnumLanguage.English ? "Printer status" : "Состояние принтера";
    public string PrintPlugin => Lang == WsEnumLanguage.English ? "Printer plugin" : "Плагин принтера";
    public string PrintPluginExt => Lang == WsEnumLanguage.English ? "Printer extension" : "Расширение принтера";
    public string QuestionUseFakeData => Lang == WsEnumLanguage.English ? "Use fake data?" : "Использовать фейк данные?";
    public string QuestionPrint => Lang == WsEnumLanguage.English ? "Continue printing?" : "Продолжить печать?";
    public string QuestionPrintCheckAccess => Lang == WsEnumLanguage.English ? "Check printer access?" : "Проверить доступ к принтеру?";
    public string QuestionPrintSendCmd => Lang == WsEnumLanguage.English ? "Send cmd to the print?" : "Отправить команду на печать?";
    public string Resource => Lang == WsEnumLanguage.English ? "Printer resource" : "Ресурс принтера";
    public string Resources => Lang == WsEnumLanguage.English ? "Printer resources" : "Ресурсы принтера";
    public string ResourcesClear => Lang == WsEnumLanguage.English ? "Clear resources" : "Удалить ресурсы";
    public string ResourcesLoadGrf => Lang == WsEnumLanguage.English ? "Load GRF (pics)" : "Загрузить GRF (картинки)";
    public string ResourcesLoadTtf => Lang == WsEnumLanguage.English ? "Load TTF (fonts)" : "Загрузить TTF (шрифты)";
    public string SensorPeeler => Lang == WsEnumLanguage.English ? "Sensor Peeler" : "Датчик Peeler";
    public string State => Lang == WsEnumLanguage.English ? "State" : "Состояние";
    public string StateCode => Lang == WsEnumLanguage.English ? "State code" : "Код состояния";
    public string Status => Lang == WsEnumLanguage.English ? "Status" : "Состояние";
    public string StatusCode => Lang == WsEnumLanguage.English ? "Status code" : "Код статуса";
    public string StatusIsHeadCold => Lang == WsEnumLanguage.English ? "Is cold" : "Холодный";
    public string StatusIsHeadOpen => Lang == WsEnumLanguage.English ? "Is open" : "Открыт";
    public string StatusIsHeadTooHot => Lang == WsEnumLanguage.English ? "Is hot" : "Горячий";
    public string StatusIsPaperOut => Lang == WsEnumLanguage.English ? "Paper out" : "Закончилась лента";
    public string StatusIsPartialFormatInProgress => Lang == WsEnumLanguage.English ? "Partial format in progress" : "Выполняется частичный формат";
    public string StatusIsPaused => Lang == WsEnumLanguage.English ? "Paused" : "Приостановлено";
    public string StatusPendingDeletion => Lang == WsEnumLanguage.English ? "Waiting for connect" : "Ожидание подключения";
    public string StatusIsReadyToPrint => Lang == WsEnumLanguage.English ? "Is ready" : "Готов";
    public string StatusIsReceiveBufferFull => Lang == WsEnumLanguage.English ? "Receive buffer full" : "Буфер приема заполнен";
    public string StatusIsRibbonOut => Lang == WsEnumLanguage.English ? "Ribbon out" : "Лента на выходе";
    public string StatusIsUnavailable => Lang == WsEnumLanguage.English ? "Status is unavailable" : "Статус не доступен";
    public string Template => Lang == WsEnumLanguage.English ? "Template" : "Шаблон";
    public string TemplateResource => Lang == WsEnumLanguage.English ? "Template resource" : "Ресурс шаблона";
    public string Type => Lang == WsEnumLanguage.English ? "Printer type" : "Тип принтера";
    public string Types => Lang == WsEnumLanguage.English ? "Printer types" : "Типы принтеров";
    public string WarningOpenCover => Lang == WsEnumLanguage.English ? "Open the cover of the separator before proceeding with the calibration!" : "Прежде чем продолжить калибровку, откройте крышку отделителя!";

    #endregion
}