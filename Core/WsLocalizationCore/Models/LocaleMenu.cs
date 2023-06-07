// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Common;

namespace WsLocalizationCore.Models;

public sealed class LocaleMenu : WsLocalizationBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleMenu _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleMenu Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public string FileChoose => Lang == WsEnumLanguage.English ? "Select a file" : "Выбрать файл";
    public string FileDialog => Lang == WsEnumLanguage.English ? "File dialog" : "Файловый диалог";
    public string FileDownload => Lang == WsEnumLanguage.English ? "Download a file" : "Скачать файл";
    public string FileSaveDialog => Lang == WsEnumLanguage.English ? "Specify the file name to save" : "Указать имя файла для сохранения";
    public string FileUpload => Lang == WsEnumLanguage.English ? "Upload a file" : "Загрузить файл";
    public string From => Lang == WsEnumLanguage.English ? "from" : "из";
    public string Login => Lang == WsEnumLanguage.English ? "Login" : "Логин";
    public string MenuAccess => Lang == WsEnumLanguage.English ? "Menu access" : "Доступ к меню";
    public string MenuAccessAllow => Lang == WsEnumLanguage.English ? "Menu access allowed" : "Доступ к меню разрешён";
    public string MenuAccessDeny => Lang == WsEnumLanguage.English ? "Menu access denied" : "Доступ к меню запрещён";
    public string MenuDbVersionHistory => Lang == WsEnumLanguage.English ? "DB version history" : "История версий БД";
    public string MenuHome => Lang == WsEnumLanguage.English ? "Home" : "Домой";
    public string MenuHelp => Lang == WsEnumLanguage.English ? "Help" : "Справка";
    public string MenuInfo => Lang == WsEnumLanguage.English ? "Info" : "Информация";
    public string MenuMain => Lang == WsEnumLanguage.English ? "Main" : "Главная";
    public string MenuReferences => Lang == WsEnumLanguage.English ? "References" : "Справочники";
    public string MenuReports => Lang == WsEnumLanguage.English ? "Diagnostic" : "Диагностика";
    public string MenuSecurity => Lang == WsEnumLanguage.English ? "Security" : "Безопасность";
    public string MenuSystem => Lang == WsEnumLanguage.English ? "System" : "Система";
    public string ServerResponse => Lang == WsEnumLanguage.English ? "Server response" : "Ответ сервера";

    #endregion
}