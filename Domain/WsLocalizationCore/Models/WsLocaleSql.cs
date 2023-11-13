namespace WsLocalizationCore.Models;

public sealed class WsLocaleSql : WsLocalizationBase
{
    #region Public and private fields, properties, constructor
    
    public string SqlItemFieldException => Lang == WsEnumLanguage.English ? "Exception" : "Исключение";
    public string SqlItemFieldFile => Lang == WsEnumLanguage.English ? "Test.cs" : "Тест.cs";
    public string SqlItemFieldInnerException => Lang == WsEnumLanguage.English ? "Inner exception" : "Вложенное исключение";
    public string SqlItemFieldMember => Lang == WsEnumLanguage.English ? "Method" : "Метод";
    public string SqlItemFieldMessage => Lang == WsEnumLanguage.English ? "Message" : "Сообщение";
    public string SqlItemFieldTemplateData => "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
    public string SqlItemFieldVersion => "0.1.2";
    public string SqlItemFieldZpl => Lang == WsEnumLanguage.English ? "ZPL" : "ЗПЛ";
    public string SqlServerVS => Lang == WsEnumLanguage.English ? "Development environment" : "Среда разработки";
    
    #endregion
}