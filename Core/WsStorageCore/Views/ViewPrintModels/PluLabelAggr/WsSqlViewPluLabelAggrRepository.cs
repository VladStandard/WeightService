namespace WsStorageCore.Views.ViewPrintModels.PluLabelAggr;

public sealed class WsSqlViewPluLabelAggrRepository : IViewPluLabelAggrRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewPluLabelAggrModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewPluLabelAggrModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetLabelsAggr(sqlCrudConfig.SelectTopRowsCount);
        object[] objects = SqlCore.GetArrayObjects(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 4) break;
            result.Add(new(
                Convert.ToDateTime(item[i++]),
                Convert.ToInt32(item[i++]),
                Convert.ToInt32(item[i++]),
                Convert.ToInt32(item[i++]))
            );
        }
        return result;
    }
}