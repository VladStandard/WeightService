namespace WsStorageCore.Views.ViewRefModels.PluStorageMethods;

public class WsSqlViewPluStorageMethodRepository : IViewStorageMethodsRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewPluStorageMethodModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewPluStorageMethodModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewPlusStorageMethods(sqlCrudConfig.SelectTopRowsCount);
        object[] objects = SqlCore.GetArrayObjects(query);
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