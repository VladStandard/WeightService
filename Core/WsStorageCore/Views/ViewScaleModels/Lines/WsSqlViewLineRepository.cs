namespace WsStorageCore.Views.ViewScaleModels.Lines;

public class WsSqlViewLineRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewLineRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewLineRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewLineModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewLineModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetLines(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8) break;
            result.Add(new()
            {
                IdentityValueId = Convert.ToInt64(item[i++]),
                IsMarked = Convert.ToBoolean(item[i++]),
                Name = item[i++] as string ?? string.Empty,
                Number = Convert.ToInt32(item[i++]),
                HostName = item[i++] as string ?? string.Empty,
                Printer = item[i++] as string ?? string.Empty,
                WorkShop = item[i++] as string ?? string.Empty,
                Counter = Convert.ToInt32(item[i++])
            });
        }
        return result;
    }
}