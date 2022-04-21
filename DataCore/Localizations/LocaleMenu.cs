// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;

namespace DataCore.Localizations
{
    public class LocaleMenu
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LocaleMenu _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LocaleMenu Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        #region Public and private fields and properties

        public string FileChoose => Lang == ShareEnums.Lang.English ? "Select a file" : "Выбрать файл";
        public string FileDialog => Lang == ShareEnums.Lang.English ? "File dialog" : "Файловый диалог";
        public string FileDownload => Lang == ShareEnums.Lang.English ? "Download a file" : "Скачать файл";
        public string FileSaveDialog => Lang == ShareEnums.Lang.English ? "Specify the file name to save" : "Указать имя файла для сохранения";
        public string FileUpload => Lang == ShareEnums.Lang.English ? "Upload a file" : "Загрузить файл";
        public string From => Lang == ShareEnums.Lang.English ? "from" : "из";
        public string Login => Lang == ShareEnums.Lang.English ? "Login" : "Логин";
        public string MenuAccess => Lang == ShareEnums.Lang.English ? "Menu access" : "Доступ к меню";
        public string MenuAccessAllow => Lang == ShareEnums.Lang.English ? "Menu access allowed" : "Доступ к меню разрешён";
        public string MenuAccessDeny => Lang == ShareEnums.Lang.English ? "Menu access denied" : "Доступ к меню запрещён";
        public string MenuHome => Lang == ShareEnums.Lang.English ? "Home" : "Домой";
        public string MenuInfo => Lang == ShareEnums.Lang.English ? "Info" : "Информация";
        public string MenuMain => Lang == ShareEnums.Lang.English ? "Main" : "Главная";
        public string MenuReferences => Lang == ShareEnums.Lang.English ? "References" : "Справочники";
        public string MenuReports => Lang == ShareEnums.Lang.English ? "Reports" : "Журналы";
        public string MenuSecurity => Lang == ShareEnums.Lang.English ? "Security" : "Безопасность";
        public string MenuSystem => Lang == ShareEnums.Lang.English ? "System" : "Система";
        public string ServerResponse => Lang == ShareEnums.Lang.English ? "Server response" : "Ответ сервера";

        #endregion
    }
}
