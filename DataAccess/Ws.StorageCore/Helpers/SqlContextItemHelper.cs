
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Enums;

namespace Ws.StorageCore.Helpers;

/// <summary>
/// SQL-контроллер таблицы записей.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlContextItemHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlContextItemHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlContextItemHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    private SqlAppEntity App { get; set; } = new();
    private SqlHostEntity Host { get; set; } = new();

    #endregion

    #region Public and private methods - Logs


    #region Private
    
    private void SaveLogCore(string message, LogTypeEnum logType, string filePath, int lineNumber, string memberName)
    {
        StrUtils.SetStringValueTrim(ref filePath, 32, true);
        StrUtils.SetStringValueTrim(ref memberName, 32);
        StrUtils.SetStringValueTrim(ref message, 1024);

        SqlLogEntity log = new()
        {
            IsMarked = false,
            Device = Host,
            App = App,
            Type = logType,
            Version = "beta",
            File = filePath,
            Line = lineNumber,
            Member = memberName,
            Message = message
        };
        SqlCore.Save(log, SqlEnumSessionType.IsolatedAsync);
    }

    #endregion
    
    
    #endregion
}