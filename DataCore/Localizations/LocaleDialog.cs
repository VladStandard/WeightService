// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleDialog
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleDialog _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleDialog Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string DialogButtonCancel => Lang == LangEnum.English ? "Cancel" : "Отмена";
    public string DialogButtonNo => Lang == LangEnum.English ? "No" : "Нет";
    public string DialogButtonYes => Lang == LangEnum.English ? "Yes" : "Да";
    public string DialogQuestion => Lang == LangEnum.English ? "Perform the operation?" : "Выполнить операцию?";
    public string DialogResultCancel => Lang == LangEnum.English ? "Cancel operation. The necessary conditions may not have been met." : "Отмена операции. Возможно, не выполнены необходимые условия.";
    public string DialogResultFail => Lang == LangEnum.English ? "Operation error!" : "Ошибка выполнения операции!";
    public string DialogResultSuccess => Lang == LangEnum.English ? "The operation was performed successfully." : "Операция выполнена успешно.";

    #endregion
}
