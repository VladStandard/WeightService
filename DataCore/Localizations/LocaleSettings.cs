// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleSettings
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleSettings _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleSettings Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string AllowedHosts => Lang == LangEnum.English ? "Allowed hosts" : "Разрешенные хосты";
    public string SectionRowsCount => Lang == LangEnum.English ? "Section's rows count" : "Количество строк в секции";
    public string ItemRowsCount => Lang == LangEnum.English ? "Records's rows count" : "Количество строк в записи";
    public string SectionAndItemRowsCount => Lang == LangEnum.English ? "Section's and record's rows count" : "Количество строк в секции и записи";
    public string SelectTopRowsCount => Lang == LangEnum.English ? "Selection's top rows count" : "Количество строк выборки";
    public string Version => Lang == LangEnum.English ? "Version of the json-settings file" : "Версия файла json-настроек";

    #endregion
}
