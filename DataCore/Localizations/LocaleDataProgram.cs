// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Localizations;

public static partial class LocaleData
{
    public static class Program
    {
        public static string IsClosed => Lang == ShareEnums.Lang.English ? "The program is closed." : "Программа закрыта.";
        public static string IsLoaded => Lang == ShareEnums.Lang.English ? "The program is loaded." : "Программа загруженна.";
        public static string IsNotLoaded => Lang == ShareEnums.Lang.English ? "The program is not yet loaded!" + Environment.NewLine + "Wait for it..." : "Программа ещё не загружена!" + Environment.NewLine + "Подождите...";
        public static string IsRunning => Lang == ShareEnums.Lang.English ? "The program is running." : "Программа запущена.";
    }
}
