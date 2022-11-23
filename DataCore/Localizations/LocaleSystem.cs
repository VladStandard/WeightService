// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;

namespace DataCore.Localizations;

public class LocaleSystem
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleSystem _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleSystem Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

    #region Public and private fields, properties, constructor

    public string JsonSettingsFileIsEmpty(string file) => Lang == Lang.English ? $"Json-settings file is empty! {file}" : $"Файл json-настроек пуст! {file}";
    public string JsonSettingsFileNotFound(string file) => Lang == Lang.English ? $"Json-settings file not found! {file}" : $"Локальный файл json-настроек не найден! {file}";
    public string JsonSettingsLocalFileException => Lang == Lang.English ? "Exception in json-settings file!" : "Ошибка файла json-настроек!";
    public string JsonSettingsLocalFileNotFound => Lang == Lang.English ? "Local json-settings file not found!" : "Локальный файл json-настроек не найден!";
    public string JsonSettingsParseVersionException(string file) => Lang == Lang.English ? $"Error parsing the json-settings file version! {file}" : $"Ошибка парсинга версии файла json-настроек! {file}";
    public string JsonSettingsRemoteFileException => Lang == Lang.English ? "Remote json-settings file exception!" : "Ошибка удалённых json-настроек!";
    public string JsonSettingsRemoteFileNotFound => Lang == Lang.English ? "Remote json-settings file not found!" : "Удалённый файл json-настроек не найден!";
    public string JsonSettingsRemoteFolderNotFound => Lang == Lang.English ? "Remote json-settings folder not found!" : "Удалённый каталог json-настроек не найден!";
    public string SystemAccess => Lang == Lang.English ? "Access" : "Доступ";
    public string SystemAccount => Lang == Lang.English ? "Account" : "Аккаунт";
    public string SystemErrors => Lang == Lang.English ? "Errors" : "Ошибки";
    public string SystemIdentityData => Lang == Lang.English ? "Data" : "Данные";
    public string SystemIdentityDataFull => Lang == Lang.English ? "Identity data" : "Идентификационные данные";
    public string SystemIdentityNotAuthorized => Lang == Lang.English ? "User is not authorized" : "Пользователь не авторизован";
    public string SystemInfo => Lang == Lang.English ? "System info" : "Системная информация";
    public string SystemLogin => Lang == Lang.English ? "Log in" : "Вход";
    public string SystemLogs => Lang == Lang.English ? "Logs" : "Журналы";
    public string SystemLogsAll => Lang == Lang.English ? "Alls" : "Все";
    public string SystemLogsErrors => Lang == Lang.English ? "Errors" : "Ошибки";
    public string SystemLogsInformations => Lang == Lang.English ? "Informations" : "Инфо";
    public string SystemLogsNones => Lang == Lang.English ? "Nones" : "Без типа";
    public string SystemLogsQuestions => Lang == Lang.English ? "Questions" : "Вопросы";
    public string SystemLogsStops => Lang == Lang.English ? "Stops" : "Остановы";
    public string SystemLogsWarnings => Lang == Lang.English ? "Warnings" : "Предупреждения";
    public string SystemWindowsUser => Lang == Lang.English ? "Windows-user" : "Windows-пользователь";
    public string UserInfo => Lang == Lang.English ? "User info" : "Информация о пользователе";
    public string Users => Lang == Lang.English ? "Users" : "Пользователи";

    #endregion
}
