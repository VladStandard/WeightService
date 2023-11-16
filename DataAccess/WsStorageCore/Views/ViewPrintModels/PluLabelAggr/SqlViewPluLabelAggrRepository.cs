namespace WsStorageCore.Views.ViewPrintModels.PluLabelAggr;

public sealed class SqlViewPluLabelAggrRepository : IViewPluLabelAggrRepository
{
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    
    public List<SqlViewPluLabelAggrModel> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        List<SqlViewPluLabelAggrModel> result = new();
        string query = SqlQueriesDiags.Views.GetLabelsAggr(sqlCrudConfig.SelectTopRowsCount);
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