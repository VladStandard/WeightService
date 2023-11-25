namespace WsLocalizationCore.Models;

public sealed class LocaleWebService : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string BoxZero => Lang == EnumLanguage.English ? "Without the box" : "Без коробки";
    public string BundleZero => Lang == EnumLanguage.English ? "Without the bundle" : "Без пакета";
    public string Name => Lang == EnumLanguage.English ? "WebService 1C" : "ВебСервис 1С";

    #endregion
}