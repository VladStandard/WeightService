// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Utils;

public static partial class WsLocaleData
{
    public static class Program
    {
        public static string IsClosed => Lang == WsEnumLanguage.English ? "The program is closed" : "Программа закрыта";
        public static string IsLoaded => Lang == WsEnumLanguage.English ? "The program is loaded" : "Программа загружена";
        public static string IsLoading => Lang == WsEnumLanguage.English ? "The program is loading" : "Программа загружается";
        public static string IsNotLoaded => Lang == WsEnumLanguage.English ? "The program is not yet loaded!" + Environment.NewLine + "Wait for it..." : "Программа ещё не загружена!" + Environment.NewLine + "Подождите...";
        public static string IsRunning => Lang == WsEnumLanguage.English ? "The program is running" : "Программа запущена";
        public static string TimeSpent => Lang == WsEnumLanguage.English ? "Time spent" : "Затраченное время";
    }
}