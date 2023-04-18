// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public class LocaleDialog
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleDialog _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleDialog Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

    #region Public and private fields, properties, constructor

    public string DialogButtonCancel => Lang == Lang.English ? "Cancel" : "Отмена";
    public string DialogButtonNo => Lang == Lang.English ? "No" : "Нет";
    public string DialogButtonYes => Lang == Lang.English ? "Yes" : "Да";
    public string DialogQuestion => Lang == Lang.English ? "Perform the operation?" : "Выполнить операцию?";
    public string DialogResultCancel => Lang == Lang.English ? "Cancel operation. The necessary conditions may not have been met." : "Отмена операции. Возможно, не выполнены необходимые условия.";
    public string DialogResultFail => Lang == Lang.English ? "Operation error!" : "Ошибка выполнения операции!";
    public string DialogResultSuccess => Lang == Lang.English ? "The operation was performed successfully." : "Операция выполнена успешно.";

    #endregion
}