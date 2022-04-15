// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;
namespace DataCore.Localization
{
    public class PrintHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static PrintHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static PrintHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;
        public string ActionPrint => Lang == ShareEnums.Lang.English ? "Print" : "Печать";
        public string Available => Lang == ShareEnums.Lang.English ? "available" : "доступен";
        public string ClearQueue => Lang == ShareEnums.Lang.English ? "Clear queue" : "Очистить очередь";
        public string ControlPanel => Lang == ShareEnums.Lang.English ? "Printer control panel" : "Панель управления принтером";
        public string DarknessLevel => Lang == ShareEnums.Lang.English ? "Level of darkness" : "Уровень темноты";
        public string Driver => Lang == ShareEnums.Lang.English ? "Driver" : "Драйвер";
        public string InfoCaption => Lang == ShareEnums.Lang.English ? "Printer info" : "Информация о принтере";
        public string Ip => Lang == ShareEnums.Lang.English ? "IP-address" : "IP-адрес";
        public string Mac => Lang == ShareEnums.Lang.English ? "MAC-address" : "MAC-адрес";
        public string ModelTsc => Lang == ShareEnums.Lang.English ? "Printer TSC" : "Принтер TSC";
        public string ModelZebra => Lang == ShareEnums.Lang.English ? "Printer Zebra" : "Принтер Zebra";
        public string Name => Lang == ShareEnums.Lang.English ? "Printer" : "Принтер";
        public string NameMain => Lang == ShareEnums.Lang.English ? "Main printer" : "Основной принтер";
        public string Names => Lang == ShareEnums.Lang.English ? "Printers" : "Принтеры";
        public string NameShipping => Lang == ShareEnums.Lang.English ? "Shipping printer" : "Транспортный принтер";
        public string NamesMain => Lang == ShareEnums.Lang.English ? "Main printers" : "Основные принтеры";
        public string NamesShipping => Lang == ShareEnums.Lang.English ? "Shipping printers" : "Транспортные принтеры";
        public string Password => Lang == ShareEnums.Lang.English ? "Printer password" : "Пароль принтера";
        public string PeelOffSet => Lang == ShareEnums.Lang.English ? "Offset" : "Смещение";
        public string Port => Lang == ShareEnums.Lang.English ? "Printer port" : "Порт принтера";
        public string PortShort => Lang == ShareEnums.Lang.English ? "Port" : "Порт";
        public string Resource => Lang == ShareEnums.Lang.English ? "Printer resource" : "Ресурс принтера";
        public string Resources => Lang == ShareEnums.Lang.English ? "Printer resources" : "Ресурсы принтера";
        public string ResourcesClear => Lang == ShareEnums.Lang.English ? "Clear resources" : "Удалить ресурсы";
        public string ResourcesLoadGrf => Lang == ShareEnums.Lang.English ? "Load GRF (pics)" : "Загрузить GRF (картинки)";
        public string ResourcesLoadTtf => Lang == ShareEnums.Lang.English ? "Load TTF (fonts)" : "Загрузить TTF (шрифты)";
        public string SensorPeeler => Lang == ShareEnums.Lang.English ? "Sensor Peeler" : "Датчик Peeler";
        public string State => Lang == ShareEnums.Lang.English ? "State" : "Состояние";
        public string StateCode => Lang == ShareEnums.Lang.English ? "State code" : "Код состояния";
        public string Status => Lang == ShareEnums.Lang.English ? "Status" : "Статус";
        public string StatusCode => Lang == ShareEnums.Lang.English ? "Status code" : "Код статуса";
        public string Type => Lang == ShareEnums.Lang.English ? "Printer type" : "Тип принтера";
        public string Types => Lang == ShareEnums.Lang.English ? "Printer types" : "Типы принтеров";
        public string Unavailable => Lang == ShareEnums.Lang.English ? "unavailable" : "не доступен";
        public string WarningOpenCover => Lang == ShareEnums.Lang.English ? "Open the cover of the separator before proceeding with the calibration!" : "Прежде чем продолжить калибровку, откройте крышку отделителя!";

        #endregion
    }
}
