namespace WsLocalizationCore.Models;

public sealed class LocaleSql : LocalizationBase
{
    #region Public and private fields, properties, constructor
    
    public string SqlItemFieldFile => Lang == EnumLanguage.English ? "Test.cs" : "Тест.cs";
    public string SqlItemFieldMember => Lang == EnumLanguage.English ? "Method" : "Метод";
    public string SqlItemFieldMessage => Lang == EnumLanguage.English ? "Message" : "Сообщение";
    public string SqlItemFieldTemplateData => "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
    public string SqlItemFieldZpl => Lang == EnumLanguage.English ? "ZPL" : "ЗПЛ";
    
    #endregion
}