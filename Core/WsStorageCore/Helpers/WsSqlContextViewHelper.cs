// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.ViewRefModels;

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
    public List<WsSqlViewLogMemoryModel> GetListViewLogsMemories(int topRecords)
    {
        List<WsSqlViewLogMemoryModel> result = new();
        string query = WsSqlQueriesDiags.Tables.Views.GetViewLogsMemories(topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 6 } item)
                break;
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
    public List<WsSqlViewTableSizeMemoryModel> GetListViewTablesSizes(int topRecords)
    {
        List<WsSqlViewTableSizeMemoryModel> result = new();
        string query = WsSqlQueriesDiags.Tables.Views.GetViewTablesSizes(topRecords);
        object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 7 } item)
                break;
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

    ///// <summary>
    /// Получить список ПЛУ линий из представления [REF].[VIEW_PLUS_SCALES].
    /// </summary>
    /// <param name="topRecords"></param>
    /// <returns></returns>
    //public List<WsSqlViewPluScaleModel> GetListViewPlusScales(int topRecords)
    //{
    //    List<WsSqlViewPluScaleModel> result = new();
    //    string query = WsSqlQueriesDiags.Tables.Views.GetViewPlusScales(topRecords);
    //    object[] objects = AccessCore.GetArrayObjectsNotNullable(query);
    //    foreach (object obj in objects)
    //    {
    //        if (obj is not object[] { Length: 7 } item)
    //            break;
    //        result.Add(new(
    //            Convert.ToString(item[0]),
    //            Convert.ToString(item[1]),
    //            Convert.ToString(item[2]),
    //            Convert.ToUInt32(item[3]),
    //            Convert.ToUInt16(item[4]),
    //            Convert.ToUInt16(item[5]),
    //            Convert.ToUInt16(item[6])
    //        ));
    //    }
    //    return result;
    //}

    #endregion
}