// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalization.Models;

public class LocaleWebService
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleWebService _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleWebService Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

    #region Public and private fields, properties, constructor

    public string IsEmpty => Lang == Lang.English ? "is empty value" : "пустое значение";
    public string IsStatusSuccess => Lang == Lang.English ? "Done successfully" : "Выполнено успешно";
    public string FieldNomenclature => Lang == Lang.English ? "Nomenclature" : "Номенклатура";
    public string FieldBundle  => Lang == Lang.English ? "Bundle" : "Пакет";

    #endregion
}