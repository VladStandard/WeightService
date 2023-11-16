namespace WsLocalizationCore.Utils;

public static partial class LocaleData
{
    public static EnumLanguage Lang { get; set; }

    static LocaleData()
    {
        Lang = EnumLanguage.Russian;
    }
}