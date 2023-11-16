using WsLocalizationCore.Common;
namespace WsLocalizationCore.Models;

public sealed class LocaleSystem : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string ConfigFileIsEmpty(string file) => Lang == EnumLanguage.English ? $"Config file is empty! {file}" : $"Файл конфига пуст! {file}";
    public string ConfigFileNotFound(string file) => Lang == EnumLanguage.English ? $"Config file not found! {file}" : $"Локальный файл конфига не найден! {file}";
    public string ConfigLocalFileException => Lang == EnumLanguage.English ? "Exception in config file!" : "Ошибка файла конфига!";
    public string ConfigLocalFileNotFound => Lang == EnumLanguage.English ? "Local config file not found!" : "Локальный файл конфига не найден!";
    public string ConfigParseVersionException(string file) => Lang == EnumLanguage.English ? $"Error parsing the config file version! {file}" : $"Ошибка парсинга версии файла конфигов! {file}";
    public string ConfigRemoteFileException => Lang == EnumLanguage.English ? "Remote config file exception!" : "Ошибка удалённых конфигов!";
    public string ConfigRemoteFileNotFound => Lang == EnumLanguage.English ? "Remote config file not found!" : "Удалённый файл конфигов не найден!";
    public string ConfigRemoteFolderNotFound => Lang == EnumLanguage.English ? "Remote config directory not found!" : "Удалённый каталог конфигов не найден!";
    public string DatabaseInfo => Lang == EnumLanguage.English ? "Database info" : "База данных";
    public string SystemAccess => Lang == EnumLanguage.English ? "Access" : "Доступ";
    public string SystemAccount => Lang == EnumLanguage.English ? "Account" : "Аккаунт";
    public string SystemErrors => Lang == EnumLanguage.English ? "Errors" : "Ошибки";
    public string SystemIdentityDataFull => Lang == EnumLanguage.English ? "Identity data" : "Идентификационные данные";
    public string SystemIdentityNotAuthorized => Lang == EnumLanguage.English ? "User is not authorized" : "Пользователь не авторизован";
    public string SystemInfo => Lang == EnumLanguage.English ? "System info" : "Системная информация";
    public string SystemLogin => Lang == EnumLanguage.English ? "Log in" : "Вход";
    public string SystemLogs => Lang == EnumLanguage.English ? "Logs" : "Журналы";
    public string SystemLogsAll => Lang == EnumLanguage.English ? "Alls" : "Все";
    public string SystemLogsErrors => Lang == EnumLanguage.English ? "Errors" : "Ошибки";
    public string SystemLogsInformations => Lang == EnumLanguage.English ? "Informations" : "Инфо";
    public string SystemLogsNones => Lang == EnumLanguage.English ? "Nones" : "Без типа";
    public string SystemLogsQuestions => Lang == EnumLanguage.English ? "Questions" : "Вопросы";
    public string SystemLogsStops => Lang == EnumLanguage.English ? "Stops" : "Остановы";
    public string SystemLogsWarnings => Lang == EnumLanguage.English ? "Warnings" : "Предупреждения";
    public string SystemWindowsUser => Lang == EnumLanguage.English ? "Windows-user" : "Windows-пользователь";
    public string UserInfo => Lang == EnumLanguage.English ? "Profile" : "Профиль";
    public string Users => Lang == EnumLanguage.English ? "Users" : "Пользователи";

    #endregion
}