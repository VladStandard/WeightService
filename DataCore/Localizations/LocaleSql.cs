// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Localizations;

public class LocaleSql
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static LocaleSql _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static LocaleSql Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public LangEnum Lang { get; set; } = LangEnum.Russian;

    #region Public and private fields, properties, constructor

    public string SqlDb => Lang == LangEnum.English ? "SQL-DB" : "SQL-БД";
    public string SqlDbCurSize => Lang == LangEnum.English ? "DB size" : "Размер БД";
    public string SqlDbFillSize => Lang == LangEnum.English ? "DB fill percentage" : "Процент заполнения БД";
    public string SqlDbMaxSize => Lang == LangEnum.English ? "DB size" : "Максимальный размер БД";
    public string SqlServer => Lang == LangEnum.English ? "SQL-server" : "SQL-сервер";
    public string SqlServerDev => Lang == LangEnum.English ? "Development server" : "Сервер разработки";
    public string SqlServerProd => Lang == LangEnum.English ? "Product server" : "Продуктовый сервер";
    public string SqlServerTest => Lang == LangEnum.English ? "Test server" : "Тестовый сервер";
    public string SqlUser => Lang == LangEnum.English ? "SQL-user" : "SQL-пользователь";
    public string StatusClosed => Lang == LangEnum.English ? @"Connecting to SQL server is close." : "Закрыто подключение к SQL-серверу.";
    public string StatusConnected => Lang == LangEnum.English ? @"Connecting to SQL server is open." : "Открыто подключение к SQL-серверу.";
    public string StatusExceptionConnect() => Lang == LangEnum.English ? @"Error connecting to SQL-server!" : "Ошибка настроек подключения к SQL-серверу!";
    public string StatusExceptionConnect(Exception ex) => Lang == LangEnum.English ? $@"Error connecting to SQL server! Message: {ex.Message}" : $"Ошибка подключения к SQL-серверу! Сообщение: {ex.Message}";
    public string StatusExceptionFieldValue(Exception ex) => Lang == LangEnum.English ? $@"Error! Field value is incorrect! Message: {ex.Message}" : $"Ошибка! Значение поля некорректно! Сообщение: {ex.Message}";
    public string StatusExceptionFieldValue(Exception ex, string waitType, string ordinalType) => Lang == LangEnum.English ? $@"Error! Field value is incorrect! Expected data type: {waitType}. Received data type: {ordinalType}. Message: {ex.Message}" : $"Ошибка! Значение поля некорректно! Ожидаемый тип данных: {waitType}. Полученный тип данных: {ordinalType}. Сообщение: {ex.Message}";

    #endregion
}
