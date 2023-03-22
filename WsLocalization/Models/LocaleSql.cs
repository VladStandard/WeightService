// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalization.Models;

public class LocaleSql
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleSql _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleSql Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public Lang Lang { get; set; } = Lang.Russian;

    #region Public and private fields, properties, constructor

    public string SqlDb => Lang == Lang.English ? "SQL-DB" : "SQL-БД";
    public string SqlDbCurSize => Lang == Lang.English ? "DB size" : "Размер БД";
    public string SqlDbFillSize => Lang == Lang.English ? "DB fill percentage" : "Процент заполнения БД";
    public string SqlDbMaxSize => Lang == Lang.English ? "DB size" : "Максимальный размер БД";
    public string SqlItemDoSelect => Lang == Lang.English ? "Select the record" : "Выберите запись";
    public string SqlItemFieldAddress => Lang == Lang.English ? "Address" : "Адрес";
    public string SqlItemFieldCode => Lang == Lang.English ? "00012345" : "ЦБД00012345";
    public string SqlItemFieldDescription => Lang == Lang.English ? "Description" : "Описание";
    public string SqlItemFieldEan13 => Lang == Lang.English ? "EAN 13" : "ЕАН 13";
    public string SqlItemFieldException => Lang == Lang.English ? "Exception" : "Исключение";
    public string SqlItemFieldFile => Lang == Lang.English ? "Test.cs" : "Тест.cs";
    public string SqlItemFieldFullName => Lang == Lang.English ? "Fullname" : "Полное наименование";
    public string SqlItemFieldGtin => Lang == Lang.English ? "GTIN" : "ГТИН";
    public string SqlItemFieldHostName => Lang == Lang.English ? "Hostname" : "Хост";
    public string SqlItemFieldIcon => Lang == Lang.English ? "Icon" : "Иконка";
    public string SqlItemFieldInnerException => Lang == Lang.English ? "Inner exception" : "Вложенное исключение";
    public string SqlItemFieldIp => "127.0.0.1";
    public string SqlItemFieldItf14 => Lang == Lang.English ? "ITF 14" : "ИТФ 14";
    public string SqlItemFieldMac => "001122334455";
    public string SqlItemFieldMeasurementTypeKg => Lang == Lang.English ? "kg" : "кг";
    public string SqlItemFieldMeasurementTypePirce => Lang == Lang.English ? "pc" : "шт";
    public string SqlItemFieldMember => Lang == Lang.English ? "Method" : "Метод";
    public string SqlItemFieldMessage => Lang == Lang.English ? "Message" : "Сообщение";
    public string SqlItemFieldName => Lang == Lang.English ? "Name" : "Наименование";
    public string SqlItemFieldNesting => Lang == Lang.English ? "Nesting" : "Вложенность";
    public string SqlItemFieldPrettyName => Lang == Lang.English ? "pretty name" : "Красивое наименование";
    public string SqlItemFieldProductXml => Lang == Lang.English ? "\"<Product Category=\\\"Meat\\\" > </Product>\"" : "\"<Product Category=\\\"Сосиски\\\" > </Product>\"";
    public string SqlItemFieldSscc => Lang == Lang.English ? "SSCC code" : "SSCC код";
    public string SqlItemFieldTemplateData => "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
    public string SqlItemFieldTemplateResourceType => "ZPL";
    public string SqlItemFieldTitle => Lang == Lang.English ? "Title" : "Заголовок";
    public string SqlItemFieldUrl => "https://{{local}}/api/info/";
    public string SqlItemFieldValue => Lang == Lang.English ? "Value" : "Значение";
    public string SqlItemFieldVersion => "0.1.2";
    public string SqlItemFieldZpl => Lang == Lang.English ? "ZPL" : "ЗПЛ";
    public string SqlItemIsNotSelect => Lang == Lang.English ? "Record is not select" : "Запись не выбрана";
    public string SqlServer => Lang == Lang.English ? "SQL-server" : "SQL-сервер";
    public string SqlServerDevelopAleksandrov => Lang == Lang.English ? "Development server | Aleksandrov" : "Сервер разработки | Александров";
    public string SqlServerDevelopMorozov => Lang == Lang.English ? "Development server | Morozov" : "Сервер разработки | Морозов";
    // ReSharper disable once InconsistentNaming
    public string SqlServerVS => Lang == Lang.English ? "Development server" : "Сервер разработки";
    // ReSharper disable once InconsistentNaming
    public string SqlServerReleaseVS => Lang == Lang.English ? "Product server" : "Продуктовый сервер";
    public string SqlServerReleaseAleksandrov => Lang == Lang.English ? "Product server | Aleksandrov" : "Продуктовый сервер | Александров";
    public string SqlServerReleaseMorozov => Lang == Lang.English ? "Product server | Morozov" : "Продуктовый сервер | Морозов";
    public string SqlServerTest => Lang == Lang.English ? "Test server" : "Тестовый сервер";
    public string SqlUser => Lang == Lang.English ? "SQL-user" : "SQL-пользователь";
    public string StatusClosed => Lang == Lang.English ? @"Connecting to SQL server is close." : "Закрыто подключение к SQL-серверу.";
    public string StatusConnected => Lang == Lang.English ? @"Connecting to SQL server is open." : "Открыто подключение к SQL-серверу.";
    public string StatusExceptionConnect() => Lang == Lang.English ? @"Error connecting to SQL-server!" : "Ошибка настроек подключения к SQL-серверу!";
    public string StatusExceptionConnect(Exception ex) => Lang == Lang.English ? $@"Error connecting to SQL server! Message: {ex.Message}" : $"Ошибка подключения к SQL-серверу! Сообщение: {ex.Message}";
    public string StatusExceptionFieldValue(Exception ex) => Lang == Lang.English ? $@"Error! Field value is incorrect! Message: {ex.Message}" : $"Ошибка! Значение поля некорректно! Сообщение: {ex.Message}";
    public string StatusExceptionFieldValue(Exception ex, string waitType, string ordinalType) => Lang == Lang.English ? $@"Error! Field value is incorrect! Expected data type: {waitType}. Received data type: {ordinalType}. Message: {ex.Message}" : $"Ошибка! Значение поля некорректно! Ожидаемый тип данных: {waitType}. Полученный тип данных: {ordinalType}. Сообщение: {ex.Message}";

    #endregion
}