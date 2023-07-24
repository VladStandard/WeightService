namespace WsStorageCore.Tables.TableDiagModels.LogsTypes;

public class WsSqlLogTypeRepository : WsSqlTableRepositoryBase<WsSqlLogTypeModel>
{
    public List<WsSqlLogTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlLogTypeModel.Number) });
        List<WsSqlLogTypeModel> list = SqlCore.GetListNotNullable<WsSqlLogTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Number).ToList();
        return list;
    }
}