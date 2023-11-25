namespace WsLocalizationCore.Models;

public sealed class LocalePrint : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string ActionPrint => Lang == EnumLanguage.English ? "Print" : "Печать";
    public string ControlPanel => Lang == EnumLanguage.English ? "Printer control panel" : "Панель управления принтером";
    public string DeviceCheckConnect => Lang == EnumLanguage.English ? "Check the device connection." : "Проверьте подключение устройства.";
    public string DeviceMainCheckStatus => Lang == EnumLanguage.English ? "Check the main printer status!" : "Основной принтер:";
    public string DeviceMainIsUnavailable => Lang == EnumLanguage.English ? "Main printer is unavailable!" : "Основной принтер не доступен!";
    public string Ip => Lang == EnumLanguage.English ? "IP-address" : "IP-адрес";
    public string Name => Lang == EnumLanguage.English ? "Printer" : "Принтер";
    public string NameMain => Lang == EnumLanguage.English ? "Printer" : "Принтер";
    public string Names => Lang == EnumLanguage.English ? "Printers" : "Принтеры";
    public string Port => Lang == EnumLanguage.English ? "Printer port" : "Порт принтера";
    public string StatusIsHeadCold => Lang == EnumLanguage.English ? "Is cold" : "Холодный";
    public string StatusIsHeadOpen => Lang == EnumLanguage.English ? "Is open" : "Открыт";
    public string StatusIsHeadTooHot => Lang == EnumLanguage.English ? "Is hot" : "Горячий";
    public string StatusIsPaperOut => Lang == EnumLanguage.English ? "Paper out" : "Закончилась лента";
    public string StatusIsPaused => Lang == EnumLanguage.English ? "Paused" : "Приостановлено";
    public string StatusPendingDeletion => Lang == EnumLanguage.English ? "Waiting for connect" : "Ожидание подключения";
    public string StatusIsReadyToPrint => Lang == EnumLanguage.English ? "Is ready" : "Готов";
    public string StatusIsUnavailable => Lang == EnumLanguage.English ? "Status is unavailable" : "Статус не доступен";
    public string Type => Lang == EnumLanguage.English ? "Printer type" : "Тип принтера";

    #endregion
}