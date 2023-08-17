// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class WsLocaleSystem : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public string ConfigFileIsEmpty(string file) => Lang == WsEnumLanguage.English ? $"Config file is empty! {file}" : $"Файл конфига пуст! {file}";
    public string ConfigFileNotFound(string file) => Lang == WsEnumLanguage.English ? $"Config file not found! {file}" : $"Локальный файл конфига не найден! {file}";
    public string ConfigLocalFileException => Lang == WsEnumLanguage.English ? "Exception in config file!" : "Ошибка файла конфига!";
    public string ConfigLocalFileNotFound => Lang == WsEnumLanguage.English ? "Local config file not found!" : "Локальный файл конфига не найден!";
    public string ConfigParseVersionException(string file) => Lang == WsEnumLanguage.English ? $"Error parsing the config file version! {file}" : $"Ошибка парсинга версии файла конфигов! {file}";
    public string ConfigRemoteFileException => Lang == WsEnumLanguage.English ? "Remote config file exception!" : "Ошибка удалённых конфигов!";
    public string ConfigRemoteFileNotFound => Lang == WsEnumLanguage.English ? "Remote config file not found!" : "Удалённый файл конфигов не найден!";
    public string ConfigRemoteFolderNotFound => Lang == WsEnumLanguage.English ? "Remote config directory not found!" : "Удалённый каталог конфигов не найден!";
    public string DatabaseInfo => Lang == WsEnumLanguage.English ? "Database info" : "База данных";
    public string SystemAccess => Lang == WsEnumLanguage.English ? "Access" : "Доступ";
    public string SystemAccount => Lang == WsEnumLanguage.English ? "Account" : "Аккаунт";
    public string SystemErrors => Lang == WsEnumLanguage.English ? "Errors" : "Ошибки";
    public string SystemIdentityDataFull => Lang == WsEnumLanguage.English ? "Identity data" : "Идентификационные данные";
    public string SystemIdentityNotAuthorized => Lang == WsEnumLanguage.English ? "User is not authorized" : "Пользователь не авторизован";
    public string SystemInfo => Lang == WsEnumLanguage.English ? "System info" : "Системная информация";
    public string SystemLogin => Lang == WsEnumLanguage.English ? "Log in" : "Вход";
    public string SystemLogs => Lang == WsEnumLanguage.English ? "Logs" : "Журналы";
    public string SystemLogsAll => Lang == WsEnumLanguage.English ? "Alls" : "Все";
    public string SystemLogsErrors => Lang == WsEnumLanguage.English ? "Errors" : "Ошибки";
    public string SystemLogsInformations => Lang == WsEnumLanguage.English ? "Informations" : "Инфо";
    public string SystemLogsNones => Lang == WsEnumLanguage.English ? "Nones" : "Без типа";
    public string SystemLogsQuestions => Lang == WsEnumLanguage.English ? "Questions" : "Вопросы";
    public string SystemLogsStops => Lang == WsEnumLanguage.English ? "Stops" : "Остановы";
    public string SystemLogsWarnings => Lang == WsEnumLanguage.English ? "Warnings" : "Предупреждения";
    public string SystemWindowsUser => Lang == WsEnumLanguage.English ? "Windows-user" : "Windows-пользователь";
    public string UserInfo => Lang == WsEnumLanguage.English ? "Profile" : "Профиль";
    public string Users => Lang == WsEnumLanguage.English ? "Users" : "Пользователи";

    #endregion
}