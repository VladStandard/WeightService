// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник представлений.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextViewHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextViewHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextViewHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor
    
    private WsSqlAccessCoreHelper AccessCore => WsSqlAccessCoreHelper.Instance;

    #endregion

    #region Public and private methods - Get list view

    /// <summary>
    /// Получить логи памяти из представления [diag].[VIEW_LOGS_MEMORIES].
    /// </summary>
    /// <returns></returns>
    public List<WsSqlViewLogMemoryModel> GetListViewLogsMemories(int topRecords = 0)
    {
        List<WsSqlViewLogMemoryModel> result = new();
        string query = WsSqlQueriesDiags.Tables.Views.GetViewLogsMemories(topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 6 } item) break;
            result.Add(new(
                Convert.ToDateTime(item[0]),
                item[1] as string ?? string.Empty,
                item[2] as string ?? string.Empty,
                item[3] as string ?? string.Empty,
                Convert.ToInt16(item[4]),
                Convert.ToInt16(item[5])
            ));
        }
        return result;
    }

    /// <summary>
    /// Получить логи размеров таблиц из представления [diag].[VIEW_TABLES_SIZES].
    /// </summary>
    /// <returns></returns>
    public List<WsSqlViewTableSizeMemoryModel> GetListViewTablesSizes(int topRecords = 0)
    {
        List<WsSqlViewTableSizeMemoryModel> result = new();
        string query = WsSqlQueriesDiags.Tables.Views.GetViewTablesSizes(topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 7 } item) break;
            result.Add(new(
                Convert.ToString(item[0]),
                Convert.ToString(item[1]),
                Convert.ToString(item[2]),
                Convert.ToUInt32(item[3]),
                Convert.ToUInt16(item[4]),
                Convert.ToUInt16(item[5]),
                Convert.ToUInt16(item[6])
            ));
        }
        return result;
    }

    /// <summary>
    /// Получить список ПЛУ линий из представления [REF].[VIEW_PLUS_SCALES].
    /// </summary>
    /// <param name="scaleId"></param>
    /// <param name="topRecords"></param>
    /// <returns></returns>
    public List<WsSqlViewPluScaleModel> GetListViewPlusScales(ushort scaleId = 0, int topRecords = 0)
    {
        List<WsSqlViewPluScaleModel> result = new();
        string query = WsSqlQueriesDiags.Tables.Views.GetViewPlusScales(scaleId, topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 19 } item) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[0])),
                Convert.ToDateTime(item[1]), Convert.ToDateTime(item[2]),
                Convert.ToBoolean(item[3]), Convert.ToBoolean(item[4]),
                Convert.ToUInt16(item[5]), Convert.ToBoolean(item[6]), Convert.ToString(item[7]),
                Guid.Parse(Convert.ToString(item[8])), Convert.ToBoolean(item[9]), Convert.ToBoolean(item[10]),
                Convert.ToUInt16(item[11]), Convert.ToString(item[12]),
                Convert.ToString(item[13]), Convert.ToString(item[14]), Convert.ToString(item[15]),
                Convert.ToUInt16(item[16]), Convert.ToBoolean(item[17]), Convert.ToString(item[18])
            ));
        }
        return result;
    }

    /// <summary>
    /// Получить список способов хранения ПЛУ из представления [REF].[VIEW_PLUS_STORAGE_METHODS].
    /// </summary>
    /// <param name="topRecords"></param>
    /// <returns></returns>
    public List<WsSqlViewPluStorageMethodModel> GetListViewPlusStorageMethods(int topRecords = 0)
    {
        List<WsSqlViewPluStorageMethodModel> result = new();
        string query = WsSqlQueriesDiags.Tables.Views.GetViewPlusStorageMethods(topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 21 } item) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[0])),
                Convert.ToBoolean(item[1]), Convert.ToBoolean(item[2]), Convert.ToUInt16(item[3]), Convert.ToString(item[4]),
                Convert.ToString(item[5]), Convert.ToString(item[6]), Convert.ToString(item[7]),
                Guid.Parse(Convert.ToString(item[8])), Convert.ToBoolean(item[9]), Convert.ToString(item[10]),
                Convert.ToInt16(item[11]), Convert.ToInt16(item[12]), Convert.ToBoolean(item[13]), Convert.ToBoolean(item[14]),
                Guid.Parse(Convert.ToString(item[15])), Convert.ToBoolean(item[16]), Convert.ToString(item[17]),
                Convert.ToUInt16(item[18]), Convert.ToBoolean(item[19]), Convert.ToString(item[20])
            ));
        }
        return result;
    }

    #endregion
}