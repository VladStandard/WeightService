// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.LogsWebsFks;

public class WsSqlLogWebFkRepository: WsSqlTableRepositoryBase<WsSqlLogWebFkModel>
{
    public List<WsSqlLogWebFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        IEnumerable<WsSqlLogWebFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlLogWebFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderByDescending(item => item.LogWebRequest.CreateDt);
        return items.ToList();
    }
}