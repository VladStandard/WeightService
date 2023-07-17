namespace WsStorageCore.Views.ViewDiagModels.TableSize;

public class WsSqlViewTableSizeRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewTableSizeRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewTableSizeRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewTableSizeModel> GetList(int topRecords = 0)
    {
        List<WsSqlViewTableSizeModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewTablesSizes(topRecords);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8) break;
            result.Add(new(
                Convert.ToString(item[i++]),
                Convert.ToString(item[i++]),
                Convert.ToString(item[i++]),
                Convert.ToUInt32(item[i++]),
                Convert.ToUInt16(item[i++]),
                Convert.ToUInt16(item[i++]),
                Convert.ToUInt16(item[i++]),
                Convert.ToString(item[i++])
            ));
        }
        return result;
    }
    
}