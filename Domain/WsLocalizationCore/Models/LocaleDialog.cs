namespace WsLocalizationCore.Models;

public sealed class LocaleDialog : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public string DialogButtonCancel => Lang == EnumLanguage.English ? "Cancel" : "Отмена";
    public string DialogButtonYes => Lang == EnumLanguage.English ? "Yes" : "Да";
    public string DialogQuestion => Lang == EnumLanguage.English ? "Perform the operation?" : "Выполнить операцию?";
    public string DialogResultFail => Lang == EnumLanguage.English ? "Operation error!" : "Ошибка выполнения операции!";
    public string DialogResultSuccess => Lang == EnumLanguage.English ? "The operation was performed successfully." : "Операция выполнена успешно.";

    #endregion
}