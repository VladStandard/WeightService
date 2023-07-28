// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewScaleModels.Aggregations;

public sealed class WsSqlViewWeightingAggrRepository : IViewWeightingAggrRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewWeightingAggrModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewWeightingAggrModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetWeightingsAggr(sqlCrudConfig.SelectTopRowsCount);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
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