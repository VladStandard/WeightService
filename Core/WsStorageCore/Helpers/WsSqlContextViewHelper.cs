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
    /// Get list of view log memory info.
    /// </summary>
    /// <returns></returns>
    public List<WsSqlViewLogMemory> GetListViewLogsMemories(int topRecords)
    {
        List<WsSqlViewLogMemory> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewLogsMemories(topRecords);
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
    /// Get list of view table sizes.
    /// </summary>
    /// <returns></returns>
    public List<WsSqlViewTableSizeMemory> GetListViewTablesSizes(int topRecords)
    {
        List<WsSqlViewTableSizeMemory> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewTablesSizes(topRecords);
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

    #endregion
}