// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewDiagModels.TableSize;

public class WsSqlViewTableSizeRepository : IViewTableSizeRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewTableSizeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewTableSizeModel> result = new();
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