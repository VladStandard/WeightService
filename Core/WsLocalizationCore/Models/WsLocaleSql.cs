// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

public sealed class WsLocaleSql : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public string SqlDb => Lang == WsEnumLanguage.English ? "SQL-DB" : "SQL-БД";
    public string SqlDbCurSize => Lang == WsEnumLanguage.English ? "DB size" : "Размер БД";
    public string SqlDbFillSize => Lang == WsEnumLanguage.English ? "DB fill percentage" : "Процент заполнения БД";
    public string SqlDbMaxSize => Lang == WsEnumLanguage.English ? "DB size" : "Максимальный размер БД";
    public string SqlItemDoSelect => Lang == WsEnumLanguage.English ? "Select the record" : "Выберите запись";
    public string SqlItemFieldAddress => Lang == WsEnumLanguage.English ? "Address" : "Адрес";
    public string SqlItemFieldCode => Lang == WsEnumLanguage.English ? "00012345" : "ЦБД00012345";
    public string SqlItemFieldDescription => Lang == WsEnumLanguage.English ? "Description" : "Описание";
    public string SqlItemFieldEan13 => Lang == WsEnumLanguage.English ? "EAN 13" : "ЕАН 13";
    public string SqlItemFieldException => Lang == WsEnumLanguage.English ? "Exception" : "Исключение";
    public string SqlItemFieldFile => Lang == WsEnumLanguage.English ? "Test.cs" : "Тест.cs";
    public string SqlItemFieldFullName => Lang == WsEnumLanguage.English ? "Fullname" : "Полное наименование";
    public string SqlItemFieldGtin => Lang == WsEnumLanguage.English ? "GTIN" : "ГТИН";
    public string SqlItemFieldHostName => Lang == WsEnumLanguage.English ? "Hostname" : "Хост";
    public string SqlItemFieldIcon => Lang == WsEnumLanguage.English ? "Icon" : "Иконка";
    public string SqlItemFieldInnerException => Lang == WsEnumLanguage.English ? "Inner exception" : "Вложенное исключение";
    public string SqlItemFieldIp => "127.0.0.1";
    public string SqlItemFieldItf14 => Lang == WsEnumLanguage.English ? "ITF 14" : "ИТФ 14";
    public string SqlItemFieldMac => "001122334455";
    public string SqlItemFieldMeasurementTypeKg => Lang == WsEnumLanguage.English ? "kg" : "кг";
    public string SqlItemFieldMeasurementTypePice => Lang == WsEnumLanguage.English ? "pc" : "шт";
    public string SqlItemFieldMember => Lang == WsEnumLanguage.English ? "Method" : "Метод";
    public string SqlItemFieldMessage => Lang == WsEnumLanguage.English ? "Message" : "Сообщение";
    public string SqlItemFieldName => Lang == WsEnumLanguage.English ? "Name" : "Наименование";
    public string SqlItemFieldNesting => Lang == WsEnumLanguage.English ? "Nesting" : "Вложенность";
    public string SqlItemFieldPrettyName => Lang == WsEnumLanguage.English ? "pretty name" : "Красивое наименование";
    public string SqlItemFieldProductXml => Lang == WsEnumLanguage.English ? "\"<Product Category=\\\"Meat\\\" > </Product>\"" : "\"<Product Category=\\\"Сосиски\\\" > </Product>\"";
    public string SqlItemFieldSscc => Lang == WsEnumLanguage.English ? "SSCC code" : "SSCC код";
    public string SqlItemFieldTemplateData => "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
    public string SqlItemFieldTemplateResourceType => "ZPL";
    public string SqlItemFieldTitle => Lang == WsEnumLanguage.English ? "Title" : "Заголовок";
    public string SqlItemFieldUrl => "https://{{local}}/api/info/";
    public string SqlItemFieldValue => Lang == WsEnumLanguage.English ? "Value" : "Значение";
    public string SqlItemFieldVersion => "0.1.2";
    public string SqlItemFieldZpl => Lang == WsEnumLanguage.English ? "ZPL" : "ЗПЛ";
    public string SqlItemIsNotSelect => Lang == WsEnumLanguage.English ? "Record is not select" : "Запись не выбрана";
    public string SqlServer => Lang == WsEnumLanguage.English ? "SQL-server" : "SQL-сервер";
    public string SqlServerDevelopAleksandrov => Lang == WsEnumLanguage.English ? "Development server | Aleksandrov" : "Сервер разработки | Александров";
    public string SqlServerDevelopMorozov => Lang == WsEnumLanguage.English ? "Development server | Morozov" : "Сервер разработки | Морозов";
    // ReSharper disable once InconsistentNaming
    public string SqlServerVS => Lang == WsEnumLanguage.English ? "Development server" : "Сервер разработки";
    // ReSharper disable once InconsistentNaming
    public string SqlServerReleaseVS => Lang == WsEnumLanguage.English ? "Product server" : "Продуктовый сервер";
    public string SqlServerReleaseAleksandrov => Lang == WsEnumLanguage.English ? "Product server | Aleksandrov" : "Продуктовый сервер | Александров";
    public string SqlServerReleaseMorozov => Lang == WsEnumLanguage.English ? "Product server | Morozov" : "Продуктовый сервер | Морозов";
    public string SqlServerTest => Lang == WsEnumLanguage.English ? "Test server" : "Тестовый сервер";
    public string SqlUser => Lang == WsEnumLanguage.English ? "SQL-user" : "SQL-пользователь";
    public string StatusClosed => Lang == WsEnumLanguage.English ? @"Connecting to SQL server is close." : "Закрыто подключение к SQL-серверу.";
    public string StatusConnected => Lang == WsEnumLanguage.English ? @"Connecting to SQL server is open." : "Открыто подключение к SQL-серверу.";
    public string StatusExceptionConnect() => Lang == WsEnumLanguage.English ? @"Error connecting to SQL-server!" : "Ошибка настроек подключения к SQL-серверу!";
    public string StatusExceptionConnect(Exception ex) => Lang == WsEnumLanguage.English ? $@"Error connecting to SQL server! Message: {ex.Message}" : $"Ошибка подключения к SQL-серверу! Сообщение: {ex.Message}";
    public string StatusExceptionFieldValue(Exception ex) => Lang == WsEnumLanguage.English ? $@"Error! Field value is incorrect! Message: {ex.Message}" : $"Ошибка! Значение поля некорректно! Сообщение: {ex.Message}";
    public string StatusExceptionFieldValue(Exception ex, string waitType, string ordinalType) => Lang == WsEnumLanguage.English ? $@"Error! Field value is incorrect! Expected data type: {waitType}. Received data type: {ordinalType}. Message: {ex.Message}" : $"Ошибка! Значение поля некорректно! Ожидаемый тип данных: {waitType}. Полученный тип данных: {ordinalType}. Сообщение: {ex.Message}";

    #endregion
}