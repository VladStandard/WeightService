namespace WsStorageCore.ViewScaleModels.Aggregations;

public sealed class WsSqlViewWeightingAggrRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewWeightingAggrRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewWeightingAggrRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion
    
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewWeightingAggrModel> GetList(int topRecords = 200)
    {
        List<WsSqlViewWeightingAggrModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetWeightingsAggr(topRecords);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 5) break;
            result.Add(new(
                Convert.ToDateTime(item[i++]),
                Convert.ToInt32(item[i++]),
                Convert.ToString(item[i++]),
                Convert.ToString(item[i++]),
                Convert.ToInt32(item[i++]))
            );
        }
        return result;
    }
}