// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;

namespace DataCore.Localizations
{
    public class LocaleSystem
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LocaleSystem _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LocaleSystem Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        #region Public and private fields and properties

        public string SystemAccess => Lang == ShareEnums.Lang.English ? "Access" : "Доступ";
        public string SystemAccount => Lang == ShareEnums.Lang.English ? "Account" : "Аккаунт";
        public string SystemErrors => Lang == ShareEnums.Lang.English ? "Errors" : "Ошибки";
        public string SystemInfo => Lang == ShareEnums.Lang.English ? "Info" : "Информация";
        public string SystemLogin => Lang == ShareEnums.Lang.English ? "Log in" : "Вход";
        public string SystemLogs => Lang == ShareEnums.Lang.English ? "Logs" : "Логи";
        public string SystemSettingsNotFound => Lang == ShareEnums.Lang.English ? "Settings not found!" : "Настройки не найдены!";
        public string SystemWindowsUser => Lang == ShareEnums.Lang.English ? "Windows-user" : "Windows-пользователь";

        #endregion
    }
}
