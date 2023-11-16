namespace WsLocalizationCore.Models;

public sealed class LocaleMenu : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string FileChoose => Lang == EnumLanguage.English ? "Select a file" : "Выбрать файл";
    public string FileDialog => Lang == EnumLanguage.English ? "File dialog" : "Файловый диалог";
    public string FileDownload => Lang == EnumLanguage.English ? "Download a file" : "Скачать файл";
    public string FileSaveDialog => Lang == EnumLanguage.English ? "Specify the file name to save" : "Указать имя файла для сохранения";
    public string FileUpload => Lang == EnumLanguage.English ? "Upload a file" : "Загрузить файл";
    public string From => Lang == EnumLanguage.English ? "from" : "из";
    public string Login => Lang == EnumLanguage.English ? "Login" : "Логин";
    public string MenuAccess => Lang == EnumLanguage.English ? "Menu access" : "Доступ к меню";
    public string MenuAccessAllow => Lang == EnumLanguage.English ? "Menu access allowed" : "Доступ к меню разрешён";
    public string MenuAccessDeny => Lang == EnumLanguage.English ? "Menu access denied" : "Доступ к меню запрещён";
    public string MenuDbVersionHistory => Lang == EnumLanguage.English ? "DB version history" : "История версий БД";
    public string MenuHome => Lang == EnumLanguage.English ? "Home" : "Домой";
    public string MenuHelp => Lang == EnumLanguage.English ? "Help" : "Справка";
    public string MenuInfo => Lang == EnumLanguage.English ? "Info" : "Информация";
    public string MenuMain => Lang == EnumLanguage.English ? "Main" : "Главная";
    public string MenuReferences => Lang == EnumLanguage.English ? "References" : "Справочники";
    public string MenuReports => Lang == EnumLanguage.English ? "Diagnostic" : "Диагностика";
    public string MenuSecurity => Lang == EnumLanguage.English ? "Security" : "Безопасность";
    public string MenuSystem => Lang == EnumLanguage.English ? "System" : "Система";
    public string ServerResponse => Lang == EnumLanguage.English ? "Server response" : "Ответ сервера";

    #endregion
}