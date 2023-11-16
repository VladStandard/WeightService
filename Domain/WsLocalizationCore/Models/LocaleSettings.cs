namespace WsLocalizationCore.Models;

public sealed class LocaleSettings : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string AllowedHosts => Lang == EnumLanguage.English ? "Allowed hosts" : "Разрешенные хосты";
    public string SectionRowsCount => Lang == EnumLanguage.English ? "Section's rows count" : "Количество строк в секции";
    public string ItemRowsCount => Lang == EnumLanguage.English ? "Records's rows count" : "Количество строк в записи";
    public string SectionAndItemRowsCount => Lang == EnumLanguage.English ? "Section's and record's rows count" : "Количество строк в секции и записи";
    public string SelectTopRowsCount => Lang == EnumLanguage.English ? "Selection's top rows count" : "Количество строк выборки";
    public string Version => Lang == EnumLanguage.English ? "Version of the json-settings file" : "Версия файла json-настроек";

    #endregion
}