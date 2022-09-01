// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Localizations;

public class LocaleDialog
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleDialog _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleDialog Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

    #region Public and private fields, properties, constructor

    public string DialogButtonCancel => Lang == ShareEnums.Lang.English ? "Cancel" : "Отмена";
    public string DialogButtonNo => Lang == ShareEnums.Lang.English ? "No" : "Нет";
    public string DialogButtonYes => Lang == ShareEnums.Lang.English ? "Yes" : "Да";
    public string DialogQuestion => Lang == ShareEnums.Lang.English ? "Perform the operation?" : "Выполнить операцию?";
    public string DialogResultCancel => Lang == ShareEnums.Lang.English ? "Cancel operation. The necessary conditions may not have been met." : "Отмена операции. Возможно, не выполнены необходимые условия.";
    public string DialogResultFail => Lang == ShareEnums.Lang.English ? "Operation error!" : "Ошибка выполнения операции!";
    public string DialogResultSuccess => Lang == ShareEnums.Lang.English ? "The operation was performed successfully." : "Операция выполнена успешно.";

    #endregion
}
