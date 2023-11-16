using WsLocalizationCore.Common;
namespace WsLocalizationCore.Utils;

public static partial class LocaleData
{
    public static class Program
    {
        public static string IsClosed => Lang == EnumLanguage.English ? "The program is closed" : "Программа закрыта";
        public static string IsLoaded => Lang == EnumLanguage.English ? "The program is loaded" : "Программа загружена";
        public static string IsLoading => Lang == EnumLanguage.English ? "The program is loading" : "Программа загружается";
        public static string IsNotLoaded => Lang == EnumLanguage.English ? "The program is not yet loaded!" + Environment.NewLine + "Wait for it..." : "Программа ещё не загружена!" + Environment.NewLine + "Подождите...";
        public static string IsRunning => Lang == EnumLanguage.English ? "The program is running" : "Программа запущена";
        public static string TimeSpent => Lang == EnumLanguage.English ? "Time spent" : "Затраченное время";
    }
}