// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleSystem
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleSystem _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleSystem Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string JsonSettingsFileIsEmpty(string file) => Lang == LangEnum.English ? $"Json-settings file is empty! {file}" : $"Файл json-настроек пуст! {file}";
    public string JsonSettingsFileNotFound(string file) => Lang == LangEnum.English ? $"Json-settings file not found! {file}" : $"Локальный файл json-настроек не найден! {file}";
    public string JsonSettingsLocalFileException => Lang == LangEnum.English ? "Exception in json-settings file!" : "Ошибка файла json-настроек!";
    public string JsonSettingsLocalFileNotFound => Lang == LangEnum.English ? "Local json-settings file not found!" : "Локальный файл json-настроек не найден!";
    public string JsonSettingsParseVersionException(string file) => Lang == LangEnum.English ? $"Error parsing the json-settings file version! {file}" : $"Ошибка парсинга версии файла json-настроек! {file}";
    public string JsonSettingsRemoteFileException => Lang == LangEnum.English ? "Remote json-settings file exception!" : "Ошибка удалённых json-настроек!";
    public string JsonSettingsRemoteFileNotFound => Lang == LangEnum.English ? "Remote json-settings file not found!" : "Удалённый файл json-настроек не найден!";
    public string JsonSettingsRemoteFolderNotFound => Lang == LangEnum.English ? "Remote json-settings folder not found!" : "Удалённый каталог json-настроек не найден!";
    public string SystemAccess => Lang == LangEnum.English ? "Access" : "Доступ";
    public string SystemAccount => Lang == LangEnum.English ? "Account" : "Аккаунт";
    public string SystemErrors => Lang == LangEnum.English ? "Errors" : "Ошибки";
    public string SystemIdentityData => Lang == LangEnum.English ? "Data" : "Данные";
    public string SystemIdentityDataFull => Lang == LangEnum.English ? "Identity data" : "Идентификационные данные";
    public string SystemIdentityNotAuthorized => Lang == LangEnum.English ? "User is not authorized" : "Пользователь не авторизован";
    public string SystemInfo => Lang == LangEnum.English ? "System info" : "Системная информация";
    public string SystemLogin => Lang == LangEnum.English ? "Log in" : "Вход";
    public string SystemLogs => Lang == LangEnum.English ? "Logs" : "Журналы";
    public string SystemLogsAll => Lang == LangEnum.English ? "Alls" : "Все";
    public string SystemLogsErrors => Lang == LangEnum.English ? "Errors" : "Ошибки";
    public string SystemLogsInformations => Lang == LangEnum.English ? "Informations" : "Инфо";
    public string SystemLogsNones => Lang == LangEnum.English ? "Nones" : "Без типа";
    public string SystemLogsQuestions => Lang == LangEnum.English ? "Questions" : "Вопросы";
    public string SystemLogsStops => Lang == LangEnum.English ? "Stops" : "Остановы";
    public string SystemLogsWarnings => Lang == LangEnum.English ? "Warnings" : "Предупреждения";
    public string SystemWindowsUser => Lang == LangEnum.English ? "Windows-user" : "Windows-пользователь";
    public string UserInfo => Lang == LangEnum.English ? "User info" : "Информация о пользователе";

    #endregion
}
