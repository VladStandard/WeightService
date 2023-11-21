namespace WsLocalizationCore.Models;

public sealed class LocaleSystem : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string ConfigFileIsEmpty(string file) => Lang == EnumLanguage.English ? $"Config file is empty! {file}" : $"Файл конфига пуст! {file}";
    public string ConfigRemoteFileNotFound => Lang == EnumLanguage.English ? "Remote config file not found!" : "Удалённый файл конфигов не найден!";
    public string ConfigRemoteFolderNotFound => Lang == EnumLanguage.English ? "Remote config directory not found!" : "Удалённый каталог конфигов не найден!";
    public string DatabaseInfo => Lang == EnumLanguage.English ? "Database info" : "База данных";
    public string SystemLogs => Lang == EnumLanguage.English ? "Logs" : "Журналы";
    public string SystemLogsAll => Lang == EnumLanguage.English ? "Alls" : "Все";
    public string UserInfo => Lang == EnumLanguage.English ? "Profile" : "Профиль";
    public string Users => Lang == EnumLanguage.English ? "Users" : "Пользователи";

    #endregion
}