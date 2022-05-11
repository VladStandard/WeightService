﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;

namespace DataCore.Localizations
{
    public class LocaleSystem
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LocaleSystem _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LocaleSystem Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        #region Public and private fields and properties

        public string JsonSettingsLocalFileException => Lang == ShareEnums.Lang.English ? "Exception in json-settings file!" : "Ошибка файла json-настроек!";
        public string JsonSettingsFileIsEmpty(string file) => Lang == ShareEnums.Lang.English ? $"Json-settings file is empty! {file}" : $"Файл json-настроек пуст! {file}";
        public string JsonSettingsFileNotFound(string file) => Lang == ShareEnums.Lang.English ? $"Json-settings file not found! {file}" : $"Локальный файл json-настроек не найден! {file}";
        public string JsonSettingsLocalFileNotFound => Lang == ShareEnums.Lang.English ? "Local json-settings file not found!" : "Локальный файл json-настроек не найден!";
        public string JsonSettingsParseVersionException(string file) => Lang == ShareEnums.Lang.English ? $"Error parsing the json-settings file version! {file}" : $"Ошибка парсинга версии файла json-настроек! {file}";
        public string JsonSettingsRemoteFileException => Lang == ShareEnums.Lang.English ? "Remote json-settings file exception!" : "Ошибка удалённых json-настроек!";
        public string JsonSettingsRemoteFileNotFound => Lang == ShareEnums.Lang.English ? "Remote json-settings file not found!" : "Удалённый файл json-настроек не найден!";
        public string JsonSettingsRemoteFolderNotFound => Lang == ShareEnums.Lang.English ? "Remote json-settings folder not found!" : "Удалённый каталог json-настроек не найден!";
        public string SystemAccess => Lang == ShareEnums.Lang.English ? "Access" : "Доступ";
        public string SystemAccount => Lang == ShareEnums.Lang.English ? "Account" : "Аккаунт";
        public string SystemErrors => Lang == ShareEnums.Lang.English ? "Errors" : "Ошибки";
        public string SystemInfo => Lang == ShareEnums.Lang.English ? "Info" : "Информация";
        public string SystemLogin => Lang == ShareEnums.Lang.English ? "Log in" : "Вход";
        public string SystemLogs => Lang == ShareEnums.Lang.English ? "Logs" : "журналы";
        public string SystemLogsAll => Lang == ShareEnums.Lang.English ? "Alls" : "Все";
        public string SystemLogsNones => Lang == ShareEnums.Lang.English ? "Nones" : "Без типа";
        public string SystemLogsInformations => Lang == ShareEnums.Lang.English ? "Informations" : "Инфо";
        public string SystemLogsErrors => Lang == ShareEnums.Lang.English ? "Errors" : "Ошибки";
        public string SystemLogsQuestions => Lang == ShareEnums.Lang.English ? "Questions" : "Вопросы";
        public string SystemLogsStops => Lang == ShareEnums.Lang.English ? "Stops" : "Остановы";
        public string SystemLogsWarnings => Lang == ShareEnums.Lang.English ? "Warnings" : "Предупреждения";
        public string SystemWindowsUser => Lang == ShareEnums.Lang.English ? "Windows-user" : "Windows-пользователь";

        #endregion
    }
}
