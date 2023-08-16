// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewScaleModels.Lines;

public class WsSqlViewLineRepository : IViewLineRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public IList<WsSqlViewLineModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IList<WsSqlViewLineModel> result = new List<WsSqlViewLineModel>();
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