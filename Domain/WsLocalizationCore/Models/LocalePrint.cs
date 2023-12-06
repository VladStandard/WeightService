namespace WsLocalizationCore.Models;

public sealed class LocalePrint : LocalizationBase
{
    #region Public and private fields, properties, constructor
    
    public string ControlPanel => Lang == EnumLanguage.English ? "Printer control panel" : "Панель управления принтером";
    public string Ip => Lang == EnumLanguage.English ? "IP-address" : "IP-адрес";
    public string Name => Lang == EnumLanguage.English ? "Printer" : "Принтер";
    public string NameMain => Lang == EnumLanguage.English ? "Printer" : "Принтер";
    public string Names => Lang == EnumLanguage.English ? "Printers" : "Принтеры";
    public string Port => Lang == EnumLanguage.English ? "Printer port" : "Порт принтера";
    public string Type => Lang == EnumLanguage.English ? "Printer type" : "Тип принтера";

    #endregion
}