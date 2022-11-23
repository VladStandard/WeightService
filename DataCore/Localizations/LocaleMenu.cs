// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;

namespace DataCore.Localizations;

public class LocaleMenu
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleMenu _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleMenu Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

    #region Public and private fields, properties, constructor

    public string FileChoose => Lang == Lang.English ? "Select a file" : "Выбрать файл";
    public string FileDialog => Lang == Lang.English ? "File dialog" : "Файловый диалог";
    public string FileDownload => Lang == Lang.English ? "Download a file" : "Скачать файл";
    public string FileSaveDialog => Lang == Lang.English ? "Specify the file name to save" : "Указать имя файла для сохранения";
    public string FileUpload => Lang == Lang.English ? "Upload a file" : "Загрузить файл";
    public string From => Lang == Lang.English ? "from" : "из";
    public string Login => Lang == Lang.English ? "Login" : "Логин";
    public string MenuAccess => Lang == Lang.English ? "Menu access" : "Доступ к меню";
    public string MenuAccessAllow => Lang == Lang.English ? "Menu access allowed" : "Доступ к меню разрешён";
    public string MenuAccessDeny => Lang == Lang.English ? "Menu access denied" : "Доступ к меню запрещён";
    public string MenuDbVersionHistory => Lang == Lang.English ? "DB version history" : "История версий БД";
    public string MenuHome => Lang == Lang.English ? "Home" : "Домой";
    public string MenuHelp => Lang == Lang.English ? "Help" : "Справка";
    public string MenuInfo => Lang == Lang.English ? "Info" : "Информация";
    public string MenuMain => Lang == Lang.English ? "Main" : "Главная";
    public string MenuReferences => Lang == Lang.English ? "References" : "Справочники";
    public string MenuReports => Lang == Lang.English ? "Reports" : "Журналы";
    public string MenuSecurity => Lang == Lang.English ? "Security" : "Безопасность";
    public string MenuSystem => Lang == Lang.English ? "System" : "Система";
    public string ServerResponse => Lang == Lang.English ? "Server response" : "Ответ сервера";

    #endregion
}
