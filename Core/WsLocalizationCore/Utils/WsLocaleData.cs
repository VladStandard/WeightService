namespace WsLocalizationCore.Utils;

public static partial class WsLocaleData
{
    public static WsEnumLanguage Lang { get; set; }

    static WsLocaleData()
    {
        Lang = WsEnumLanguage.Russian;
    }
}