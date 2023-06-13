// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class WsLocaleSystem : WsLocaleBase
{
    #region Public and private fields, properties, constructor

    public string JsonSettingsFileIsEmpty(string file) => Lang == WsEnumLanguage.English ? $"Json-settings file is empty! {file}" : $"Файл json-настроек пуст! {file}";
    public string JsonSettingsFileNotFound(string file) => Lang == WsEnumLanguage.English ? $"Json-settings file not found! {file}" : $"Локальный файл json-настроек не найден! {file}";
    public string JsonSettingsLocalFileException => Lang == WsEnumLanguage.English ? "Exception in json-settings file!" : "Ошибка файла json-настроек!";
    public string JsonSettingsLocalFileNotFound => Lang == WsEnumLanguage.English ? "Local json-settings file not found!" : "Локальный файл json-настроек не найден!";
    public string JsonSettingsParseVersionException(string file) => Lang == WsEnumLanguage.English ? $"Error parsing the json-settings file version! {file}" : $"Ошибка парсинга версии файла json-настроек! {file}";
    public string JsonSettingsRemoteFileException => Lang == WsEnumLanguage.English ? "Remote json-settings file exception!" : "Ошибка удалённых json-настроек!";
    public string JsonSettingsRemoteFileNotFound => Lang == WsEnumLanguage.English ? "Remote json-settings file not found!" : "Удалённый файл json-настроек не найден!";
    public string JsonSettingsRemoteFolderNotFound => Lang == WsEnumLanguage.English ? "Remote json-settings folder not found!" : "Удалённый каталог json-настроек не найден!";
    public string SystemAccess => Lang == WsEnumLanguage.English ? "Access" : "Доступ";
    public string SystemAccount => Lang == WsEnumLanguage.English ? "Account" : "Аккаунт";
    public string SystemErrors => Lang == WsEnumLanguage.English ? "Errors" : "Ошибки";
    public string DatabaseInfo => Lang == WsEnumLanguage.English ? "Database info" : "База данных";
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