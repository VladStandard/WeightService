// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.LogsWebsFks;

public class WsSqlLogWebFkRepository: WsSqlTableRepositoryBase<WsSqlLogWebFkModel>
{
    public List<WsSqlLogWebFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { 
        //        Name = $"{nameof(LogWebFkModel.LogWebRequest)}.{nameof(LogWebModel.CreateDt)}", Direction = SqlOrderDirection.Desc });
        sqlCrudConfig.IsReadUncommitted = true;
        List<WsSqlLogWebFkModel> list = SqlCore.GetListNotNullable<WsSqlLogWebFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.LogWebRequest.CreateDt).ToList();
        return list;
    }
}