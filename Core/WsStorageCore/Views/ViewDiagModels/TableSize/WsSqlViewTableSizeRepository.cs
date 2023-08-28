namespace WsStorageCore.Views.ViewDiagModels.TableSize;

public sealed class WsSqlViewTableSizeRepository : IViewTableSizeRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public IEnumerable<WsSqlViewTableSizeModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewTableSizeModel> result = new List<WsSqlViewTableSizeModel>();
        string query = WsSqlQueriesDiags.Views.GetViewTablesSizes(sqlCrudConfig.SelectTopRowsCount);
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
                Convert.ToString(item[i])
            ));
        }
        return result;
    }
}