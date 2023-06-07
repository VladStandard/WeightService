// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Utils;

public static partial class WsLocaleData
{
    public static WsEnumLanguage Lang { get; set; }

    static WsLocaleData()
    {
        Lang = WsEnumLanguage.Russian;
    }
}