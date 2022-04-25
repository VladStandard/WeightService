// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;

namespace DataCore.Localizations
{
    public class LocaleSettings
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LocaleSettings _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LocaleSettings Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        #region Public and private fields and properties

        public string SectionRowsCount => Lang == ShareEnums.Lang.English ? "Section's rows count" : "Количество строк в секции";
        public string ItemRowsCount => Lang == ShareEnums.Lang.English ? "Records's rows count" : "Количество строк в записи";
        public string SelectTopRowsCount => Lang == ShareEnums.Lang.English ? "Selection's top rows count" : "Подсчет верхних строк селекции";
        public string AllowedHosts => Lang == ShareEnums.Lang.English ? "Allowed hosts" : "Разрешенные хосты";

        #endregion
    }
}
