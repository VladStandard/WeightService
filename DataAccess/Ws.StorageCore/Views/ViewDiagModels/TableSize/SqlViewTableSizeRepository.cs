using Ws.StorageCore.Helpers;
using Ws.StorageCore.Models;
using Ws.StorageCore.Utils;
namespace Ws.StorageCore.Views.ViewDiagModels.TableSize;

public sealed class SqlViewTableSizeRepository : IViewTableSizeRepository
{
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    
    public IEnumerable<SqlViewTableSizeModel> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        List<SqlViewTableSizeModel> result = new();
        string query = SqlQueriesDiags.Views.GetViewTablesSizes(sqlCrudConfig.SelectTopRowsCount);
        object[] objects = SqlCore.GetArrayObjects(query);
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