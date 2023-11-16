using WsLocalizationCore.Common;
namespace WsLocalizationCore.Models;

public sealed class LocaleSql : LocalizationBase
{
    #region Public and private fields, properties, constructor
    
    public string SqlItemFieldException => Lang == EnumLanguage.English ? "Exception" : "Исключение";
    public string SqlItemFieldFile => Lang == EnumLanguage.English ? "Test.cs" : "Тест.cs";
    public string SqlItemFieldInnerException => Lang == EnumLanguage.English ? "Inner exception" : "Вложенное исключение";
    public string SqlItemFieldMember => Lang == EnumLanguage.English ? "Method" : "Метод";
    public string SqlItemFieldMessage => Lang == EnumLanguage.English ? "Message" : "Сообщение";
    public string SqlItemFieldTemplateData => "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
    public string SqlItemFieldVersion => "0.1.2";
    public string SqlItemFieldZpl => Lang == EnumLanguage.English ? "ZPL" : "ЗПЛ";
    public string SqlServerVS => Lang == EnumLanguage.English ? "Development environment" : "Среда разработки";
    
    #endregion
}