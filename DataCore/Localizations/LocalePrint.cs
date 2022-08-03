// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;

namespace DataCore.Localizations
{
    public class LocalePrint
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LocalePrint _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LocalePrint Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        #region Public and private fields, properties, constructor

        public string ActionPrint => Lang == ShareEnums.Lang.English ? "Print" : "Печать";
        public string Available => Lang == ShareEnums.Lang.English ? "available" : "доступен";
        public string ClearQueue => Lang == ShareEnums.Lang.English ? "Clear queue" : "Очистить очередь";
        public string Communication => Lang == ShareEnums.Lang.English ? "Communication" : "Связь";
        public string ControlPanel => Lang == ShareEnums.Lang.English ? "Printer control panel" : "Панель управления принтером";
        public string DarknessLevel => Lang == ShareEnums.Lang.English ? "Level of darkness" : "Уровень темноты";
        public string DeviceCheckConnect => Lang == ShareEnums.Lang.English ? "Check the device connection." : "Проверьте подключение устройства.";
        public string DeviceCommunication => Lang == ShareEnums.Lang.English ? "Communication with the printer" : "Связь с принтером";
        public string DeviceIsUnavailable => Lang == ShareEnums.Lang.English ? "Printer is unavailable!" : "Принтер не доступен!";
        public string DeviceMainCheckStatus => Lang == ShareEnums.Lang.English ? "Check the main printer status!" : "Проверьте состояние основного принтера!";
        public string DeviceMainIsUnavailable => Lang == ShareEnums.Lang.English ? "Main printer is unavailable!" : "Основной принтер не доступен!";
        public string DeviceName => Lang == ShareEnums.Lang.English ? "Printer name" : "Имя принтера";
        public string DeviceNameIsUnavailable => Lang == ShareEnums.Lang.English ? "Device is unavailable" : "Устройство не доступно";
        public string DeviceNameShort => Lang == ShareEnums.Lang.English ? "Printer" : "Принтер";
        public string DeviceShippingCheckStatus => Lang == ShareEnums.Lang.English ? "Check the shipping printer status!" : "Проверьте состояние транспортного принтера!";
        public string DeviceShippingIsUnavailable => Lang == ShareEnums.Lang.English ? "Shipping printer is unavailable!" : "Транспортный принтер не доступен!";
        public string Driver => Lang == ShareEnums.Lang.English ? "Driver" : "Драйвер";
        public string ErrorPlu(int pluNumber, string goodName) => Lang == ShareEnums.Lang.English ? $"Print error for PLU: {pluNumber} ({goodName})!" : "Ошибка печати для ПЛУ: {pluNumber} ({goodName})";
        public string HttpStatusCode => Lang == ShareEnums.Lang.English ? "Http status code" : "Код состояния Http";
        public string InfoCaption => Lang == ShareEnums.Lang.English ? "Printer info" : "Информация о принтере";
        public string Ip => Lang == ShareEnums.Lang.English ? "IP-address" : "IP-адрес";
        public string Mac => Lang == ShareEnums.Lang.English ? "MAC-address" : "MAC-адрес";
        public string Mode => Lang == ShareEnums.Lang.English ? "Mode" : "Режим";
        public string Model => Lang == ShareEnums.Lang.English ? "Model" : "Модель";
        public string ModeApplicator => Lang == ShareEnums.Lang.English ? "Applicator" : "Аппликатор";
        public string ModeCutter => Lang == ShareEnums.Lang.English ? "Cutter" : "Отрезание";
        public string ModeDelayedCut => Lang == ShareEnums.Lang.English ? "Delayed Cut" : "Отложенный срез";
        public string ModeKiosk => Lang == ShareEnums.Lang.English ? "Kiosk" : "Киоск";
        public string ModeLinerlessPeel => Lang == ShareEnums.Lang.English ? "Linerless Peel" : "Бесслойный пилинг";
        public string ModeLinerlessRewind => Lang == ShareEnums.Lang.English ? "Linerless Rewind" : "Бесслойная перемотка";
        public string ModePartialCutter => Lang == ShareEnums.Lang.English ? "Partial Cutter" : "Частичная резка";
        public string ModePeelOff => Lang == ShareEnums.Lang.English ? "Peel-Off" : "Отклеивание";
        public string ModeRewind => Lang == ShareEnums.Lang.English ? "Rewind" : "Перемотка";
        public string ModeRfid => Lang == ShareEnums.Lang.English ? "RFID" : "РФИД";
        public string ModeTearOff => Lang == ShareEnums.Lang.English ? "Tear-Off" : "Отрывание";
        public string ModeUnknown => Lang == ShareEnums.Lang.English ? "Unknown" : "Неизвестный";
        public string Name => Lang == ShareEnums.Lang.English ? "Printer" : "Принтер";
        public string NameMain => Lang == ShareEnums.Lang.English ? "Main printer" : "Основной принтер";
        public string NameMainTsc => Lang == ShareEnums.Lang.English ? "Main printer TSC" : "Основной принтер ТСК";
        public string NameMainTscShort => Lang == ShareEnums.Lang.English ? "TSC" : "ТСК";
        public string NameMainZebra => Lang == ShareEnums.Lang.English ? "Main printer Zebra" : "Основной принтер Зебра";
        public string NameMainZebraShort => Lang == ShareEnums.Lang.English ? "Zebra" : "Зебра";
        public string Names => Lang == ShareEnums.Lang.English ? "Printers" : "Принтеры";
        public string NameShipping => Lang == ShareEnums.Lang.English ? "Shipping printer" : "Транспортный принтер";
        public string NameShippingTsc => Lang == ShareEnums.Lang.English ? "Shipping printer TSC" : "Транспортный принтер ТСК";
        public string NameShippingTscShort => Lang == ShareEnums.Lang.English ? "Shipping TSC" : "Транспортный ТСК";
        public string NameShippingZebra => Lang == ShareEnums.Lang.English ? "Shipping printer Zebra" : "Транспортный принтер Зебра";
        public string NameShippingZebraShort => Lang == ShareEnums.Lang.English ? "Shipping Zebra" : "Транспортный Зебра";
        public string NamesMain => Lang == ShareEnums.Lang.English ? "Main printers" : "Основные принтеры";
        public string NamesShipping => Lang == ShareEnums.Lang.English ? "Shipping printers" : "Транспортные принтеры";
        public string Password => Lang == ShareEnums.Lang.English ? "Printer password" : "Пароль принтера";
        public string PeelOffSet => Lang == ShareEnums.Lang.English ? "Offset" : "Смещение";
        public string Port => Lang == ShareEnums.Lang.English ? "Printer port" : "Порт принтера";
        public string PortShort => Lang == ShareEnums.Lang.English ? "Port" : "Порт";
        public string PrinterStatus => Lang == ShareEnums.Lang.English ? "Printer status" : "Состояние принтера";
        public string PrintManager => Lang == ShareEnums.Lang.English ? "Print manager" : "Менеджер принтера";
        public string QuestionPrint => Lang == ShareEnums.Lang.English ? "Continue printing?" : "Продолжить печать?";
        public string Resource => Lang == ShareEnums.Lang.English ? "Printer resource" : "Ресурс принтера";
        public string Resources => Lang == ShareEnums.Lang.English ? "Printer resources" : "Ресурсы принтера";
        public string ResourcesClear => Lang == ShareEnums.Lang.English ? "Clear resources" : "Удалить ресурсы";
        public string ResourcesLoadGrf => Lang == ShareEnums.Lang.English ? "Load GRF (pics)" : "Загрузить GRF (картинки)";
        public string ResourcesLoadTtf => Lang == ShareEnums.Lang.English ? "Load TTF (fonts)" : "Загрузить TTF (шрифты)";
        public string SensorPeeler => Lang == ShareEnums.Lang.English ? "Sensor Peeler" : "Датчик Peeler";
        public string State => Lang == ShareEnums.Lang.English ? "State" : "Состояние";
        public string StateCode => Lang == ShareEnums.Lang.English ? "State code" : "Код состояния";
        public string Status => Lang == ShareEnums.Lang.English ? "Status" : "Состояние";
        public string StatusCode => Lang == ShareEnums.Lang.English ? "Status code" : "Код статуса";
        public string StatusIsHeadCold => Lang == ShareEnums.Lang.English ? "Is cold" : "Холодный";
        public string StatusIsHeadOpen => Lang == ShareEnums.Lang.English ? "Is open" : "Открыт";
        public string StatusIsHeadTooHot => Lang == ShareEnums.Lang.English ? "Is hot" : "Горячий";
        public string StatusIsPaperOut => Lang == ShareEnums.Lang.English ? "Paper out" : "Закончилась лента";
        public string StatusIsPartialFormatInProgress => Lang == ShareEnums.Lang.English ? "Partial format in progress" : "Выполняется частичный формат";
        public string StatusIsPaused => Lang == ShareEnums.Lang.English ? "Paused" : "Приостановлено";
        public string StatusIsReadyToPrint => Lang == ShareEnums.Lang.English ? "Is ready" : "Готов";
        public string StatusIsReceiveBufferFull => Lang == ShareEnums.Lang.English ? "Receive buffer full" : "Буфер приема заполнен";
        public string StatusIsRibbonOut => Lang == ShareEnums.Lang.English ? "Ribbon out" : "Лента на выходе";
        public string StatusIsUnavailable => Lang == ShareEnums.Lang.English ? "Status is unavailable" : "Статус не доступен";
        public string Type => Lang == ShareEnums.Lang.English ? "Printer type" : "Тип принтера";
        public string Types => Lang == ShareEnums.Lang.English ? "Printer types" : "Типы принтеров";
        public string WarningOpenCover => Lang == ShareEnums.Lang.English ? "Open the cover of the separator before proceeding with the calibration!" : "Прежде чем продолжить калибровку, откройте крышку отделителя!";

        #endregion
    }
}
