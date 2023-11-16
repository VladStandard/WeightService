namespace WsLocalizationCore.Models;

public sealed class LocaleDialog : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string DialogButtonCancel => Lang == EnumLanguage.English ? "Cancel" : "Отмена";
    public string DialogButtonNo => Lang == EnumLanguage.English ? "No" : "Нет";
    public string DialogButtonYes => Lang == EnumLanguage.English ? "Yes" : "Да";
    public string DialogQuestion => Lang == EnumLanguage.English ? "Perform the operation?" : "Выполнить операцию?";
    public string DialogResultCancel => Lang == EnumLanguage.English ? "Cancel operation. The necessary conditions may not have been met." : "Отмена операции. Возможно, не выполнены необходимые условия.";
    public string DialogResultFail => Lang == EnumLanguage.English ? "Operation error!" : "Ошибка выполнения операции!";
    public string DialogResultSuccess => Lang == EnumLanguage.English ? "The operation was performed successfully." : "Операция выполнена успешно.";

    #endregion
}