namespace WsStorageCore.ViewRefModels.PluStorageMethods;

public class WsSqlViewPluStorageMethodRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewPluStorageMethodRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewPluStorageMethodRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewPluStorageMethodModel> GetList()
    {
        List<WsSqlViewPluStorageMethodModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewPlusStorageMethods();
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 22) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[i++])), Guid.Parse(Convert.ToString(item[i++])),
                Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]), Convert.ToUInt16(item[i++]), 
                Convert.ToString(item[i++]), Convert.ToString(item[i++]), Convert.ToString(item[i++]), 
                Convert.ToString(item[i++]), Guid.Parse(Convert.ToString(item[i++])), 
                Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]),
                Convert.ToInt16(item[i++]), Convert.ToInt16(item[i++]), Convert.ToBoolean(item[i++]), 
                Convert.ToBoolean(item[i++]), Guid.Parse(Convert.ToString(item[i++])), 
                Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]),
                Convert.ToUInt16(item[i++]), Convert.ToBoolean(item[i++]), 
                Convert.ToString(item[i++])
            ));
        }
        return result;
    }
}