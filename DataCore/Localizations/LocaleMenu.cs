// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleMenu
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleMenu _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleMenu Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string FileChoose => Lang == LangEnum.English ? "Select a file" : "Выбрать файл";
    public string FileDialog => Lang == LangEnum.English ? "File dialog" : "Файловый диалог";
    public string FileDownload => Lang == LangEnum.English ? "Download a file" : "Скачать файл";
    public string FileSaveDialog => Lang == LangEnum.English ? "Specify the file name to save" : "Указать имя файла для сохранения";
    public string FileUpload => Lang == LangEnum.English ? "Upload a file" : "Загрузить файл";
    public string From => Lang == LangEnum.English ? "from" : "из";
    public string Login => Lang == LangEnum.English ? "Login" : "Логин";
    public string MenuAccess => Lang == LangEnum.English ? "Menu access" : "Доступ к меню";
    public string MenuAccessAllow => Lang == LangEnum.English ? "Menu access allowed" : "Доступ к меню разрешён";
    public string MenuAccessDeny => Lang == LangEnum.English ? "Menu access denied" : "Доступ к меню запрещён";
    public string MenuDbVersionHistory => Lang == LangEnum.English ? "DB version history" : "История версий БД";
    public string MenuHome => Lang == LangEnum.English ? "Home" : "Домой";
    public string MenuInfo => Lang == LangEnum.English ? "Info" : "Информация";
    public string MenuMain => Lang == LangEnum.English ? "Main" : "Главная";
    public string MenuReferences => Lang == LangEnum.English ? "References" : "Справочники";
    public string MenuReports => Lang == LangEnum.English ? "Reports" : "Журналы";
    public string MenuSecurity => Lang == LangEnum.English ? "Security" : "Безопасность";
    public string MenuSystem => Lang == LangEnum.English ? "System" : "Система";
    public string ServerResponse => Lang == LangEnum.English ? "Server response" : "Ответ сервера";

    #endregion
}
