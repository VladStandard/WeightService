namespace WsStorageCore.Views.ViewScaleModels.Lines;

public class WsSqlViewLineRepository : IViewLineRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewLineModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewLineModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetLines(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
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