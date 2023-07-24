namespace WsStorageCore.Tables.TableScaleModels.TasksTypes;

public class WsSqlTaskTypeRepository : WsSqlTableRepositoryBase<WsSqlTaskTypeModel>
{
    public List<WsSqlTaskTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlTaskTypeModel> list = SqlCore.GetListNotNullable<WsSqlTaskTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }
}