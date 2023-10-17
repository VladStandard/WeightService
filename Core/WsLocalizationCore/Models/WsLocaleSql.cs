namespace WsLocalizationCore.Models;

public sealed class WsLocaleSql : WsLocalizationBase
{
    #region Public and private fields, properties, constructor
    
    public string SqlItemFieldException => Lang == WsEnumLanguage.English ? "Exception" : "Исключение";
    public string SqlItemFieldFile => Lang == WsEnumLanguage.English ? "Test.cs" : "Тест.cs";
    public string SqlItemFieldIcon => Lang == WsEnumLanguage.English ? "Icon" : "Иконка";
    public string SqlItemFieldInnerException => Lang == WsEnumLanguage.English ? "Inner exception" : "Вложенное исключение";
    public string SqlItemFieldIp => "127.0.0.1";
    public string SqlItemFieldMac => "001122334455";
    public string SqlItemFieldMember => Lang == WsEnumLanguage.English ? "Method" : "Метод";
    public string SqlItemFieldMessage => Lang == WsEnumLanguage.English ? "Message" : "Сообщение";
    public string SqlItemFieldTemplateData => "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
    public string SqlItemFieldTemplateResourceType => "ZPL";
    public string SqlItemFieldUrl => "https://{{local}}/api/info/";
    public string SqlItemFieldVersion => "0.1.2";
    public string SqlItemFieldZpl => Lang == WsEnumLanguage.English ? "ZPL" : "ЗПЛ";
    public string SqlServerDevelopAleksandrov => Lang == WsEnumLanguage.English ? "Development environment | Aleksandrov" : "Среда разработки | Александров";
    public string SqlServerDevelopMorozov => Lang == WsEnumLanguage.English ? "Development environment | Morozov" : "Среда разработки | Морозов";
    public string SqlServerVS => Lang == WsEnumLanguage.English ? "Development environment" : "Среда разработки";
    
    #endregion
}