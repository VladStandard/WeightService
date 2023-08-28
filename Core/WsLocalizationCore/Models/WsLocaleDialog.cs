namespace WsLocalizationCore.Models;

public sealed class WsLocaleDialog : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public string DialogButtonCancel => Lang == WsEnumLanguage.English ? "Cancel" : "Отмена";
    public string DialogButtonNo => Lang == WsEnumLanguage.English ? "No" : "Нет";
    public string DialogButtonYes => Lang == WsEnumLanguage.English ? "Yes" : "Да";
    public string DialogQuestion => Lang == WsEnumLanguage.English ? "Perform the operation?" : "Выполнить операцию?";
    public string DialogResultCancel => Lang == WsEnumLanguage.English ? "Cancel operation. The necessary conditions may not have been met." : "Отмена операции. Возможно, не выполнены необходимые условия.";
    public string DialogResultFail => Lang == WsEnumLanguage.English ? "Operation error!" : "Ошибка выполнения операции!";
    public string DialogResultSuccess => Lang == WsEnumLanguage.English ? "The operation was performed successfully." : "Операция выполнена успешно.";

    #endregion
}